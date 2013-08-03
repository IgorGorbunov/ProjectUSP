using System.Globalization;
using NXOpen;
using NXOpen.Assemblies;
using NXOpen.Positioning;

/// <summary>
/// Класс для создания ограничений по расстоянию.
/// </summary>
class Distance : Constrainter
{
    /// <summary>
    /// Создание соединения между двумя объектами.
    /// </summary>
    /// <param name="firstComponent">Первый компонент.</param>
    /// <param name="firstObject">"Объект с первого компонента.</param>
    /// <param name="secondComponent">Второй компонент.</param>
    /// <param name="secondObject">Объект со второго компонента.</param>
    /// <param name="distance">Расстояние между объектами</param>
    public void Create(Component firstComponent, NXObject firstObject,
                       Component secondComponent, NXObject secondObject, double distance)
    {
        Constr = (ComponentConstraint)CompPositioner.CreateConstraint();
        Constr.ConstraintType = Constraint.Type.Distance;

        ConstraintReference constraintReference1 = Constr.CreateConstraintReference(firstComponent,
                                         firstObject, false, false, false);

        ConstraintReference constraintReference2 = Constr.CreateConstraintReference(secondComponent,
                                         secondObject, false, false, false);


        Constr.SetExpression(distance.ToString(CultureInfo.InvariantCulture));

        ExecuteConstraints();
    }
}
