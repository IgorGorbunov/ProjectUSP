using NXOpen;
using NXOpen.Assemblies;
using NXOpen.Positioning;

/// <summary>
/// Класс перемещений объектов в NX.
/// </summary>
public static class Movement
{
    /// <summary>
    /// Перемещение компонента по заданному направлению.
    /// </summary>
    /// <param name="comp">Перемещаемый компонент.</param>
    /// <param name="vec">Вектор перемещения.</param>
    public static void MoveByDirection(Component comp, Vector vec)
    {
        ComponentPositioner compPositioner =
                Config.WorkPart.ComponentAssembly.Positioner;
        compPositioner.BeginMoveComponent();

        Network network = compPositioner.EstablishNetwork();
        ComponentNetwork compNetwork = (ComponentNetwork)network;

        NXObject[] moveObjects = new NXObject[1];
        moveObjects[0] = comp;
        compNetwork.SetMovingGroup(moveObjects);

        compNetwork.BeginDrag();
        Vector3d translation1 = vec.GetCoordsVector3D();
        compNetwork.DragByTranslation(translation1);
        compNetwork.EndDrag();

        //нужно для нормального выхода из меню
        Session.UndoMarkId markId = 
            Config.TheSession.SetUndoMark(Session.MarkVisibility.Invisible, "Move Component Update");

        compNetwork.Solve();
        compNetwork.ResetDisplay();
        compNetwork.ApplyToModel();
        compPositioner.ClearNetwork();
        Config.TheSession.UpdateManager.DoUpdate(markId);
        compPositioner.EndMoveComponent();
    
    }
}
