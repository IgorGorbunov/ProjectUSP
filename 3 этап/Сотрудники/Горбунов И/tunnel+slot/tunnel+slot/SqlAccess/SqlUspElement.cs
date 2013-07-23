using System;
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
        string query = "select " + SqlTabUspData.CCatalogNum + " from " + SqlTabUspData.Name +
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
    
}
