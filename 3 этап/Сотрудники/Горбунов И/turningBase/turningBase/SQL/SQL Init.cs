using System;
using System.Collections.Generic;
using Devart.Data.Oracle;
using Devart.Data;
using System.Windows.Forms;
using System.Data;
using System.Drawing;
using LOG;

/// <summary>
/// ����� c ������������� ���������� � ��������� ���������� � ��
/// </summary>
partial class SqlOracle1
{

    /// <summary>
    /// ���������� �����������
    /// </summary>
    static OracleConnection _conn;

    static private OracleDataAdapter oracleDataAdapter1 = null;

    static private System.Data.DataSet dataSet11 = null;

    static private OracleCommand oracleSelectCommand1 = null;

    static private OracleCommand oracleInsertCommand1 = null;

    static private OracleCommand oracleUpdateCommand1 = null;

    static private OracleDataReader reader =  null;



    /// <summary>
    /// ��������� ��������� ������
    /// </summary>


    public static string _connectionString;// =  "User id=system;password=123;Direct = true; Host = 192.168.1.170; Service Name = baseeoi; Port = 1521";
        //"User id=591014;password=591000;Service Name = baseeoi;";
    


    /// <summary>
    /// C�������� � �� (������ ����������)
    /// </summary>
    static void _open()
    {
        try
        {

            _conn = new OracleConnection(_connectionString);
            _conn.Open();
        }
        catch (System.Exception ex)
        {
            System.Windows.Forms.MessageBox.Show(ex.ToString());
        }
    }
    static void _close()
    {
        _conn.Close();
    }


    /// <summary>
    /// ������������� �������� ��� �������������������� SELECT
    /// </summary>
    static private void initObjectsForSelectQuery()
    {

        oracleDataAdapter1 = new OracleDataAdapter();

        _conn = new OracleConnection(_connectionString);

        dataSet11 = new System.Data.DataSet();

        oracleSelectCommand1 = new OracleCommand();

        oracleDataAdapter1.SelectCommand = oracleSelectCommand1;

        oracleSelectCommand1.Connection = _conn;

    }



    /// <summary>
    /// ������������� �������� ��� �������������������� INSERT
    /// </summary>
    static private void initObjectsForInsertQuery()
    {

        oracleDataAdapter1 = new OracleDataAdapter();

        _conn = new OracleConnection(_connectionString);

        dataSet11 = new System.Data.DataSet();

        oracleInsertCommand1 = new OracleCommand();

        oracleDataAdapter1.InsertCommand = oracleInsertCommand1;

        oracleInsertCommand1.Connection = _conn;
    }


    /// <summary>
    /// ������������� �������� ��� �������������������� UPDATE
    /// </summary>
    static private void initObjectsForUpdateQuery()
    {

        oracleDataAdapter1 = new OracleDataAdapter();

        _conn = new OracleConnection(_connectionString);

        dataSet11 = new System.Data.DataSet();

        oracleUpdateCommand1 = new OracleCommand();

        oracleDataAdapter1.UpdateCommand = oracleUpdateCommand1;

        oracleUpdateCommand1.Connection = _conn;
    }



    /// <summary>
    /// ���������� �������� �������������������� Select
    /// </summary>
    static private void disposeObjectsForSelectQuery()
    {
        oracleDataAdapter1.Dispose();

        _conn.Dispose();

        dataSet11.Dispose();

        oracleSelectCommand1.Dispose();

    }


    /// <summary>
    /// ���������� �������� �������������������� Insert
    /// </summary>
    static private void disposeObjectsForInsertQuery()
    {
        oracleDataAdapter1.Dispose();

        _conn.Dispose();

        dataSet11.Dispose();

        oracleInsertCommand1.Dispose();

    }


    /// <summary>
    /// ���������� �������� �������������������� Update
    /// </summary>
    static private void disposeObjectsForUpdateQuery()
    {
        oracleDataAdapter1.Dispose();

        _conn.Dispose();

        dataSet11.Dispose();

        oracleUpdateCommand1.Dispose();

    }


    /// <summary>
    /// ����� ���������� ������ �����������
    /// </summary>   
    /// <returns></returns>
    public static void BuildConnectionString(string user, string password, string dataSource,string Host,string Port)
    {
        _connectionString = "User id=" + user +
                                             ";password=" + password +
                                               ";Service Name  = " + dataSource +
                                                    ";Host = " + Host +
                                                        ";Direct = true" +
                                                            ";Port = " + Port;

    }

    /// <summary>
    /// ����� �������� ���������� � �� Oracle
    /// </summary>   
    /// <returns></returns>
    public static bool CheckConnection()
    {
        initObjectsForSelectQuery();

        try
        {
            oracleSelectCommand1.Connection.Open();

            oracleSelectCommand1.Connection.Close();

            return true;

        }
        catch (OracleException ex)
        {
            
            oracleSelectCommand1.Connection.Close();

            Log.WriteLog(ex);

            return false;

        }
        finally
        {
            disposeObjectsForSelectQuery();
        }

    }


}