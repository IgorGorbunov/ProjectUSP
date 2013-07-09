using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace UchetUSP.LOG
{
     class Log
    {
        /// <summary>
        ///Запись лога;
        /// </summary>
        /// <param name="CodError">        
        ///Код ошибки</param>
        /// <param name="Message">
        ///Сообщение об ошибке</param>
        ///<param name="Line">
        ///Строка</param>
        ///<param name="File">
        ///Файл </param>
        /// <returns></returns>  
         public static void WriteLog(Exception exept)
        {
            DirectoryInfo LogDir = new DirectoryInfo(Path.GetTempPath() + "LogsUchetUSP");

            if (!LogDir.Exists)
            {
                LogDir.Create();
            }
           
            StreamWriter str;
     
            if (!File.Exists((LogDir.FullName + "\\logfile " + DateTime.Today.ToLongDateString() + ".log")))
            {
                str = new StreamWriter((LogDir.FullName + "\\logfile " + DateTime.Today.ToLongDateString() + ".log"));
            }
            else
            {
                str = File.AppendText((LogDir.FullName + "\\logfile " + DateTime.Today.ToLongDateString() + ".log"));
            }

            str.WriteLine();
            str.WriteLine("Время: " + DateTime.Now);                    
            str.WriteLine("************************");          
            str.WriteLine("Ошибка: " + exept.ToString());
            str.WriteLine("************************"); 
            str.WriteLine();

            str.Close();

            IDisposable d = (IDisposable)str;  
            d.Dispose();           
            str.Dispose();
            
             

            

        }

         public static void WriteLog(string exept)
         {
             DirectoryInfo LogDir = new DirectoryInfo(Path.GetTempPath() + "LogsUchetUSP");

             if (!LogDir.Exists)
             {
                 LogDir.Create();
             }

             StreamWriter str;

             if (!File.Exists((LogDir.FullName + "\\logfile " + DateTime.Today.ToLongDateString() + ".log")))
             {
                 str = new StreamWriter((LogDir.FullName + "\\logfile " + DateTime.Today.ToLongDateString() + ".log"));
             }
             else
             {
                 str = File.AppendText((LogDir.FullName + "\\logfile " + DateTime.Today.ToLongDateString() + ".log"));
             }

             str.WriteLine();
             str.WriteLine("Время: " + DateTime.Now);
             str.WriteLine("************************");
             str.WriteLine("Ошибка: " + exept);
             str.WriteLine("************************");
             str.WriteLine();

             str.Close();

             IDisposable d = (IDisposable)str;
             d.Dispose();
             str.Dispose();





         }
    }
}
