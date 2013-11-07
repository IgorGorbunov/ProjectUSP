﻿using NXOpen;
using NXOpen.Assemblies;
using NXOpen.Positioning;

/// <summary>
/// Класс для соединения двух граней методом касания.
/// </summary>
public class Touch : Constrainter
{
    /// <summary>
    /// Создание соединения между гранями двух компонентов.
    /// </summary>
    /// <param name="firstComponent">Первый компонент.</param>
    /// <param name="firstFace">Грань с первого компонента.</param>
    /// <param name="secondComponent">Второй компонент.</param>
    /// <param name="secondFace">Грань со второго компонента.</param>
    public void Create(Component firstComponent, Face firstFace, 
                                      Component secondComponent, Face secondFace)
    {
        Constr = (ComponentConstraint)CompPositioner.CreateConstraint();
        Constr.ConstraintAlignment = Constraint.Alignment.ContraAlign;
        Constr.ConstraintType = Constraint.Type.Touch;

        Constr.CreateConstraintReference(firstComponent,
                                         firstFace, false, false, false);

        Constr.CreateConstraintReference(secondComponent,
                                         secondFace, false, false, false);
        ExecuteConstraints();

        string logMess = "Соединение касанием " + firstFace + " и " + secondFace;
        Logger.WriteLine(logMess);
    }
    /// <summary>
    /// Создание соединения между гранями двух компонентов.
    /// </summary>
    /// <param name="firstFace">Грань с первого компонента.</param>
    /// <param name="secondFace">Грань со второго компонента.</param>
    public void Create(Face firstFace, Face secondFace)
    {
        Constr = (ComponentConstraint)CompPositioner.CreateConstraint();
        Constr.ConstraintAlignment = Constraint.Alignment.ContraAlign;
        Constr.ConstraintType = Constraint.Type.Touch;

        Constr.CreateConstraintReference(firstFace.OwningComponent,
                                         firstFace, false, false, false);

        Constr.CreateConstraintReference(secondFace.OwningComponent,
                                         secondFace, false, false, false);
        ExecuteConstraints();

        string logMess = "Соединение касанием " + firstFace + " и " + secondFace;
        Logger.WriteLine(logMess);
    }
    
}

