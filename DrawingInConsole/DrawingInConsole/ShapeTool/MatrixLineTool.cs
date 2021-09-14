using DrawingInConsole.Shape;
using System;

namespace DrawingInConsole.ShapeTool
{
	public class MatrixLineTool : IMatrixShapeTool
	{
		private readonly Line l;

		public MatrixLineTool(Line l)
		{
			this.l = l ?? throw new ArgumentNullException(nameof(l));
		}

		public void Draw(char[,] matrix)
		{
			if (matrix == null)
				throw new ArgumentNullException(nameof(matrix));

			var cols = (uint)matrix.GetLength(0);
			var rows = (uint)matrix.GetLength(1);

			if (cols > 10000 || rows > 10000)
				throw new ApplicationException("Maximum supported matrix size is 10,000 by 10,000");

			bool isDot = (l.X1 == l.Y1) && (l.X1 == l.X2) && (l.X1 == l.Y2);

			if (!isDot && !(l.X1 == l.X2 ^ l.Y1 == l.Y2))
				throw new ApplicationException("Can't draw the diagonal lines.");

			if (l.X1 > cols - 1 || l.X2 > cols - 1 || l.Y1 > rows - 1 || l.Y2 > rows - 1)
				throw new ApplicationException("Can't draw the line ouside of the canvas.");

			for (uint x = l.X1; x <= l.X2; x++)
				for (uint y = l.Y1; y <= l.Y2; y++)
					matrix[x, y] = l.Outline;
		}
	}
}
