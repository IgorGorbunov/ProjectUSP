using System.Windows.Forms;

/// <summary>
/// Форма выбора серии каталога УСП.
/// </summary>
public partial class CatalogForm : Form
{
    /// <summary>
    /// Выбранная серия каталога УСП пользователем.
    /// </summary>
    public Catalog SelectedCatalog = null;

    public CatalogForm()
    {
        InitializeComponent();
    }

    private void cancelBtn_Click(object sender, System.EventArgs e)
    {
        Close();
    }

    private void cat8Btn_Click(object sender, System.EventArgs e)
    {
        SelectedCatalog = new Catalog8();
        Close();
    }

    private void cat12Btn_Click(object sender, System.EventArgs e)
    {
        SelectedCatalog = new Catalog12();
        Close();
    }
}