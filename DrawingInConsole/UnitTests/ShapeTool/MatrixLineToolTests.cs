using DrawingInConsole.Shape;
using DrawingInConsole.ShapeTool;
using Moq;
using NUnit.Framework;
using System;

namespace UnitTests.ShapeTool
{
	[TestFixture]
	class MatrixLineToolTests
	{
		[Test]
		public void GuardExceptions()
		{
			Assert.That(() => new MatrixLineTool(null), Throws.ArgumentNullException,
				"Drawing on the matrix requires the line.");

			Assert.That(() => new MatrixLineTool(new Mock<Line>().Object).Draw(null), Throws.ArgumentNullException,
				"Drawing on the matrix requires the matrix.");

			Assert.That(() => new MatrixLineTool(new Mock<Line>().Object).Draw(new char[10001, 10001]), Throws.InstanceOf<ApplicationException>(),
				"Matrices over 10000 cells are not supported.");
		}

		[TestCase((uint)0, (uint)0, (uint)3, (uint)3, 10, 10)]
		[TestCase((uint)3, (uint)3, (uint)0, (uint)0, 10, 10)]
		public void DrawDiagonalExceptions(uint x1, uint y1, uint x2, uint y2, int width, int height)
		{
			var matrix = new char[width, height];
			matrix.InitMatrix(' ');

			var diagonalLine = new Line(x1, y1, x2, y2);
			var sut = new MatrixLineTool(diagonalLine);

			Assert.That(() => sut.Draw(matrix), Throws.InstanceOf<ApplicationException>(),
				"Diagonal lines are not allowed.");
		}

		[TestCase((uint)0, (uint)0, (uint)3, (uint)3, 2, 2)]
		[TestCase((uint)0, (uint)0, (uint)0, (uint)3, 2, 2)]
		[TestCase((uint)0, (uint)3, (uint)0, (uint)1, 2, 2)]
		public void DrawOusideExceptions(uint x1, uint y1, uint x2, uint y2, int width, int height)
		{
			var matrix = new char[width, height];
			matrix.InitMatrix(' ');

			var diagonalLine = new Line(x1, y1, x2, y2);
			var sut = new MatrixLineTool(diagonalLine);

			Assert.That(() => sut.Draw(matrix), Throws.InstanceOf<ApplicationException>(),
				"Drawing outside of the matrix is not allowed.");
		}

		[TestCase((uint)0, (uint)0, (uint)0, (uint)0, 2, 2)]
		[TestCase((uint)4, (uint)4, (uint)4, (uint)4, 5, 5)]
		[TestCase((uint)1, (uint)1, (uint)1, (uint)1, 2, 2)]
		public void DrawDot(uint x1, uint y1, uint x2, uint y2, int width, int height)
		{
			var actualMatrix = new char[width, height];
			actualMatrix.InitMatrix(' ');

			var expectedMatrix = new char[width, height];
			expectedMatrix.InitMatrix(' ');
			expectedMatrix[x1, y1] = 'x';

			var dot = new Line(x1, y1, x2, y2);
			var sut = new MatrixLineTool(dot);

			sut.Draw(actualMatrix);

			Assert.IsTrue(actualMatrix.CompareMatrix(expectedMatrix),
				"Should draw point on the matrix.");
		}

		[TestCase((uint)0, (uint)0, (uint)1, (uint)0, 2, 2)]
		[TestCase((uint)0, (uint)4, (uint)4, (uint)4, 5, 5)]
		[TestCase((uint)1, (uint)4, (uint)3, (uint)4, 5, 5)]
		[TestCase((uint)3, (uint)4, (uint)4, (uint)4, 5, 5)]
		public void DrawHorisontalLine(uint x1, uint y1, uint x2, uint y2, int width, int height)
		{
			var actualMatrix = new char[width, height];
			actualMatrix.InitMatrix(' ');

			var expectedMatrix = new char[width, height];
			expectedMatrix.InitMatrix(' ');
			for(uint x = x1; x <= x2; x++)
				expectedMatrix[x, y1] = 'x';

			var horisontalLine = new Line(x1, y1, x2, y2);
			var sut = new MatrixLineTool(horisontalLine);

			sut.Draw(actualMatrix);

			Assert.IsTrue(actualMatrix.CompareMatrix(expectedMatrix),
				"Should draw horizonal line.");
		}

		[TestCase((uint)0, (uint)0, (uint)0, (uint)1, 2, 2)]
		[TestCase((uint)2, (uint)2, (uint)2, (uint)5, 10, 10)]
		[TestCase((uint)9, (uint)0, (uint)9, (uint)9, 10, 10)]
		public void DrawVerticalLine(uint x1, uint y1, uint x2, uint y2, int width, int height)
		{
			var actualMatrix = new char[width, height];
			actualMatrix.InitMatrix(' ');

			var expectedMatrix = new char[width, height];
			expectedMatrix.InitMatrix(' ');
			for (uint y = y1; y <= y2; y++)
				expectedMatrix[x1, y] = 'x';

			var verticalLine = new Line(x1, y1, x2, y2);
			var sut = new MatrixLineTool(verticalLine);

			sut.Draw(actualMatrix);

			Assert.IsTrue(actualMatrix.CompareMatrix(expectedMatrix),
				"Should draw vertical line.");
		}
	}
}
