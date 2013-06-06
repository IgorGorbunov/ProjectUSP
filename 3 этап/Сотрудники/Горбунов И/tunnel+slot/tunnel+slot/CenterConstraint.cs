using NXOpen;
using NXOpen.Assemblies;
using NXOpen.Positioning;

/// <summary>
/// Класс для соединения двух граней/рёбер центрированием.
/// </summary>
class CenterConstraint : Constrainter
{
    /// <summary>
    /// Cоздаёт соединение между объектами двух элементов.
    /// </summary>
    /// <param name="firstComponent">Первый элемент.</param>
    /// <param name="firstObj1">Первый объект первого элемента.</param>
    /// <param name="firstObj2">Второй объект первого элемента.</param>
    /// <param name="secondComponent">Второй элемент.</param>
    /// <param name="secondObj1">Первый объект второго элемента.</param>
    /// <param name="secondObj2">Второй объект второго элемента.</param>
    public void Create(Component firstComponent, NXObject firstObj1, NXObject firstObj2,
                       Component secondComponent, NXObject secondObj1, NXObject secondObj2)
    {
        Constr = (ComponentConstraint)ComponentPositioner.CreateConstraint();
        Constr.ConstraintType = Constraint.Type.Center22;

        Constr.CreateConstraintReference(firstComponent, firstObj1, false, false, false);
        Constr.CreateConstraintReference(firstComponent, firstObj2, false, false, false);

        Constr.CreateConstraintReference(secondComponent, secondObj1, false, false, false);
        Constr.CreateConstraintReference(secondComponent, secondObj2, false, false, false);

        ExecuteConstraints();
    }
}

