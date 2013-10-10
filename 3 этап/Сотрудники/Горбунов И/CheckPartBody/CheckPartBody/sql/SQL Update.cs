using System;
using System.Data.OracleClient;
using System.Windows.Forms;
using UchetUSP.LOG;


/// <summary>
/// ����� c �������� ���� Update
/// </summary>
partial class SQLOracle
{



    /// <summary>
    /// ����� �� ������������������� ���������� ������ � ������������ (����������� ������)
    /// </summary>
    /// <param name="cmdQuery">������ �������</param>
    /// <param name="Parameters">��� ���������</param>
    /// <param name="DataFromTextBox">�������� ���������</param> 
    /// <param name="BMPInByte">�������� ������������� �����������</param>
    /// <returns></returns>
    static public void SpecificUpdateQuery(string cmdQuery, System.Collections.Generic.List<string> Parameters, System.Collections.Generic.List<string> DataFromTextBox, byte[] BMPInByte)
    {
        initObjectsForUpdateQuery();

        try
        {
            oracleUpdateCommand1.Connection.Open();

            oracleUpdateCommand1.CommandText = cmdQuery;

            oracleUpdateCommand1.Parameters.Clear();

            for (int i = 0; i < (Parameters.Count - 1); i++)
            {
                oracleUpdateCommand1.Parameters.Add(new OracleParameter((string)(":" + Parameters[i].ToString()), DataFromTextBox[i].ToString()));
            }


            oracleUpdateCommand1.Parameters.Add(new OracleParameter(":DET", OracleType.Blob, BMPInByte.Length, System.Data.ParameterDirection.Input, false, 0, 0, null, System.Data.DataRowVersion.Current, BMPInByte));

            oracleUpdateCommand1.Parameters.Add(new OracleParameter((string)(":" + Parameters[(Parameters.Count - 1)].ToString()), DataFromTextBox[(Parameters.Count - 1)].ToString()));

            oracleUpdateCommand1.ExecuteNonQuery();

            oracleUpdateCommand1.Connection.Close();

            //System.Windows.Forms.MessageBox.Show("��������� ������� ��� ������.���������� ������ ������ �������!", "���������!");


        }
        catch (OracleException ex)
        {
            //System.Windows.Forms.MessageBox.Show(ex.Message);


            oracleUpdateCommand1.Connection.Close();

            //Logger.WriteLog(ex);
        }
        finally
        {
            disposeObjectsForUpdateQuery();
        }

    }



    /// <summary>
    /// ����� �� ������������������� �������
    /// </summary>
    /// <param name="cmdQuery">������ �������</param>
    /// <param name="Parameters">��� ���������</param>
    /// <param name="DataFromTextBox">�������� ���������</param> 
    /// <returns>true - �������� ������ �������; false - �������� ������ ��������</returns>
    static public bool UpdateQuery(string cmdQuery, System.Collections.Generic.List<string> Parameters, System.Collections.Generic.List<string> DataFromTextBox)
    {
        initObjectsForUpdateQuery();

        try
        {
            oracleUpdateCommand1.Connection.Open();

            oracleUpdateCommand1.CommandText = cmdQuery;

            oracleUpdateCommand1.Parameters.Clear();

            for (int i = 0; i < Parameters.Count; i++)
            {
                oracleUpdateCommand1.Parameters.Add(new OracleParameter((string)(":" + Parameters[i].ToString()), DataFromTextBox[i].ToString()));
            }

            oracleUpdateCommand1.ExecuteNonQuery();

            oracleUpdateCommand1.Connection.Close();

            return true;

        }
        catch (Exception ex)
        {
            //System.Windows.Forms.MessageBox.Show(ex.Message);

            oracleUpdateCommand1.Connection.Close();

            Log.WriteLog(ex);

            return false;
        }
        finally
        {
            disposeObjectsForUpdateQuery();

        }


    }

     
}