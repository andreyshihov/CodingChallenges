using DrawingInConsole.AreaTool;
using Moq;
using NUnit.Framework;
using System;

namespace UnitTests.AreaTool
{
	[TestFixture]
	class AreaToolTests
	{
		[Test]
		public void GuardExceptions()
		{
			Assert.That(() => new MatrixBfsAreaFillingTool(It.IsAny<uint>(), It.IsAny<uint>(), It.IsAny<char>()).Fill(null), Throws.ArgumentNullException,
				"Filling the matrix requires the matrix.");

			Assert.That(() => new MatrixBfsAreaFillingTool(It.IsAny<uint>(), It.IsAny<uint>(), It.IsAny<char>()).Fill(new char[10001, 10001]), Throws.InstanceOf<ApplicationException>(),
				"Matrices over 10000 cells are not supported.");

			Assert.That(() => new MatrixBfsAreaFillingTool(2 /*zero based*/, 2, It.IsAny<char>()).Fill(new char[2, 2]), Throws.InstanceOf<ApplicationException>(),
				"Filling is not allowed outside the canvas.");
		}

		[TestCase(200, 20, 'c', (uint)1, (uint)0)]
		[TestCase(1000, 1000, 'c', (uint)1, (uint)0)]
		[TestCase(1, 1, 'c', (uint)0, (uint)0)]
		[TestCase(2, 5, 'c', (uint)0, (uint)1)]
		public void FillEmpy(int rows, int cols, char colour, uint startX, uint startY)
		{
			var matrix = new char[rows, cols];
			matrix.InitMatrix(' ');

			var sut = new MatrixBfsAreaFillingTool(startX, startY, colour);
			sut.Fill(matrix);

			Assert.IsTrue(matrix.CheckMatrix(colour),
				"The matrix should be filled with this colour.");
		}

		[Test]
		public void RefillEmpy()
		{
			var matrix = new char[200, 20];
			matrix.InitMatrix('.');

			var sut = new MatrixBfsAreaFillingTool(0, 0, 'c');
			sut.Fill(matrix);

			Assert.IsTrue(matrix.CheckMatrix('c'),
				"The matrix should be re-filled with this colour.");
		}

		[Test]
		public void FillWithLines()
		{
			var actualMatrix = new char[20, 20];
			actualMatrix.InitMatrix(' ');
			actualMatrix[1, 1] = 'x';
			actualMatrix[1, 2] = 'x';
			actualMatrix[1, 3] = 'x';

			var expectedMatrix = new char[20, 20];
			expectedMatrix.InitMatrix('c');
			expectedMatrix[1, 1] = 'x';
			expectedMatrix[1, 2] = 'x';
			expectedMatrix[1, 3] = 'x';

			var sut = new MatrixBfsAreaFillingTool(0, 0, 'c');
			sut.Fill(actualMatrix);

			Assert.IsTrue(actualMatrix.CompareMatrix(expectedMatrix),
				"The matrix should be filled with this colour.");
		}

		[Test]
		public void FillTheLine()
		{
			var actualMatrix = new char[20, 20];
			actualMatrix.InitMatrix(' ');
			actualMatrix[1, 1] = 'x';
			actualMatrix[1, 2] = 'x';
			actualMatrix[1, 3] = 'x';

			var expectedMatrix = new char[20, 20];
			expectedMatrix.InitMatrix(' ');
			expectedMatrix[1, 1] = 'c';
			expectedMatrix[1, 2] = 'c';
			expectedMatrix[1, 3] = 'c';

			var sut = new MatrixBfsAreaFillingTool(1, 1, 'c');
			sut.Fill(actualMatrix);

			Assert.IsTrue(actualMatrix.CompareMatrix(expectedMatrix),
				"The line should be filled with this colour.");
		}

		[Test]
		public void FillWithRectangle()
		{
			var actualMatrix = new char[20, 20];
			actualMatrix.InitMatrix(' ');
			actualMatrix[1, 1] = 'x';
			actualMatrix[1, 2] = 'x';
			actualMatrix[1, 3] = 'x';

			actualMatrix[2, 1] = 'x';
			actualMatrix[2, 3] = 'x';

			actualMatrix[3, 1] = 'x';
			actualMatrix[3, 2] = 'x';
			actualMatrix[3, 3] = 'x';

			var expectedMatrix = new char[20, 20];
			expectedMatrix.InitMatrix('c');
			expectedMatrix[1, 1] = 'x';
			expectedMatrix[1, 2] = 'x';
			expectedMatrix[1, 3] = 'x';

			expectedMatrix[2, 1] = 'x';
			expectedMatrix[2, 2] = ' ';
			expectedMatrix[2, 3] = 'x';

			expectedMatrix[3, 1] = 'x';
			expectedMatrix[3, 2] = 'x';
			expectedMatrix[3, 3] = 'x';

			var sut = new MatrixBfsAreaFillingTool(5, 5, 'c');
			sut.Fill(actualMatrix);

			Assert.IsTrue(actualMatrix.CompareMatrix(expectedMatrix),
				"The matrix should be filled with this colour.");
		}

		[Test]
		public void FillWithin3by3Rectangle()
		{
			var actualMatrix = new char[20, 20];
			actualMatrix.InitMatrix(' ');
			actualMatrix[1, 1] = 'x';
			actualMatrix[1, 2] = 'x';
			actualMatrix[1, 3] = 'x';

			actualMatrix[2, 1] = 'x';
			actualMatrix[2, 3] = 'x';

			actualMatrix[3, 1] = 'x';
			actualMatrix[3, 2] = 'x';
			actualMatrix[3, 3] = 'x';

			var expectedMatrix = new char[20, 20];
			expectedMatrix.InitMatrix(' ');
			expectedMatrix[1, 1] = 'x';
			expectedMatrix[1, 2] = 'x';
			expectedMatrix[1, 3] = 'x';

			expectedMatrix[2, 1] = 'x';
			expectedMatrix[2, 2] = 'c';
			expectedMatrix[2, 3] = 'x';

			expectedMatrix[3, 1] = 'x';
			expectedMatrix[3, 2] = 'x';
			expectedMatrix[3, 3] = 'x';

			var sut = new MatrixBfsAreaFillingTool(2/*zero based*/, 2, 'c');
			sut.Fill(actualMatrix);

			Assert.IsTrue(actualMatrix.CompareMatrix(expectedMatrix),
				"The matrix should be filled with this colour.");
		}

		[Test]
		public void FillWithin4by4Rectangle()
		{
			var actualMatrix = new char[20, 30];
			actualMatrix.InitMatrix(' ');
			actualMatrix[1, 1] = 'x';
			actualMatrix[1, 2] = 'x';
			actualMatrix[1, 3] = 'x';
			actualMatrix[1, 4] = 'x';

			actualMatrix[2, 1] = 'x';
			actualMatrix[2, 4] = 'x';
			actualMatrix[3, 1] = 'x';
			actualMatrix[3, 4] = 'x';

			actualMatrix[4, 1] = 'x';
			actualMatrix[4, 2] = 'x';
			actualMatrix[4, 3] = 'x';
			actualMatrix[4, 4] = 'x';

			var expectedMatrix = new char[20, 30];
			expectedMatrix.InitMatrix(' ');
			expectedMatrix[1, 1] = 'x';
			expectedMatrix[1, 2] = 'x';
			expectedMatrix[1, 3] = 'x';
			expectedMatrix[1, 4] = 'x';

			expectedMatrix[2, 1] = 'x';
			expectedMatrix[2, 2] = 'c';
			expectedMatrix[2, 3] = 'c';
			expectedMatrix[2, 4] = 'x';

			expectedMatrix[3, 1] = 'x';
			expectedMatrix[3, 2] = 'c';
			expectedMatrix[3, 3] = 'c';
			expectedMatrix[3, 4] = 'x';

			expectedMatrix[4, 1] = 'x';
			expectedMatrix[4, 2] = 'x';
			expectedMatrix[4, 3] = 'x';
			expectedMatrix[4, 4] = 'x';

			var sut = new MatrixBfsAreaFillingTool(3/*zero based*/, 3, 'c');
			sut.Fill(actualMatrix);

			Assert.IsTrue(actualMatrix.CompareMatrix(expectedMatrix),
				"The matrix should be filled with this colour.");
		}
	}
}
