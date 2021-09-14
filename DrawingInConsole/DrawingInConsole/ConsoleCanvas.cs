using DrawingInConsole.AreaTool;
using DrawingInConsole.ShapeTool;
using System;

namespace DrawingInConsole
{
	public class ConsoleCanvas
	{
		private readonly char[,] matrix;

		public uint CanvasWidth { get; }
		public uint CanvasHeight { get; }
		public uint WindowWidth { get; }
		public uint WindowHeight { get; }

		private static bool? consolePresent;
		public static bool ConsolePresent
		{
			get
			{
				if (consolePresent == null)
				{
					consolePresent = true;
					try { int window_height = Console.WindowHeight; }
					catch { consolePresent = false; }
				}
				return consolePresent.Value;
			}
		}

		// This constructor is nessessary for the testing
		protected ConsoleCanvas() { }

		protected ConsoleCanvas(uint width, uint height)
		{
			this.CanvasWidth = width;
			this.CanvasHeight = height;

			this.matrix = new char[CanvasWidth, CanvasHeight];

			for (uint x = 0; x < CanvasWidth; x++)
				for (uint y = 0; y < CanvasHeight; y++)
					matrix[x, y] = ' ';

			if (ConsolePresent)
			{
				this.WindowWidth = (uint)Console.WindowWidth;
				this.WindowHeight = (uint)Console.WindowHeight;
			}
		}

		public virtual void Draw(IMatrixShapeTool shapeTool)
		{
			if (shapeTool == null)
				throw new ArgumentNullException(nameof(shapeTool));

			shapeTool.Draw(matrix);
		}

		public virtual void Fill(IMatrixAreaFillingTool areaTool)
		{
			if (areaTool == null)
				throw new ArgumentNullException(nameof(areaTool));

			areaTool.Fill(matrix);
		}

		public virtual void Render()
		{
			if (ConsolePresent)
			{
				if (WindowWidth != Console.WindowWidth)
					throw new ApplicationException("The console width has been changed since the original canvas size has been defined. Restart the application.");

				if (WindowHeight != Console.WindowHeight)
					throw new ApplicationException("The console height has been changed since the original canvas size has been defined. Restart the application.");
			}

			Console.WriteLine(new string('-', (int)CanvasWidth + 2));
			for (int y = 0; y < CanvasHeight; y++)
			{
				Console.Write("|");

				for (int x = 0; x < CanvasWidth; x++)
				{
					Console.Write(matrix[x, y]);
				}

				Console.Write("|");
				Console.WriteLine();
			}
			Console.WriteLine(new string('-', (int)CanvasWidth + 2));
			Console.WriteLine();
		}

		public static ConsoleCanvas FromArgs(string args)
		{
			if (args.Equals("Q"))
				return null;

			var tokens = args.Trim().Split(" ");

			if (tokens.Length != 3)
				throw new ArgumentException("Invalid number of parameters.");

			if (!tokens[0].Equals("C"))
				throw new ArgumentException("Can't create the canvas. Unknown command.");

			if (!uint.TryParse(tokens[1], out uint width))
				throw new ArgumentException("Can't define the width of the the canvas.");

			if (!uint.TryParse(tokens[2], out uint height))
				throw new ArgumentException("Can't define the height of the the canvas.");

			if (ConsolePresent)
			{
				if (width >= Console.WindowWidth)
					throw new ArgumentException("The canvas width can't be larger than the console width.");

				if (height >= Console.WindowHeight)
					throw new ArgumentException("The canvas width can't be larger than the console width.");
			}

			return new ConsoleCanvas(width, height);
		}
	}
}
