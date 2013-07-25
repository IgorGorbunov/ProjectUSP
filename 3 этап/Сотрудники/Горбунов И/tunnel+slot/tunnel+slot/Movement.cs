using NXOpen;
using NXOpen.Assemblies;
using NXOpen.Positioning;

public static class Movement
{
    public static void MoveByDirection(Component comp, Vector vec)
    {
        ComponentPositioner componentPositioner1 =
                Config.WorkPart.ComponentAssembly.Positioner;
        componentPositioner1.BeginMoveComponent();

        Network network2 = componentPositioner1.EstablishNetwork();
        ComponentNetwork componentNetwork2 = (ComponentNetwork)network2;

        NXObject[] movableObjects2 = new NXObject[1];
        movableObjects2[0] = comp;
        componentNetwork2.SetMovingGroup(movableObjects2);

        componentNetwork2.BeginDrag();
        Vector3d translation1 = vec.GetCoordsVector3D();
        componentNetwork2.DragByTranslation(translation1);
        componentNetwork2.EndDrag();

        componentNetwork2.Solve();
    }
}
