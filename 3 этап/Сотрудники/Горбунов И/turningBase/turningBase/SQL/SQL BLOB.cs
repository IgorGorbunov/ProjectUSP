using System;
using System.Collections.Generic;
using Devart.Data.Oracle;
using Devart.Data;
using System.Windows.Forms;
using System.Data;
using System.Drawing;
using LOG;

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
            System.Windows.Forms.MessageBox.Show(ex.Message, "Ошибка");

            Log.WriteLog(ex);

            return "0";
        }
        finally
        {
            if (reader != null)
            { 
               reader.Dispose(); 
            }
            

            oracleSelectCommand1.Connection.Close();

            disposeObjectsForSelectQuery();
        }
    }


    /// <summary>
    /// Метод возвращающий Image по заданному select-запросу через буфер
    /// </summary>
    /// <param name="cmdQuery">строка запроса</param>
    /// <param name="TheFirstParametr">Параметр NAME</param>
    /// <param name="TheSecondParametr">Параметр GOST</param>
    /// <param name="TheThirdParametr">Параметр OBOZN</param>
    /// <returns>Image - изображение через буфер</returns>
    public static System.Drawing.Image getBlobImageWithBuffer(string cmdQuery, string TheFirstParametr, string TheSecondParametr, string TheThirdParametr)
    {

        System.Drawing.Image returnImage = null;

        initObjectsForSelectQuery();

        try
        {
            int PictureCol = 0;


            oracleSelectCommand1.Parameters.Clear();

            oracleSelectCommand1.Parameters.Add(new OracleParameter(":NAME", TheFirstParametr));

            oracleSelectCommand1.Parameters.Add(new OracleParameter(":GOST", TheSecondParametr));

            oracleSelectCommand1.Parameters.Add(new OracleParameter(":OBOZN", TheThirdParametr));

            oracleSelectCommand1.CommandText = cmdQuery;

            oracleSelectCommand1.Connection.Open();

            reader = oracleSelectCommand1.ExecuteReader();

            reader.Read();

            System.Byte[] b = new System.Byte[System.Convert.ToInt32((reader.GetBytes(PictureCol, 0, null, 0, System.Int32.MaxValue)))];

            reader.GetBytes(PictureCol, 0, b, 0, b.Length);
               
            System.IO.MemoryStream str = new System.IO.MemoryStream(b);

            str.Write(b, 0, b.Length);
                     

            returnImage = System.Drawing.Image.FromStream(str);
         
            oracleSelectCommand1.Connection.Close();

            IDisposable d = (IDisposable)str;
            
            str.Close();

            d.Dispose();
            
           
            

        }
        catch (Exception ex)
        {
            oracleSelectCommand1.Connection.Close();

            returnImage = null;
            MessageBox.Show(ex.ToString());
            Log.WriteLog(ex);
        }
        finally
        {
            if (reader != null)
            {
                reader.Close();
                reader.Dispose();
            }
            
            disposeObjectsForSelectQuery();
        }

        return returnImage;

    }

    


  
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
            if (reader != null)
            {
              
                reader.Dispose();
            }

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
            if (reader != null)
            {

                reader.Dispose();
            }

            oracleSelectCommand1.Connection.Close();

            disposeObjectsForSelectQuery();
        }
    }

    /// <summary>
    /// Возвращает картинку
    /// </summary>
    /// <param name="cmdQuery">select-запрос</param>
    /// <param name="TheFirstParametr">параметр</param>
    /// <returns></returns>
    public static Image getBlobImageWithBuffer(string cmdQuery, string TheFirstParametr)
    {

        Image returnImage = null;

        initObjectsForSelectQuery();

        try
        {
            int PictureCol = 0;


            oracleSelectCommand1.Parameters.Clear();

            oracleSelectCommand1.Parameters.Add(new OracleParameter(":OBOZN", TheFirstParametr));


            oracleSelectCommand1.CommandText = cmdQuery;

            oracleSelectCommand1.Connection.Open();

            reader = oracleSelectCommand1.ExecuteReader();


            reader.Read();

            Byte[] b = null;
            try
            {
                b = new System.Byte[System.Convert.ToInt32((reader.GetBytes(PictureCol, 0, null, 0, System.Int32.MaxValue)))];

                reader.GetBytes(PictureCol, 0, b, 0, b.Length);

                oracleSelectCommand1.Connection.Close();

                System.IO.MemoryStream str = new System.IO.MemoryStream(b);

                str.Write(b, 0, b.Length);

                returnImage = System.Drawing.Image.FromStream(str);

                IDisposable d = (IDisposable)str;

                str.Close();

                d.Dispose();
            }
            catch (InvalidOperationException) { } //если нет выборки


        }
        catch (Exception Ex)
        {
            oracleSelectCommand1.Connection.Close();

            string mess = cmdQuery + "\n" + Ex.ToString();
            Log.WriteLog(mess);
            MessageBox.Show("Ошибка! Обратитесь к администратору!");

            returnImage = null;
        }
        finally
        {
            reader.Close();
            reader.Dispose();
            disposeObjectsForSelectQuery();
        }


        return returnImage;

    }

    /// <summary>
    /// Метод на параметризированную загрузку данных в BLOB21
    /// </summary>
    /// <param name="cmdQuery">строка запроса</param>
    /// <param name="Parameters">Имя параметра</param>
    /// <param name="DataFromTextBox">Значение параметра</param> 
    /// <param name="BMPInByte">Байтовое представление файла</param>
    /// <returns></returns>
    static public void BlobInsertQuery(string cmdQuery, System.Collections.Generic.List<string> Parameters, System.Collections.Generic.List<string> DataFromTextBox, byte[] BMPInByte)
    {
        initObjectsForInsertQuery();

        try
        {
            oracleInsertCommand1.Connection.Open();

            oracleInsertCommand1.CommandText = cmdQuery;

            oracleInsertCommand1.Parameters.Clear();

            for (int i = 0; i < (Parameters.Count); i++)
            {
                oracleInsertCommand1.Parameters.Add(new OracleParameter((string)(":" + Parameters[i].ToString()), DataFromTextBox[i].ToString()));
            }


            oracleInsertCommand1.Parameters.Add(new OracleParameter(":BL", OracleDbType.Blob, BMPInByte.Length, System.Data.ParameterDirection.Input, false, 0, 0, null, System.Data.DataRowVersion.Current, BMPInByte));

            oracleInsertCommand1.ExecuteNonQuery();

            oracleInsertCommand1.Connection.Close();

            MessageBox.Show("Загрузка прошла успешно!");
        }
        catch (OracleException ex)
        {
            System.Windows.Forms.MessageBox.Show(ex.Message);

            oracleInsertCommand1.Connection.Close();

        }
        finally
        {
            disposeObjectsForInsertQuery();
        }

    }
  
}
