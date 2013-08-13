using NXOpen;
using NXOpen.Assemblies;
using NXOpen.Positioning;

/// <summary>
/// Класс для соединения двух отверстий вдоль оси.
/// </summary>
public sealed class TouchAxe : Constrainter
{
    /// <summary>
    /// Создание соединения между отверстиями двух компонентов.
    /// Грани должны быть обязательно ОККУРЕНСАМИ!!!
    /// </summary>
    /// <param name="firstComponent">Первый компонент.</param>
    /// <param name="firstFace">Грань отверстия с первого компонента.</param>
    /// <param name="secondComponent">Второй компонент.</param>
    /// <param name="secondFace">Грань отверстия со второго компонента.</param>
    public void Create(Component firstComponent, Face firstFace, 
                       Component secondComponent, Face secondFace)
    {
        Constr = (ComponentConstraint)CompPositioner.CreateConstraint();
        Constr.ConstraintAlignment = Constraint.Alignment.ContraAlign;
        Constr.ConstraintType = Constraint.Type.Touch;

        Constr.CreateConstraintReference(firstComponent,
                                         firstFace, true, false, false);

        Constr.CreateConstraintReference(secondComponent,
                                         secondFace, true, false, false);
        
        ExecuteConstraints();

        if (Constr.GetConstraintStatus() == Constraint.SolverStatus.OverConstrained)
        {
            Reverse();
        }
        
    }
}

