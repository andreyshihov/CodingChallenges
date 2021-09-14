using DrawingInConsole.Shape;
using System;

namespace DrawingInConsole.ShapeTool
{
	public class MatrixRectangleTool : IMatrixShapeTool
	{
		private readonly Rectangle r;

		public MatrixRectangleTool(Rectangle r)
		{
			this.r = r ?? throw new ArgumentNullException(nameof(r));
		}

		public void Draw(char[,] matrix)
		{
			if (matrix == null)
				throw new ArgumentNullException(nameof(matrix));

			var cols = (uint)matrix.GetLength(0);
			var rows = (uint)matrix.GetLength(1);

			if (cols > 10000 || rows > 10000)
				throw new ApplicationException("Maximum supported matrix size is 10,000 by 10,000");

			if (r.X1 > cols - 1 || r.X2 > cols - 1 || r.Y1 > rows - 1 || r.Y2 > rows - 1)
				throw new ApplicationException("Can't draw the rectangle ouside of the canvas.");

			for (uint x = r.X1; x <= r.X2; x++)
			{
				for (uint y = r.Y1; y <= r.Y2; y++)
				{
					matrix[x, y] = r.Colour;

					if (x == r.X1)
						matrix[r.X1, y] = r.Outline;
					if (x == r.X2)
						matrix[r.X2, y] = r.Outline;
				}

				matrix[x, r.Y1] = r.Outline;
				matrix[x, r.Y2] = r.Outline;
			}
		}
	}
}
