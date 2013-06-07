using System;
using NXOpen;
using NXOpen.Assemblies;

/// <summary>
/// Класс для соединения отверстие-паз посредством т-образного болта.
/// </summary>
public class TunnelSlotConstraint
{
    
    SlotConstraint _slotConstr;
    TunnelConstraint _tunnelConstsr;
    private Parallel _parallelConstr;
    private Distance _distanceConstr;
    private Fix _fixConstr;

    private readonly UspElement _firstElement;
    private readonly UspElement _secondElement;

    private readonly Tunnel _tunnel;
    private readonly Slot _slot;

    /// <summary>
    /// Инициализирует новый экземпляр класса связей для соединения деталей.
    /// </summary>
    /// <param name="firstElement">Первый элемент УСП.</param>
    /// <param name="tunnel">Базовое отверстие на первом элементе УСП.</param>
    /// <param name="secondElement">Второй элемент УСП.</param>
    /// <param name="slot">Паз на втором элементе УСП</param>
    public TunnelSlotConstraint(UspElement firstElement, Tunnel tunnel,
                         UspElement secondElement, Slot slot)
    {
        _tunnel = tunnel;
        _slot = slot;

        _firstElement = firstElement;
        _secondElement = secondElement;
    }
    /// <summary>
    /// Создание связей.
    /// </summary>
    public void Create()
    {
        Move();
        bool isFixed = Fix();
        Center();
        Parallel();

        Face face1 = _tunnel.Slot.GetOrtDirectionFace();
        Face face2 = _slot.GetOrtDirectionFace();
        SetDistance(face1, face2);

        Touch();
        Delete(isFixed);
        Config.TheUfSession.Modl.Update();
    }

    void Move()
    {
        Point3d tunnelProectionPoint = _tunnel.CentralPoint;

        Log.WriteLine("Координаты центра туннеля на НГП: " + tunnelProectionPoint);
        Log.WriteLine("Координаты проэкции точки выбора паза: " + _slot.SlotPoint);

        Vector moveVector = new Vector(tunnelProectionPoint, _slot.SlotPoint);
        MoveByDirection(_tunnel.ParentComponent, moveVector);
    }

    static void MoveByDirection(Component comp, Vector vec)
    {
        NXOpen.Positioning.ComponentPositioner componentPositioner1 =
                Config.WorkPart.ComponentAssembly.Positioner;
        componentPositioner1.BeginMoveComponent();

        NXOpen.Positioning.Network network2 = componentPositioner1.EstablishNetwork();
        NXOpen.Positioning.ComponentNetwork componentNetwork2 = (NXOpen.Positioning.ComponentNetwork)network2;

        NXObject[] movableObjects2 = new NXObject[1];
        movableObjects2[0] = comp;
        componentNetwork2.SetMovingGroup(movableObjects2);

        componentNetwork2.BeginDrag();
        Vector3d translation1 = vec.GetCoordsVector3D();
        componentNetwork2.DragByTranslation(translation1);
        componentNetwork2.EndDrag();

        componentNetwork2.Solve();
    }

    bool Fix()
    {
        if (!_secondElement.ElementComponent.IsFixed)
        {
            _fixConstr = new Fix();
            _fixConstr.Create(_secondElement.ElementComponent);

            return true;
        }
        return false;
    }

    void Center()
    {
        _slotConstr = new SlotConstraint(_tunnel.Slot, _slot);
        _slotConstr.SetCenterConstraint();
    }

    void Parallel()
    {
        _parallelConstr = new Parallel();
        _parallelConstr.Create(_firstElement.ElementComponent, _tunnel.Slot.BottomFace,
                               _secondElement.ElementComponent, _slot.BottomFace);


        if (Geom.IsEqual(Geom.GetDirection(_tunnel.Slot.BottomFace),
                        (Geom.GetDirection(_slot.BottomFace))))
        {
            _parallelConstr.Reverse();
        }
    }

    void Touch()
    {
        _tunnelConstsr = new TunnelConstraint(_tunnel, _slot);

        Config.FreezeDisplay();
        _tunnelConstsr.SetTouchFaceConstraint(false, false);
        Config.UnFreezeDisplay();
    }

    void Delete(bool isFixed)
    {
        _parallelConstr.Delete();
        _distanceConstr.Delete();

        if (isFixed)
        {
            _fixConstr.Delete();
        }
    }

    void SetDistance(Face firstFace, Face secondFace)
    {
        Edge[] edges = firstFace.GetEdges();
        Point3d point3D, tmpP;
        edges[0].GetVertices(out point3D, out tmpP);

        Platan facePlatan = new Platan(secondFace);
        double distance = Math.Abs(facePlatan.GetDistanceToPoint(point3D));

        _distanceConstr = new Distance();
        _distanceConstr.Create(_tunnel.ParentComponent, firstFace,
                               _slot.ParentComponent, secondFace, distance);
    }
}

