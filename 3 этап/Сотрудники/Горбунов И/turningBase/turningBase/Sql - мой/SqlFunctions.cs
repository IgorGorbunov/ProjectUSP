using System.Collections.Generic;
using System.Data;

public static class SqlFunctions
{
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
}

