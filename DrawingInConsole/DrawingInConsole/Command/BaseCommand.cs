using DrawingInConsole.ToolFactory;
using System;

namespace DrawingInConsole.Command
{
    public abstract class BaseCommand
    {
		protected BaseCommand() { }

		protected BaseCommand(string args)
		{
			if (string.IsNullOrWhiteSpace(args))
				throw new ArgumentNullException(nameof(args));
		}

		public abstract void Do(ConsoleCanvas canvas, MatrixToolFactory toolsFactory);

		protected virtual void SwapArguments(ref uint a, ref uint b)
		{
			var tmp = a;
			a = b;
			b = tmp;
		}
	}
}
