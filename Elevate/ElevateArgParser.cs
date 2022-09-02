using System.Text;
using System.Globalization;

namespace Elevate
{
    /// <summary>
    /// The command line argument parsing class.
    /// </summary>
    internal sealed class ElevateArgParser
    {
        private readonly string[] rawArgs;
        readonly StringBuilder argBuilder;

        /// <summary>
        /// Constructs the class.
        /// </summary>
        /// <param name="args">
        /// The command line arguments passed to the applicationToRun.
        /// </param>
        public ElevateArgParser(string[] args)
        {
            rawArgs = args;
            ParseError = string.Empty;
            argBuilder = new StringBuilder(100);
        }

        /// <summary>
        /// True if help was requested.
        /// </summary>
        public bool ShowHelp { get; set; }

        /// <summary>
        /// True if using the %COMSPEC% environment value and keeping the
        /// elevated command prompt open.
        /// </summary>
        public bool UseComSpecEnvironment { get; set; }

        /// <summary>
        /// True if Elevate is supposed to wait for termination.
        /// </summary>
        public bool WaitForTermination { get; set; }

        /// <summary>
        /// The parsing error to report.
        /// </summary>
        public string ParseError { get; set; }

        /// <summary>
        /// The applicationToRun to execute.
        /// </summary>
        public string ApplicationToRun { get; set; }

        /// <summary>
        /// Arguments to the program to run.
        /// </summary>
        public string Arguments => argBuilder.ToString().Trim();

        /// <summary>
        /// Parses up the argument string.
        /// </summary>
        /// <returns>
        /// True if life is happy.
        /// </returns>
        public bool ParseArguments()
        {
            // Do the easy check.
            if (rawArgs.Length == 0)
            {
                ShowHelp = true;
                return false;
            }

            // Have we seen the applicationToRun?
            var seenProgramArg = false;
            for (
            var currentIndex = 0; currentIndex < rawArgs.Length; currentIndex++)
            {
                // Check to see if this is an Elevate argument.
                if (!seenProgramArg)
                {
                    var elevateArg = rawArgs[currentIndex];
                    if ((elevateArg[0] == '-') || (elevateArg[0] == '/'))
                    {
                        elevateArg = elevateArg.Substring(1);
                        if (string.Compare(elevateArg,
                                                   Constants.HelpArg,
                                                   true,
                                                  CultureInfo.CurrentCulture) == 0)
                        {
                            ShowHelp = true;
                            return false;
                        }
                        else if (string.Compare(elevateArg,
                                                        Constants.KeepArg,
                                                        true,
                                                  CultureInfo.CurrentCulture) == 0)
                        {
                            UseComSpecEnvironment = true;
                        }
                        else if (string.Compare(elevateArg,
                                                        Constants.WaitArg,
                                                        true,
                                                  CultureInfo.CurrentCulture) == 0)
                        {
                            WaitForTermination = true;
                        }
                        else
                        {
                            ParseError = elevateArg + Constants.InvalidElevateArgFmt;
                            return false;
                        }
                    }
                    else
                    {
                        seenProgramArg = true;

                        // Got our applicationToRun.
                        ApplicationToRun = rawArgs[currentIndex];
                    }
                }
                else
                {
                    // We're doing arguments at this point.
                    var currArg = rawArgs[currentIndex];
                    var fmt = "{0} ";
                    if (currArg.Contains(' '))
                    {
                        fmt = "\"{0}\" ";
                    }
                    argBuilder.AppendFormat(fmt, currArg);
                }
            }

            // If the program is empty, it's an error.
            if (string.IsNullOrEmpty(ApplicationToRun))
            {
                ShowHelp = true;
                return false;
            }
            return true;
        }
    }
}
