using  System;
using NXOpen;

/// <summary>
/// Класс UG сообщений.
/// </summary>
public static class Message
{
    /// <summary>
    /// Тип сообщений.
    /// </summary>
    public enum MessageType
    {
        Error = 0,
        Information = 1,
        Warning = 2,
        Question = 3
    }

    /// <summary>
    /// Выводит пустое сообщение-ошибку. Работает только в Debug-версии.
    /// </summary>
    public static void Tst()
    {
#if(DEBUG)
        Show("test", MessageType.Error, "");
#endif
    }
    /// <summary>
    /// Выводит сообщение-ошибку. Работает только в Debug-версии.
    /// </summary>
    /// <param name="mess">Текст сообщения.</param>
    public static void Tst(object mess)
    {
#if(DEBUG)
        Show("test", MessageType.Error, mess);
#endif
    }
    /// <summary>
    /// Выводит сообщение-ошибку. Работает только в Debug-версии.
    /// </summary>
    /// <param name="vars">Переменные для текста сообщения. 
    /// В сообщениии каждая будет располагаться на новой строке</param>
    public static void Tst(params object[] vars)
    {
#if(DEBUG)
        string mess = "";
        for (int i = 0; i < vars.Length; i++)
        {
            if (vars[i] == null)
            {
                mess += "null" + Environment.NewLine;
            }
            mess += vars[i] + Environment.NewLine;
        }
        Show("test", MessageType.Error, mess);
#endif
    }
    
    /// <summary>
    /// Выводит сообщение.
    /// </summary>
    /// <param name="message">Текст сообщения.</param>
    public static void Show(object message)
    {
        Show("Error", MessageType.Error, message);
    }
    /// <summary>
    /// Выводит сообщение об ошибке.
    /// </summary>
    /// <param name="message">Текст сообщения.</param>
    public static void ShowError(object message)
    {
        Show("Ошибка!", MessageType.Error, message);
    }
    /// <summary>
    /// Выводит сообщение о предупреждении.
    /// </summary>
    /// <param name="message">Текст сообщения.</param>
    public static void ShowWarn(object message)
    {
        Show("Предупреждение!", MessageType.Warning, message);
    }
    /// <summary>
    /// Выводи сообщение.
    /// </summary>
    /// <param name="title">Текст заголовка окна сообщения.</param>
    /// <param name="type">Тип сообщения.</param>
    /// <param name="mess">Текст сообщения.</param>
    public static void Show(string title, MessageType type, object mess)
    {
        NXMessageBox.DialogType dialog = NXMessageBox.DialogType.Error;
        switch (type)
        {
            case MessageType.Error:
                {
                    dialog = NXMessageBox.DialogType.Error;
                    break;
                }
            case MessageType.Information:
                {
                    dialog = NXMessageBox.DialogType.Information;
                    break;
                }
            case MessageType.Question:
                {
                    dialog = NXMessageBox.DialogType.Question;
                    break;
                }
            case MessageType.Warning:
                {
                    dialog = NXMessageBox.DialogType.Warning;
                    break;
                }
        }

        Config.TheUi.NXMessageBox.Show(title, dialog, mess == null ? "null" : mess.ToString());
    }
}

