using System;
using System.Windows.Forms;

/// <summary>
/// Форма для вывода пользователю выбора ближайшей высоты автоматического набора.
/// </summary>
public partial class ExactHeightForm : Form
{
    private const string _BEGIN_LABEL = "Для набора высоты ";
    private const string _END_LABEL = " мм элементов нет. Выберите другое расстояние.";

    private readonly double _lessHeight;
    private readonly double _moreHeight;

    /// <summary>
    /// Создает форму для выбора ближайшей высоты автоматического набора.
    /// </summary>
    /// <param name="height">Высота, которая не была набрана.</param>
    /// <param name="minHeight">Ближайшая меньшая высота.</param>
    /// <param name="maxHeight">Ближайшая большая высота.</param>
    public ExactHeightForm(double height, double minHeight, double maxHeight)
    {
        InitializeComponent();
        SetLabel(height);
        _lessHeight = minHeight;
        _moreHeight = maxHeight;
        SetButtons();

        HeightSet.UserHeight = -1;
    }

    private void SetLabel(double height)
    {
        label.Text = _BEGIN_LABEL + Math.Round(height, 2) + _END_LABEL;
    }

    private void SetButtons()
    {
        SetButton(lessBtn, _lessHeight);
        SetButton(moreBtn, _moreHeight);
    }

    private void SetButton(Button button, double height)
    {
        if (height == -1)
        {
            button.Visible = false;
        }
        else
        {
            button.Text = Math.Round(height, 2) + " мм";
        }
    }

    //-----------------------------------------------------------------------------

    private void lessBtn_Click(object sender, EventArgs e)
    {
        HeightSet.UserHeight = _lessHeight;
        Close();
    }

    private void moreBtn_Click(object sender, EventArgs e)
    {
        HeightSet.UserHeight = _moreHeight;
        Close();
    }

    private void cancelBtn_Click(object sender, EventArgs e)
    {
        Close();
    }
}