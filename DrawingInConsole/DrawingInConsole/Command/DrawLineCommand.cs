using DrawingInConsole.ToolFactory;
using DrawingInConsole.Shape;
using DrawingInConsole.ShapeTool;
using System;

namespace DrawingInConsole.Command
{
	public class DrawLineCommand : BaseCommand
	{
		private readonly uint x1;
		private readonly uint y1;
		private readonly uint x2;
		private readonly uint y2;

		public DrawLineCommand(string args) : base(args)
		{
			var tokens = args.Trim().Split(" ");

			if (tokens.Length != 5)
				throw new ArgumentException("Invalid number of parameters.");

			if (!tokens[0].Equals("L"))
				throw new ArgumentException("Unsupported command type.");

			if (!uint.TryParse(tokens[1], out uint x1))
				throw new ArgumentException("Can't define x1 point.");

			if (!uint.TryParse(tokens[2], out uint y1))
				throw new ArgumentException("Can't define y1 point.");

			if (!uint.TryParse(tokens[3], out uint x2))
				throw new ArgumentException("Can't define x2 point.");

			if (!uint.TryParse(tokens[4], out uint y2))
				throw new ArgumentException("Can't define y2 point.");

			x1--;
			y1--;
			x2--;
			y2--;

			if (x1 > x2)
				SwapArguments(ref x2, ref x1);

			if (y1 > y2)
				SwapArguments(ref y2, ref y1);


			this.x1 = x1;
			this.y1 = y1;
			this.x2 = x2;
			this.y2 = y2;
		}

		public override void Do(ConsoleCanvas canvas, MatrixToolFactory toolsFactory)
		{
			if (canvas == null)
				throw new ArgumentNullException(nameof(canvas));

			if (toolsFactory == null)
				throw new ArgumentNullException(nameof(toolsFactory));

			var lineDrawer = toolsFactory.CreateLineTool(x1, y1, x2, y2);

			canvas.Draw(lineDrawer);
			canvas.Render();
		}
	}
}
