using System;
using System.Collections.Generic;
using System.Text;
using NXOpen;
using NXOpen.Assemblies;
using NXOpen.Positioning;

/// <summary>
/// Класс для соединения двух граней методом касания.
/// </summary>
public class TouchConstraint : Constrainter
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    public TouchConstraint() : base()
    {

    }

    /// <summary>
    /// Создание соединения между гранями двух компонентов.
    /// </summary>
    /// <param name="firstComponent">Первый компонент.</param>
    /// <param name="firstFace">Грань с первого компонента.</param>
    /// <param name="secondComponent">Второй компонент.</param>
    /// <param name="secondFace">Грань со второго компонента.</param>
    public void create(Component firstComponent, Face firstFace, 
                                      Component secondComponent, Face secondFace)
    {
        constr = (ComponentConstraint)componentPositioner.CreateConstraint();
        constr.ConstraintAlignment = Constraint.Alignment.ContraAlign;
        constr.ConstraintType = NXOpen.Positioning.Constraint.Type.Touch;

        ConstraintReference constraintReference1 =
            constr.CreateConstraintReference(firstComponent,
                                             firstFace, false, false, false);

        ConstraintReference constraintReference3 =
            constr.CreateConstraintReference(secondComponent,
                                             secondFace, false, false, false);

        executeConstraints();
    }
    /// <summary>
    /// Удаляет соединение.
    /// </summary>
    public void delete()
    {
        NXObject object_to_delete = base.constr;
        Config.theSession.UpdateManager.AddToDeleteList(object_to_delete);
    }

}

