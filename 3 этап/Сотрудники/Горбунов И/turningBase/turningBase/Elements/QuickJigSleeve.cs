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
}
