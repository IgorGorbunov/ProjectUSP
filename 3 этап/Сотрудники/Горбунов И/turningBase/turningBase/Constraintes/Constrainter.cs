using NXOpen;
using NXOpen.Positioning;

/// <summary>
/// Класс для создания различный связей и ограничений.
/// </summary>
public class Constrainter
{
    protected ComponentConstraint Constr;
    protected ComponentPositioner CompPositioner;

    ComponentNetwork _componentNetwork;

    protected Constrainter()
    {
        InitConstraints();
    }

    /// <summary>
    /// Производит реверс констрэйнта.
    /// </summary>
    public void Reverse()
    {
        Constr.FlipAlignment();
    }

    /// <summary>
    /// Удаляет соединение.
    /// </summary>
    public void Delete()
    {
        NXObject objectToDelete = Constr;
        Config.TheSession.UpdateManager.AddToDeleteList(objectToDelete);
    }
    /// <summary>
    /// Возвращает true, если сопряжение переограничено.
    /// </summary>
    /// <returns></returns>
    public bool IsOverConstrained()
    {
        return Constr.GetConstraintStatus() == Constraint.SolverStatus.OverConstrained;
    }

    protected void ExecuteConstraints()
    {
        _componentNetwork.Solve();

        //для того чтобы нормально работали фиксы
        CompPositioner.ClearNetwork();
    }

    private void InitConstraints()
    {
        CompPositioner = Config.WorkPart.ComponentAssembly.Positioner;

        _componentNetwork = (ComponentNetwork)CompPositioner.EstablishNetwork();
        _componentNetwork.MoveObjectsState = true;
    }
    



}

