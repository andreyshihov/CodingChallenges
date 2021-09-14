using DrawingInConsole.ToolFactory;
using System;

namespace DrawingInConsole.Command
{
	public class UnknownCommand : BaseCommand
	{
		public override void Do(ConsoleCanvas canvas, MatrixToolFactory toolsFactory)
		{
			return;
		}
	}
}
