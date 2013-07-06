using System;
using System.Collections.Generic;
using System.Data.OracleClient;
using System.Windows.Forms;
using System.Data;
using System.Drawing;
using UchetUSP.LOG;
using UchetUSP.DifferentCalsses;

/// <summary>
/// Класс c методами типа Select
/// </summary>
partial class SQLOracle
{

    /// <summary>
    /// Метод, реализующий параметризированный select-запрос
    /// </summary>
    /// <param name="cmdQuery">SQL-текст запроса</param>
    /// <param name="ParamsDict">Dictionary c параметризаторами запроса</param>
    /// <returns></returns>
    public static object sel(string cmdQuery, Dictionary<string, string> ParamsDict)
    {
        object value = null;

        try
        {
            _open();
            OracleCommand Cmd = new OracleCommand(cmdQuery, _conn);

            foreach (KeyValuePair<string, string> Pair in ParamsDict)
            {
                Cmd.Parameters.AddWithValue(":" + Pair.Key, Pair.Value);
            }

            OracleDataReader Reader = Cmd.ExecuteReader();

            if (Reader.Read())
            {
                value = Reader.GetValue(0);
            }

            Reader.Close();
            Cmd.Dispose();
        }
        catch (Exception Ex)
        {
            string mess = cmdQuery + "\n";
            mess += "Parametrs:" + "\n";

            foreach (KeyValuePair<string, string> Pair in ParamsDict)
            {
                mess += Pair.Key + " - " + Pair.Value + "\n";
            }
            mess += Ex.ToString();
            Log.WriteLog(mess);
            MessageBox.Show("Ошибка! Обратитесь к администратору!");
        }
        finally
        {
            _close();
        }

        return value;
    }

    /// <summary>
    /// Метод параметризированного запроса на получение листа данных
    /// </summary>
    /// <param name="id">Поле</param>
    /// <param name="table">Таблица</param>
    /// <param name="parametr">Параметр</param>
    /// <param name="value">Значение параметра</param>
    /// <returns>Данные в виде List</returns>
    public static System.Collections.Generic.List<string> GetInformationListWithParamQuery(string id, string table, string parametr, string value)
    {
        System.Collections.Generic.List<string> AcquiredInformation = new System.Collections.Generic.List<string>();

        initObjectsForSelectQuery();

        try
        {
            string cmdQuery = "select " + id + " from " + table + " where " + parametr + " = :" + parametr;

            oracleSelectCommand1.Connection.Open();

            oracleSelectCommand1.CommandText = cmdQuery;

            oracleSelectCommand1.Parameters.Clear();

            oracleSelectCommand1.Parameters.Add(new OracleParameter((string)(":" + parametr), value));

            reader = oracleSelectCommand1.ExecuteReader();


            while (reader.Read())
            {
                AcquiredInformation.Add(reader.GetString(0));
            }

            reader.Close();

            oracleSelectCommand1.Connection.Close();
        }
        catch (System.Exception ex)
        {
            System.Windows.Forms.MessageBox.Show(ex.Message);

            Log.WriteLog(ex);

        }
        finally
        {
            reader.Dispose();
            oracleSelectCommand1.Connection.Close();
            disposeObjectsForSelectQuery();
        }

        return AcquiredInformation;
    }



    /// <summary>
    /// Метод возвращающий string значение через праметризированный селект с единичным условияем Where
    /// </summary>
    /// <param name="cmdQuery">select-запрос</param>
    /// <param name="Parameters">Название параметра</param>
    /// <param name="DataFromTextBox">Значение параметра</param>
    /// <returns></returns>
    static public string ParamQuerySelect(string cmdQuery, string Parameters, string DataFromTextBox)
    {

        initObjectsForSelectQuery();

        string value = "";

        try
        {

            oracleSelectCommand1.Connection.Open();

            oracleDataAdapter1.SelectCommand.CommandText = cmdQuery;

            oracleSelectCommand1.Parameters.Clear();

            oracleDataAdapter1.SelectCommand.Parameters.Add(new OracleParameter((string)(":" + Parameters), DataFromTextBox));

            reader = oracleSelectCommand1.ExecuteReader();


            if (reader.Read())
                try
                {
                    value = reader.GetString(0);
                }
                catch (InvalidCastException) { } //ловим пустое значение
                catch (InvalidOperationException) { } //ловим нулевое значение поля

            reader.Close();

            oracleSelectCommand1.Connection.Close();

            return value;

        }
        catch (OracleException ex)
        {
            System.Windows.Forms.MessageBox.Show(ex.Message);

            oracleSelectCommand1.Connection.Close();

            Log.WriteLog(ex);

            return value;
        }
        finally
        {
            reader.Dispose();
            disposeObjectsForSelectQuery();
        }

    }

    /// <summary>
    /// Метод возвращающий DataSet по заданному параметризированному select-запросу 
    /// </summary>
    /// <param name="cmdQuery">строка запроса</param>
    /// <param name="Parameters">Имя параметра</param>
    /// <param name="DataFromTextBox">Значение параметра</param>    
    /// <returns>DataSet - изображение через буфер</returns>
    static public System.Data.DataSet ParamQuerySelect(string cmdQuery, System.Collections.Generic.List<string> Parameters, System.Collections.Generic.List<string> DataFromTextBox)
    {

        initObjectsForSelectQuery();


        try
        {
            oracleSelectCommand1.Connection.Open();

            oracleDataAdapter1.SelectCommand.CommandText = cmdQuery;

            oracleSelectCommand1.Parameters.Clear();

            for (int i = 0; i < Parameters.Count; i++)
            {
                oracleDataAdapter1.SelectCommand.Parameters.Add(new OracleParameter((string)(":" + Parameters[i].ToString()), (string)("%" + DataFromTextBox[i].ToString() + "%")));
            }

            if (dataSet11.Tables.Count > 0)
            {
                dataSet11.Tables[0].Clear();
            }

            oracleDataAdapter1.Fill(dataSet11);

            oracleSelectCommand1.Connection.Close();

            return dataSet11;

        }
        catch (OracleException ex)
        {
            System.Windows.Forms.MessageBox.Show(ex.Message);

            oracleSelectCommand1.Connection.Close();

            Log.WriteLog(ex);

            return dataSet11;
        }
        finally
        {
            disposeObjectsForSelectQuery();
        }

    }





    /// <summary>
    /// Метод возвращающий DataSet по заданному select-запросу
    /// </summary>
    /// <param name="cmdQuery">select-запрос</param>
    /// <returns></returns>
    public static DataSet getDS(string cmdQuery)
    {
        DataSet DS = new DataSet();

        _open();
        try
        {
            OracleCommand Cmd = new OracleCommand(cmdQuery, _conn);
            Cmd.CommandType = CommandType.Text;

            OracleDataAdapter DA = new OracleDataAdapter(Cmd);

            DA.Fill(DS);

            DA.Dispose();
            Cmd.Dispose();
        }
        catch (Exception ex)
        {
            MessageBox.Show(cmdQuery + "\n" + ex.Message);
        }
        finally
        {
            _close();
        }

        return DS;
}

    /// <summary>
    /// Возвращает таблицу с параметризированным select-запросом
    /// </summary>
    /// <param name="cmdQuery">select-запрос</param>
    /// <param name="Dict">Dictioanry с параметризаторрами запроса</param>
    /// <returns></returns>
    public static DataTable getDT(string cmdQuery, Dictionary<string, string> ParamsDict)
{
    DataSet DS = new DataSet();

    try
    {
        _open();
        OracleCommand Cmd = new OracleCommand(cmdQuery, _conn);
        OracleDataAdapter DA = new OracleDataAdapter(Cmd);

        foreach (KeyValuePair<string, string> pair in ParamsDict)
        {
            DA.SelectCommand.Parameters.AddWithValue(":" + pair.Key, pair.Value);
        }

        DA.Fill(DS);

        DA.Dispose();
        Cmd.Dispose();

    }
    catch (Exception Ex)
    {
        string mess = cmdQuery + "\n";
        mess += "Parametrs:" + "\n";

        foreach (KeyValuePair<string, string> Pair in ParamsDict)
        {
            mess += Pair.Key + " - " + Pair.Value + "\n";
        }
        mess += Ex.ToString();
        Log.WriteLog(mess);
        MessageBox.Show("Ошибка! Обратитесь к администратору!");
    }
    finally
    {
        _close();
    }

    if (DS.Tables.Count == 0)
    {
        return null;
    }
    else
    {
        return DS.Tables[0];
    }
}


}