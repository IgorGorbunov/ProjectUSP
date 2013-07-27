using  System;
using NXOpen;


public static class Message
{
    public enum MessageIcon
    {
        Error = 0,
        Information = 1,
        Warning = 2,
        Question = 3
    }

    public static void Tst()
    {
        Show("test", MessageIcon.Error, "");
    }
    public static void Tst(object mess)
    {
        Show("test", MessageIcon.Error, mess);
    }
    public static void Tst(params object[] vars)
    {
        string mess = "";
        for (int i = 0; i < vars.Length; i++)
        {
            if (vars[i] == null)
            {
                mess += "null" + Environment.NewLine;
            }
            mess += vars[i] + Environment.NewLine;
        }
        Show("test", MessageIcon.Error, mess);
    }
    

    public static void Show(object message)
    {
        Show("Error", MessageIcon.Error, message);
    }
    public static void Show(string title, MessageIcon icon, object mess)
    {
        NXMessageBox.DialogType dialog = NXMessageBox.DialogType.Error;
        switch (icon)
        {
            case MessageIcon.Error:
                {
                    dialog = NXMessageBox.DialogType.Error;
                    break;
                }
            case MessageIcon.Information:
                {
                    dialog = NXMessageBox.DialogType.Information;
                    break;
                }
            case MessageIcon.Question:
                {
                    dialog = NXMessageBox.DialogType.Question;
                    break;
                }
            case MessageIcon.Warning:
                {
                    dialog = NXMessageBox.DialogType.Warning;
                    break;
                }
        }

        Config.TheUi.NXMessageBox.Show(title, dialog, mess == null ? "null" : mess.ToString());
    }
}

