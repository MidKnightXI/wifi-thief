using System.Diagnostics;

namespace WifiThief.Commands.Unix;

public static class UnixCommands
{
    public const string GetWifiNameCommandLinux = "iwgetid -r";

    public static string RunUnixCommand(string command)
    {
        var process = new Process();
        process.StartInfo.FileName = "/bin/bash";
        process.StartInfo.Arguments = $"-c \"{command}\"";
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.RedirectStandardOutput = true;

        process.Start();

        string output = process.StandardOutput.ReadToEnd();

        process.WaitForExit();

        return output;
    }
}