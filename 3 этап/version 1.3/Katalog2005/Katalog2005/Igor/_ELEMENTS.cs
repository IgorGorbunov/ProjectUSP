using System;
using System.Collections.Generic;
using System.Text;

// Igor
class _ELEMENTS
{
    static string _elementsTable = "DB_DATA";

    /// <summary>
    /// Возвращает количество элементов (всего)
    /// </summary>
    /// <param name="title">Обозначение</param>
    /// <returns></returns>
    public static int getAllN(string title)
    {
        Dictionary<string, string> Dict = new Dictionary<string, string>();
        Dict.Add("TITLE", title);

        int i = 0;
        object val = null;
        string cmdQuery = "select NALICHI from " + _elementsTable + " where obozn = :TITLE and NALICHI <> 999";

        val = SQLOracle.sel(cmdQuery, Dict);
        if (val != null)
        {
            i = Int32.Parse(val.ToString());
        }

        return i;
    }

    /// <summary>
    /// Возвращает количество элементов в работе
    /// </summary>
    /// <param name="title">Обозначение</param>
    /// <returns></returns>
    public static int getBusyN(string title)
    {
        Dictionary<string, string> Dict = new Dictionary<string, string>();
        Dict.Add("TITLE", title);

        object val = null;
        int i = 0;

        val = SQLOracle.sel("select SUM(ELEMENTS_COUNT) from USP_HOT_STATS where ELEMENT_TITLE = :TITLE", Dict);
        if (val != null)
        {
            i = Int32.Parse(val.ToString());
        }

        return i;
    }

    /// <summary>
    /// Возвращает количество элементов на складе
    /// </summary>
    /// <param name="title">Обозначение</param>
    /// <returns></returns>
    public static int getFreeN(string title)
    {
        Dictionary<string, string> Dict = new Dictionary<string, string>();
        Dict.Add("TITLE", title);

        int busy = 0;
        object val = null;
        int i = 0;

        busy = getBusyN(title);
        val = SQLOracle.sel("select NALICHI - " + busy + " from DB_DATA where obozn = :TITLE", Dict);
        if (val != null)
        {
            i = Int32.Parse(val.ToString());
        }

        return i;
    }

    /// <summary>
    /// Возвращает ГОСТ элемента
    /// </summary>
    /// <param name="title">Обозначение</param>
    /// <returns></returns>
    public static string getGOST(string title)
    {
        Dictionary<string, string> Dict = new Dictionary<string, string>();
        Dict.Add("TITLE", title);

        object val = null;
        string str = "";

        val = SQLOracle.sel("select GOST from " + _elementsTable + " where obozn = :TITLE", Dict);
        if (val != null)
        {
            str = val.ToString();
        }

        return str;
    }

    /// <summary>
    /// Возвращает группу элемента
    /// </summary>
    /// <param name="title">Обозначение</param>
    /// <returns></returns>
    public static string getGroup(string title)
    {
        Dictionary<string, string> Dict = new Dictionary<string, string>();
        Dict.Add("TITLE", title);


        object val = null;
        int i = 0;
        string groupS = "";

        val = SQLOracle.sel("select GROUP_USP from DB_DATA where obozn = :TITLE", Dict);
        if (val != null)
        {
            i = Int32.Parse(val.ToString());
            switch (i)
            {
                case 0:
                    groupS = "Базовые детали";
                    break;
                case 1:
                    groupS = "Корпусные детали";
                    break;
                case 2:
                    groupS = "Установочные детали";
                    break;
                case 3:
                    groupS = "Направляющие детали";
                    break;
                case 4:
                    groupS = "Прижимные детали";
                    break;
                case 5:
                    groupS = "Крепежные детали";
                    break;
                case 6:
                    groupS = "Разные детали";
                    break;
                case 7:
                    groupS = "Сборочные единицы";
                    break;
            }
        }

        return groupS;
    }

    /// <summary>
    /// Возвращает каталог детали
    /// </summary>
    /// <param name="title">Обозначение</param>
    /// <returns></returns>
    public static string getKatalog(string title)
    {
        Dictionary<string, string> Dict = new Dictionary<string, string>();
        Dict.Add("TITLE", title);

        int catalog = 0;
        object val = null;
        string catalogS = "";

        val = SQLOracle.sel("select KATALOG_USP from " + _elementsTable + " where obozn = :TITLE", Dict);
        if (val != null)
        {
            catalog = Int32.Parse(val.ToString());
            switch (catalog)
            {
                case 0:
                    catalogS = "УСП-8";
                    break;
                case 1:
                    catalogS = "УСП-12";
                    break;
                case 2:
                    catalogS = "Специальные детали";
                    break;
            }
        }

        return catalogS;
    }

    /// <summary>
    /// Возвращает наименование элемента
    /// </summary>
    /// <param name="title">Обозначение</param>
    /// <returns></returns>
    public static string getName(string title)
    {
        Dictionary<string, string> Dict = new Dictionary<string, string>();
        Dict.Add("TITLE", title);

        object val = null;
        string str = "";
        val = SQLOracle.sel("select NAME from " + _elementsTable + " where obozn = :TITLE", Dict);

        if (val != null)
        {
            str = val.ToString();
        }

        return str;
    }

    /// <summary>
    /// Возвращает массу элемента
    /// </summary>
    /// <param name="title">Обозначение</param>
    /// <returns></returns>
    public static string getWeight(string title)
    {
        Dictionary<string, string> Dict = new Dictionary<string, string>();
        Dict.Add("TITLE", title);

        string str = "";
        object val = null;

        val = SQLOracle.sel("select MASSA from " + _elementsTable + " where obozn = :TITLE", Dict);
        if (val != null)
        {
            str = val.ToString();
        }

        return str;
    }
}

