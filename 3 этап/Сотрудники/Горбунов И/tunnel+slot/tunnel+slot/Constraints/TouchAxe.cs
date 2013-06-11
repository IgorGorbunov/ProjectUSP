using NXOpen;
using NXOpen.Assemblies;
using NXOpen.Positioning;

/// <summary>
/// Класс для соединения двух отверстий вдоль оси.
/// </summary>
public class TouchAxe : Constrainter
{
    /// <summary>
    /// Создание соединения между отверстиями двух компонентов.
    /// </summary>
    /// <param name="firstComponent">Первый компонент.</param>
    /// <param name="firstFace">Грань отверстия с первого компонента.</param>
    /// <param name="secondComponent">Второй компонент.</param>
    /// <param name="secondFace">Грань отверстия со второго компонента.</param>
    public void Create(Component firstComponent, Face firstFace, 
                       Component secondComponent, Face secondFace)
    {
        Constr = (ComponentConstraint)ComponentPositioner.CreateConstraint();
        Constr.ConstraintAlignment = Constraint.Alignment.ContraAlign;
        Constr.ConstraintType = Constraint.Type.Touch;

        Constr.CreateConstraintReference(firstComponent,
                                         firstFace, true, false, false);
        Line line1;
        line1 = Config.WorkPart.Lines.CreateFaceAxis(firstFace, 
                SmartObject.UpdateOption.AfterModeling);

        Constr.CreateConstraintReference(secondComponent,
                                         secondFace, true, false, false);

        Line line2;
        line2 = Config.WorkPart.Lines.CreateFaceAxis(secondFace, 
                SmartObject.UpdateOption.AfterModeling);

        ExecuteConstraints();
    }

    public void Create(Component firstComponent, Edge firstEdge,
                       Component secondComponent, Edge secondEdge)
    {
        Constr = (ComponentConstraint)ComponentPositioner.CreateConstraint();
        Constr.ConstraintAlignment = Constraint.Alignment.ContraAlign;
        Constr.ConstraintType = Constraint.Type.Touch;

        Constr.CreateConstraintReference(firstComponent,
                                         firstEdge, true, false, false);

        Constr.CreateConstraintReference(secondComponent,
                                         secondEdge, true, false, false);

        ExecuteConstraints();
    }


}

