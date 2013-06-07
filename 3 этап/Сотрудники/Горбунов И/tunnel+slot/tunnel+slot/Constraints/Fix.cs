using NXOpen.Assemblies;
using NXOpen.Positioning;

/// <summary>
/// Класс для фиксации компонента.
/// </summary>
class Fix : Constrainter
{
    /// <summary>
    /// Наложение на компонент.
    /// </summary>
    /// <param name="component">Компонент.</param>
    public void Create(Component component)
    {
        Constr = (ComponentConstraint)ComponentPositioner.CreateConstraint();
        Constr.ConstraintType = Constraint.Type.Fix;

        Constr.CreateConstraintReference(component,
                                         component, false, false, false);

        ExecuteConstraints();
    }
}

