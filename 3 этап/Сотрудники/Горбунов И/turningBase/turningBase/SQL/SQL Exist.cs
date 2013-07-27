
using System.Collections.Generic;
using System.Data.OracleClient;
using UchetUSP.LOG;


/// <summary>
/// Класс c методами типа Exist
/// </summary>
partial class SQLOracle
{
    /// <summary>
    /// Mетод возвращает true, если значение в соответствующем поле и таблице найдено
    /// </summary>
    /// <param name="value">Значение</param>
    /// <param name="column">Поле</param>
    /// <param name="table">Таблица</param>
    /// <returns></returns>
    public static bool exist(object value, string column, string table)
    {
        Dictionary<string, string> Dict = new Dictionary<string, string>();
        Dict.Add("VALUE", value.ToString());

        object val = sel("select " + column + " from " + table + " where " +
            column + " = :VALUE", Dict);

        if (val == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }


    /// <summary>
    /// Метод проверки существования данных
    /// </summary>
    /// <param name="table">Наименование таблицы</param>
    /// <param name="where">Условие Where</param>
    /// <returns>true - данные существуют; false - данные не существуют</returns>
    public static bool exist(string table, string where)
    {
        bool flag = false;

        _open();
        try
        {
            string cmdQuery = "select * from " + table + " where " + where;

            OracleCommand cmd = new OracleCommand(cmdQuery, _conn);
            OracleDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
                flag = true;
            else
                flag = false;

            reader.Close();
            cmd.Dispose();
        }
        catch (System.Exception e)
        {
            //System.Windows.Forms.MessageBox.Show(e.ToString());
            Message.Show(e);
        }
        finally
        {
            _close();
        }

        return flag;
    }

    /// <summary>
    /// Метод проверки существования данных по полю, таблице и параметрам из where
    /// </summary>
    /// <param name="idField">Поле</param>
    /// <param name="table">Таблица</param>
    /// <param name="where">Условие</param>
    /// <returns>true - данные существуют; false - данные не существуют</returns>
    public static bool exist(string idField, string table, string where)
    {
        bool flag = false;

        _open();

        try
        {
            string cmdQuery = "select " + idField + " from " + table + " where " + where;
            Message.Show(cmdQuery);
            OracleCommand cmd = new OracleCommand(cmdQuery, _conn);
            OracleDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
                flag = true;
            else
                flag = false;

            reader.Close();
            cmd.Dispose();
            reader.Dispose();
        }
        catch (System.Exception e)
        {
            //System.Windows.Forms.MessageBox.Show(e.ToString());
            Message.Show(e);

            Log.WriteLog(e);

        }
        finally
        {
            _close();
        }

        return flag;
    }



    /// <summary>
    /// Метод параметризированного запроса на проверку наличия данных в таблице
    /// </summary>
    /// <param name="id">Поле</param>
    /// <param name="table">Таблица</param>
    /// <param name="parametr">Параметр</param>
    /// <param name="value">Значение параметра</param>
    /// <returns></returns>
    public static bool existParamQuery(string id, string table, string parametr, string value)
    {
        bool flag = false;

        initObjectsForSelectQuery();

        try
        {
            string cmdQuery = "select " + id + " from " + table + " where " + parametr + " = :" + parametr;

            oracleSelectCommand1.Connection.Open();

            oracleSelectCommand1.CommandText = cmdQuery;

            oracleSelectCommand1.Parameters.Clear();

            oracleSelectCommand1.Parameters.Add(new OracleParameter((string)(":" + parametr), value));

            reader = oracleSelectCommand1.ExecuteReader();



            if (reader.Read())
                flag = true;
            else
                flag = false;

            reader.Close();


        }
        catch (System.Exception ex)
        {
            //System.Windows.Forms.MessageBox.Show(ex.Message);
            Message.Show(ex);
            Log.WriteLog(ex);

        }
        finally
        {
            reader.Dispose();
            oracleSelectCommand1.Connection.Close();
            disposeObjectsForSelectQuery();
        }

        return flag;
    }

  


}
