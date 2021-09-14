using DrawingInConsole;
using DrawingInConsole.AreaTool;
using DrawingInConsole.ShapeTool;
using Moq;
using NUnit.Framework;
using System;

namespace UnitTests
{
	[TestFixture]
	class ConsoleCanvasTests
	{
		[SetUp]
		public void Setup()
		{
		}

		[Test]
		public void Constructor()
		{
			var actual = ConsoleCanvas.FromArgs("C 1 1");

			Assert.True(actual.CanvasWidth == 1, "Width of the canvas should match with the value from the second parameter");
			Assert.True(actual.CanvasHeight == 1, "Width of the canvas should match with the value from the second parameter");
		}

		[Test]
		public void Draw()
		{
			var mockTool = new Mock<IMatrixShapeTool>();

			var actual = ConsoleCanvas.FromArgs("C 1 2");

			actual.Draw(mockTool.Object);

			mockTool.Verify(m => m.Draw(It.Is<char[,]>(arg => arg.GetLength(0) == 1 && arg.GetLength(1) == 2)), Times.Once,
				"Canvas matrix should have appropriate size for drawing.");

			mockTool.Verify(m => m.Draw(It.Is<char[,]>(arg => arg[0,0].Equals(' ') && arg[0, 1].Equals(' '))), Times.Once,
				"Canvas matrix should be empty for drawing.");
		}

		[Test]
		public void Fill()
		{
			var mockTool = new Mock<IMatrixAreaFillingTool>();

			var actual = ConsoleCanvas.FromArgs("C 1 2");

			actual.Fill(mockTool.Object);

			mockTool.Verify(m => m.Fill(It.Is<char[,]>(arg => arg.GetLength(0) == 1 && arg.GetLength(1) == 2)), Times.Once,
				"Canvas matrix should have appropriate size for filling.");

			mockTool.Verify(m => m.Fill(It.Is<char[,]>(arg => arg[0, 0].Equals(' ') && arg[0, 1].Equals(' '))), Times.Once,
				"Canvas matrix should be empty for filling.");
		}

		[Test]
		public void GuardExceptions()
		{
			Assert.IsNull(ConsoleCanvas.FromArgs("Q"),
				"Canvas should be null if user entered Q command.");

			Assert.That(() => ConsoleCanvas.FromArgs("a b"), Throws.ArgumentException,
				"Should throw exception if number actual number of arguments is less than 3.");

			Assert.That(() => ConsoleCanvas.FromArgs(string.Empty), Throws.InstanceOf<ArgumentException>(),
				"Should throw exception if number actual number of arguments is less than 3.");

			Assert.That(() => ConsoleCanvas.FromArgs("a b c d"), Throws.InstanceOf<ArgumentException>(),
				"Should throw exception if number actual number of arguments is more than 3.");

			Assert.That(() => ConsoleCanvas.FromArgs("c b c"), Throws.InstanceOf<ArgumentException>(),
				"Should throw exception if first argument is not equal to C.");

			Assert.That(() => ConsoleCanvas.FromArgs("C b c"), Throws.InstanceOf<ArgumentException>(),
				"Should throw exception if can't int.Parse second parameter");

			Assert.That(() => ConsoleCanvas.FromArgs("C 1 c"), Throws.InstanceOf<ArgumentException>(),
				"Should throw exception if can't int.Parse third parameter");

			Assert.That(() => ConsoleCanvas.FromArgs("C 1 1").Fill(null), Throws.InstanceOf<ArgumentNullException>(),
				"Should throw exception if Filler instance is not present.");

			Assert.That(() => ConsoleCanvas.FromArgs("C 1 1").Draw(null), Throws.InstanceOf<ArgumentNullException>(),
				"Should throw exception if Drawer instance is not present.");
		}
	}
}