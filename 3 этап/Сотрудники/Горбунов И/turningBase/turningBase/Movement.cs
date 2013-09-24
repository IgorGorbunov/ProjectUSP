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
        Vector3d translation1 = vec.GetCoords2();
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

    public static void MoveByRotation(Component component, Vector vector, double degreeAngle)
    {

        ComponentPositioner compPositioner =
                Config.WorkPart.ComponentAssembly.Positioner;
        compPositioner.BeginMoveComponent();

        Network network = compPositioner.EstablishNetwork();
        ComponentNetwork compNetwork = (ComponentNetwork)network;

        NXObject[] moveObjects = new NXObject[1];
        moveObjects[0] = component;
        compNetwork.SetMovingGroup(moveObjects);

        compNetwork.BeginDrag();
        double[] tran = vector.Direction2;
        Vector3d vector3D = vector.GetCoords2();

        double[] mtx = new double[9];
        Config.TheUfSession.Mtx3.RotateAboutAxis(tran, Geom.Rad(degreeAngle), mtx);
        Matrix3x3 rotation = ConvertTo3X3(mtx);

        compNetwork.DragByTransform(vector3D, rotation);
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

    static Matrix3x3 ConvertTo3X3(double[] mtx)
    {
        Matrix3x3 mx;
        mx.Xx = mtx[0];
        mx.Xy = mtx[1];
        mx.Xz = mtx[2];
        mx.Yx = mtx[3];
        mx.Yy = mtx[4];
        mx.Yz = mtx[5];
        mx.Zx = mtx[6];
        mx.Zy = mtx[7];
        mx.Zz = mtx[8];

        return mx;
    }
}
