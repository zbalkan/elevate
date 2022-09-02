using System;
using System.ComponentModel;
using System.Diagnostics;

namespace Elevate
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            var argData = new ElevateArgParser(args);
            if (argData.ParseArguments())
            {
                var progToRun = argData.ApplicationToRun;
                var progArgs = argData.Arguments;

                if (argData.UseComSpecEnvironment)
                {
                    if (progToRun.Contains(' '))
                    {
                        progToRun = $"\"{progToRun}\"";
                    }
                    // Add the program as an argument.
                    progArgs = $"{progToRun} /k {progArgs}";
                    progToRun = Environment.GetEnvironmentVariable("ComSpec");
                }

                var psInfo = new ProcessStartInfo
                {
                    Arguments = progArgs,
                    FileName = progToRun,
                    UseShellExecute = true
                };

                if (IsVistaOrBetter())
                {
                    psInfo.Verb = "runas";
                }
                try
                {
                    var p = Process.Start(psInfo);
                    if (argData.WaitForTermination)
                    {
                        p?.WaitForExit();
                    }
                }
                catch (Win32Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                ShowHelp();
                if (!argData.ShowHelp)
                {
                    Console.WriteLine(argData.ParseError);
                }
            }
        }

        static bool IsVistaOrBetter() => (Environment.OSVersion.Version.Major >= 6) &&
                 (Environment.OSVersion.Version.Minor >= 0);

        static void ShowHelp() => Console.WriteLine(Constants.HelpArg);
    }
}
