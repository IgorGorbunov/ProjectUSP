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

    /// <summary>
    /// Наименование НГП.
    /// </summary>
    public static readonly string SlotBottomName = SlotSymbol + FaceNameSplitter[0] + SlotBottomSymbol;

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
    /// <summary>
    /// Возвращает имя файла с формой для выгрузки токарной базы.
    /// </summary>
    public static string DlxTurningBase = @"turningBase.dlx";

    

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

    /// <summary>
    /// Возвращает тип паза в зависимости от расстояния между НРП и типа каталога.
    /// </summary>
    /// <param name="slotWidth">Расстояние между Нижними рёбрами паза.</param>
    /// <param name="catalog">Каталог.</param>
    /// <returns></returns>
    public static SlotType GetSlotType(double slotWidth, Catalog catalog)
    {
        return Round(slotWidth) == catalog.SlotWidthB ? SlotType.Tslot : SlotType.Pslot;
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

