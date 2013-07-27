using System;

using System.Data.OracleClient;
using UchetUSP.LOG;


/// <summary>
/// Класс c методами по работе с BLOB данными
/// </summary>
partial class SQLOracle
{

    /// <summary>
    /// выгрузка моделей в TEMP
    /// </summary>
    /// <param name="obonachenie">обозначение модели</param>
    /// <returns></returns>
    static public string UnloadPartToTEMPFolder(string obonachenie)
    {

        System.Collections.Generic.List<string> AcquiredInformation = new System.Collections.Generic.List<string>();

        initObjectsForSelectQuery();

        
        try
        {
            System.String[] Path = { System.IO.Path.GetTempPath(),"UGH\\", obonachenie, ".prt" };

            System.String Path_dir = System.String.Concat(Path);

            
            if (!System.IO.File.Exists(Path_dir))
            {
                
                System.IO.FileStream fs;

                int Prt_file = 0;

                string cmdQuery = "SELECT BL FROM FILE_BLOB21 WHERE NMF = :NMF";

                oracleSelectCommand1.CommandText = cmdQuery;

                oracleSelectCommand1.Parameters.Clear();

                oracleSelectCommand1.Parameters.Add(new OracleParameter(":NMF", (string)(obonachenie + ".prt")));

                oracleSelectCommand1.Connection.Open();

                reader = oracleSelectCommand1.ExecuteReader();

                reader.Read();

                System.Byte[] b = new System.Byte[System.Convert.ToInt32((reader.GetBytes(Prt_file, 0, null, 0, System.Int32.MaxValue)))];

                reader.GetBytes(Prt_file, 0, b, 0, b.Length);

                reader.Close();

                oracleSelectCommand1.Connection.Close();

                fs = new System.IO.FileStream(Path_dir, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Write);

                IDisposable d = (IDisposable)fs;

                fs.Write(b, 0, b.Length);

                d.Dispose();

               
            }

            return Path_dir;
        }
        catch (Exception ex)
        {
            //System.Windows.Forms.MessageBox.Show(ex.Message, "Ошибка");\
            Message.Show(ex);

            Log.WriteLog(ex);

            return "0";
        }
        finally
        {
            reader.Dispose();

            oracleSelectCommand1.Connection.Close();

            disposeObjectsForSelectQuery();
        }
    }

    //refactor
    ///// <summary>
    ///// Метод возвращающий Image по заданному select-запросу через буфер
    ///// </summary>
    ///// <param name="cmdQuery">строка запроса</param>
    ///// <param name="TheFirstParametr">Параметр NAME</param>
    ///// <param name="TheSecondParametr">Параметр GOST</param>
    ///// <param name="TheThirdParametr">Параметр OBOZN</param>
    ///// <returns>Image - изображение через буфер</returns>
    //public static System.Drawing.Image getBlobImageWithBuffer(string cmdQuery, string TheFirstParametr, string TheSecondParametr, string TheThirdParametr)
    //{

    //    System.Drawing.Image returnImage;

    //    initObjectsForSelectQuery();

    //    try
    //    {
    //        int PictureCol = 0;


    //        oracleSelectCommand1.Parameters.Clear();

    //        oracleSelectCommand1.Parameters.Add(new OracleParameter(":NAME", TheFirstParametr));

    //        oracleSelectCommand1.Parameters.Add(new OracleParameter(":GOST", TheSecondParametr));

    //        oracleSelectCommand1.Parameters.Add(new OracleParameter(":OBOZN", TheThirdParametr));

    //        oracleSelectCommand1.CommandText = cmdQuery;

    //        oracleSelectCommand1.Connection.Open();

    //        reader = oracleSelectCommand1.ExecuteReader();

    //        reader.Read();

    //        System.Byte[] b = new System.Byte[System.Convert.ToInt32((reader.GetBytes(PictureCol, 0, null, 0, System.Int32.MaxValue)))];

    //        reader.GetBytes(PictureCol, 0, b, 0, b.Length);

    //        oracleSelectCommand1.Connection.Close();

    //        System.IO.MemoryStream str = new System.IO.MemoryStream(b);

    //        str.Write(b, 0, b.Length);

    //        returnImage = System.Drawing.Image.FromStream(str);

    //        IDisposable d = (IDisposable)str;

    //        str.Close();

    //        d.Dispose();

    //    }
    //    catch (Exception ex)
    //    {
    //        oracleSelectCommand1.Connection.Close();

    //        returnImage = null;

    //        Logger.WriteLog(ex);
    //    }
    //    finally
    //    {
    //        reader.Close();
    //        reader.Dispose();
    //        disposeObjectsForSelectQuery();
    //    }

    //    return returnImage;

    //}

    


  
    /// <summary>
    /// Метод выгрузки моделей УСП 20 в TEMP
    /// </summary>
    /// <param name="NMF">обозначение NMF</param>
    /// <returns>Данные в виде List</returns>
    static public string UnloadOsnasToTEMPFolderFile20(string NMF)
    {

        initObjectsForSelectQuery();

        try
        {
            UnloadPartToPAthFolderFile_blob20(NMF.Trim());

            System.Collections.Generic.List<string> ChildrenList;

            if (SQLOracle.existParamQuery("NMF", "MODEL_STRUCT20", "NMF", NMF))
            {
                ChildrenList = SQLOracle.GetInformationListWithParamQuery("NMF", "MODEL_STRUCT20", "PARENT", NMF);

                for (int i = 0; i < ChildrenList.Count; i++)
                {
                    UnloadPartToPAthFolderFile_blob20(ChildrenList[i].Trim());
                }
            }


            return System.IO.Path.GetTempPath()+ "UGH" + "\\" + NMF;
        }
        catch (Exception ex)
        {
            Log.WriteLog(ex);

            return "0";
        }
        finally
        {
            reader.Dispose();

            oracleSelectCommand1.Connection.Close();

            disposeObjectsForSelectQuery();
        }
    }



    /// <summary>
    /// Метод выгрузки отдельных моделей оснастки в TEMP file_blob20
    /// </summary>
    /// <param name="obonachenie">обозначение модели</param>
    /// <returns></returns>
    static public string UnloadPartToPAthFolderFile_blob20(string obonachenie)
    {

        initObjectsForSelectQuery();


        try
        {
            System.String[] Path = { System.IO.Path.GetTempPath(),"UGH", "\\", obonachenie };

            System.String Path_dir = System.String.Concat(Path);


            if (!System.IO.File.Exists(Path_dir))
            {

                System.IO.FileStream fs;

                int Prt_file = 0;

                string cmdQuery = "SELECT BL FROM FILE_BLOB20 WHERE NMF = :NMF";

                oracleSelectCommand1.CommandText = cmdQuery;

                oracleSelectCommand1.Parameters.Clear();

                oracleSelectCommand1.Parameters.Add(new OracleParameter(":NMF", obonachenie));

                oracleSelectCommand1.Connection.Open();

                reader = oracleSelectCommand1.ExecuteReader();

                reader.Read();

                System.Byte[] b = new System.Byte[System.Convert.ToInt32((reader.GetBytes(Prt_file, 0, null, 0, System.Int32.MaxValue)))];

                reader.GetBytes(Prt_file, 0, b, 0, b.Length);

                reader.Close();

                oracleSelectCommand1.Connection.Close();

                fs = new System.IO.FileStream(Path_dir, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Write);

                IDisposable d = (IDisposable)fs;

                fs.Write(b, 0, b.Length);

                d.Dispose();


            }

            return Path_dir;
        }
        catch (Exception ex)
        {
            //System.Windows.Forms.MessageBox.Show(ex.Message, "Ошибка");

            Log.WriteLog(ex);

            return "0";
        }
        finally
        {
            reader.Dispose();

            oracleSelectCommand1.Connection.Close();

            disposeObjectsForSelectQuery();
        }
    }


  
}
