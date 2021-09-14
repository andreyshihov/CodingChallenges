// Depth First Search algorithm implementation aknowledgement.
// Algorithm implementation has been inspired by the following example.
// https://zeroreversesoft.wordpress.com/2013/02/17/searching-for-the-largest-area-of-equal-elements-in-a-matrix-using-bfsdfs-hybrid-algorithm/#more-222

using System;
using System.Collections.Generic;

namespace DrawingInConsole.AreaTool
{
	public class MatrixBfsAreaFillingTool : IMatrixAreaFillingTool
	{
		private Stack<uint> branchingCellCol;
		private Stack<uint> branchingCellRow;
		private char[,] matrix;
		private bool[,] visitedCells;
		private uint rows;
		private uint cols;
		private readonly uint startX;
		private readonly uint startY;
		private readonly char colour;

		protected MatrixBfsAreaFillingTool() { }

		public MatrixBfsAreaFillingTool(uint startX, uint startY, char colour)
		{
			this.startX = startX;
			this.startY = startY;
			this.colour = colour;
		}

		public void Fill(char[,] matrix)
		{
			this.matrix = matrix ?? throw new ArgumentNullException(nameof(matrix));

			cols = (uint)matrix.GetLength(1);
			rows = (uint)matrix.GetLength(0);

			if (cols > 10000 || rows > 10000)
				throw new ApplicationException("Maximum supported matrix size is 10,000 by 10,000");

			if (startX > rows - 1 || startY > cols - 1)
				throw new ApplicationException("Can't fill outside canvas.");

			branchingCellCol = new Stack<uint>();
			branchingCellRow = new Stack<uint>();

			visitedCells = new bool[rows, cols];

			for (int row = 0; row < rows; row++)
			{
				for (int col = 0; col < cols; col++)
				{
					visitedCells[row, col] = false;
				}
			}

			for (uint row = startX; row < rows; row++)
			{
				for (uint col = startY; col < cols; col++)
				{
					if (visitedCells[row, col])
						continue;

					if (IsBranching(row, col))
					{
						branchingCellCol.Push(col);
						branchingCellRow.Push(row);
						Explore(col, row);
						return;
					}
					else
					{
						matrix[row, col] = colour;
						return;
					}
				}
			}
		}

		private bool IsBranching(uint row, uint col)
		{
			// inline functions for better performance
			bool wontMinOverflow(uint val)
			{
				return (val - 1 != uint.MaxValue);
			}

			bool wontMaxOverflow(uint val)
			{
				return (val + 1 != uint.MinValue);
			}

			bool isVisited(uint row, uint col)
			{
				return visitedCells[row, col];
			}

			int currentValue = matrix[row, col];

			// move left
			if (wontMinOverflow(col)
				&& col - 1 >= 0
				&& matrix[row, col - 1] == currentValue
				&& isVisited(row, col - 1) == false)
			{
				return true;
			}
			// move right
			else if (wontMaxOverflow(col)
				&& col + 1 < cols
				&& matrix[row, col + 1] == currentValue
				&& isVisited(row, col + 1) == false)
			{
				return true;
			}
			// move up
			else if (wontMinOverflow(row)
				&& row - 1 >= 0
				&& matrix[row - 1, col] == currentValue
				&& isVisited(row - 1, col) == false)
			{
				return true;
			}
			// move down
			else if (wontMaxOverflow(row)
				&& row + 1 < rows
				&& matrix[row + 1, col] == currentValue
				&& isVisited(row + 1, col) == false)
			{
				return true;
			}

			return false;
		}

		private void Explore(uint col, uint row)
		{
			int currentValue = matrix[row, col];

			while (branchingCellCol.Count > 0 && branchingCellRow.Count > 0)
			{
				// inline functions for better performance
				bool wontMinOverflow(uint val)
				{
					return (val - 1 != uint.MaxValue);
				}

				bool wontMaxOverflow(uint val)
				{
					return (val + 1 != uint.MinValue);
				}

				bool isVisited(uint row, uint col)
				{
					return visitedCells[row, col];
				}

				col = branchingCellCol.Pop();
				row = branchingCellRow.Pop();

				// left
				uint colTemp = col;
				uint rowTemp = row;
				while (wontMinOverflow(colTemp)
					&& colTemp - 1 >= 0
					&& matrix[rowTemp, colTemp - 1] == currentValue
					&& isVisited(rowTemp, colTemp - 1) == false)
				{
					colTemp--;
					branchingCellCol.Push(colTemp);
					branchingCellRow.Push(rowTemp);
					visitedCells[rowTemp, colTemp] = true;
					matrix[rowTemp, colTemp] = colour;
				}

				// right
				colTemp = col;
				rowTemp = row;
				while (wontMaxOverflow(colTemp)
					&& colTemp + 1 < cols
					&& matrix[rowTemp, colTemp + 1] == currentValue
					&& isVisited(rowTemp, colTemp + 1) == false)
				{
					colTemp++;
					branchingCellCol.Push(colTemp);
					branchingCellRow.Push(rowTemp);
					visitedCells[rowTemp, colTemp] = true;
					matrix[rowTemp, colTemp] = colour;
				}

				// up
				colTemp = col;
				rowTemp = row;
				while (wontMinOverflow(rowTemp)
					&& rowTemp - 1 >= 0
					&& matrix[rowTemp - 1, colTemp] == currentValue
					&& isVisited(rowTemp - 1, colTemp) == false)
				{
					rowTemp--;
					branchingCellCol.Push(colTemp);
					branchingCellRow.Push(rowTemp);
					visitedCells[rowTemp, colTemp] = true;
					matrix[rowTemp, colTemp] = colour;
				}

				// down
				colTemp = col;
				rowTemp = row;
				while (wontMaxOverflow(rowTemp)
					&& rowTemp + 1 < rows
					&& matrix[rowTemp + 1, colTemp] == currentValue
					&& isVisited(rowTemp + 1, colTemp) == false)
				{
					rowTemp++;
					branchingCellCol.Push(colTemp);
					branchingCellRow.Push(rowTemp);
					visitedCells[rowTemp, colTemp] = true;
					matrix[rowTemp, colTemp] = colour;
				}
			}
		}
	}
}