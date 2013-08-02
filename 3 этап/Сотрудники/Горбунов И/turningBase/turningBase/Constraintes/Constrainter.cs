﻿using NXOpen;
using NXOpen.Positioning;

/// <summary>
/// Класс для создания различный связей и ограничений.
/// </summary>
public class Constrainter
{
    protected ComponentConstraint Constr;
    protected ComponentPositioner ComponentPositioner;

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
        ComponentPositioner = Config.WorkPart.ComponentAssembly.Positioner;

        _componentNetwork = (ComponentNetwork)ComponentPositioner.EstablishNetwork();
        _componentNetwork.MoveObjectsState = true;
    }
    protected void ExecuteConstraints()
    {
        _componentNetwork.Solve();

        //для того чтобы нормально работали фиксы
        ComponentPositioner.ClearNetwork();
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

