/// <summary>
/// Класс с информацией о sql-таблице планок для набора большего угла.
/// </summary>
public static class SqlTabBigAngleData
{
    /// <summary>
    /// Наименование таблицы.
    /// </summary>
    public const string Name = "USP_ANGLE_BIG";
    /// <summary>
    /// ГОСТ.
    /// </summary>
    public const string CGost = Name + "." + "GOST";
    /// <summary>
    /// Наименование столбца с реальным типом угла в детали.
    /// </summary>
    public const string CAngleType = Name + "." + "REAL_ANGLE_TYPE";

    /// <summary>
    /// Тип угла в базе данных.
    /// </summary>
    public enum AngleType
    {
        Acute = 0,
        Supplementary = 1,
        Obtuse = 2
    }
}

