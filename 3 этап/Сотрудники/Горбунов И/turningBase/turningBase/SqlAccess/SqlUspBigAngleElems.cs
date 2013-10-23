using System;
using System.Collections.Generic;

/// <summary>
/// Класс запросов информации о элементе УСП для большег набора на угол.
/// </summary>
static class SqlUspBigAngleElems
{
    private const string _FROM = Sql.From + SqlTabBigAngleData.Name;

    /// <summary>
    /// Возвращает список ГОСТов для набора тупого большего угла.
    /// </summary>
    /// <param name="catalog">Каталог элементов УСП.</param>
    /// <returns></returns>
    public static List<string> GetGosts_ObtuseAngle(Catalog catalog)
    {
        Dictionary<string, string> paramDict = new Dictionary<string, string>();
        string cat = ((int)catalog.CatalogUsp).ToString();
        paramDict.Add("CAT", cat);
        List<string> gosts;

        string from = _FROM + Sql.AddTable(SqlTabUspData.Name);
        string query = Sql.GetFirst(Sql.GetBegin(SqlTabBigAngleData.CGost) + from + Sql.Where +
            Sql.Equal(SqlTabUspData.CCatalog, ":CAT") +
            Sql.GetNewCond(Sql.Equal(SqlTabUspData.CGost, SqlTabBigAngleData.CGost)) +
            Sql.GetNewCond(Sql.Equal((int)SqlTabBigAngleData.AngleType.Obtuse, SqlTabBigAngleData.CAngleType)));


        if (SqlOracle.Sel(query, paramDict, out gosts))
        {
            return gosts;
        }
        throw new TimeoutException();
    }
    /// <summary>
    /// Возвращает список ГОСТов для набора острого большего угла.
    /// </summary>
    /// <param name="catalog">Каталог элементов УСП.</param>
    /// <returns></returns>
    public static List<string> GetGosts_AcuteAngle(Catalog catalog)
    {
        Dictionary<string, string> paramDict = new Dictionary<string, string>();
        string cat = ((int)catalog.CatalogUsp).ToString();
        paramDict.Add("CAT", cat);
        List<string> gosts;

        string from = _FROM + Sql.AddTable(SqlTabUspData.Name);
        string query = Sql.GetBegin(SqlTabBigAngleData.CGost) + from + Sql.Where +
                       Sql.Equal(SqlTabUspData.CCatalog, ":CAT") +
                       Sql.GetNewCond(Sql.Equal(SqlTabUspData.CGost,
                                                SqlTabBigAngleData.CGost)) +
                       " and (" +
                       Sql.Equal((int) SqlTabBigAngleData.AngleType.Acute,
                                 SqlTabBigAngleData.CAngleType) +
                       " or " +
                       Sql.Equal((int) SqlTabBigAngleData.AngleType.Supplementary,
                                 SqlTabBigAngleData.CAngleType) + ")" +
                       " group by " + SqlTabBigAngleData.CGost;


        if (SqlOracle.Sel(query, paramDict, out gosts))
        {
            return gosts;
        }
        throw new TimeoutException();
    }

    /// <summary>
    /// Возвращает список ГОСТов для набора большего угла.
    /// </summary>
    /// <param name="catalog">Каталог элементов УСП.</param>
    /// <returns></returns>
    public static List<string> GetGosts(Catalog catalog)
    {
        Dictionary<string, string> paramDict = new Dictionary<string, string>();
        string cat = ((int)catalog.CatalogUsp).ToString();
        paramDict.Add("CAT", cat);
        List<string> gosts;

        string from = _FROM + Sql.AddTable(SqlTabUspData.Name);
        string query = Sql.GetBegin(SqlTabBigAngleData.CGost) + from + Sql.Where +
                                    Sql.Equal(SqlTabUspData.CCatalog, ":CAT") +
                                    Sql.GetNewCond(Sql.Equal(SqlTabUspData.CGost,
                                                             SqlTabBigAngleData.CGost)) + " group by " + SqlTabBigAngleData.CGost;


        if (SqlOracle.Sel(query, paramDict, out gosts))
        {
            return gosts;
        }
        throw new TimeoutException();
    }

    
}
