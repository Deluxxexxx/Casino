using System.ComponentModel;
using System.Formats.Asn1;
using System.IO;
using Casino.UI.Assets.Rulers;
using Casino.Controllers;
using Spectre.Console;
using Casino.Services;

namespace Casino.UI {
    public static class Menus {
        public static void Index() {
            
            string selectedOption = null;
            bool exit = false;

            while (exit == false) {

                AnsiConsole.Write(Rulers.HeaderRule("Welcome To Caligula's Casino"));

                selectedOption = AnsiConsole.Prompt(new SelectionPrompt<string>()
                    .Title("[white]What do you wish to do?[/]")
                    .PageSize(3)
                    .HighlightStyle(Color.Green)
                    .AddChoices(new[] { "Login", "Register", "Exit" })
                );

                switch (selectedOption) {
                    case "Login":
                    LoginMenu();
                    exit = true;
                    break;
                    case "Register":
                    RegisterMenu();
                    break;
                    case "Exit":
                    exit = true;
                    break;
                }
            }
        }

        public static void RegisterMenu() {

            while (true) {
                Console.WriteLine();
                AnsiConsole.Write(Rulers.HeaderRule("Register"));

                string name = AnsiConsole.Prompt(new TextPrompt<string>("Enter your Name: "));
                string email = AnsiConsole.Prompt(new TextPrompt<string>("Enter your Email: "));
                string password = AnsiConsole.Prompt(new TextPrompt<string>("Enter your Password: ").Secret());
                string confirmPassword = AnsiConsole.Prompt(new TextPrompt<string>("Confirm Password: ").Secret());

                AuthController.Instance.Register(name, email, password, confirmPassword);
                return;
            }
        }

        public static void LoginMenu() {
            while (true) {
                Console.WriteLine();
                AnsiConsole.Write(Rulers.HeaderRule("Login"));

                string email = AnsiConsole.Prompt(new TextPrompt<string>("Enter your Email: "));
                string password = AnsiConsole.Prompt(new TextPrompt<string>("Enter your Password: ").Secret());

                if (AuthController.Instance.Login(email, password)) {
                    AnsiConsole.Status().Start("Successful Login", ctx => {
                        ctx.Spinner(Spinner.Known.Star);
                        ctx.SpinnerStyle(Style.Parse("green"));
                        Thread.Sleep(800);
                    });
                    return;
                }
            }
        }

        public static void MainMenu() {
            Console.Clear();
            AnsiConsole.Write(Rulers.HeaderRule("Lobby"));
            AnsiConsole.MarkupLine($"[green]Welcome {AuthController.Instance.getLoggedUser().Name}, what do you wish to do? [/]");
        }
    }
}