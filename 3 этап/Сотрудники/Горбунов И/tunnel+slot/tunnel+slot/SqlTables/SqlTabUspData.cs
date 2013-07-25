/// <summary>
/// Класс с информацией о sql-таблице с информацией о деталях УСП в каталогах.
/// </summary>
public static class SqlTabUspData
{
    /// <summary>
    /// Название таблицы.
    /// </summary>
    public const string Name = "DB_DATA";

    /// <summary>
    /// Столбец с обозначением элемента.
    /// </summary>
    public const string CTitle = "OBOZN";
    /// <summary>
    /// Возвращает столбец с наименованием элемента.
    /// </summary>
    public const string CName = "NAME";
    /// <summary>
    /// Возвращает столбец с главным диаметром элемента.
    /// </summary>
    public const string CDiametr = "D";
    /// <summary>
    /// Столбец с обозначением длины елемента.
    /// </summary>
    public const string CLength = "L";
    /// <summary>
    /// Столбец с номером каталога для детали (0 - 8ой паз, 1 - 12ый паз).
    /// </summary>
    public const string CCatalog = "KATALOG_USP";
    /// <summary>
    /// Столбец с обозначением группы деталей.
    /// </summary>
    public const string CGroup = "GROUP_USP";

    /// <summary>
    /// Номера имён ГОСТов деталей.
    /// </summary>
    public enum NameUsp
    {
        SlotBolt = 0
    }
    /// <summary>
    /// Возвращает наименова ние ГОСТа.
    /// </summary>
    /// <param name="name">ГОСТ</param>
    /// <returns></returns>
    public static string GetName(NameUsp name)
    {
        switch (name)
        {
            case 0:
                return "'Болты пазовые'";
        }
        return "''";
    }
    /// <summary>
    /// Значение каталогов УСП.
    /// </summary>
    public enum CatalogUsp
    {
        Slot8 = 0,
        Slot12 = 1
    }
    /// <summary>
    /// Значение групп УСП.
    /// </summary>
    public enum GroupUsp
    {
        Fixture = 5
    }
}

