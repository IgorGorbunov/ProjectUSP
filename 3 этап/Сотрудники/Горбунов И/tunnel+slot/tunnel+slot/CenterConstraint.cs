using System;
using System.Collections.Generic;
using System.Text;
using NXOpen;
using NXOpen.Assemblies;
using NXOpen.Positioning;

/// <summary>
/// Класс для соединения двух граней/рёбер центрированием.
/// </summary>
class CenterConstraint : Constrainter
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    public CenterConstraint()
        : base()
    {

    }

    /// <summary>
    /// Cоздаёт соединение между объектами двух элементов.
    /// </summary>
    /// <param name="firstComponent">Первый элемент.</param>
    /// <param name="firstObj1">Первый объект первого элемента.</param>
    /// <param name="firstObj2">Второй объект первого элемента.</param>
    /// <param name="secondComponent">Второй элемент.</param>
    /// <param name="secondObj1">Первый объект второго элемента.</param>
    /// <param name="secondObj2">Второй объект второго элемента.</param>
    public void create(Component firstComponent, NXObject firstObj1, NXObject firstObj2,
                       Component secondComponent, NXObject secondObj1, NXObject secondObj2)
    {
        constr = (ComponentConstraint)componentPositioner.CreateConstraint();
        constr.ConstraintType = NXOpen.Positioning.Constraint.Type.Center22;

        ConstraintReference constraintReference1 =
            constr.CreateConstraintReference(firstComponent, firstObj1, false, false, false);
        ConstraintReference constraintReference2 =
            constr.CreateConstraintReference(firstComponent, firstObj2, false, false, false);

        ConstraintReference constraintReference3 =
            constr.CreateConstraintReference(secondComponent, secondObj1, false, false, false);
        ConstraintReference constraintReference4 =
            constr.CreateConstraintReference(secondComponent, secondObj2, false, false, false);

        executeConstraints();
    }
}

