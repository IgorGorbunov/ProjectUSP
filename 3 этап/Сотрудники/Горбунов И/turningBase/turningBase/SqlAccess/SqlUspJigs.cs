using System;
using System.Collections.Generic;

/// <summary>
/// ����� ������� � ��������� �����������.
/// </summary>
public static class SqlUspJigs
{
    /// <summary>
    /// ���������� ����������� �� ���� ������������ ������ � ���������� ��������� ��� ������.
    /// </summary>
    /// <param name="catalog">�������.</param>
    /// <param name="diametr">������� ��� ������.</param>
    /// <param name="gost">���� ������ ���.</param>
    /// <returns></returns>
    public static string GetByDiametr(Catalog catalog, double diametr, string gost)
    {
        Dictionary<string, string> parametrs = new Dictionary<string, string>();
        const string par1 = "CAT", par2 = "diametr", par3 = "gost";
        parametrs.Add(par1, ((int)catalog.CatalogUsp).ToString());
        parametrs.Add(par2, diametr.ToString());
        parametrs.Add(par3, gost);

        string query = Sql.GetBegin(SqlTabUspData.CTitle);
        query += SqlUspElement.From + Sql.AddTable(SqlTabJigData.Name) + Sql.Where;
        query += SqlTabUspData.CInnerDiametr + Sql.Eq + Sql.Par(par2);
        query += Sql.GetNewCond(SqlTabUspData.CCatalog + Sql.Eq + Sql.Par(par1));
        query += Sql.GetNewCond(SqlTabUspData.CGost + Sql.Eq + SqlTabJigData.CGost);
        query += Sql.GetNewCond(Sql.Equal(SqlTabUspData.CGost, Sql.Par(par3)));
        query += Sql.OrderBy(SqlTabUspData.CWeight);
        query = Sql.GetFirst(query);

        string str;
        if (SqlOracle.Sel(query, parametrs, out str))
        {
            return str;
        }
        throw new TimeoutException();
    }

    public static string GetQueryJigTypes(Catalog catalog, out Dictionary<string, string> param)
    {
        param = new Dictionary<string, string>();
        const string cat = "CAT";
        param.Add(cat, ((int)catalog.CatalogUsp).ToString());


        string query = Sql.GetBegin(SqlTabUspData.CImage, SqlTabUspData.CName);
        query += ",GOST ";
        query += Sql.From;
        query += "(" +
                 Sql.GetBegin(SqlTabUspData.CImage, SqlTabUspData.CName, SqlTabUspData.CGost);
        query += ",ROW_NUMBER() OVER (partition BY " + SqlTabUspData.CName + "," +
                 SqlTabUspData.CGost + " ORDER BY 1) RN ";
        query += SqlUspElement.From + Sql.AddTable(SqlTabJigData.Name) + Sql.Where;
        query += SqlTabUspData.CCatalog + Sql.Eq + Sql.Par(cat);
        query += Sql.GetNewCond(SqlTabUspData.CGost + Sql.Eq + SqlTabJigData.CGost);
        query += Sql.GetNewCond(SqlTabUspData.ThereIs);
        query += Sql.OrderBy(SqlTabUspData.CGost);
        query += ") WHERE RN = 1";

        return query;

//    SELECT DET, NAME, GOST
//    FROM (
//    SELECT DET
//         , NAME
//         , DB_DATA.GOST 
//         , ROW_NUMBER() OVER (partition BY NAME,DB_DATA.GOST ORDER BY 1) RN
//    FROM DB_DATA, USP_KONDUKTORS 
//    WHERE DB_DATA.GOST = USP_KONDUKTORS.GOST 
//    )
//    WHERE RN = 1"
    }

    public static string GetQueryJigTypes(Catalog catalog, List<double> sleeveOutDiametrs, out Dictionary<string, string> param)
    {
        param = new Dictionary<string, string>();
        const string cat = "CAT";
        param.Add(cat, ((int)catalog.CatalogUsp).ToString());


        string query = Sql.GetBegin(SqlTabUspData.CImage, SqlTabUspData.CName);
        query += ",GOST ";
        query += Sql.From;
        query += "(" +
                 Sql.GetBegin(SqlTabUspData.CImage, SqlTabUspData.CName, SqlTabUspData.CGost);
        query += ",ROW_NUMBER() OVER (partition BY " + SqlTabUspData.CName + "," +
                 SqlTabUspData.CGost + " ORDER BY 1) RN ";
        query += SqlUspElement.From + Sql.AddTable(SqlTabJigData.Name) + Sql.Where;
        query += SqlTabUspData.CCatalog + Sql.Eq + Sql.Par(cat);
        query += Sql.GetNewCond(SqlTabUspData.CGost + Sql.Eq + SqlTabJigData.CGost);
        query += Sql.GetNewCond(SqlTabUspData.ThereIs);
        query += "and ";
        if (sleeveOutDiametrs.Count > 1)
        {
            query += "(";
        }
        
        int i = 0;
        foreach (double sleeveOutDiametr in sleeveOutDiametrs)
        {
            query += SqlTabUspData.CInnerDiametr + "='" + sleeveOutDiametr + "'";
            i++;
            if (i != sleeveOutDiametrs.Count)
            {
                query += " or ";
            }
        }
        if (sleeveOutDiametrs.Count > 1)
        {
            query += ")";
        }
        query += Sql.OrderBy(SqlTabUspData.CGost);
        query += ") WHERE RN = 1";

        return query;

        //    SELECT DET, NAME, GOST
        //    FROM (
        //    SELECT DET
        //         , NAME
        //         , DB_DATA.GOST 
        //         , ROW_NUMBER() OVER (partition BY NAME,DB_DATA.GOST ORDER BY 1) RN
        //    FROM DB_DATA, USP_KONDUKTORS 
        //    WHERE DB_DATA.GOST = USP_KONDUKTORS.GOST 
        //    )
        //    WHERE RN = 1"
    }
}


