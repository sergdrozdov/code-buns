using System.Diagnostics;

/*
    How can I get the line number which threw exception?
    Note that this will only work if there is a .pdb file available for the assembly.
*/

try
{

}
catch (Exception ex)
{
    var stackTrace = new StackTrace(ex, true);
    var frame = stackTrace.GetFrame(0);
    var fileName = frame.GetFileName();
    var methodName = frame?.GetMethod()?.Name ?? "Unknown";
    var lineNumber = frame?.GetFileLineNumber() ?? 0;
    var column = frame?.GetFileColumnNumber() ?? 0;
}
