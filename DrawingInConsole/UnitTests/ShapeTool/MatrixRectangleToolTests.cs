using DrawingInConsole.Shape;
using DrawingInConsole.ShapeTool;
using Moq;
using NUnit.Framework;
using System;

namespace UnitTests.ShapeTool
{
	[TestFixture]
	class MatrixRectangleToolTests
	{
		[Test]
		public void GuardExceptions()
		{
			Assert.That(() => new MatrixRectangleTool(null), Throws.ArgumentNullException,
				"Drawing on the matrix requires the rectable.");

			Assert.That(() => new MatrixRectangleTool(new Mock<Rectangle>().Object).Draw(null), Throws.ArgumentNullException,
				"Drawing on the matrix requires the matrix.");

			Assert.That(() => new MatrixRectangleTool(new Mock<Rectangle>().Object).Draw(new char[10001, 10001]), Throws.InstanceOf<ApplicationException>(),
				"Matrices over 10000 cells are not supported.");
		}

		[TestCase((uint)0, (uint)0, (uint)3, (uint)3, 2, 2)]
		[TestCase((uint)0, (uint)0, (uint)0, (uint)3, 2, 2)]
		[TestCase((uint)0, (uint)3, (uint)0, (uint)1, 2, 2)]
		[TestCase((uint)3, (uint)3, (uint)5, (uint)5, 2, 2)]
		public void DrawOusideExceptions(uint x1, uint y1, uint x2, uint y2, int width, int height)
		{
			var matrix = new char[width, height];
			matrix.InitMatrix(' ');

			var outsideRectangle = new Rectangle(x1, y1, x2, y2);
			var sut = new MatrixRectangleTool(outsideRectangle);

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

			var dot = new Rectangle(x1, y1, x2, y2);
			var sut = new MatrixRectangleTool(dot);

			sut.Draw(actualMatrix);

			Assert.IsTrue(actualMatrix.CompareMatrix(expectedMatrix),
				"Should draw point on the matrix.");
		}

		[Test]
		public void DrawPortrait()
		{
			var actualMatrix = new char[5, 5];
			actualMatrix.InitMatrix(' ');

			var expectedMatrix = new char[5, 5];
			expectedMatrix.InitMatrix(' ');
			expectedMatrix[1, 1] = 'x';
			expectedMatrix[1, 2] = 'x';
			expectedMatrix[1, 3] = 'x';
			expectedMatrix[2, 1] = 'x';
			expectedMatrix[2, 2] = 'x';
			expectedMatrix[2, 3] = 'x';

			var portraitRectangle = new Rectangle(1, 1, 2, 3);
			var sut = new MatrixRectangleTool(portraitRectangle);

			sut.Draw(actualMatrix);

			Assert.IsTrue(actualMatrix.CompareMatrix(expectedMatrix),
				"Should draw portrait rectangle.");
		}

		[Test]
		public void DrawLanscape()
		{
			var actualMatrix = new char[5, 5];
			actualMatrix.InitMatrix(' ');

			var expectedMatrix = new char[5, 5];
			expectedMatrix.InitMatrix(' ');
			expectedMatrix[1, 1] = 'x';
			expectedMatrix[1, 2] = 'x';
			expectedMatrix[2, 1] = 'x';
			expectedMatrix[2, 2] = 'x';
			expectedMatrix[3, 1] = 'x';
			expectedMatrix[3, 2] = 'x';

			var landscapeRectangle = new Rectangle(1, 1, 3, 2);
			var sut = new MatrixRectangleTool(landscapeRectangle);

			sut.Draw(actualMatrix);

			Assert.IsTrue(actualMatrix.CompareMatrix(expectedMatrix),
				"Should draw landscape rectangle.");
		}

		[Test]
		public void DrawEqualRectangle()
		{
			var actualMatrix = new char[5, 5];
			actualMatrix.InitMatrix(' ');

			var expectedMatrix = new char[5, 5];
			expectedMatrix.InitMatrix(' ');
			expectedMatrix[1, 1] = 'x';
			expectedMatrix[1, 2] = 'x';
			expectedMatrix[1, 3] = 'x';
			expectedMatrix[2, 1] = 'x';
			expectedMatrix[2, 2] = ' ';
			expectedMatrix[2, 3] = 'x';
			expectedMatrix[3, 1] = 'x';
			expectedMatrix[3, 2] = 'x';
			expectedMatrix[3, 3] = 'x';

			var equalRectangle = new Rectangle(1, 1, 3, 3);
			var sut = new MatrixRectangleTool(equalRectangle);

			sut.Draw(actualMatrix);

			Assert.IsTrue(actualMatrix.CompareMatrix(expectedMatrix),
				"Should draw landscape rectangle.");
		}
	}
}
