using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;


public partial class ImageSqlForm : ImageForm
{
    public ImageSqlForm(string sqlQuery, Dictionary<string, string> param, MouseEventHandler mouseClickEventHandler)
        : base(mouseClickEventHandler)
    {
        DataTable dataTable;
        if (SqlOracle.SelData(sqlQuery, param, out dataTable))
        {
            SetImages(dataTable);
            Initialize();
            DrawItems();
        }
        else
        {
            
        }

    }

    private void SetImages(DataTable dataTable)
    {
        List<ImageInfo> images = new List<ImageInfo>();
        foreach (DataRow row in dataTable.Rows)
        {
            Image image = null;
            string title = "";
            for (int i = 0; i < dataTable.Columns.Count; i++)
            {
                bool titleSet = false;
                if (i == 0)
                {
                    image = Instr.GetImage(row[i] as byte[]);
                }
                else
                {
                    title += row[i].ToString();
                    titleSet = true;
                }

                if (titleSet && i < (dataTable.Columns.Count - 1))
                {
                    title += " - ";
                }
            }
            ImageInfo info = new ImageInfo(image, title, 1, false, null);

            if (image != null) images.Add(info);
        }
        
        Images = images;
    }

    private void Initialize()
    {
        InitializeComponent();
    }

    
}
