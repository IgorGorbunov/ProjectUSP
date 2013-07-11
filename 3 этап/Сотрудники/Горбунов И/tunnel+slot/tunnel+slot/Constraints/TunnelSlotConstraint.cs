using NXOpen;
using NXOpen.Assemblies;
using NXOpen.Positioning;

/// <summary>
/// Класс для соединения отверстие-паз посредством т-образного болта.
/// </summary>
public sealed class TunnelSlotConstraint
{
    
    SlotConstraint _slotConstr;
    TunnelConstraint _tunnelConstsr;
    private Fix _fixConstr, _fixFixture;
    private TouchAxe _touchAxe;

    private readonly UspElement _firstElement;
    private readonly UspElement _secondElement;
    private readonly Component _fixture;

    private readonly Tunnel _tunnel;
    private readonly Slot _slot;

    Face _tunnelFixtureFace;
    Face _bottomFace;
    Face[] _tunnelSideFaces;

    /// <summary>
    /// Инициализирует новый экземпляр класса связей для соединения деталей.
    /// </summary>
    /// <param name="firstElement">Первый элемент УСП.</param>
    /// <param name="tunnel">Базовое отверстие на первом элементе УСП.</param>
    /// <param name="secondElement">Второй элемент УСП.</param>
    /// <param name="slot">Паз на втором элементе УСП.</param>
    /// <param name="fixture">Компонент болта для крепления.</param>
    public TunnelSlotConstraint(UspElement firstElement, Tunnel tunnel,
                         UspElement secondElement, Slot slot, UspElement fixture)
    {
        _tunnel = tunnel;
        _slot = slot;

        _firstElement = firstElement;
        _secondElement = secondElement;

        _fixture = fixture.ElementComponent;
    }
    /// <summary>
    /// Создание связей.
    /// </summary>
    public void Create()
    {
        bool isFixed = Fix();
        InsertBolt();

        Center();
        _touchAxe = new TouchAxe();
        _touchAxe.Create(_firstElement.ElementComponent, _tunnel.TunnelFace, _fixture, _tunnelFixtureFace);
        if (Geom.IsEqual(Geom.GetDirection(_tunnel.Slot.BottomFace),
                        (Geom.GetDirection(_slot.BottomFace))))
        {
            _touchAxe.Reverse();
        }

        Touch();
        Delete(isFixed);

        Config.TheUfSession.Modl.Update();
        _slot.Unhighlight();
        _slot.Highlight();

    }
    /// <summary>
    /// Реверс детали УСП вдоль паза.
    /// </summary>
    public void Reverse()
    {
        Fix fixFlement = new Fix();
        Fix fixFixture = new Fix();

        fixFixture.Create(_fixture);
        fixFlement.Create(_secondElement.ElementComponent);

        _slotConstr.Reverse();
        Config.TheUfSession.Modl.Update();

        fixFixture.Delete();
        fixFlement.Delete();
        Config.TheUfSession.Modl.Update();
    }

    void InsertBolt()
    {
        SetFixtureFaces();

        Center center = new Center();
        center.Create(_fixture, _tunnelSideFaces[0], _tunnelSideFaces[1], _secondElement.ElementComponent,
                        _slot.SideFace1, _slot.SideFace2);

        Touch touch = new Touch();
        touch.Create(_fixture, _bottomFace, _secondElement.ElementComponent, _slot.BottomFace);

        MoveBolt();

        _fixFixture = new Fix();
        _fixFixture.Create(_fixture);
    }

    void SetFixtureFaces()
    {
        bool bottomIsSet = false, tunnelIsSet = false;

        Body body = SetBody(_fixture);

        Face[] faces = body.GetFaces();
        foreach (Face face in faces)
        {
            switch (face.SolidFaceType)
            {
                case Face.FaceType.Cylindrical:
                    {
                        _tunnelFixtureFace = face;
                        tunnelIsSet = true;
                        Logger.WriteLine("Цилиндрическая грань крепления болта - " + face);
                    }
                    break;
                case Face.FaceType.Planar:
                    {
                        if (IsBottomFace(face))
                        {
                            _bottomFace = face;
                            Logger.WriteLine("Нижняя грань крепления болта - " + face);
                            _tunnelSideFaces = GetSideFaces(_bottomFace);
                            bottomIsSet = true;
                        }
                    }
                    break;
            }
            if (tunnelIsSet && bottomIsSet)
            {
                break;
            }
        }
    }

    void MoveBolt()
    {
        Point3d tunnelProectionPoint = new Point3d();
        Edge[] edges = _tunnelFixtureFace.GetEdges();
        foreach (Edge edge in edges)
        {
            Point3d tmp, point3D;
            edge.GetVertices(out tmp, out point3D);
            tunnelProectionPoint = point3D;
        }
        Vector vec = new Vector(tunnelProectionPoint, _slot.SlotPoint);
        MoveByDirection(_fixture, vec);
    }

    static Body SetBody(Component component)
    {
        Body bb = null;
        BodyCollection bc = ((Part)component.Prototype).Bodies;
        foreach (Body body in bc)
        {
            NXObject tmpNxObject = component.FindOccurrence(body);
            if (tmpNxObject != null)
            {
                bb = (Body)tmpNxObject;
            }
        }

        return bb;
    }

    static bool IsBottomFace(Face face)
    {
        Edge[] edges = face.GetEdges();

        for (int i = 0; i < edges.Length; i++)
        {
            if (Config.Round(edges[i].GetLength()) != FixtureConfig.Width) continue;

            for (int j = 0; j < edges.Length; j++)
            {
                if (Config.Round(edges[j].GetLength()) == FixtureConfig.Length)
                {
                    return true;
                }
            }
            return false;
        }

        return false;
    }

    static Face[] GetSideFaces(Face bottomFace)
    {
        int i = 0;
        Face[] sideFaces = new Face[2];

        Edge[] edges = bottomFace.GetEdges();
        foreach (Edge edge in edges)
        {
            if (Config.Round(edge.GetLength()) != FixtureConfig.Length) continue;

            Face[] faces = edge.GetFaces();
            foreach (Face face in faces)
            {
                if (face == bottomFace) continue;

                sideFaces[i++] = face;
            }
        }
        return sideFaces;
    }

    static void MoveByDirection(Component comp, Vector vec)
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

    void Touch()
    {
        _tunnelConstsr = new TunnelConstraint(_tunnel, _slot);

        Config.FreezeDisplay();
        _tunnelConstsr.SetTouchFaceConstraint();
        Config.UnFreezeDisplay();
    }

    void Delete(bool isFixed)
    {
        _fixFixture.Delete();
        if (isFixed)
        {
            _fixConstr.Delete();
        }
    }
}

