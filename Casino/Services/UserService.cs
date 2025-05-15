using LiteDB;
using Casino.Models;
using System.Security.Cryptography.X509Certificates;
using System.Runtime.CompilerServices;
using System.IO;

namespace Casino.Services {
    public class UserService {

        private readonly string documentsPath;
        private readonly string folderPath;
        private readonly string dbPath;

        public UserService() {
            documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            folderPath = Path.Combine(documentsPath, "CasinoApp");

            if (!Directory.Exists(folderPath)) {
                Directory.CreateDirectory(folderPath);
            }
            dbPath = Path.Combine(folderPath, "userdata.db");
        }

        public bool RegisterUser(string name, string email, string password, int balance, List<int> tokens) {
            using (var db = new LiteDatabase(dbPath)) {
                try {
                    var col = db.GetCollection<User>("users");

                    var user = new User { Name = name, Email = email, Balance = balance, Password = password, Tokens = tokens };

                    col.Insert(user);
                    return true;
                }
                catch (Exception e) {
                    Console.WriteLine(e.Message);
                    return false;
                }
            }
        }

        public User LoginUser(string email, string password) {
            using (var db = new LiteDatabase(dbPath)) {
                var col = db.GetCollection<User>("users");

                try {
                    var user = col.Find(u => u.Email.Equals(email) && u.Password.Equals(password)).FirstOrDefault();
                    return user;
                }
                catch (Exception e) {
                    Console.WriteLine(e.Message);
                    return null;
                }
            }
        }

        /*
        public string findByName(string name) { 
            using (var db = new LiteDatabase(dbPath)) {
                var col = db.GetCollection<User>("users");
            }
        } */
    }
}