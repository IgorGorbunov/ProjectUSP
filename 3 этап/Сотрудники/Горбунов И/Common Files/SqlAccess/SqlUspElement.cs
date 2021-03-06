﻿using System;
using System.Collections.Generic;

/// <summary>
/// Класс запросов информации для детали.
/// </summary>
static class SqlUspElement
{
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
}
