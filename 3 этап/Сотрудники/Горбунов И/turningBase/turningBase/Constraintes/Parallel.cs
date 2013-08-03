using NXOpen;
using NXOpen.Assemblies;
using NXOpen.Positioning;

/// <summary>
/// Класс для создания ограничений параллельности.
/// </summary>
class Parallel : Constrainter
{
    /// <summary>
    /// Создание соединения между двумя объектами.
    /// </summary>
    /// <param name="firstComponent">Первый компонент.</param>
    /// <param name="firstFace">Грань отверстия с первого компонента.</param>
    /// <param name="secondComponent">Второй компонент.</param>
    /// <param name="secondFace">Грань отверстия со второго компонента.</param>
    public void Create(Component firstComponent, Face firstFace,
                       Component secondComponent, Face secondFace)
    {
        Constr = (ComponentConstraint)CompPositioner.CreateConstraint();
        Constr.ConstraintType = Constraint.Type.Parallel;


        Constr.CreateConstraintReference(firstComponent,
                                         firstFace, false, false, false);

        Constr.CreateConstraintReference(secondComponent,
                                         secondFace, false, false, false);

        ExecuteConstraints();
    }
}

