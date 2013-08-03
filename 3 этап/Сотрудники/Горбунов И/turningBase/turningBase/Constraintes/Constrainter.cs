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

    private void InitConstraints()
    {
        CompPositioner = Config.WorkPart.ComponentAssembly.Positioner;

        _componentNetwork = (ComponentNetwork)CompPositioner.EstablishNetwork();
        _componentNetwork.MoveObjectsState = true;
    }
    protected void ExecuteConstraints()
    {
        _componentNetwork.Solve();

        //для того чтобы нормально работали фиксы
        CompPositioner.ClearNetwork();
    }

    /// <summary>
    /// Удаляет соединение.
    /// </summary>
    public void Delete()
    {
        NXObject objectToDelete = Constr;
        Config.TheSession.UpdateManager.AddToDeleteList(objectToDelete);
    }

}

