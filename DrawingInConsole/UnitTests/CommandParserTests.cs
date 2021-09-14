using DrawingInConsole;
using DrawingInConsole.Command;
using NUnit.Framework;

namespace UnitTests
{
	[TestFixture]
	class CommandParserTests
	{
		[Test]
		public void GuardExceptions()
		{
			Assert.That(() => CommandParser.ParseCommand(string.Empty), Throws.ArgumentNullException,
				"There should be at least one argument.");

			Assert.That(() => CommandParser.ParseCommand(null), Throws.ArgumentNullException,
				"There should be at least one argument.");

			Assert.That(() => CommandParser.ParseCommand("C"), Throws.ArgumentException,
				"Canvas resizing is not supported.");

			Assert.IsInstanceOf<DrawLineCommand>(CommandParser.ParseCommand("L 1 1 1 1"),
				"L command should create DrawLineCommand instance.");

			Assert.IsInstanceOf<DrawRectangleCommand>(CommandParser.ParseCommand("R 1 1 1 1"),
				"R command should create DrawRectangleCommand instance.");

			Assert.IsInstanceOf<BucketFillAreaCommand>(CommandParser.ParseCommand("B 1 1 o"),
				"B command should create BucketFillAreaCommand instance.");

			Assert.IsInstanceOf<QuitCommand>(CommandParser.ParseCommand("Q"),
				"Q command should create QuitCommand instance.");

			Assert.That(() => CommandParser.ParseCommand("Cc"), Throws.ArgumentException,
				"Should throw exception for unknown commands.");
		}
	}
}
