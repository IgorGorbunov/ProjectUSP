using NXOpen;
using NXOpen.Assemblies;

/// <summary>
/// Класс обычных кондукторных втулок.
/// </summary>
public class CommonJigSleeve : JigSleeve
{



    /// <summary>
    /// Инициализирует новый экземпляр класса быстросменной кондуктрорной втулки УСП 
    /// для заданного компонента.
    /// </summary>
    /// <param name="component">Компонент детали УСП.</param>
    public CommonJigSleeve(Component component)
        : base(component)
    {
        
    }

    /// <summary>
    /// Создаёт констрэйнты TouchAxe и Align кондукторной планки и втулки.
    /// </summary>
    /// <param name="jigPlank">Кондукторная планка.</param>
    public override void SetInJig(JigPlank jigPlank)
    {
        Align align = new Align();
        align.Create(TopFace, jigPlank.TopJigFace);
        TouchAxe touchAxe = new TouchAxe();
        touchAxe.Create(SleeveFace, jigPlank.SleeveFace);
        NxFunctions.Update();
    }

    
}
