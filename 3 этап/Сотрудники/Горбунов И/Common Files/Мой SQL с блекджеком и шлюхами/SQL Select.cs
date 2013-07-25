using System;
using System.Collections.Generic;
using System.Data.OracleClient;

/// <summary>
/// Класс c методами типа Select
/// </summary>
partial class SqlOracle
{
    /// <summary>
    /// Метод, реализующий параметризированный select-запрос
    /// </summary>
    /// <param name="cmdQuery">SQL-текст запроса</param>
    /// <param name="paramsDict">Dictionary c параметризаторами запроса</param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static bool Sel<T>(string cmdQuery, Dictionary<string, string> paramsDict, out T value)
    {
        value = default(T);
        try
        {
            _open();

            OracleCommand cmd = new OracleCommand(cmdQuery, _conn);
            foreach (KeyValuePair<string, string> pair in paramsDict)
            {
                cmd.Parameters.AddWithValue(":" + pair.Key, pair.Value);
            }
            
            OracleDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                value = (T)reader.GetValue(0);
            }

            reader.Close();
            cmd.Dispose();

            ProcessSuccess(cmdQuery, paramsDict, value);
            return true;
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

    public static bool Sel<T>(string cmdQuery, Dictionary<string, string> paramsDict, out List<T> values)
    {
        values = new List<T>();
        try
        {
            _open();

            OracleCommand cmd = new OracleCommand(cmdQuery, _conn);
            foreach (KeyValuePair<string, string> pair in paramsDict)
            {
                cmd.Parameters.AddWithValue(":" + pair.Key, pair.Value);
            }
            
            OracleDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                values.Add((T)reader.GetValue(0));
                Message.Show((T)reader.GetValue(0));
            }

            reader.Close();
            cmd.Dispose();

            ProcessSuccess(cmdQuery, paramsDict, values);
            return true;
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

    public static bool Sel<T1, T2>(string cmdQuery, Dictionary<string, string> paramsDict, out Dictionary<T1, T2> values)
    {
        values = new Dictionary<T1, T2>();
        try
        {
            _open();

            OracleCommand cmd = new OracleCommand(cmdQuery, _conn);
            foreach (KeyValuePair<string, string> pair in paramsDict)
            {
                cmd.Parameters.AddWithValue(":" + pair.Key, pair.Value);
            }
            
            OracleDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                T1 t1 = (T1) reader.GetValue(0);
                T2 t2 = (T2) reader.GetValue(1);
                values.Add(t1, t2);
            }

            reader.Close();
            cmd.Dispose();

            ProcessSuccess(cmdQuery, paramsDict, values);
            return true;
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

    public static List<string> TestSelect(string cmdQuery)
    {
        _open();
        Logger.WriteLine(cmdQuery);
        List<string> list = new List<string>();

        OracleCommand cmd = new OracleCommand(cmdQuery, _conn);
        OracleDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            list.Add(reader.GetValue(0).ToString());
            Message.Show(reader.GetValue(0));
        }

        reader.Close();
        cmd.Dispose();
        _close();

        return list;
    }


    static void ProcessSuccess<T>(string cmdQuery, Dictionary<string, string> paramsDict, T value)
    {
        string mess = "Запрос прошёл!";
        mess = RecordQuery(mess, cmdQuery, paramsDict);
        mess += Environment.NewLine + "-";
        mess += Environment.NewLine + "Data:";
        mess += Environment.NewLine + value;
        Logger.WriteLine(mess);
    }
    static void ProcessSuccess<T>(string cmdQuery, Dictionary<string, string> paramsDict, List<T> values)
    {
        string mess = "Запрос прошёл!";
        mess = RecordQuery(mess, cmdQuery, paramsDict);
        mess += Environment.NewLine + "-";
        mess += Environment.NewLine + "Data:";
        foreach (T value in values)
        {
            mess += Environment.NewLine + value;
        }
        Logger.WriteLine(mess);
    }
    static void ProcessSuccess<T1, T2>(string cmdQuery, Dictionary<string, string> paramsDict, Dictionary<T1, T2> values)
    {
        string mess = "Запрос прошёл!";
        mess = RecordQuery(mess, cmdQuery, paramsDict);
        mess += Environment.NewLine + "-";
        mess += Environment.NewLine + "Data:";
        foreach (KeyValuePair<T1, T2> keyValuePair in values)
        {
            mess += Environment.NewLine + keyValuePair.ToString();
        }
        Logger.WriteLine(mess);
    }

    static void ProcessUnSuccess(string cmdQuery, Dictionary<string, string> paramsDict, Exception ex)
    {
        string mess = "Запрос НЕ прошёл!";
        Message.Show(mess);
        mess = RecordQuery(mess, cmdQuery, paramsDict);
        mess += Environment.NewLine + ex;
        Logger.WriteError(mess);
    }

    static string RecordQuery(string mess, string cmdQuery, Dictionary<string, string> paramsDict)
    {
        mess += Environment.NewLine + cmdQuery;
        mess += Environment.NewLine + "-";
        mess += Environment.NewLine + "Parametrs:";
        foreach (KeyValuePair<string, string> pair in paramsDict)
        {
            mess += Environment.NewLine + pair.Key + " - " + pair.Value;
        }
        return mess;
    }
}