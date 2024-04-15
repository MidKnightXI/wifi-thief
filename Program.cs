using System;
using System.Diagnostics;

internal class Program
{
    public static void Main()
    {
        string command = "sudo wdutil info | grep 'SSID                 :'";;
        string output = RunCommand(command);
        Console.WriteLine(output);
    }

    private static string RunCommand(string command)
    {
        // Create a process to execute the command
        Process process = new Process();
        process.StartInfo.FileName = "/bin/bash";
        process.StartInfo.Arguments = $"-c \"{command}\"";
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.RedirectStandardOutput = true;

        // Start the process
        process.Start();

        // Read the output
        string output = process.StandardOutput.ReadToEnd();

        // Wait for the process to exit
        process.WaitForExit();

        // Return the output
        return output;
    }
}
