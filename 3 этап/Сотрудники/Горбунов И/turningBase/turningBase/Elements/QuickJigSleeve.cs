using NXOpen;
using NXOpen.Assemblies;

/// <summary>
/// Класс быстросменных кондукторных втулок.
/// </summary>
public class QuickJigSleeve : JigSleeve
{
    /// <summary>
    /// Возвращает грань для касания втулки и кондукторной планки.
    /// </summary>
    public Face TopFace
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

    private Face _topFace, _bottomFace;
    private double _innerDiametr;

    /// <summary>
    /// Инициализирует новый экземпляр класса быстросменной кондуктрорной втулки УСП 
    /// для заданного компонента.
    /// </summary>
    /// <param name="component">Компонент детали УСП.</param>
    public QuickJigSleeve(Component component) : base(component)
    {
        
    }
    
    /// <summary>
    /// Устанавливает верхнюю грань касания с кондукторной планкой, использовать после Replacement.
    /// </summary>
    public void SetTopFace()
    {
        _topFace = GetFace(Config.SleeveTopName);
    }

    /// <summary>
    /// Устанавливает нижнюю грань касания с кондукторной планкой, использовать после Replacement.
    /// </summary>
    public void SetBottomFace()
    {
        _bottomFace = GetFace(Config.SleeveBottomName);
    }

    /// <summary>
    /// Создаёт констрэйнт TouchAxe кондукторной планки и втулки.
    /// </summary>
    /// <param name="jigPlank">Кондукторная планка.</param>
    public TouchAxe SetToJig(JigPlank jigPlank)
    {
        TouchAxe touchAxe = new TouchAxe();
        touchAxe.Create(ElementComponent, SleeveFace, jigPlank.ElementComponent, jigPlank.SleeveFace);
        return touchAxe;
    }

    /// <summary>
    /// Создаёт констрэйнт Touch кондукторной планки и втулки.
    /// </summary>
    /// <param name="jigPlank">Кондукторная планка.</param>
    public Touch SetOnJig(JigPlank jigPlank)
    {
        Touch touch = new Touch();
        touch.Create(ElementComponent, TopFace, jigPlank.ElementComponent, jigPlank.TopJigFace);
        return touch;
    }

    /// <summary>
    /// Устанавливает внутренний диаметр втулки, использовать после Replacement.
    /// </summary>
    public void SetInnerDiametr()
    {
        string[] split = ElementComponent.Name.Split('_');
        string titlePrt = split[split.Length - 1];
        split = titlePrt.Split('.');
        string title = split[0];
        string range = SqlUspElement.GetInnerDiametr(title);
        split = range.Split(' ');
        _innerDiametr = double.Parse(split[split.Length - 1]);
    }
}
