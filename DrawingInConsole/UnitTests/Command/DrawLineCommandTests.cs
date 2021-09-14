using DrawingInConsole;
using DrawingInConsole.AreaTool;
using DrawingInConsole.Command;
using DrawingInConsole.ShapeTool;
using DrawingInConsole.ToolFactory;
using Moq;
using NUnit.Framework;

namespace UnitTests.Command
{
	[TestFixture]
	class DrawLineCommandTests
	{
		[Test]
		public void GuardExceptions()
		{
			Assert.That(() => new DrawLineCommand(null), 
				Throws.ArgumentNullException,
				"There should be at least one argument.");

			Assert.That(() => new DrawLineCommand(string.Empty), 
				Throws.ArgumentNullException,
				"There should be at least one argument.");

			Assert.That(() => new DrawLineCommand("L 1 1 1 "), 
				Throws.ArgumentException,
				"There should be exactly 5 arguments in the command.");

			Assert.That(() => new DrawLineCommand("L 1 1 1 1 1"), 
				Throws.ArgumentException,
				"There should be exactly 5 arguments in the command.");

			Assert.That(() => new DrawLineCommand("L x 1 1 1"), 
				Throws.ArgumentException,
				"Second argument should be number.");

			Assert.That(() => new DrawLineCommand("L 1 x 1 1"), 
				Throws.ArgumentException,
				"Third argument should be number.");

			Assert.That(() => new DrawLineCommand("L 1 1 x 1"), 
				Throws.ArgumentException,
				"Fourth argument should be number.");

			Assert.That(() => new DrawLineCommand("L 1 1 1 x"),
				Throws.ArgumentException,
				"Fifth argument should be number.");

			Assert.That(() => new DrawLineCommand("L 1 1 1 1").Do(null, It.IsAny<MatrixToolFactory>()), 
				Throws.ArgumentNullException,
				"Canvas can't be null.");

			Assert.That(() => new DrawLineCommand("L 1 1 1 1").Do(It.IsAny<ConsoleCanvas>(), null),
				Throws.ArgumentNullException,
				"Drawing tool factory can't be null.");
		}

		[Test]
		public void Do()
		{
			var mockToolsFactory = new Mock<MatrixToolFactory>();
			mockToolsFactory.Setup(m => m.CreateLineTool(0, 0, 0, 0)).Returns(new Mock<IMatrixShapeTool>().Object);

			var mockCanvas = new Mock<ConsoleCanvas>();

			var sut = new DrawLineCommand("L 1 1 1 1");

			sut.Do(mockCanvas.Object, mockToolsFactory.Object);

			mockToolsFactory.Verify(m => m.CreateLineTool(0 /*zero based*/, 0, 0, 0), Times.Once,
				"Factory should instatiate drawing tool with valid parameters.");

			mockCanvas.Verify(m => m.Draw(It.IsAny<IMatrixShapeTool>()), Times.Once,
				"Shape should be drawn.");

			mockCanvas.Verify(m => m.Render(), Times.Once,
				"Shape should be rendered on the canvas.");
		}

		[TestCase((uint)0, (uint)0, (uint)9, (uint)0, "L 10 1 1 1")]
		[TestCase((uint)0, (uint)0, (uint)0, (uint)9, "L 1 10 1 1")]
		public void Swap(uint x1, uint y1, uint x2, uint y2, string command)
		{
			var mockToolsFactory = new Mock<MatrixToolFactory>();
			mockToolsFactory.Setup(m => m.CreateLineTool(x1, y1, x2, y2)).Returns(new Mock<IMatrixShapeTool>().Object);

			var mockCanvas = new Mock<ConsoleCanvas>();

			var sut = new DrawLineCommand(command);

			sut.Do(mockCanvas.Object, mockToolsFactory.Object);

			mockToolsFactory.Verify(m => m.CreateLineTool(x1 /*zero based*/, y1, x2, y2), Times.Once,
				"Factory should instatiate drawing tool with swapped parameters.");
		}
	}
}
