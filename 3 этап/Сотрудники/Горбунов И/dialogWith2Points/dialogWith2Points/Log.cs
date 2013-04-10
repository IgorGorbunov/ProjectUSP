using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

/// <summary>
/// Класс логирования.
/// </summary>
public static class Log
{
    static string name = @"SlotterLog";
    static string extension = @".log";
    static int count = 5;

    /// <summary>
    /// Записывает новую строку в лог.
    /// </summary>
    /// <param name="line">Строка.</param>
    public static void writeLine(string line)
    {
        StreamWriter sW = new StreamWriter(Config.logPath + name + extension, 
                                           true,
                                           Encoding.UTF8);

        sW.WriteLine(DateTime.Now + Environment.NewLine + line + Environment.NewLine);
        sW.Flush();

        sW.Close();
    }
    /// <summary>
    /// Записывает новую строку с сообщением о предупреждении или ошибки пользователю.
    /// </summary>
    /// <param name="warning"></param>
    public static void writeWarning(string warning)
    {
        string message = "||||||||||||||||||||||||||||||||||||||||||||" +
            Environment.NewLine + warning;
        writeLine(message);
    }
}

