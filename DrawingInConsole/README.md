# Console drawing application

## Assumptions based on the original requirements and Sample I/O

- The application should work with the reasonably sized matrices. _Assumed solution_:
  - The application limits matrix size up to the Console Width and Height properties
  - The application's API limits matrix size up to 10K cells
- The application doesn't support drawing and filling shapes outside of the canvas. _Assumed solution_:
  - If the user's entered parameters include any point ouside of the canvas, application throws new `ApplicationException`.
- The application doesn't support canvas resizing. _Assumed solution_:
  - Once the initial canvas has been created and the user attempts to execute another canvas command, the application throws new `ApplicationException`.
  - Once the initial canvas has been created and user resized Console Window, the application throws new `ApplicationException` after the next command.
- "_Basic_" user  experience in the Sample I/O. _Assumed solution_:
  - The application user's experience has been enriched with friendly messages and command usage instructions.

## Instructions (How to run)

### 1. Find the console application

- If not done so earlier, extract the archived solution folder into the new folder, then navigate into that folder
- After opening the root solution folder, navigate to the following path: `DrawingInConsole\bin\Debug\netcoreapp3.1`

### 2. Run the application

```text
Warning! If you are not in the isolated environemnt, consult your corporate security team if it safe to do the following steps:
```

- double clik on `DrawingInConsole.exe` file and follow the instruction prompted on the screen.

## Instructions (How to build)

### Prerequisites

To be able to build this solution, you'll need the following programs and frameworks to be installed on your machine:

- `Microsoft Visual Studio Community 2019 Version 16.11.1` or higher
- `.NET Core 3.1`

```text
Warning! If you are not in the isolated environemnt, consult your corporate security team if it safe to do the following steps:
```

1. Extract an archived solution folder into the new folder
2. Navigate to the new folder
3. Find and double click on the `DrawingInConsole.sln` file. This action will launch Visual Studio and open up a development environment for this application. Click _Yes_ to all prompt Warning messages.
4. Once the solution is fully loaded, click on the Green Arrow (Play) buttion located in a tool bar, below the main menu.

### E2E tests notes

#### Hanging process

E2E tests create the new process and interact with it using Standard I/O. Keep in mind that during interrupted debuggin session, Application Process might not be finished/exited/killed and continue consuming machine's CPU and Memory.

To resolve this issue, open up the `Task Manager` tool and find the `DrawingInConsole` subprocess (might be many) under the `Microsoft Visual Studio` main process. Right click on each process and select _End_ in the context menu.

#### Thread.Sleep(n)

E2E tests might be runnint too fast and
falsely `Assert` before Application process will be able to respond with produced output. To manage this issue few `Thread.Sleep(100)` commands have been added in `AppProcess` class of the `E2eTests` solution. Depending on the performance of the machine where these tests are executed, it might be necessary to increase or decrease this value.

The end.
