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
    /// Возвращает список баз, используя заданную таблицу.
    /// </summary>
    /// <param name="dataTable">Таблица из БД в формате Обозначение-string|Наименование-string|Длина-double|Ширина-double.</param>
    /// <returns></returns>
    public static List<NoRoundBaseData> ToNoRoundBaseDataList(DataTable dataTable)
    {
        List<NoRoundBaseData> baseData = new List<NoRoundBaseData>();
        foreach (DataRow row in dataTable.Rows)
        {
            NoRoundBaseData data = new NoRoundBaseData();
            data.Title = (string)row[0];
            data.Name = (string)row[1];
            data.Length = double.Parse(row[2].ToString());
            data.Width = double.Parse(row[3].ToString());
            baseData.Add(data);
        }
        return baseData;
    }
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

    public static string OrderBy(string column)
    {
        return " ORDER BY " + column;
    }

    public static string AddTable(string table)
    {
        return "," + table;
    }

    public static string Equal(object param1, object param2)
    {
        return param1.ToString() + Eq + param2.ToString();
    }
}

