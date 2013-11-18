using System;
using NXOpen;
using NXOpen.Assemblies;
using NXOpen.UF;

/// <summary>
/// Класс настроек системы.
/// </summary>
public static partial class Config
{
    public static readonly Session TheSession = Session.GetSession();
    public static readonly UI TheUi = UI.GetUI();
    public static readonly UFSession TheUfSession = UFSession.GetUFSession();
    public static readonly Part WorkPart = TheSession.Parts.Work;

    public const int NPointsInEdge = 2;

    public const int NumberOfNearestEdges = 12;


    public enum SlotType
    {
        Pslot,
        Tslot
    };


    //снизил с 4 на 3 из-за невозможности пазирования (почему-то) коротких пазов на главной
    //плоскости с длинными на боковых у плит
    //снизил с 3 на 2 из-за невозможности пазирования (почему-то) длинных пазов - неточно
    //вычислялась ширина паза
    private const int Precision = 5;



    /// <summary>
    /// Возвращает наименование temp-папки для проекта.
    /// </summary>
    public const string TmpFolder = @"UGH";

    /// <summary>
    /// Возвращает наименование нашей temp-папки для проекта.
    /// </summary>
    public const string OurTmpFolder = @"USP";

    /// <summary>
    /// Расширение файлов деталей УСП.
    /// </summary>
    public const string PartFileExtension = ".prt";



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
}