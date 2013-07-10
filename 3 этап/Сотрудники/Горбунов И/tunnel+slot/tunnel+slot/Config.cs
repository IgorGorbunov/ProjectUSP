using System;
using NXOpen;
using NXOpen.Assemblies;
using NXOpen.UF;

/// <summary>
/// Класс настроек системы.
/// </summary>
public static class Config
{
    public static readonly Session TheSession = Session.GetSession();
    public static readonly UI TheUi = UI.GetUI();
    public static readonly UFSession TheUfSession = UFSession.GetUFSession();
    public static readonly Part WorkPart= TheSession.Parts.Work;

    public const int NPointsInEdge = 2;

    public const int NumberOfNearestEdges = 6;

    public const double PSlotWidth = 12;
    public const double PSlotHeight = 3;

    private const double SlotA = 12;
    public const double SlotWidth = 20;
    private const double SlotB1 = 13;
    public static readonly double[] SlotHeight = { 6, 8, 10 };
    public static readonly double[] SlotHeight1 = { 4, 4.2, 7, 7.2, 7.5, 8, 9 };
    public const double SlotHeight2 = 4;
    public static readonly double[] SlotHeight3 = { 2, 4, 6 };

    public const double StepWidthTSlot1 = (SlotWidth - SlotA)/2.0;
    public const double StepDownWidthTSlot2 = (SlotWidth - SlotB1)/2.0;
    public const double StepUpWidthTSlot2 = (SlotB1 - SlotA)/2.0;

    public enum SlotType
    {
        Pslot,
        Tslot,
        Tslot1,
        Tslot2
    };

    public static readonly char[] FaceNameSplitter = { '_' };
    public const string SlotSymbol = "SLOT";
    public const string SlotBottomSymbol = "BOTTOM";

    //снизил с 4 на 3 из-за невозможности пазирования (почему-то) коротких пазов на главной
    //плоскости с длинными на боковых у плит
    //снизил с 3 на 2 из-за невозможности пазирования (почему-то) длинных пазов - неточно
    //вычислялась ширина паза
    const int Precision = 5;

    /// <summary>
    /// Папка с формами для диалогов.
    /// </summary>
    public const string DlxFolder = @"dialogs";

    /// <summary>
    /// Возвращает наименование temp-папки для проекта.
    /// </summary>
    public const string TmpFolder = @"UGH";

    /// <summary>
    /// Расширение файлов деталей УСП.
    /// </summary>
    public const string PartFileExtension = ".prt";
    /// <summary>
    /// Имя файла с формой базирования элементов по отверстиям.
    /// </summary>
    public static string DlxTunnelTunnel = @"tunnel+tunnel.dlx";
    /// <summary>
    /// Имя файла с формой для базирования отверстие-паз.
    /// </summary>
    public const string DlxTunnelSlot = @"tunnel+slot.dlx";
    /// <summary>
    /// Имя файла с формой c двумя точками.
    /// </summary>
    public static string DlxPointPoint = @"point+point.dlx";

    

    //------------------------ Methods ------------------------------------------------------------

    public static Component FindCompByBodyTag(Tag tag)
    {
        Component[] comps = WorkPart.ComponentAssembly.RootComponent.GetChildren();

        foreach (Component comp in comps)
	    {
            if (comp.Tag == tag)
            {
                return comp;
            }
	    }

        return null;
    }

    public static double Round(double d)
    {
        return Math.Round(d, Precision);
    }

    public static SlotType GetSlotType(double slotWidth)
    {
        return Round(slotWidth) == SlotWidth ? SlotType.Tslot : SlotType.Pslot;
    }

    /// <summary>
    /// Заморозить экран.
    /// </summary>
    public static void FreezeDisplay()
    {
        TheUfSession.Disp.SetDisplay(UFConstants.UF_DISP_SUPPRESS_DISPLAY);
    }
    /// <summary>
    /// Разморозить экран.
    /// </summary>
    public static void UnFreezeDisplay()
    {
        int displayCode;
        TheUfSession.Disp.AskDisplay(out displayCode);

        TheUfSession.Disp.SetDisplay(UFConstants.UF_DISP_UNSUPPRESS_DISPLAY);

        if (displayCode == UFConstants.UF_DISP_SUPPRESS_DISPLAY)
        {
            TheUfSession.Disp.RegenerateDisplay();
        }
    }
}

