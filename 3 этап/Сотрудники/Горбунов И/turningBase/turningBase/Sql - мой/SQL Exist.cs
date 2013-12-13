using System;
using System.Collections.Generic;
using System.Data;

using System.Drawing;
using Devart.Data.Oracle;

/// <summary>
/// Класс c методами типа Select
/// </summary>
partial class SqlOracle
{
    /// <summary>
    /// Mетод возвращает true, если значение в соответствующем поле и таблице найдено.
    /// </summary>
    /// <param name="value">Значение.</param>
    /// <param name="column">Поле.</param>
    /// <param name="table">Таблица.</param>
    /// <returns></returns>
    public static bool Exist<T>(T value, string column, string table)
    {
        Dictionary<string, string> paramDict = new Dictionary<string, string>();
        paramDict.Add("VALUE", value.ToString());
        object num;

        string query = "select " + column + " from " + table + " where " +
                       column + " = :VALUE";

        if (Sel(query, paramDict, out num))
        {
            return num != null;
        }
        throw new TimeoutException();
    }
}