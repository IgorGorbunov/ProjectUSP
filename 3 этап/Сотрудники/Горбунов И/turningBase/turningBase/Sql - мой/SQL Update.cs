using Devart.Data.Oracle;


/// <summary>
/// Класс c методами типа Select
/// </summary>
partial class SqlOracle
{

    public static void TestUpdate(string cmdQuery)
    {
        _open();
        Logger.WriteLine(cmdQuery);

        OracleCommand cmd = new OracleCommand(cmdQuery, _conn);
        cmd.ExecuteNonQuery();
        //OracleDataReader reader = cmd.ExecuteReader();
        //while (reader.Read())
        //{
        //    val = reader.GetValue(0);
        //}

        //reader.Close();
        cmd.Dispose();
        _close();

        //return val;
        //try
        //{
        //    oracleUpdateCommand1.Connection.Open();

        //    oracleUpdateCommand1.CommandText = cmdQuery;

        //    oracleUpdateCommand1.Parameters.Clear();

        //    for (int i = 0; i < (UFFacet.Parameters.Count - 1); i++)
        //    {
        //        oracleUpdateCommand1.Parameters.Add(
        //            new OracleParameter((string) (":" + Parameters[i].ToString()),
        //                                DataFromTextBox[i].ToString()));
        //    }


        //    oracleUpdateCommand1.Parameters.Add(new OracleParameter(":DET", OracleDbType.Blob,
        //                                                            BMPInByte.Length,
        //                                                            System.Data.ParameterDirection
        //                                                                  .Input, false, 0, 0, null,
        //                                                            System.Data.DataRowVersion
        //                                                                  .Current, BMPInByte));

        //    oracleUpdateCommand1.Parameters.Add(
        //        new OracleParameter((string) (":" + Parameters[(Parameters.Count - 1)].ToString()),
        //                            DataFromTextBox[(Parameters.Count - 1)].ToString()));

        //    oracleUpdateCommand1.ExecuteNonQuery();

        //    oracleUpdateCommand1.Connection.Close();

        //    System.Windows.Forms.MessageBox.Show(
        //        "Параметры введены без ошибок.Обновление данных прошло успешно!", "Сообщение!");

        //}
        //catch (OracleException ex)
        //{
        //    System.Windows.Forms.MessageBox.Show(ex.Message);

        //    oracleUpdateCommand1.Connection.Close();
        //}
        //finally
        //{
        //    disposeObjectsForUpdateQuery();
        //}
    }
}