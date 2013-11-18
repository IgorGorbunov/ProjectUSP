using System;
using System.IO;
using Devart.Data.Oracle;


/// <summary>
/// Класс c методами по работе с BLOB данными
/// </summary>
static partial class SqlOracle
{
    /// <summary>
    /// выгрузка моделей в TEMP
    /// </summary>
    /// <param name="obonachenie">обозначение модели</param>
    /// <returns></returns>
    static public string UnloadPartToTempFolder(string obonachenie)
    {
        try
        {
            String[] path = { Path.GetTempPath(),"UGH\\", obonachenie, ".prt" };
            String pathDir = String.Concat(path);

            if (!File.Exists(pathDir))
            {
                const int prtFile = 0;

                const string cmdQuery = "SELECT BL FROM FILE_BLOB21 WHERE NMF = :NMF";
                OracleCommand cmd = new OracleCommand(cmdQuery, _conn);

                cmd.Parameters.Add(new OracleParameter(":NMF", obonachenie + ".prt"));
                _open();

                OracleDataReader reader = cmd.ExecuteReader();
                reader.Read();

                Byte[] b = new Byte[Convert.ToInt32((reader.GetBytes(prtFile, 0, null, 0, Int32.MaxValue)))];

                reader.GetBytes(prtFile, 0, b, 0, b.Length);

                reader.Close();
                cmd.Dispose();
                _close();

                FileStream fs = new FileStream(pathDir, FileMode.OpenOrCreate, FileAccess.Write);
                fs.Write(b, 0, b.Length);
                fs.Close();
            }

            return pathDir;
        }
        catch (Exception ex)
        {
            string mess = "Ошибка при выгрузке детали!" + Environment.NewLine + ex;
            Logger.WriteLine(mess);
            Message.Show(mess);

            return "0";
        }
        finally
        {
            _close();
        }
    }

  
}
