using DrawingInConsole.Command;
using DrawingInConsole.ToolFactory;
using System;

namespace DrawingInConsole
{
	public static class CommandParser
	{
		public static BaseCommand ParseCommand(string args)
		{
			if (string.IsNullOrWhiteSpace(args))
				throw new ArgumentNullException(nameof(args));

			var tokens = args.Trim().Split(" ");

			if (tokens.Length < 1)
				throw new ArgumentException("Invalid number of parameters.");

			if (tokens[0].Equals("C"))
			{
				throw new ArgumentException("Canvas resizing functionality is not avaialbe at this stage. Restart the program.");
			}
			else if (tokens[0].Equals("L"))
			{
				return new DrawLineCommand(args);
			}
			else if (tokens[0].Equals("R"))
			{
				return new DrawRectangleCommand(args);
			}
			else if (tokens[0].Equals("B"))
			{
				return new BucketFillAreaCommand(args);
			}
			else if (tokens[0].Equals("Q"))
			{
				return new QuitCommand();
			}

			throw new ArgumentException("Unknown command.");
		}
	}
}
