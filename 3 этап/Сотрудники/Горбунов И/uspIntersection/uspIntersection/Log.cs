using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

/// <summary>
/// Класс логирования.
/// </summary>
public static class Log
{
    static StreamWriter sW;

    static string name = @"IntersectionLog";
    static string extension = @".log";
    static int count = 5;

    static long maxSize = 1000000;//~ мегабайт

    /// <summary>
    /// Записывает новую строку в лог.
    /// </summary>
    /// <param name="line">Строка.</param>
    public static void writeLine(string line)
    {
        setFile();
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

    static void setFile()
    {
        string currFile = Config.logPath + name + extension;
        try
        {
            FileInfo info = new FileInfo(currFile);

            bool append = true;
            if (info.Length > maxSize)
            {
                copyFiles();
                append = false;
            }

            sW = new StreamWriter(currFile, append, Encoding.UTF8);
        }
        catch (FileNotFoundException)
        {
            sW = new StreamWriter(currFile, false, Encoding.UTF8);
        }        
    }

    static void copyFiles()
    {
        for (int i = count; i > 2; i--)
        {
            try
            {
                FileInfo f = new FileInfo(Config.logPath + name + (i - 1).ToString() + extension);
                f.CopyTo(Config.logPath + name + i + extension, true);
            }
            catch (FileNotFoundException ) { }
            
        }

        FileInfo firstFile = new FileInfo(Config.logPath + name + extension);
        firstFile.CopyTo(Config.logPath + name + "2" + extension, true);
    }
}

