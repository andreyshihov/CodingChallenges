using DrawingInConsole.AreaTool;
using DrawingInConsole.ShapeTool;
using DrawingInConsole.ToolFactory;
using Moq;
using NUnit.Framework;

namespace UnitTests.ToolFactory
{
	[TestFixture]
	class ConsoleMatrixToolFactoryTests
	{
		[Test]
		public void CreatesTools()
		{
			Assert.IsInstanceOf<MatrixBfsAreaFillingTool>(
				new ConsoleMatrixToolFactory().CreateFillingTool(It.IsAny<uint>(), It.IsAny<uint>(), It.IsAny<char>()),
				"Factory should create an instance of filling tool.");

			Assert.IsInstanceOf<MatrixLineTool>(
				new ConsoleMatrixToolFactory().CreateLineTool(It.IsAny<uint>(), It.IsAny<uint>(), It.IsAny<uint>(), It.IsAny<uint>()),
				"Factory should create an instance of line tool.");

			Assert.IsInstanceOf<MatrixRectangleTool>(
				new ConsoleMatrixToolFactory().CreateRectangleTool(It.IsAny<uint>(), It.IsAny<uint>(), It.IsAny<uint>(), It.IsAny<uint>()),
				"Factory should create an instance of rectangle tool.");

		}
	}
}
