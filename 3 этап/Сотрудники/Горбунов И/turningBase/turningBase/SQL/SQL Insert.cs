using System;
using System.Collections.Generic;
using Devart.Data.Oracle;
using Devart.Data;
using System.Windows.Forms;
using System.Data;
using System.Drawing;
using LOG;

/// <summary>
/// Класс c методами типа Insert
/// </summary>
partial class SqlOracle1
{


    /// <summary>
    /// Метод на параметризированную вставку с загрузкой картинки (специальный запрос)
    /// </summary>
    /// <param name="cmdQuery">строка запроса</param>
    /// <param name="Parameters">Имя параметра</param>
    /// <param name="DataFromTextBox">Значение параметра</param> 
    /// <param name="BMPInByte">Байтовое представление изображения</param>
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


            oracleInsertCommand1.Parameters.Add(new OracleParameter(":DET", OracleDbType.Blob, BMPInByte.Length, System.Data.ParameterDirection.Input, false, 0, 0, null, System.Data.DataRowVersion.Current, BMPInByte));


            oracleInsertCommand1.ExecuteNonQuery();

            oracleInsertCommand1.Connection.Close();

            System.Windows.Forms.MessageBox.Show("Параметры введены без ошибок.Загрузка прошла успешно!", "Сообщение!");

        }
        catch (OracleException ex)
        {
            System.Windows.Forms.MessageBox.Show(ex.Message);

            Log.WriteLog(ex);

            oracleInsertCommand1.Connection.Close();
        }
        finally
        {
            disposeObjectsForInsertQuery();
        }


    }

}