using NXOpen;
using NXOpen.Assemblies;

/// <summary>
/// Класс быстросменных кондукторных втулок.
/// </summary>
public class QuickJigSleeve : JigSleeve
{
    /// <summary>
    /// Инициализирует новый экземпляр класса быстросменной кондуктрорной втулки УСП 
    /// для заданного компонента.
    /// </summary>
    /// <param name="component">Компонент детали УСП.</param>
    public QuickJigSleeve(Component component) : base(component)
    {
        
    }

    /// <summary>
    /// Создаёт констрэйнты TouchAxe и Touch кондукторной планки и втулки.
    /// </summary>
    /// <param name="jigPlank">Кондукторная планка.</param>
    public override void SetInJig(JigPlank jigPlank)
    {
        Touch touch = new Touch();
        touch.Create(TopFace, jigPlank.TopJigFace);
        TouchAxe touchAxe = new TouchAxe();
        touchAxe.Create(SleeveFace, jigPlank.SleeveFace);
        NxFunctions.Update();
    }
}
