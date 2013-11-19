using System;
using System.Collections.Generic;
using Devart.Data.Oracle;
using Devart.Data;
using System.Windows.Forms;
using System.Data;
using System.Drawing;
using LOG;

/// <summary>
/// ����� c �������� ���� Update
/// </summary>
partial class SqlOracle1
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


            oracleUpdateCommand1.Parameters.Add(new OracleParameter(":DET", OracleDbType.Blob, BMPInByte.Length, System.Data.ParameterDirection.Input, false, 0, 0, null, System.Data.DataRowVersion.Current, BMPInByte));

            oracleUpdateCommand1.Parameters.Add(new OracleParameter((string)(":" + Parameters[(Parameters.Count - 1)].ToString()), DataFromTextBox[(Parameters.Count - 1)].ToString()));

            oracleUpdateCommand1.ExecuteNonQuery();

            oracleUpdateCommand1.Connection.Close();

            System.Windows.Forms.MessageBox.Show("��������� ������� ��� ������.���������� ������ ������ �������!", "���������!");

        }
        catch (OracleException ex)
        {
            System.Windows.Forms.MessageBox.Show(ex.Message);

            oracleUpdateCommand1.Connection.Close();

            Log.WriteLog(ex);
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
            System.Windows.Forms.MessageBox.Show(ex.Message);

            oracleUpdateCommand1.Connection.Close();

            Log.WriteLog(ex);

            return false;
        }
        finally
        {
            disposeObjectsForUpdateQuery();
        }


    }


    ///// <summary>
    ///// ����� �� ��������� ���������� ��������� � ��
    ///// </summary>
    ///// <param name="dgv">DataGridView - ������� ������</param>  
    ///// <returns></returns>
    //static public void EditNumberOFElemnt(System.Windows.Forms.DataGridView dgv)
    //{
    //    using (Katalog2005.WinFroms.Common.InputBox InputnumberOfElements = new Katalog2005.WinFroms.Common.InputBox("��������������", "��������", "���������� ���������"))
    //    {

    //        if (InputnumberOfElements.ShowDialog() == System.Windows.Forms.DialogResult.OK)
    //        {
    //            if (string.Compare(InputnumberOfElements.textBox1.Text, "") != 0)
    //            {
                    
    //                    System.Collections.Generic.List<string> Parameters = new System.Collections.Generic.List<string>();

    //                    System.Collections.Generic.List<string> DataFromTextBox = new System.Collections.Generic.List<string>();
                                               
    //                    Parameters.Add("NALICHI"); DataFromTextBox.Add(InputnumberOfElements.textBox1.Text.ToString());
    //                    Parameters.Add("OBOZN"); DataFromTextBox.Add(dgv[1, dgv.SelectedCells[0].RowIndex].Value.ToString());

    //                    string cmd = "UPDATE DB_DATA SET NALICHI =:NALICHI WHERE OBOZN = :OBOZN";

    //                    MessageBox.Show("Test");

    //                    if (SqlOracle1.UpdateQuery(cmd, Parameters, DataFromTextBox) == true)
    //                    {
    //                        System.Windows.Forms.MessageBox.Show("���������� ������ ������ �������!");
    //                    }

    //                    Parameters.Clear();
    //                    DataFromTextBox.Clear();
                    
    //            }

    //        }

    //    }

    //}
     
}