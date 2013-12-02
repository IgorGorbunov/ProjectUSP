using NXOpen;
using NXOpen.Assemblies;
using NXOpen.Positioning;

/// <summary>
/// Класс для соединения двух граней/рёбер центрированием.
/// </summary>
sealed class Center : Constrainter
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
    /// Cоздаёт соединение между объектами двух элементов.
    /// </summary>
    /// <param name="firstObj1">Первый объект первого элемента.</param>
    /// <param name="firstObj2">Второй объект первого элемента.</param>
    /// <param name="secondObj1">Первый объект второго элемента.</param>
    /// <param name="secondObj2">Второй объект второго элемента.</param>
    /// <param name="name">Наименование констрэйнта.</param>
    public void Create(NXObject firstObj1, NXObject firstObj2,
                       NXObject secondObj1, NXObject secondObj2, string name)
    {
        Constr = (ComponentConstraint)CompPositioner.CreateConstraint();
        Constr.ConstraintType = Constraint.Type.Center22;

        Constr.CreateConstraintReference(firstObj1.OwningComponent, firstObj1, false, false, false);
        Constr.CreateConstraintReference(firstObj2.OwningComponent, firstObj2, false, false, false);

        Constr.CreateConstraintReference(secondObj1.OwningComponent, secondObj1, false, false, false);
        Constr.CreateConstraintReference(secondObj2.OwningComponent, secondObj2, false, false, false);

        Constr.SetName(name);

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

    /// <summary>
    /// Cоздаёт соединение центра между пазами двух элементов.
    /// </summary>
    /// <param name="slot1">Паз первого элемента.</param>
    /// <param name="slot2">Паз второго элемента.</param>
    public void Create(Slot slot1, Slot slot2)
    {
        Constr = (ComponentConstraint)CompPositioner.CreateConstraint();
        Constr.ConstraintType = Constraint.Type.Center22;

        Constr.CreateConstraintReference(slot1.SlotSet.ParentComponent, slot1.SideFace1, false, false, false);
        Constr.CreateConstraintReference(slot1.SlotSet.ParentComponent, slot1.SideFace2, false, false, false);

        Constr.CreateConstraintReference(slot2.SideFace1.OwningComponent, slot2.SideFace1, false, false, false);
        Constr.CreateConstraintReference(slot2.SideFace2.OwningComponent, slot2.SideFace2, false, false, false);

        ExecuteConstraints();
    }
}

