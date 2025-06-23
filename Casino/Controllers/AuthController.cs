using Casino.Services;
using Casino.UI;
using Casino.Models;
using Spectre.Console;

namespace Casino.Controllers {
	public class AuthController {

		UserService userService = new Casino.Services.UserService();
		User loggedUser;
		private static AuthController _instance;
		public static AuthController Instance => _instance ??= new AuthController();
		public User getLoggedUser() { return loggedUser; }
		public void Register(string name, string email, string password, string confirmPassword) {
			if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email)) {
				AnsiConsole.MarkupLine("[red]Email and Username cannot be empty[/]");
				return;
			}

			if (!email.Contains("@")) {
				AnsiConsole.MarkupLine("[red]invalid Email[/]");
				return;
			}

			if (password != confirmPassword) {
				AnsiConsole.MarkupLine("[red]Passwords dont match[/]");
				return;
			}

			if (userService.RegisterUser(name, email, password, 0, new List<int>())) {
				AnsiConsole.MarkupLine("[green]Successful registration[/]");
				return;
			} else {
				AnsiConsole.MarkupLine("[red]Error during registration, try again.[/]");
				return;
			}
		}

		public bool Login(string email, string password) {
            loggedUser = userService.LoginUser(email, password);
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password)) {
				AnsiConsole.MarkupLine("[red]Email and Password cannot be empty[/]");
				return false;
			}
			
			if (getLoggedUser() == null) {
				Console.Clear();
				AnsiConsole.MarkupLine("[red]Invalid Email or Password, try again[/]");
				return false;
			} else {
				return true;
			}
		}
	}
}