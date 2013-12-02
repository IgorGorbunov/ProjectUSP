﻿/// <summary>
/// Класс настроек системы.
/// </summary>
public static partial class Config
{
    public static readonly char[] FaceNameSplitter = { '_' };
    public const string SlotSymbol = "SLOT";
    public const string SlotBottomSymbol = "BOTTOM";

    /// <summary>
    /// Наименование НГП.
    /// </summary>
    public static readonly string SlotBottomName = SlotSymbol + FaceNameSplitter[0] +
                                                   SlotBottomSymbol;

    /// <summary>
    /// Наименование главного отверстия на токарной базе.
    /// </summary>
    public const string BaseHoleName = "BASE_HOLE";

    /// <summary>
    /// Наименование верхней грани кондукторной планки.
    /// </summary>
    public const string JigTopName = "JIG_TOP";
    /// <summary>
    /// Наименование цилиндрической грани кондукторной планки.
    /// </summary>
    public const string JigSleeveName = "JIG_SLEEVE";

    public const string FoldAlongEdgeName = "FOLD_ALONG";

    public const string FoldAcrossEdgeName = "FOLD_ACROSS";

    /// <summary>
    /// Наименование цилиндрической грани кондукторной втулки.
    /// </summary>
    public const string SleeveFaceName = "SLEEVE_JIG";
    /// <summary>
    /// Наименование грани касания быстросменной кондукторной втулки.
    /// </summary>
    public const string SleeveTopName = "SLEEVE_TOP";
    /// <summary>
    /// Наименование нижней грани касания кондукторной втулки.
    /// </summary>
    public const string SleeveBottomName = "SLEEVE_BOTTOM";

    /// <summary>
    /// Наименование грани отверстия в корпусных элементах для набора высоты.
    /// </summary>
    public const string HeightHole = "HOLE";
    /// <summary>
    /// Наименование нижней грани элементов.
    /// </summary>
    public const string BottomFace = "BOTTOM";
    /// <summary>
    /// Наименование верхней грани элементов.
    /// </summary>
    public const string TopFace = "TOP";

    /// <summary>
    /// Наименование наклонной грани элементов для набора на большой угол.
    /// </summary>
    public const string Incline = "INCLINE";
    /// <summary>
    /// Наименование ребра поперечного паза элемента.
    /// </summary>
    public const string AcrossSlot = "ACROSS_SLOT";
    /// <summary>
    /// Наименование ребра продольного паза элемента.
    /// </summary>
    public const string AlongSlot = "ALONG_SLOT";

    /// <summary>
    /// Наименование нижнего ребра у БОЛЬШЕГО КРАЯ элемента УСП.
    /// </summary>
    public const string BottomEdge = "BOTTOM_EDGE";
    /// <summary>
    /// Наименование верхнего ребра у БОЛЬШЕГО КРАЯ элемента УСП.
    /// </summary>
    public const string TopEdge = "TOP_EDGE";
    /// <summary>
    /// Наименование первой боковой грани отверстия в продольном пазе элемента.
    /// </summary>
    public const string HoleSide0 = "HOLE_SIDE_0";
    /// <summary>
    /// Наименование второй боковой грани отверстия в продольном пазе элемента.
    /// </summary>
    public const string HoleSide1 = "HOLE_SIDE_1";


}