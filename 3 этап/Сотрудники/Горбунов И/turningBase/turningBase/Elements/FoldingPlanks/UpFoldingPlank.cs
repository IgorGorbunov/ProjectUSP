using NXOpen.Assemblies;
using NXOpen.Positioning;

/// <summary>
/// Класс верхнего элемента складывающейся планки.
/// </summary>
public class UpFoldingPlank : SingleElement
{


    /// <summary>
    /// Инициализирует новый экземпляр класса верхнего элемента
    /// складывающейся планки для заданного компонента.
    /// </summary>
    /// <param name="component">Компонент из сборки NX.</param>
    /// <param name="foldingPlank">Складывающаяся планка для данной детали.</param>
    public UpFoldingPlank(Component component, FoldingPlank foldingPlank) : base(component, foldingPlank)
    {

    }

    /// <summary>
    /// Удаляет сопряжение касания между верхним элементом УСП складывающейся
    ///  и кондукторной планки.
    /// </summary>
    public void DeleteJigTouch()
    {
        ClearConstraint(Constraint.Type.Touch, Config.JigFoldingTouch);
    }
}

