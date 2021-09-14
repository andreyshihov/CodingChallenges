using DrawingInConsole;
using DrawingInConsole.AreaTool;
using DrawingInConsole.Command;
using DrawingInConsole.ToolFactory;
using Moq;
using NUnit.Framework;

namespace UnitTests.Command
{
	[TestFixture]
	class BucketFillAreaCommandTests
	{
		[Test]
		public void GuardExceptions()
		{
			Assert.That(() => new BucketFillAreaCommand(null), 
				Throws.ArgumentNullException,
				"There should be at least one argument.");

			Assert.That(() => new BucketFillAreaCommand(string.Empty), 
				Throws.ArgumentNullException,
				"There should be at least one argument.");

			Assert.That(() => new BucketFillAreaCommand("B 1 1"), 
				Throws.ArgumentException,
				"There should be exactly 4 arguments in the command.");

			Assert.That(() => new BucketFillAreaCommand("B 1 1 a b"), 
				Throws.ArgumentException,
				"There should be exactly 4 arguments in the command.");

			Assert.That(() => new BucketFillAreaCommand("B x 1 o"), 
				Throws.ArgumentException,
				"Second argument should be number.");

			Assert.That(() => new BucketFillAreaCommand("B 1 y o"), 
				Throws.ArgumentException,
				"Third argument should be number.");

			Assert.That(() => new BucketFillAreaCommand("B 1 1 ox"), 
				Throws.ArgumentException,
				"Fourth argument should be one character.");

			Assert.That(() => new BucketFillAreaCommand("B 1 1 x").Do(null, It.IsAny<MatrixToolFactory>()), 
				Throws.ArgumentNullException,
				"Canvas can't be null.");

			Assert.That(() => new BucketFillAreaCommand("B 1 1 x").Do(It.IsAny<ConsoleCanvas>(), null),
				Throws.ArgumentNullException,
				"Drawing tool factory can't be null.");
		}

		[Test]
		public void Do()
		{
			var mockToolsFactory = new Mock<MatrixToolFactory>();
			mockToolsFactory.Setup(m => m.CreateFillingTool(0, 0, 'x')).Returns(new Mock<IMatrixAreaFillingTool>().Object);

			var mockCanvas = new Mock<ConsoleCanvas>();

			var sut = new BucketFillAreaCommand("B 1 1 x");

			sut.Do(mockCanvas.Object, mockToolsFactory.Object);

			mockToolsFactory.Verify(m => m.CreateFillingTool(0, 0, 'x'), Times.Once,
				"Factory should instatiate filling tool with valid parameters.");

			mockCanvas.Verify(m => m.Fill(It.IsAny<IMatrixAreaFillingTool>()), Times.Once,
				"Area should be filled.");

			mockCanvas.Verify(m => m.Render(), Times.Once,
				"Filled area should be rendered on the canvas.");
		}
	}
}
