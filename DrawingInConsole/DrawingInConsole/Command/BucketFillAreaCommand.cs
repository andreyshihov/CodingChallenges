using DrawingInConsole.ToolFactory;
using System;

namespace DrawingInConsole.Command
{
	public class BucketFillAreaCommand : BaseCommand
	{
		private readonly uint startX;
		private readonly uint startY;
		private readonly char colour;

		public BucketFillAreaCommand(string args) : base(args)
		{
			var tokens = args.Trim().Split(" ");

			if (tokens.Length != 4)
				throw new ArgumentException("Invalid number of parameters.");

			if (!tokens[0].Equals("B"))
				throw new ArgumentException("Unsupported command type.");

			if (!uint.TryParse(tokens[1], out uint x))
				throw new ArgumentException("Can't define x point.");

			if (!uint.TryParse(tokens[2], out uint y))
				throw new ArgumentException("Can't define y point.");

			if (tokens[3].Length > 1)
				throw new ArgumentException("Colour should be represented by 1 character.");

			colour = tokens[3][0];

			x--;
			y--;

			startX = x;
			startY = y;
		}

		public override void Do(ConsoleCanvas canvas, MatrixToolFactory toolsFactory)
		{
			if (canvas == null)
				throw new ArgumentNullException(nameof(canvas));

			if (toolsFactory == null)
				throw new ArgumentNullException(nameof(toolsFactory));

			var areaFiller = toolsFactory.CreateFillingTool(startX, startY, colour);

			canvas.Fill(areaFiller);
			canvas.Render();
		}
	}
}
