using NXOpen;
using NXOpen.Positioning;

/// <summary>
/// Класс для соединения двух граней методом задания угла.
/// </summary>
public class Angle : Constrainter
{
    /// <summary>
    /// Создание соединения между гранями/ребрами двух компонентов.
    /// </summary>
    /// <param name="first">Грань/ребро с первого компонента.</param>
    /// <param name="second">Грань/ребро со второго компонента.</param>
    /// <param name="angle">Угол в градусах.</param>
    public void Create(NXObject first, NXObject second, double angle)
    {
        Constr = (ComponentConstraint)CompPositioner.CreateConstraint();
        Constr.ConstraintAlignment = Constraint.Alignment.ContraAlign;
        Constr.ConstraintType = Constraint.Type.Angle;

        Constr.CreateConstraintReference(first.OwningComponent,
                                         first, false, false, false);

        Constr.CreateConstraintReference(second.OwningComponent,
                                         second, false, false, false);
        //Constr.SetExpression(angle.ToString());
        ExecuteConstraints();

        string logMess = "Создание угла между " + first.OwningComponent + " и " + 
            second.OwningComponent + " со значением " + angle;
        Logger.WriteLine(logMess);
    }
    
}

