using DrawingInConsole.Command;
using DrawingInConsole.ToolFactory;
using System;

namespace DrawingInConsole
{
	public class ConsoleUserInput
	{
		private readonly ConsoleCanvas canvas;
		private readonly MatrixToolFactory factory;

		public ConsoleUserInput(ConsoleCanvas canvas, MatrixToolFactory toolsFactory)
		{
			this.canvas = canvas ?? throw new ArgumentNullException(nameof(canvas));
			this.factory = toolsFactory ?? throw new ArgumentNullException(nameof(toolsFactory));
		}

		public void ProcessCommand(BaseCommand command)
		{
			if (command == null)
				throw new ArgumentNullException(nameof(command));

			command.Do(canvas, factory);
		}
	}
}
