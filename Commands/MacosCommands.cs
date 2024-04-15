using System.Diagnostics;

namespace WifiThief.Commands.Unix.Macos;

public static class MacosCommands
{
    public const string GetWifiNameCommandMacos = "sudo wdutil info | grep 'SSID                 :'";
}