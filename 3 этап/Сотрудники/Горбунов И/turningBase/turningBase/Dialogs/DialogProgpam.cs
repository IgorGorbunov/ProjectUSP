using System;
using NXOpen;
using NXOpen.BlockStyler;

/// <summary>
/// Класс диалогов UI Styler.
/// </summary>
public abstract class DialogProgpam
{
    protected BlockDialog TheDialog;

    //------------------------------------------------------------------------------
    //This method shows the dialog on the screen
    //------------------------------------------------------------------------------
    public void Show()
    {
        try
        {
            Logger.WriteLine("~~~~~~~~~~ Запущен диалог " + GetType().Name);
            TheDialog.Show();
        }
        catch (Exception ex)
        {
            //---- Enter your exception handling code here -----
            Message.Show(ex);
        }
    }

    //------------------------------------------------------------------------------
    //Method Name: Dispose
    //------------------------------------------------------------------------------
    public void Dispose()
    {
        if (TheDialog == null) return;

        TheDialog.Dispose();
        TheDialog = null;
    }

    //------------------------------------------------------------------------------
    // This method specifies how a shared image is unloaded from memory
    // within NX. This method gives you the capability to unload an
    // internal NX Open application or user  exit from NX. Specify any
    // one of the three constants as a return value to determine the type
    // of unload to perform:
    //
    //
    //    Immediately : unload the library as soon as the automation program has completed
    //    Explicitly  : unload the library from the "Unload Shared Image" dialog
    //    AtTermination : unload the library when the NX session terminates
    //
    //
    // NOTE:  A program which associates NX Open applications with the menubar
    // MUST NOT use this option since it will UNLOAD your NX Open application image
    // from the menubar.
    //------------------------------------------------------------------------------
    // ReSharper disable UnusedMember.Global
    // ReSharper disable UnusedParameter.Global
    public static int GetUnloadOption(string arg)
    // ReSharper restore UnusedParameter.Global
    // ReSharper restore UnusedMember.Global
    {
        //return System.Convert.ToInt32(Session.LibraryUnloadOption.Explicitly);
        return Convert.ToInt32(Session.LibraryUnloadOption.Immediately);
        // return System.Convert.ToInt32(Session.LibraryUnloadOption.AtTermination);
    }

    //---------------------------------------

    protected static void UnSelectObjects(UIBlock block)
    {
        PropertyList propList = block.GetProperties();
        propList.SetTaggedObjectVector("SelectedObjects", new TaggedObject[0]);
    }

    protected static void SetEnable(UIBlock block, bool enable)
    {
        PropertyList propList = block.GetProperties();
        SetEnable(propList, enable);
    }

    protected static void SetEnable(PropertyList propList, bool enable)
    {
        propList.SetLogical("Enable", enable);
    }
}

