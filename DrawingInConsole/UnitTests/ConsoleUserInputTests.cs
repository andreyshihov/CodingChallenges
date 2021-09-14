using DrawingInConsole;
using DrawingInConsole.Command;
using DrawingInConsole.ToolFactory;
using Moq;
using NUnit.Framework;

namespace UnitTests
{
	[TestFixture]
	class ConsoleUserInputTests
	{
		[Test]
		public void GuardExceptions()
		{
			Assert.That(() => new ConsoleUserInput(null, It.IsAny<MatrixToolFactory>()), Throws.ArgumentNullException,
					"User input can't be instatiated without canvas.");

			Assert.That(() => new ConsoleUserInput(It.IsAny<ConsoleCanvas>(), null), Throws.ArgumentNullException,
					"User input can't be instatiated without factory.");

			Assert.That(() => new ConsoleUserInput(new Mock<ConsoleCanvas>().Object, new Mock<MatrixToolFactory>().Object).ProcessCommand(null), Throws.ArgumentNullException,
					"ProcessCommand requires BaseCommand instance.");
		}

		[Test]
		public void ProcessCommand()
		{
			var mockConsoleCanvas = new Mock<ConsoleCanvas>();
			var mockBaseCommand = new Mock<BaseCommand>();
			var mockToolsFactory = new Mock<MatrixToolFactory>();

			var userInput = new ConsoleUserInput(mockConsoleCanvas.Object, mockToolsFactory.Object);
			userInput.ProcessCommand(mockBaseCommand.Object);

			mockBaseCommand.Verify(m => m.Do(It.Is<ConsoleCanvas>(cc => cc == mockConsoleCanvas.Object), It.Is<MatrixToolFactory>(cc => cc == mockToolsFactory.Object)), Times.Once,
				"Command should be executed on canvas.");
		}
	}
}
