using LiteDB;

namespace Casino.Models {
	public class User {
		public string Name { get; set; }
		public string Email { get; set; }
		public int Id { get; set; }
		public int Balance { get; set; }
		public string Password { get; set; }
		public List<int> Tokens { get; set; }
        public User() { }
        public User(string Name, string Email, int Balance, string Password, List<int> Tokens) {
			this.Name = Name;
			this.Email = Email;
			this.Password = Password;
			this.Balance = Balance;
			this.Tokens = Tokens;
		}
	}
}