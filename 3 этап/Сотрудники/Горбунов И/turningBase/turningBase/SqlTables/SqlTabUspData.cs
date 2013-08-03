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
    /// Столбец с количеством элементов.
    /// </summary>
    public const string CCount = "NALICHI";

    /// <summary>
    /// Номера имён ГОСТов деталей.
    /// </summary>
    public enum NameUsp
    {
        /// <summary>
        /// 'Болты пазовые'
        /// </summary>
        SlotBolt = 0,
        /// <summary>
        /// 'Плиты круглые'
        /// </summary>
        RoundPlates = 1,
        /// <summary>
        /// 'Плита круглая'
        /// </summary>
        RoundPlate = 2,
        /// <summary>
        /// 'радиальн'
        /// </summary>
        RadialPlate = 3
    }
    /// <summary>
    /// Возвращает наименование ГОСТа.
    /// </summary>
    /// <param name="name">ГОСТ</param>
    /// <returns></returns>
    public static string GetName(NameUsp name)
    {
        switch ((int)name)
        {
            case 0:
                return "'Болты пазовые'";
            case 1:
                return "Плиты круглые";
            case 2:
                return "Плита круглая";
            case 3:
                return "радиальн";
        }
        return "''";
    }
    /// <summary>
    /// Значение каталогов УСП.
    /// </summary>
    public enum CatalogUsp
    {
        /// <summary>
        /// Каталог на 8ой паз (серия 2).
        /// </summary>
        Slot8 = 0,
        /// <summary>
        /// Каталог на 12ый паз (серия 3).
        /// </summary>
        Slot12 = 1
    }
    /// <summary>
    /// Значение групп УСП.
    /// </summary>
    public enum GroupUsp
    {
        /// <summary>
        /// Базовые детали.
        /// </summary>
        Base = 0,
        /// <summary>
        /// Крепежные детали.
        /// </summary>
        Fixture = 5
    }

    public static string ThereIs
    {
        get { return "(" + CCount + " <> 0 and " + CCount + " <> " + _NULL_NUMBER + ")"; }
    }

    private const int _NULL_NUMBER = 999;
}

