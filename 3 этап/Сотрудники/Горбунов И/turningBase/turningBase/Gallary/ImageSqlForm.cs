using System.Collections.Generic;
using System.Data;
using System.Drawing;


public partial class ImageSqlForm : img_gallery.ImageForm
{
    public ImageSqlForm(string sqlQuery, Dictionary<string, string> param)
    {
        Initialize();
        DataTable dataTable;
        if (SqlOracle.SelData(sqlQuery, param, out dataTable))
        {
            SetImages(dataTable);
            DrawItems();
        }
        else
        {
            
        }

    }

    private void SetImages(DataTable dataTable)
    {
        Dictionary<Image, string> images = new Dictionary<Image, string>();
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

                if (titleSet)
                {
                    title += " - ";
                }
            }

            if (image != null) images.Add(image, title);
        }
        Images = images;
    }

    private void Initialize()
    {
        InitializeComponent();
    }

    
}
