namespace UnitTests
{
	public static class MatrixExtensions
	{
		public static void InitMatrix(this char[,] matrix, char color)
		{
			var cols = matrix.GetLength(1);
			var rows = matrix.GetLength(0);

			for (int row = 0; row < rows; row++)
			{
				for (int col = 0; col < cols; col++)
				{
					matrix[row, col] = color;
				}
			}
		}

		public static bool CheckMatrix(this char[,] matrix, char color)
		{
			var cols = matrix.GetLength(1);
			var rows = matrix.GetLength(0);

			for (int row = 0; row < rows; row++)
			{
				for (int col = 0; col < cols; col++)
				{
					if (matrix[row, col] != color)
						return false;
				}
			}

			return true;
		}

		public static bool CompareMatrix(this char[,] actualMatrix, char[,] expectedMatrix)
		{
			var cols = actualMatrix.GetLength(1);
			var rows = actualMatrix.GetLength(0);

			if (cols != expectedMatrix.GetLength(1) || rows != expectedMatrix.GetLength(0))
				return false;

			for (int row = 0; row < rows; row++)
			{
				for (int col = 0; col < cols; col++)
				{
					if (expectedMatrix[row, col] != actualMatrix[row, col])
						return false;
				}
			}

			return true;
		}
	}
}
