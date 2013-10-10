/// <summary>
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
    /// Наименование нижней грани корпусных элементов для набора высоты.
    /// </summary>
    public const string HeightBottom = "BOTTOM";
    /// <summary>
    /// Наименование верхней грани корпусных элементов для набора высоты.
    /// </summary>
    public const string HeightTop = "TOP";
}