using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Devart.Data.Oracle;

/// <summary>
/// Класс c методами по работе с BLOB данными
/// </summary>
partial class SqlOracle
{
    /// <summary>
    /// Выгрузка файла в заданную папку.
    /// </summary>
    /// <param name="cmdQuery"></param>
    /// <param name="path"></param>
    /// <param name="paramsDict"></param>
    /// <returns></returns>
    static public bool UnloadFile(string cmdQuery, string path, Dictionary<string, string> paramsDict)
    {
        try
        {
            _open();

            OracleCommand cmd = new OracleCommand(cmdQuery, _conn);
            foreach (KeyValuePair<string, string> pair in paramsDict)
            {
                cmd.Parameters.AddWithValue(":" + pair.Key, pair.Value);
            }

            Byte[] b = null;
            OracleDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                b = new Byte[Convert.ToInt32((reader.GetBytes(0, 0, null, 0, Int32.MaxValue)))];
                reader.GetBytes(0, 0, b, 0, b.Length);
            }

            reader.Close();
            cmd.Dispose();

            FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
            IDisposable d = fs;

            Debug.Assert(b != null, "b != null");
            fs.Write(b, 0, b.Length);
            d.Dispose();

            ProcessSuccess(cmdQuery, paramsDict, path);
            return true;
        }
        catch (TimeoutException)
        {
            return false;
        }
        catch (Exception ex)
        {
            ProcessUnSuccess(cmdQuery, paramsDict, ex);
            return false;
        }
        finally
        {
            _close();
        }

    }

  
}
