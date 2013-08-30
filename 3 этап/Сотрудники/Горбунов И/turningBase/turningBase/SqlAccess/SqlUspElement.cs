using System;
using System.Collections.Generic;
using System.Data;

/// <summary>
/// Класс запросов информации для детали.
/// </summary>
static class SqlUspElement
{
    static readonly string _SELECT_QUERY_BASES = "select " + SqlTabUspData.CTitle + "," + SqlTabUspData.CDiametr +
                       " from " + SqlTabUspData.Name +
                       " where " + SqlTabUspData.CGroup + " = " + (int)SqlTabUspData.GroupUsp.Base +
                           " and " + SqlTabUspData.ThereIs +
                           " and (" + SqlTabUspData.CName + " like '" + SqlTabUspData.GetName(SqlTabUspData.NameUsp.RoundPlate) + "%'" +
                                " or " + SqlTabUspData.CName + " like '" + SqlTabUspData.GetName(SqlTabUspData.NameUsp.RoundPlates) + "%')";

    /// <summary>
    /// Возвращает номер каталога для детали.
    /// </summary>
    /// <param name="elementTitle"></param>
    /// <returns></returns>
    public static string GetCatalogNum(string elementTitle)
    {
        const string query = "select " + SqlTabUspData.CCatalog + " from " + SqlTabUspData.Name +
                             " where " + SqlTabUspData.CTitle + " = :elementTitle";

        Dictionary<string, string> paramDict = new Dictionary<string, string>();
        paramDict.Add("elementTitle", elementTitle);
        string num;

        if (SqlOracle.Sel(query, paramDict, out num))
        {
            return num;
        }
        throw new TimeoutException();
    }

    /// <summary>
    /// Возвращает пазовые болты по выборке обозначение-длина.
    /// </summary>
    /// <param name="length">Минимальная длина.</param>
    /// <param name="catalog">Каталог для элементов.</param>
    /// <returns></returns>
    public static Dictionary<string, string> GetTitleMinLengthFixture(double length, Catalog catalog)
    {
        Dictionary<string, string> paramDict = new Dictionary<string, string>();
        paramDict.Add("diametr", "%" + catalog.Diametr + "%");
        paramDict.Add("length", length.ToString());

        Dictionary<string, string> dictionary;

        string query = "select " + SqlTabUspData.CTitle + "," + SqlTabUspData.CLength +
                       " from " + SqlTabUspData.Name +
                       " where " + SqlTabUspData.CCatalog + " = " + (int) catalog.CatalogUsp +
                           " and " + SqlTabUspData.CGroup + " = " + (int) SqlTabUspData.GroupUsp.Fixture +
                           " and " + SqlTabUspData.CDiametr + " like :diametr" +
                           " and " + SqlTabUspData.CLength + " >= :length" +
                           " and " + SqlTabUspData.CName + " = " + SqlTabUspData.GetName(SqlTabUspData.NameUsp.SlotBolt);

        if (SqlOracle.Sel(query, paramDict, out dictionary))
        {
            return dictionary;
        }
        throw new TimeoutException();
    }

    /// <summary>
    /// Возвращает круглые плиты по выборке обозначение-длина.
    /// </summary>
    /// <param name="catalog">Каталог для элементов.</param>
    /// <returns></returns>
    public static Dictionary<string, string> GetTitleLengthRoundPlates(Catalog catalog)
    {
        Dictionary<string, string> paramDict = new Dictionary<string, string>();
        Dictionary<string, string> dictionary;

        string query = _SELECT_QUERY_BASES + 
            " and " + SqlTabUspData.CCatalog + " = " + (int) catalog.CatalogUsp;

        if (SqlOracle.Sel(query, paramDict, out dictionary))
        {
            return dictionary;
        }
        throw new TimeoutException();
    }

    /// <summary>
    /// Возвращает круглые плиты c крестообразным расположением пазов по выборке 
    /// обозначение-длина.
    /// </summary>
    /// <param name="catalog">Каталог для элементов.</param>
    /// <returns></returns>
    public static Dictionary<string, string> GetTitleLengthRoundCrossPlates(Catalog catalog)
    {
        Dictionary<string, string> paramDict = new Dictionary<string, string>();
        Dictionary<string, string> dictionary;

        string query = _SELECT_QUERY_BASES +
            " and " + SqlTabUspData.CCatalog + " = " + (int)catalog.CatalogUsp + 
            " and " + SqlTabUspData.CName + " not like '%" + SqlTabUspData.GetName(SqlTabUspData.NameUsp.RadialPlate) + "%'";

        if (SqlOracle.Sel(query, paramDict, out dictionary))
        {
            return dictionary;
        }
        throw new TimeoutException();
    }

    /// <summary>
    /// Возвращает круглые плиты c радиально-поперечным расположением пазов по выборке 
    /// обозначение-длина.
    /// </summary>
    /// <param name="catalog">Каталог для элементов.</param>
    /// <returns></returns>
    public static Dictionary<string, string> GetTitleLengthRoundRadialPlates(Catalog catalog)
    {
        Dictionary<string, string> paramDict = new Dictionary<string, string>();
        Dictionary<string, string> dictionary;

        string query = _SELECT_QUERY_BASES +
            " and " + SqlTabUspData.CCatalog + " = " + (int)catalog.CatalogUsp +
            " and " + SqlTabUspData.CName + " like '%" + SqlTabUspData.GetName(SqlTabUspData.NameUsp.RadialPlate) + "%'";

        if (SqlOracle.Sel(query, paramDict, out dictionary))
        {
            return dictionary;
        }
        throw new TimeoutException();
    }

    /// <summary>
    /// Возвращает экземпляр базы, минимальный по подходящим параметрам.
    /// </summary>
    /// <param name="minLen">Минимальная длина.</param>
    /// <param name="minWid">Минимальная ширина.</param>
    /// <param name="colums">Дополнительные даннные.</param>
    /// <param name="conditions">Дополнительные условия.</param>
    /// <param name="catalog">Каталог.</param>
    /// <returns></returns>
    public static NoRoundBaseData GetNoRoundBase(double minLen, double minWid, string colums,
                                                 string conditions, Catalog catalog)
    {
        Dictionary<string, string> paramDict = new Dictionary<string, string>();
        paramDict.Add("minLen", minLen.ToString());
        paramDict.Add("minWid", minWid.ToString());

        string qS = "select " + SqlTabUspData.CTitle + "," + SqlTabUspData.CName + "," + colums +
                    " from " + SqlTabUspData.Name + " where " +
                    SqlTabUspData.ThereIs +
                    " and " + SqlTabUspData.CCatalog + " = " + (int)catalog.CatalogUsp +                     
                    " and " + SqlTabUspData.CGroup + " = " + (int)SqlTabUspData.GroupUsp.Base + 
                    " and " + SqlTabUspData.CName + " like '" +
                    SqlTabUspData.GetName(SqlTabUspData.NameUsp.Plates) + "%'" +
                    " and rownum = 1" +
                    " and (" + SqlTabUspData.CLength + " > :minLen or " + SqlTabUspData.CDiametr +
                        " > :minLen)" +
                    " and (" + SqlTabUspData.CWidth + " > :minWid or " + SqlTabUspData.CDiametr +
                        " > :minWid)" +
                    conditions + " order by Len,Wid";

        DataTable dataTable;
        if (SqlOracle.SelData(qS, paramDict, out dataTable))
        {
            List<NoRoundBaseData> list = SqlFunctions.ToNoRoundBaseDataList(dataTable);
            return list[0];
        }
        throw new TimeoutException();
    }
}
