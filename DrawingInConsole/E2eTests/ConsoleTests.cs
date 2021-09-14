//////////////////////////////////////////////////////////////////////////
/// WARNING! When debuggin these tests, keep an eye on building up 
/// (unexited) DrawingInConsole.exe processes in Task Manager and end them when start experience difficulties
//////////////////////////////////////////////////////////////////////////

using NUnit.Framework;
using System.Threading;

namespace E2eTests
{
	public class ConsoleTests
	{
		[Test]
		public void RenderCanvas()
		{
			var expected =
@"-----
|   |
|   |
|   |
-----";

			using var process = new AppProcess();
			process.WriteLine("C 3 3");

			var actual = process.ReadAllLines();

			Assert.That(actual.Contains(expected),
				"The application must render the canvas.");
		}

		[Test]
		public void QuitBeforeCanvasCreated()
		{
			using var process = new AppProcess();
			process.WriteLine("Q");

			Assert.That(process.HasExited,
				"The application should quit.");
		}

		[Test]
		public void QuitAfterCanvasCreated()
		{
			using var process = new AppProcess();
			process.WriteLine("C 3 3");
			process.ReadAllLines(); // flush the output
			process.WriteLine("Q");

			Assert.That(process.HasExited,
				"The application should quit.");
		}

		[Test]
		public void UnknownCommandCantCreate()
		{
			var expected = "Can't create canvas";

			using var process = new AppProcess();
			process.WriteLine("asdfsd");
			var actual = process.ReadAllLines();

			Assert.That(actual.Contains(expected),
				"The application must handle unknown command.");
		}

		[Test]
		public void UnknownCommandCantProcess()
		{
			var expected = "Can't process command";

			using var process = new AppProcess();
			process.WriteLine("C 3 3");
			process.ReadAllLines(); // flush the output
			process.WriteLine("asdasd");
			var actual = process.ReadAllLines();

			Assert.That(actual.Contains(expected),
				"The application must handle unknown command.");
		}

		[Test]
		public void RenderHorizontalLine()
		{
			var expected =
@"-----
|   |
|xxx|
|   |
-----";

			using var process = new AppProcess();
			process.WriteLine("C 3 3");
			process.ReadAllLines(); // flush the output
			process.WriteLine("L 1 2 3 2");
			var actual = process.ReadAllLines();

			Assert.That(actual.Contains(expected),
				"The application must render the horizontal line.");
		}

		[Test]
		public void RenderVerticalLine()
		{
			var expected =
@"-----
| x |
| x |
| x |
-----";

			using var process = new AppProcess();
			process.WriteLine("C 3 3");
			process.ReadAllLines(); // flush the output
			process.WriteLine("L 2 1 2 3");
			var actual = process.ReadAllLines();

			Assert.That(actual.Contains(expected),
				"The application must render the vertical line.");
		}

		[Test]
		public void RenderRectangle()
		{
			var expected =
@"-------
|xxx  |
|x x  |
|xxx  |
|     |
|     |
-------";

			using var process = new AppProcess();
			process.WriteLine("C 5 5");
			process.ReadAllLines(); // flush the output
			process.WriteLine("R 1 1 3 3");
			var actual = process.ReadAllLines();

			Assert.That(actual.Contains(expected),
				"The application must render the equal sided rectangle.");
		}

		[Test]
		public void FillRectangle()
		{
			var expected =
@"-------
|xxx  |
|xox  |
|xxx  |
|     |
|     |
-------";

			using var process = new AppProcess();
			process.WriteLine("C 5 5");
			process.ReadAllLines(); // flush the output
			process.WriteLine("R 1 1 3 3");
			process.ReadAllLines(); // flush the output
			process.WriteLine("B 2 2 o");
			var actual = process.ReadAllLines();

			Assert.That(actual.Contains(expected),
				"The application must render 'bucket filled' equal sided rectangle.");
		}

		[Test]
		public void FillArea()
		{
			var expected =
@"-------
|xxxoo|
|x xoo|
|xxxoo|
|  xoo|
|  xoo|
-------";

			using var process = new AppProcess();
			process.WriteLine("C 5 5");
			process.ReadAllLines(); // flush the output
			process.WriteLine("R 1 1 3 3");
			process.ReadAllLines(); // flush the output
			process.WriteLine("L 3 4 3 5");
			process.ReadAllLines(); // flush the output
			process.WriteLine("B 5 5 o");
			var actual = process.ReadAllLines();

			Assert.That(actual.Contains(expected),
				"The application must render 'bucket filled' equal sided rectangle.");
		}
	}
}