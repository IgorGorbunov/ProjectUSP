/// <summary>
/// Класс с информацией о sql-таблице с информацией о деталях УСП в каталогах.
/// </summary>
static class SqlTabUspData
{
    /// <summary>
    /// Название таблицы.
    /// </summary>
    public static string Name = "DB_DATA";

    /// <summary>
    /// Столбец с номером каталога для детали (0 - 8ой паз, 1 - 12ый паз).
    /// </summary>
    public static string CCatalogNum = "KATALOG_USP";
    /// <summary>
    /// Столбец с обозначением элемента.
    /// </summary>
    public static string CTitle = "OBOZN";
}

