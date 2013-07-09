
using System.Data.OracleClient;
using UchetUSP.LOG;


/// <summary>
/// ����� c �������� ���� Insert
/// </summary>
partial class SQLOracle
{


    /// <summary>
    /// ����� �� ������������������� ������� � ��������� �������� (����������� ������)
    /// </summary>
    /// <param name="cmdQuery">������ �������</param>
    /// <param name="Parameters">��� ���������</param>
    /// <param name="DataFromTextBox">�������� ���������</param> 
    /// <param name="BMPInByte">�������� ������������� �����������</param>
    /// <returns></returns>
    static public void SpecificInsertQuery(string cmdQuery, System.Collections.Generic.List<string> Parameters, System.Collections.Generic.List<string> DataFromTextBox, byte[] BMPInByte)
    {
        initObjectsForInsertQuery();

        try
        {
            oracleInsertCommand1.Connection.Open();

            oracleInsertCommand1.CommandText = cmdQuery;

            oracleInsertCommand1.Parameters.Clear();

            for (int i = 0; i < Parameters.Count; i++)
            {
                oracleInsertCommand1.Parameters.Add(new OracleParameter((string)(":" + Parameters[i].ToString()), DataFromTextBox[i].ToString()));
            }


            oracleInsertCommand1.Parameters.Add(new OracleParameter(":DET", OracleType.Blob, BMPInByte.Length, System.Data.ParameterDirection.Input, false, 0, 0, null, System.Data.DataRowVersion.Current, BMPInByte));


            oracleInsertCommand1.ExecuteNonQuery();

            oracleInsertCommand1.Connection.Close();

            Message.Show("��������� ������� ��� ������.�������� ������ �������!");
            //System.Windows.Forms.MessageBox.Show("��������� ������� ��� ������.�������� ������ �������!", "���������!");

        }
        catch (OracleException ex)
        {
            //System.Windows.Forms.MessageBox.Show(ex.Message);
            Message.Show(ex);

            Log.WriteLog(ex);

            oracleInsertCommand1.Connection.Close();
        }
        finally
        {
            disposeObjectsForInsertQuery();
        }


    }

}