using System;
using System.Collections.Generic;
using System.Text;
using NXOpen;
using NXOpen.Assemblies;
using NXOpen.Positioning;

/// <summary>
/// Класс для соединения двух отверстий вдоль оси.
/// </summary>
public class TouchAxeConstraint : Constrainter
{
    /// <summary>
    /// Инициализирует новый пустой экземпляр класса.
    /// </summary>
    public TouchAxeConstraint() : base()
    {

    }

    /// <summary>
    /// Создание соединения между отверстиями двух компонентов.
    /// </summary>
    /// <param name="firstComponent">Первый компонент.</param>
    /// <param name="firstFace">Грань отверстия с первого компонента.</param>
    /// <param name="secondComponent">Второй компонент.</param>
    /// <param name="secondFace">Грань отверстия со второго компонента.</param>
    public void create(Component firstComponent, Face firstFace, 
                       Component secondComponent, Face secondFace)
    {
        constr = (ComponentConstraint)componentPositioner.CreateConstraint();
        constr.ConstraintAlignment = Constraint.Alignment.ContraAlign;
        constr.ConstraintType = NXOpen.Positioning.Constraint.Type.Touch;

        ConstraintReference constraintReference1 =
            constr.CreateConstraintReference(firstComponent,
                                             firstFace, true, false, false);

        ConstraintReference constraintReference3 =
            constr.CreateConstraintReference(secondComponent,
                                             secondFace, true, false, false);

        executeConstraints();
    }


}

