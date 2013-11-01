using NXOpen;
using NXOpen.Assemblies;
using NXOpen.Positioning;

/// <summary>
/// Класс для соединения двух граней/рёбер центрированием.
/// </summary>
class Center : Constrainter
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
        Constr = (ComponentConstraint)CompPositioner.CreateConstraint();
        Constr.ConstraintType = Constraint.Type.Center22;

        Constr.CreateConstraintReference(firstComponent, firstObj1, false, false, false);
        Constr.CreateConstraintReference(firstComponent, firstObj2, false, false, false);

        Constr.CreateConstraintReference(secondComponent, secondObj1, false, false, false);
        Constr.CreateConstraintReference(secondComponent, secondObj2, false, false, false);

        ExecuteConstraints();
    }

    /// <summary>
    /// Cоздаёт соединение центра между пазами двух элементов.
    /// </summary>
    /// <param name="slot1">Паз первого элемента.</param>
    /// <param name="face1">Грань второго паза.</param>
    /// <param name="face2">Грань второго паза.</param>
    public void Create(Slot slot1, Face face1, Face face2)
    {
        Constr = (ComponentConstraint)CompPositioner.CreateConstraint();
        Constr.ConstraintType = Constraint.Type.Center22;

        Constr.CreateConstraintReference(slot1.SlotSet.ParentComponent, slot1.SideFace1, false, false, false);
        Constr.CreateConstraintReference(slot1.SlotSet.ParentComponent, slot1.SideFace2, false, false, false);

        Constr.CreateConstraintReference(face1.OwningComponent, face1, false, false, false);
        Constr.CreateConstraintReference(face2.OwningComponent, face2, false, false, false);

        ExecuteConstraints();
    }
}

