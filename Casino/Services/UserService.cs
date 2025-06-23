using LiteDB;
using Casino.Models;
using System.Security.Cryptography.X509Certificates;
using System.Runtime.CompilerServices;
using System.IO;

namespace Casino.Services {
    public class UserService {

        private readonly string _documentsPath;
        private readonly string _folderPath;
        private readonly string _dbPath;

        public UserService() {
            _documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            _folderPath = Path.Combine(_documentsPath, "CasinoApp");

            if (!Directory.Exists(_folderPath)) {
                Directory.CreateDirectory(_folderPath);
            }
            _dbPath = Path.Combine(_folderPath, "userdata.db");
        }

        public bool RegisterUser(string name, string email, string password, int balance, List<int> tokens) {
            using (var db = new LiteDatabase(_dbPath)) {
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
            using (var db = new LiteDatabase(_dbPath)) {
                var col = db.GetCollection<User>("users");

                try {
                    var user = col.Find(Query.And(Query.EQ("Email", email), Query.EQ("Password", password))).FirstOrDefault();
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