using System.Diagnostics;

namespace Elevate
{
    internal static class Constants
    {
        public static string HelpArg => $"Elevate {Version?? string.Empty}\r\n(c) 2007 - John Robbins - www.wintellect.com\r\nContributors: 2022 - Zafer Balkan: .NET 6 migration & refactoring\r\n\r\nExecute a process on the command line with elevated rights on Vista\r\n\r\nUsage: Elevate [-?|-wait|-k] prog [args]\r\n   -?    - Shows this help  \r\n   -wait - Waits until prog terminates\r\n   -k    - Starts the the %comspec% environment variable value and \r\n           executes prog in it (CMD.EXE, 4NT.EXE, etc.)\r\n   prog  - The program to execute\r\n   args  - Optional command line arguments to prog\r\n\r\nNote that because the way ShellExecute works, Elevate cannot set the\r\ncurrent directory for prog. Consequently, relative paths as args will\r\nprobably not work.";
        public const string WaitArg = "WAIT";
        public const string InvalidElevateArgFmt = "?";
        public const string KeepArg = "K";

        private static string? Version
        {
            get
            {
                if (string.IsNullOrEmpty(version))
                {
                    version = Process.GetCurrentProcess().Modules[0].FileVersionInfo.FileVersion;
                }
                return version;
            }
        }

        private static string? version;
    }
}