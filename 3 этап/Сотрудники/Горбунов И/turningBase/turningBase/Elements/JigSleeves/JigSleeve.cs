using NXOpen;
using NXOpen.Assemblies;

/// <summary>
/// Класс кондукторных втулок.
/// </summary>
public class JigSleeve : SingleElement
{
    /// <summary>
    /// Возвращает грань для центрирования втулки в кондукторной планке.
    /// </summary>
    protected Face SleeveFace
    {
        get
        {
            if (_cyllindricalFace == null)
            {
                SetSleeveFace();
            }
            return _cyllindricalFace;
        }
    }
    /// <summary>
    /// Возвращает нижнюю грань втулки.
    /// </summary>
    public Face BottomFace
    {
        get
        {
            if (_bottomFace == null)
            {
                SetBottomFace();
            }
            return _bottomFace;
        }
    }
    /// <summary>
    /// Возвращает внутренний диаметр втулки.
    /// </summary>
    public double InnerDiametr
    {
        get
        {
            if (_innerDiametr == default(double))
            {
                SetInnerDiametr();
            }
            return _innerDiametr;
        }
    }

    /// <summary>
    /// Возвращает грань для касания втулки и кондукторной планки.
    /// </summary>
    protected Face TopFace
    {
        get
        {
            if (_topFace == null)
            {
                SetTopFace();
            }
            return _topFace;
        }
    }



    private Face _topFace, _bottomFace;
    private Face _cyllindricalFace;
    private double _innerDiametr;

    protected JigSleeve(Component component) : base(component)
    {
        
    }

    /// <summary>
    /// Создаёт констрэйнты TouchAxe и Touch/Align кондукторной планки и втулки.
    /// </summary>
    /// <param name="jigPlank">Кондукторная планка.</param>
    public virtual void SetInJig(JigPlank jigPlank)
    {

    }

    /// <summary>
    /// Устанавливает цилиндрическую грань для установки в кондукторную планку, использовать после Replacement.
    /// </summary>
    private void SetSleeveFace()
    {
        _cyllindricalFace = GetFace(Config.SleeveFaceName);
    }

    /// <summary>
    /// Устанавливает внутренний диаметр втулки, использовать после Replacement.
    /// </summary>
    private void SetInnerDiametr()
    {
        string range = SqlUspElement.GetInnerDiametr(Title);
        string[] split = range.Split(' ');
        _innerDiametr = double.Parse(split[split.Length - 1]);
    }

    /// <summary>
    /// Устанавливает верхнюю грань касания с кондукторной планкой, использовать после Replacement.
    /// </summary>
    private void SetTopFace()
    {
        _topFace = GetFace(Config.SleeveTopName);
    }

    /// <summary>
    /// Устанавливает нижнюю грань касания с кондукторной планкой, использовать после Replacement.
    /// </summary>
    private void SetBottomFace()
    {
        _bottomFace = GetFace(Config.SleeveBottomName);
    }


}

