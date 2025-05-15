using Casino.Services;
using Casino.UI;
using Casino.Models;
using Spectre.Console;

namespace Casino.Controllers {
	public class AuthController {

		UserService userService = new Casino.Services.UserService();
		User loggedUser;
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
			if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password)) {
				AnsiConsole.MarkupLine("[red]Email and Password cannot be empty[/]");
				return false;
			}
			
			loggedUser = userService.LoginUser(email, password);
			if (loggedUser == null) {
				AnsiConsole.MarkupLine("[red]Error during login, try again[/]");
				return false;
			} else {
				return true;
			}
		}
	}
}