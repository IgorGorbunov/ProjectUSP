using System.Collections.Generic;
using System.Data;

/// <summary>
/// Функции для работы с sql запросами.
/// </summary>
public static class Sql
{
    public const string Where = " where ";
    public const string Eq = " = ";
    public const string From = "from ";

    /// <summary>
    /// Возвращает строку для начала запроса.
    /// </summary>
    /// <param name="columns">Столбцы.</param>
    /// <returns></returns>
    public static string GetBegin(params string[] columns)
    {
        string query = "select ";
        for (int i = 0; i < columns.Length; i++)
        {
            query += columns[i];
            if (i == columns.Length - 1)
            {
                query += " ";
                break;
            }
            query += ",";
        }
        return query;
    }

    public static string GetPar(string parametr)
    {
        return ":" + parametr;
    }

    public static string GetNewCond(string condition)
    {
        return " and " + condition;
    }

    public static string GetFirst(string query)
    {
        return "select * from (" + query + ") where rownum = 1";
    }

    public static string ToNumber(string str)
    {
        return "TO_NUMBER(" + str + ")";
    }

    public static string orderBy(string column)
    {
        return " ORDER BY " + column;
    }

    public static string addTable(string table)
    {
        return "," + table;
    }
}

