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

    
}
