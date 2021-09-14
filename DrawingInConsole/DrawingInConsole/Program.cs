using DrawingInConsole.Command;
using DrawingInConsole.ToolFactory;
using System;

namespace DrawingInConsole
{
	class Program
	{
		static void Main()
		{
			Console.WriteLine("Welcome to the Drawing application!");
			Console.WriteLine();
			PrintCanvasMessage();
			Console.WriteLine("Or enter Q to exit the application.");
			Console.WriteLine();

			ConsoleCanvas consoleCanvas = null;

			// The canvas instatiation has to be done before program execution can be continued.
			// Instantiation requires user's interaction via command line.

			do
			{
				try
				{
					PrintEnterCommandMessage();
					consoleCanvas = ConsoleCanvas.FromArgs(Console.ReadLine());

					// Canvas expected to be null if, and only if, Q command has been executed.

					if (consoleCanvas == null)
					{
						PrintFarewellMessage();
						return;
					}
					else
						consoleCanvas.Render();
				}
				catch (ArgumentException ae)
				{
					Console.WriteLine();
					Console.WriteLine(string.Concat("Can't create canvas: ", ae.Message));
					PrintCanvasMessage();
					Console.WriteLine();
				}
			} while (consoleCanvas == null);

			// Canvas, UserInput and Commands implementing 'Command' design pattern.
			// Tools (Line, Rectangle) are created with Abstract Factory design pattern.

			MatrixToolFactory toolsFactory = new ConsoleMatrixToolFactory();
			ConsoleUserInput consoleUserInput = new ConsoleUserInput(consoleCanvas, toolsFactory);
			BaseCommand command = new UnknownCommand();

			do
			{
				try
				{
					PrintEnterCommandMessage();
					command = CommandParser.ParseCommand(Console.ReadLine());
					consoleUserInput.ProcessCommand(command);
				}
				catch (Exception ae)
				{
					Console.WriteLine();
					Console.WriteLine(string.Concat("Can't process command: ", ae.Message));
					PrintAvailableCommands();
					Console.WriteLine();
				}
			} while (command.GetType() != typeof(QuitCommand));

			PrintFarewellMessage();
		}

		static void PrintEnterCommandMessage()
		{
			Console.Write("enter command: ");
		}

		static void PrintCanvasMessage()
		{
			Console.WriteLine("Please enter Canvas command in the following format: C w h; where 'w' and 'h' are the width and the height of the canvas.");
		}

		static void PrintFarewellMessage()
		{
			Console.WriteLine();
			Console.WriteLine("The drawing session has ended. Good bye!");
		}

		static void PrintAvailableCommands()
		{
			Console.WriteLine("Please choose from the following commands:");
			Console.WriteLine("	L x1 y1 x2 y2 - to draw a line.");
			Console.WriteLine("	R x1 y1 x2 y2 - to draw a rectangle.");
			Console.WriteLine("	B x y c - to fill an area with 'bucket fill'.");
			Console.WriteLine("	Q - to quit the application.");
		}
	}
}
