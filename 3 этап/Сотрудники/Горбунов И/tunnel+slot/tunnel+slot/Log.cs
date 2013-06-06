using System;
using System.Globalization;
using System.Text;
using System.IO;

/// <summary>
/// Класс логирования.
/// </summary>
public static class Log
{
    static StreamWriter _sW;

    private const string Name = @"Log";
    private const string Extension = @".log";
    private const int Count = 5;

    private const long MaxSize = 1000000; //~ мегабайт

    /// <summary>
    /// Записывает новую строку в лог.
    /// </summary>
    /// <param name="line">Строка.</param>
    public static void WriteLine(string line)
    {
        SetFile();
        _sW.WriteLine(DateTime.Now + Environment.NewLine + line + Environment.NewLine);
        _sW.Flush();

        _sW.Close();
    }
    /// <summary>
    /// Записывает новую строку с сообщением о предупреждении или ошибки пользователю.
    /// </summary>
    /// <param name="warning"></param>
    public static void WriteWarning(string warning)
    {
        string message = "||||||||||||||||||||||||||||||||||||||||||||" +
            Environment.NewLine + warning;
        WriteLine(message);
    }

    static void SetFile()
    {
        string currFile = AppDomain.CurrentDomain.BaseDirectory + Name + Extension;
        try
        {
            FileInfo info = new FileInfo(currFile);

            bool append = true;
            if (info.Length > MaxSize)
            {
                CopyFiles();
                append = false;
            }

            _sW = new StreamWriter(currFile, append, Encoding.UTF8);
        }
        catch (FileNotFoundException)
        {
            _sW = new StreamWriter(currFile, false, Encoding.UTF8);
        }        
    }

    static void CopyFiles()
    {
        for (int i = Count; i > 2; i--)
        {
            try
            {
                FileInfo f = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + Name + 
                                (i - 1).ToString(CultureInfo.InvariantCulture) + Extension);
                f.CopyTo(AppDomain.CurrentDomain.BaseDirectory + Name + i + Extension, true);
            }
            catch (FileNotFoundException ) { }
            
        }

        FileInfo firstFile = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + Name + Extension);
        firstFile.CopyTo(AppDomain.CurrentDomain.BaseDirectory + Name + "2" + Extension, true);
    }
}

