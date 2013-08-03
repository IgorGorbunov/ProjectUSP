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

            _startProgram = new TurningBase();
            Logger.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++++" + " Начало работы программы " + _startProgram.GetType().Name);

            // The following method shows the dialog immediately
            _startProgram.Show();
            Logger.WriteLine("--------------------------------------------------" + " Конец работы программы " + _startProgram.GetType().Name);
        }
        catch (Exception ex)
        {
            //---- Enter your exception handling code here -----
            Message.Show(ex);
        }
        finally
        {
            _startProgram.Dispose();
        }
    }
}

