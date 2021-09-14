using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace E2eTests
{
	class AppProcess : IDisposable
	{
		private readonly Process process;

		public string OutputData { get; private set; }

		public bool HasExited { get { return process.HasExited; } }

		public AppProcess()
		{
			process = StartApplication();
		}

		public void WriteLine(string cmd)
		{
			process.StandardInput.WriteLine(cmd);
			process.StandardInput.Flush();
			Thread.Sleep(150); // wait for the app to respond
		}

		public string ReadAllLines()
		{
			Thread.Sleep(100); // wait for the app to respond
			var lines = OutputData;
			OutputData = string.Empty;
			return lines;
		}

		public void Dispose()
		{
			if (process != null)
			{
				WriteLine("Q");
				Thread.Sleep(100);
				process.Kill();
			}
		}

		private Process StartApplication()
		{
			var processInfo = new ProcessStartInfo
			{
				FileName = Path.Combine(AppContext.BaseDirectory, "DrawingInConsole.exe"),
				WindowStyle = ProcessWindowStyle.Hidden,
				CreateNoWindow = true,
				UseShellExecute = false,
				RedirectStandardInput = true,
				RedirectStandardOutput = true
			};

			var process = Process.Start(processInfo);
			process.OutputDataReceived += new DataReceivedEventHandler(OutputHandler);
			process.BeginOutputReadLine();
			return process;
		}

		void OutputHandler(object sendingProcess, DataReceivedEventArgs outLine)
		{
			OutputData += outLine.Data + Environment.NewLine;
		}
	}
}
