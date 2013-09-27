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
        if (Constr == null)
            return;
        NXObject objectToDelete = Constr;
        Config.TheSession.UpdateManager.AddToDeleteList(objectToDelete);
    }
    /// <summary>
    /// Возвращает true, если сопряжение переограничено.
    /// </summary>
    /// <returns></returns>
    public bool IsOverConstrained()
    {
        if (Constr.GetConstraintStatus() == Constraint.SolverStatus.OverConstrained ||
            Constr.GetConstraintStatus() == Constraint.SolverStatus.NotConsistentUnknown)
        {
            return true;
        }
        return false;
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

