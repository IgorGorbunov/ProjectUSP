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
    public Face TopSleeveFace
    {
        get
        {
            if (_topFace == null)
            {
                SetTopSleeveFace();
            }
            return _topFace;
        }
    }

    private Face _topFace;

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
    public void SetTopSleeveFace()
    {
        _topFace = GetFace(Config.SleeveTopName);
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
        touch.Create(ElementComponent, TopSleeveFace, jigPlank.ElementComponent, jigPlank.TopJigFace);
        return touch;
    }
}
