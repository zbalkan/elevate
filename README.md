# elevate

Based on the [work of John Robbins](https://web.archive.org/web/20090802180832/http://www.wintellect.com/cs/blogs/jrobbins/archive/2007/03/27/elevate-a-process-at-the-command-line-in-vista.aspx), elevate is just a simplified rewrite of the original work. It does not add any value to the original code but the style changes.

## Usage

```shell
Elevate
(c) 2007 - John Robbins - www.wintellect.com
Contributors: 2022 - Zafer Balkan: .NET 6 migration & refactoring

Execute a process on the command line with elevated rights on Vista

Usage: Elevate [-?|-wait|-k] prog [args]
    -?    - Shows this help
    -wait - Waits until prog terminates
    -k    - Starts the the %comspec% environment variable value and
            executes prog in it (CMD.EXE, 4NT.EXE, etc.)
            prog  - The program to execute
    args  - Optional command line arguments to prog

Note that because the way ShellExecute works, Elevate cannot set the
current directory for prog. Consequently, relative paths as args will
probably not work.
```
