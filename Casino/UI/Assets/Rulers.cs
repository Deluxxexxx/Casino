using Spectre.Console;
using Spectre.Console.Rendering;

namespace Casino.UI.Assets.Rulers;
public static class Rulers {
    public static Rule HeaderRule(string header) => new Rule($"[#FFD700]{header}[/]") {
		Justification = Justify.Center,
		Border = BoxBorder.Heavy,
		Style = Style.Parse("green")
	};

	public static void MainMenu() => new Table().AddColumns("Menu", "") {
		// UNFINISHED
	};

}	