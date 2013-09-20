using System;
using System.Collections.Generic;

public static class SqlUspJigs
{
    public static string GetByDiametr(Catalog catalog, double diametr)
    {
        Dictionary<string, string> parametrs = new Dictionary<string, string>();
        const string par1 = "CAT", par2 = "diametr";
        parametrs.Add(par1, ((int)catalog.CatalogUsp).ToString());
        parametrs.Add(par2, diametr.ToString());

        string query = Sql.GetBegin(SqlTabUspData.CTitle);
        query += SqlUspElement.From + Sql.addTable(SqlTabJigData.Name) + Sql.Where;
        query += SqlTabUspData.CInnerDiametr + Sql.Eq + Sql.GetPar(par2);
        query += Sql.GetNewCond(SqlTabUspData.CCatalog + Sql.Eq + Sql.GetPar(par1));
        query += Sql.GetNewCond(SqlTabUspData.CGost + Sql.Eq + SqlTabJigData.CGost);
        query += Sql.orderBy(SqlTabUspData.CWeight);
        query = Sql.GetFirst(query);

        string str;
        if (SqlOracle.Sel(query, parametrs, out str))
        {
            return str;
        }
        throw new TimeoutException();
    }
}


