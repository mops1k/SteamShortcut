﻿using System.Diagnostics;
using Microsoft.Win32;

namespace SteamShortcut.Service;

public class SteamProcess
{
    private string? _steamPath
    {
        get
        {
            RegistryKey? key = Registry.LocalMachine.OpenSubKey(@"Software\Wow6432Node\Valve\Steam");
            if (key == null)
            {
                return null;
            }

            object? path = key.GetValue("InstallPath", null);

            return path == null ? null : Path.Combine((string)path, "steam.exe");
        }
    }

    private bool IsInstalled => _steamPath != null;
    public bool IsRunning => IsInstalled && FindSteamProcess() != null;

    private bool Start()
    {
        if (IsRunning)
        {
            return false;
        }

        Process process = new();
        process.StartInfo.FileName = _steamPath;
        process.StartInfo.WindowStyle = ProcessWindowStyle.Normal;

        return process.Start();
    }

    private bool Stop()
    {
        if (!IsRunning)
        {
            return true;
        }

        Process? steamProcess = FindSteamProcess();
        if (steamProcess == null)
        {
            return true;
        }

        if (steamProcess.CloseMainWindow())
        {
            return false;
        }

        steamProcess.Kill();
        Thread.Sleep(2000);

        return true;
    }

    public bool Restart() => Stop() && Start();

    private Process? FindSteamProcess() =>
        Process
            .GetProcessesByName("steam")
            .FirstOrDefault(p => p.MainModule?.FileName == _steamPath);
}
