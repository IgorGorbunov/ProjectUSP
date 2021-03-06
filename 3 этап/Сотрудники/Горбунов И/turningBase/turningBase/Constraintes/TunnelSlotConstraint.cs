﻿using System;
using System.Collections.Generic;
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
    private Parallel _parallel;
    private Fix _fixConstr;
    private TouchAxe _touchAxe;

    private readonly SingleElement _firstElement;
    private readonly SingleElement _secondElement;
    private SingleElement _fixture;

    private readonly Tunnel _tunnel;
    private readonly Slot _slot;

    private bool _hasFixture;

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
    public TunnelSlotConstraint(SingleElement firstElement, Tunnel tunnel,
                         SingleElement secondElement, Slot slot)
    {
        _tunnel = tunnel;
        _slot = slot;

        _firstElement = firstElement;
        _secondElement = secondElement;


    }
    /// <summary>
    /// Создание связей.
    /// </summary>
    public void Create(bool withBolt)
    {
        bool isFixed = Fix();

        Center();
        //чтобы ровно в то место, что нужно встала
        Parallel();
        _parallel.Delete();
        Touch();

        
        
        if (withBolt)
        {
            try
            {
            _hasFixture = true;
            InsertBolt();

            _touchAxe = new TouchAxe();
            _touchAxe.Create(_firstElement.ElementComponent, _tunnel.TunnelFace,
                             _fixture.ElementComponent, _tunnelFixtureFace);
            }
            catch (EmptyQueryExeption)
            {
                const string mess = "Подходящий болт не найден!";
                Logger.WriteError(mess);
                Message.ShowError(mess);
            }
        }
        
        
        if (Geom.IsEqual(Geom.GetDirection(_tunnel.Slot.BottomFace),
                        (Geom.GetDirection(_slot.BottomFace))))
        {
            _touchAxe.Reverse();
        }

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

        if (_hasFixture)
        {
            fixFixture.Create(_fixture.ElementComponent);
        }
        
        fixFlement.Create(_secondElement.ElementComponent);

        _slotConstr.Reverse();
        Config.TheUfSession.Modl.Update();

        fixFixture.Delete();
        fixFlement.Delete();
        Config.TheUfSession.Modl.Update();
    }

    public void InsertTBolt()
    {
            if (!_hasFixture)
            {
                InsertBolt();

                _touchAxe = new TouchAxe();
                _touchAxe.Create(_firstElement.ElementComponent, _tunnel.TunnelFace,
                                 _fixture.ElementComponent, _tunnelFixtureFace);
                NxFunctions.Update();
                _hasFixture = true;
            }
            else
            {
                DeleteBolt();
                _hasFixture = false;
            }
    }

    void DeleteBolt()
    {
        ComponentConstraint[] componentConstraints = _fixture.ElementComponent.GetConstraints();
        foreach (ComponentConstraint componentConstraint in componentConstraints)
        {
            NxFunctions.Delete(componentConstraint);
        }
        NxFunctions.Delete(_fixture.ElementComponent);
        _fixture = null;
    }

    void InsertBolt()
    {
        double length;
        SetBoltParams(out length);
        UnloadBolt(length);

        bool isFixed1 = _firstElement.ElementComponent.IsFixed;
        bool isFixed2 = _secondElement.ElementComponent.IsFixed;
        FixElems(isFixed1, isFixed2);

        SetBoltConstraints();
        MoveBolt();

        UnfixElems(isFixed1, isFixed2);
    }

    void FixElems(bool isFixed1, bool isFixed2)
    {
        
        if (!isFixed1)
        {
            _firstElement.Fix();
        }
        if (!isFixed2)
        {
            _secondElement.Fix();
        }
    }

    void UnfixElems(bool isFixed1, bool isFixed2)
    {
        if (!isFixed1)
        {
            _firstElement.Unfix();
        }
        if (!isFixed2)
        {
            _secondElement.Unfix();
        }
    }

    void SetBoltConstraints()
    {
        SetFixtureFaces();

        Center center = new Center();
        center.Create22(_fixture.ElementComponent, _tunnelSideFaces[0], _tunnelSideFaces[1],
                      _secondElement.ElementComponent, _slot.SideFace1, _slot.SideFace2);

        Touch touch = new Touch();
        touch.Create(_fixture.ElementComponent, _bottomFace,
                     _secondElement.ElementComponent, _slot.BottomFace);
    }

    void SetBoltParams(out double len)
    {
        Point3d topPoint = GetTopPoint();
        KeyValuePair<Face, double>[] faces = _tunnel.Slot.ParallelFaces;

        double maxLen = double.MinValue;
        foreach (KeyValuePair<Face, double> pair in faces)
        {
            Surface surface = new Surface(pair.Key);
            Point3d projection = surface.GetProection(topPoint);
            Vector vector = new Vector(topPoint, projection);

            double length = vector.Length;
            if (length >= maxLen)
            {
                maxLen = length;
            }
        }
        len = maxLen;
    }

    Point3d GetTopPoint()
    {
        Edge[] sigeEdges = _slot.SideFace1.GetEdges();
        foreach (Edge sigeEdge in sigeEdges)
        {
            Face[] faces = sigeEdge.GetFaces();
            foreach (Face face in faces)
            {
                double[] dir = Geom.GetDirection(face);

                if (face.Name == Config.SlotBottomName ||
                    !Geom.DirectionsAreOnStraight(dir, _slot.BottomDirection)) continue;

                Point3d tmpPoint, topPoint;
                sigeEdge.GetVertices(out topPoint, out tmpPoint);
                return topPoint;
            }
        }
        return new Point3d();
    }

    void UnloadBolt(double setLen)
    {
        double requiredLen = setLen + Catalog.SlotBoltLendthTolerance;
        Dictionary<string, string> dictionary =
            SqlUspElement.GetTitleMinLengthFixtures(requiredLen, _secondElement.UspCatalog);

        if (dictionary.Count <= 0)
        {
            throw new EmptyQueryExeption();
        }
        string title = "";
        int minLen = int.MaxValue;
        foreach (KeyValuePair<string, string> keyValuePair in dictionary)
        {
            int len = Int32.Parse(keyValuePair.Value);
            if (len >= minLen || len < requiredLen) continue;
            title = keyValuePair.Key;
            minLen = len;
        }

        Katalog2005.Algorithm.SpecialFunctions.LoadPart(title, false);
        _fixture = new SingleElement(Katalog2005.Algorithm.SpecialFunctions.LoadedPart);
    }

    void SetFixtureFaces()
    {
        bool bottomIsSet = false, tunnelIsSet = false;

        Body body = SetBody(_fixture.ElementComponent);

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
        MoveByDirection(_fixture.ElementComponent, vec);
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

    bool IsBottomFace(Face face)
    {
        Edge[] edges = face.GetEdges();
        //4 стороны - 4 ребра
        if (edges.Length != 4)
        {
            return false;
        }

        for (int i = 0; i < edges.Length; i++)
        {
            if (Config.Round(edges[i].GetLength()) != _secondElement.UspCatalog.SlotBoltWidth) continue;
            
            for (int j = 0; j < edges.Length; j++)
            {
                if (Config.Round(edges[j].GetLength()) == _secondElement.UspCatalog.SlotBoltLength)
                {
                    return true;
                }
            }
            return false;
        }
        return false;
    }

    Face[] GetSideFaces(Face bottomFace)
    {
        int i = 0;
        Face[] sideFaces = new Face[2];

        Edge[] edges = bottomFace.GetEdges();
        foreach (Edge edge in edges)
        {
            if (Config.Round(edge.GetLength()) != _secondElement.UspCatalog.SlotBoltLength) continue;

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
        Vector3d translation1 = vec.GetCoords2();
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
        _parallel = new Parallel();
        _parallel.Create(_firstElement.ElementComponent, _tunnel.Slot.BottomFace,
                         _secondElement.ElementComponent, _slot.BottomFace);
    }

    void Touch()
    {
        _tunnelConstsr = new TunnelConstraint(_tunnel, _slot);

#if(!DEBUG)
        NxFunctions.FreezeDisplay();
#endif
        _tunnelConstsr.SetTouchFaceConstraint(false);
#if(!DEBUG)
        NxFunctions.UnFreezeDisplay();
#endif
    }

    void Delete(bool isFixed)
    {
        if (isFixed)
        {
            _fixConstr.Delete();
        }
    }
}

