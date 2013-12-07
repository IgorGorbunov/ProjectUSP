using System;
using System.Windows.Forms;

// ReSharper disable UnusedMember.Global
public sealed class Program
// ReSharper restore UnusedMember.Global
{
    private static Form _form;

// ReSharper disable UnusedMember.Global
    public static void Main()
// ReSharper restore UnusedMember.Global
    {
        try
        {
            //SqlOracle1.BuildConnectionString("591014", "591000", "EOI");
            //SqlOracle.BuildConnectionString("591014", "591000", "EOI");

#if(DEBUG)
            Message.Show("Дебаггг", Message.MessageType.Warning, "DEBUG!");
#endif

            //_startProgram = new Buttons();
            Logger.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++++" + " Начало работы программы ");

            _form = new SootherForm();
            _form.Show();

            try
            {
                SootherForm._startProgram = new Buttons();
                SootherForm._startProgram.Show();
            }
            catch (UnauthorizedAccessException)
            {
                string mess = "Не удалось выгрузить файлы на диск!" + Environment.NewLine + "Доступ запрещён!";
                Message.ShowError(mess);
            }
            catch (TimeoutException ex)
            {
                const string mess = "Нет соединения с БД!";
                Logger.WriteError(mess, ex);
            }
            // The following method shows the dialog immediately

            Logger.WriteLine("--------------------------------------------------" + " Конец работы программы ");
        }
        catch (Exception ex)
        {
            //---- Enter your exception handling code here -----
            Logger.WriteError(ex);
            Message.Show(ex);
        }
        finally
        {
            _form.Close();
            _form.Dispose();
        }
    }
}