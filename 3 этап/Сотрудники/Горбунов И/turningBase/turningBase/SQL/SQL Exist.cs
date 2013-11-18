using System;
using System.Collections.Generic;
using Devart.Data.Oracle;
using Devart.Data;
using System.Windows.Forms;
using System.Data;
using System.Drawing;
using LOG;

/// <summary>
/// ����� c �������� ���� Exist
/// </summary>
partial class SQLOracle
{
    /// <summary>
    /// M���� ���������� true, ���� �������� � ��������������� ���� � ������� �������
    /// </summary>
    /// <param name="value">��������</param>
    /// <param name="column">����</param>
    /// <param name="table">�������</param>
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
    /// ����� �������� ������������� ������
    /// </summary>
    /// <param name="table">������������ �������</param>
    /// <param name="where">������� Where</param>
    /// <returns>true - ������ ����������; false - ������ �� ����������</returns>
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
            System.Windows.Forms.MessageBox.Show(e.ToString());
        }
        finally
        {
            _close();
        }

        return flag;
    }

    /// <summary>
    /// ����� �������� ������������� ������ �� ����, ������� � ���������� �� where
    /// </summary>
    /// <param name="idField">����</param>
    /// <param name="table">�������</param>
    /// <param name="where">�������</param>
    /// <returns>true - ������ ����������; false - ������ �� ����������</returns>
    public static bool exist(string idField, string table, string where)
    {
        bool flag = false;

        _open();

        try
        {
            string cmdQuery = "select " + idField + " from " + table + " where " + where;

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
            System.Windows.Forms.MessageBox.Show(e.ToString());

            Log.WriteLog(e);

        }
        finally
        {
            _close();
        }

        return flag;
    }



    /// <summary>
    /// ����� �������������������� ������� �� �������� ������� ������ � �������
    /// </summary>
    /// <param name="id">����</param>
    /// <param name="table">�������</param>
    /// <param name="parametr">��������</param>
    /// <param name="value">�������� ���������</param>
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
            System.Windows.Forms.MessageBox.Show(ex.Message);

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
