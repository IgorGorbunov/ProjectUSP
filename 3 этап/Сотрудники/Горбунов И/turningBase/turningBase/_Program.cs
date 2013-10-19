using System;

// ReSharper disable UnusedMember.Global
public sealed class Program
// ReSharper restore UnusedMember.Global
{
    private static DialogProgpam _startProgram;

// ReSharper disable UnusedMember.Global
    public static void Main()
// ReSharper restore UnusedMember.Global
    {
        try
        {
            SQLOracle.BuildConnectionString("ktc", "ktc", "BASEEOI");
            SqlOracle.BuildConnectionString("ktc", "ktc", "BASEEOI");

#if(DEBUG)
            Message.Show("Дебаггг", Message.MessageType.Warning, "DEBUG!");
#endif

            //_startProgram = new TurningBase();
            //_startProgram = new MilingBase();
            //_startProgram = new Jig();
            _startProgram = new Buttons();
            Logger.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++++" + " Начало работы программы " + _startProgram.GetType().Name);

            // The following method shows the dialog immediately
            _startProgram.Show();
            Logger.WriteLine("--------------------------------------------------" + " Конец работы программы " + _startProgram.GetType().Name);
        }
        catch (Exception ex)
        {
            //---- Enter your exception handling code here -----
            Logger.WriteError(ex);
            Message.Show(ex);
        }
        finally
        {
            _startProgram.Dispose();
        }
    }
}