namespace OrdersFiltration.Tools
{
	public static class ConsoleTools
	{

		public static void LogError(string msg)
		{
			Draw(ConsoleColor.Red, ConsoleColor.Black, "err:");
			Console.WriteLine(" " + msg);
		}

		public static void LogInfo(string msg)
		{
			Draw(ConsoleColor.Black, ConsoleColor.Cyan, "info:");
			Console.WriteLine(" " + msg);
		}

		private static void Draw(ConsoleColor bg, ConsoleColor fg, string msg)
		{
			Console.BackgroundColor = bg;
			Console.ForegroundColor = fg;
			Console.Write(msg);
			Console.ResetColor();
		}
	}
}
