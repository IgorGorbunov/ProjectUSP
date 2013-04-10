using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

/// <summary>
/// Класс логирования.
/// </summary>
public static class Log
{
    string name = @"SlotterLog";
    string extension = @".log";
    int count = 5;

    void writeLine(string line)
    {
        StreamWriter sW = new StreamWriter(Config.logPath + name + extension, true, Encoding.UTF8);

        sW.WriteLine(DateTime.Now + Environment.NewLine + line);
        sW.Flush();

        sW.Close();
    }
}

