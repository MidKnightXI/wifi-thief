using System.Diagnostics;

namespace WifiThief.Commands;

public static class Commands
{
    public const string GetWifiNameCommandMacos = "sudo wdutil info | grep 'SSID                 :'";
    public const string GetWifiNameCommandWindows = "netsh wlan show interfaces | findstr 'SSID'";
    public const string GetWifiNameCommandLinux = "iwgetid -r";

    private static string RunUnixCommand(string command)
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

    private static string RunCmdCommand(string command)
    {
        var process = new Process();
        process.StartInfo.FileName = "cmd.exe";
        process.StartInfo.Arguments = $"/c \"{command}\"";
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.RedirectStandardOutput = true;

        process.Start();

        string output = process.StandardOutput.ReadToEnd();

        process.WaitForExit();

        return output;
    }


    private static string RunPowerShellCommand(string command)
    {
        var process = new Process();
        process.StartInfo.FileName = "powershell.exe";
        process.StartInfo.Arguments = $"-NoProfile -Command \"{command}\"";
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.RedirectStandardOutput = true;

        process.Start();

        string output = process.StandardOutput.ReadToEnd();

        process.WaitForExit();

        return output;
    }
}