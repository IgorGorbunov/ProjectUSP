﻿using System;
using System.Collections.Generic;
using System.Data;

/// <summary>
/// Класс запросов информации для детали.
/// </summary>
static class SqlUspElement
{
    private static readonly string _SELECT_QUERY_BASES = "select " + SqlTabUspData.CTitle + "," + SqlTabUspData.CDiametr +
                       " from " + SqlTabUspData.Name +
                       " where " + SqlTabUspData.CGroup + " = " + (int)SqlTabUspData.GroupUsp.Base +
                           " and " + SqlTabUspData.ThereIs +
                           " and (" + SqlTabUspData.CName + " like '" + SqlTabUspData.GetName(SqlTabUspData.NameUsp.RoundPlate) + "%'" +
                                " or " + SqlTabUspData.CName + " like '" + SqlTabUspData.GetName(SqlTabUspData.NameUsp.RoundPlates) + "%')";

    public const string From = "from " + SqlTabUspData.Name + "";


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
                           //Sql.GetNewCond(SqlTabUspData.ThereIs) + 
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

        string qS = "select * from (";
        qS += "select " + SqlTabUspData.CTitle + "," + SqlTabUspData.CName + "," + colums +
                " from " + SqlTabUspData.Name + " where " +
                SqlTabUspData.ThereIs +
                " and " + SqlTabUspData.CCatalog + " = " + (int)catalog.CatalogUsp +                     
                " and " + SqlTabUspData.CGroup + " = " + (int)SqlTabUspData.GroupUsp.Base + 
                " and " + SqlTabUspData.CName + " like '" +
                SqlTabUspData.GetName(SqlTabUspData.NameUsp.Plates) + "%'" +
                " and (TO_NUMBER(" + SqlTabUspData.CLength + ") > :minLen or TO_NUMBER(" + SqlTabUspData.CDiametr +
                    ") > :minLen)" +
                " and (TO_NUMBER(" + SqlTabUspData.CWidth + ") > :minWid or TO_NUMBER(" + SqlTabUspData.CDiametr +
                    ") > :minWid)" +
                conditions + " order by Len,Wid";
        qS += ") where rownum = 1";

        DataTable dataTable;
        if (SqlOracle.SelData(qS, paramDict, out dataTable))
        {
            if (dataTable == null)
            {
                return null;
            }
            List<NoRoundBaseData> list = Sql.ToNoRoundBaseDataList(dataTable);
            return list[0];
        }
        throw new TimeoutException();
    }

    public static DataTable GetSleeves(Catalog catalog, string conditionType, string plankGost)
    {
        Dictionary<string, string> parametrs = new Dictionary<string, string>();
        parametrs.Add("CAT", ((int)catalog.CatalogUsp).ToString());
        parametrs.Add("GOST", plankGost);

        string query = Sql.GetBegin(SqlTabUspData.CTitle, 
                                             SqlTabUspData.CInnerDiametr,
                                             SqlTabUspData.CDiametr,
                                             SqlTabUspData.CHeight);
        query += "from " + SqlTabUspData.Name + " where " +
                 SqlTabUspData.CCatalog + " = :CAT" +
                 " and " + SqlTabUspData.ThereIs +
                 conditionType +
                 " and " + SqlTabUspData.CDiametr + " = any (select " +
                 SqlTabUspData.CInnerDiametr +
                 " from " + SqlTabUspData.Name +
                 " where " + SqlTabUspData.CGost + " = :GOST)";

        DataTable dataTable;
        if (SqlOracle.SelData(query, parametrs, out dataTable))
        {
            return dataTable;
        }
        throw new TimeoutException();
    }

    public static double GetDiametr(Catalog catalog, string title)
    {
        Dictionary<string, string> parametrs = new Dictionary<string, string>();
        const string par1 = "CAT", par2 = "TITLE";
        parametrs.Add(par1, ((int)catalog.CatalogUsp).ToString());
        parametrs.Add(par2, title);

        string query = Sql.GetBegin(SqlTabUspData.CDiametr);
        query += From + Sql.Where;
        query += SqlTabUspData.CTitle + Sql.Eq + Sql.GetPar(par2);
        query += Sql.GetNewCond(SqlTabUspData.CCatalog + Sql.Eq + Sql.GetPar(par1));

        string str;
        if (SqlOracle.Sel(query, parametrs, out str))
        {
            return Double.Parse(str);
        }
        throw new TimeoutException();
    }

    public static string GetInnerDiametr(string title)
    {
        Dictionary<string, string> parametrs = new Dictionary<string, string>();
        const string par1 = "TITLE";
        parametrs.Add(par1, title);

        string query = Sql.GetBegin(SqlTabUspData.CInnerDiametr);
        query += From + Sql.Where;
        query += SqlTabUspData.CTitle + Sql.Eq + Sql.GetPar(par1);

        string str;
        if (SqlOracle.Sel(query, parametrs, out str))
        {
            return str;
        }
        throw new TimeoutException();
    }

    
}
