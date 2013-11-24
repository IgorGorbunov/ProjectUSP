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
    static public bool UnloadFile(string cmdQuery, string path, string fileName, Dictionary<string, string> paramsDict)
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

            string fullPath = Path.Combine(path, fileName);
            FileStream fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write);
            IDisposable d = fs;

            if (b == null)
            {
                throw new TimeoutException();
            }
            fs.Write(b, 0, b.Length);
            d.Dispose();

            ProcessSuccess(cmdQuery, paramsDict, path);
            return true;
        }
        catch (UnauthorizedAccessException ex)
        {
            Logger.WriteError(ex, "Путь для записи - " + path);
            throw;
        }
        catch (TimeoutException)
        {
            return false;
        }
        catch (OracleException ex)
        {
            ProcessUnSuccess(cmdQuery, paramsDict, ex);
            throw new BadQueryExeption();
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
