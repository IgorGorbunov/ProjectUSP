using System;
using System.Collections.Generic;
using System.Data.OracleClient;
using System.Windows.Forms;
using System.Data;
using System.Drawing;
using UchetUSP.LOG;
using UchetUSP.DifferentCalsses;

/// <summary>
/// ����� c �������� ���� Select
/// </summary>
partial class SQLOracle
{

    /// <summary>
    /// �����, ����������� ������������������� select-������
    /// </summary>
    /// <param name="cmdQuery">SQL-����� �������</param>
    /// <param name="ParamsDict">Dictionary c ����������������� �������</param>
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
            MessageBox.Show("������! ���������� � ��������������!");
        }
        finally
        {
            _close();
        }

        return value;
    }

    /// <summary>
    /// ����� �������������������� ������� �� ��������� ����� ������
    /// </summary>
    /// <param name="id">����</param>
    /// <param name="table">�������</param>
    /// <param name="parametr">��������</param>
    /// <param name="value">�������� ���������</param>
    /// <returns>������ � ���� List</returns>
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
    /// ����� ������������ string �������� ����� ������������������ ������ � ��������� ��������� Where
    /// </summary>
    /// <param name="cmdQuery">select-������</param>
    /// <param name="Parameters">�������� ���������</param>
    /// <param name="DataFromTextBox">�������� ���������</param>
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
                catch (InvalidCastException) { } //����� ������ ��������
                catch (InvalidOperationException) { } //����� ������� �������� ����

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
    /// ����� ������������ DataSet �� ��������� �������������������� select-������� 
    /// </summary>
    /// <param name="cmdQuery">������ �������</param>
    /// <param name="Parameters">��� ���������</param>
    /// <param name="DataFromTextBox">�������� ���������</param>    
    /// <returns>DataSet - ����������� ����� �����</returns>
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
    /// ����� ������������ DataSet �� ��������� select-�������
    /// </summary>
    /// <param name="cmdQuery">select-������</param>
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
    /// ���������� ������� � ������������������� select-��������
    /// </summary>
    /// <param name="cmdQuery">select-������</param>
    /// <param name="Dict">Dictioanry � ������������������ �������</param>
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
        MessageBox.Show("������! ���������� � ��������������!");
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