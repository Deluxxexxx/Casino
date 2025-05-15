using System.ComponentModel;
using System.Formats.Asn1;
using System.IO;
using Casino.Controllers;
using Spectre.Console;

namespace Casino.UI {
    public static class Menus {
        public static void MainMenu() {
            
            string selectedOption = null;
            bool salir = false;

            while (salir == false) {

                var rule = new Rule("[#FFD700]Welcome to Caligula's Casino[/]");
                rule.Justification = Justify.Center;
                rule.Border = BoxBorder.Heavy;
                rule.Style = Style.Parse("green");
                AnsiConsole.Write(rule);

                selectedOption = AnsiConsole.Prompt(new SelectionPrompt<string>()
                    .Title("[white]What do you wish to do?[/]")
                    .PageSize(3)
                    .HighlightStyle(Color.Green)
                    .AddChoices(new[] { "Login", "Register", "Exit" })
                );

                switch (selectedOption) {
                    case "Login":
                    LoginMenu();
                    salir = true;
                    break;
                    case "Register":
                    RegisterMenu();
                    break;
                    case "Exit":
                    salir = true;
                    break;
                }
            }
        }

        public static void RegisterMenu() {

            while (true) {
                AuthController authController = new AuthController();
                Console.WriteLine();
                var rule = new Rule("[#FFD700]Register[/]");
                rule.Justification = Justify.Center;
                rule.Border = BoxBorder.Heavy;
                rule.Style = Style.Parse("green");
                AnsiConsole.Write(rule);

                string name = AnsiConsole.Prompt(new TextPrompt<string>("Enter your Name: "));
                string email = AnsiConsole.Prompt(new TextPrompt<string>("Enter your Email: "));
                string password = AnsiConsole.Prompt(new TextPrompt<string>("Enter your Password: ").Secret());
                string confirmPassword = AnsiConsole.Prompt(new TextPrompt<string>("Confirm Password: ").Secret());

                authController.Register(name, email, password, confirmPassword);
                return;
            }
        }

        public static void LoginMenu() {
            while (true) { 
                AuthController authController = new AuthController();
                Console.WriteLine();
                var rule = new Rule("[#FFD700]Login[/]");
                rule.Justification = Justify.Center;
                rule.Border = BoxBorder.Heavy;
                rule.Style = Style.Parse("green");
                AnsiConsole.Write(rule);

                string email = AnsiConsole.Prompt(new TextPrompt<string>("Enter your Email: "));
                string password = AnsiConsole.Prompt(new TextPrompt<string>("Enter your Password: "));

                if (authController.Login(email, password)) {
                    AnsiConsole.MarkupLine("[green]Successful Login[/]");
                }
                return;
            }
        }

        public static void GameMenu() {
            AnsiConsole.WriteLine("test");
        }
    }
}