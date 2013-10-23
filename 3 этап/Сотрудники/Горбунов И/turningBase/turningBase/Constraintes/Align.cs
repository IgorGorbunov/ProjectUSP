using NXOpen;
using NXOpen.Assemblies;
using NXOpen.Positioning;

/// <summary>
/// Класс для соединения двух граней методом выравнивания.
/// </summary>
public class Align : Constrainter
{
    /// <summary>
    /// Создание соединения между гранями/ребрами двух компонентов.
    /// </summary>
    /// <param name="firstComponent">Первый компонент.</param>
    /// <param name="first">Грань/ребро с первого компонента.</param>
    /// <param name="secondComponent">Второй компонент.</param>
    /// <param name="second">Грань/ребро со второго компонента.</param>
    public void Create(Component firstComponent, NXObject first,
                                      Component secondComponent, NXObject second)
    {
        Constr = (ComponentConstraint)CompPositioner.CreateConstraint();
        Constr.ConstraintAlignment = Constraint.Alignment.CoAlign;
        Constr.ConstraintType = Constraint.Type.Touch;

        Constr.CreateConstraintReference(firstComponent,
                                         first, false, false, false);

        Constr.CreateConstraintReference(secondComponent,
                                         second, false, false, false);
        ExecuteConstraints();

        string logMess = "Соединение выравниванием " + first + " и " + second;
        Logger.WriteLine(logMess);
    }
    
}

