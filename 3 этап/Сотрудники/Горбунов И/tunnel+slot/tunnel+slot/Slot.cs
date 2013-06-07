using System.Collections.Generic;
using NXOpen;
using NXOpen.Assemblies;

/// <summary>
/// Класс Т-образного и П-образного паза.
/// </summary>
public class Slot
{
    public Component ParentComponent
    {
        get
        {
            return _slotSet.ParentComponent;
        }
    }

    public Face BottomFace
    {
        get
        {
            return _slotSet.BottomFace;
        }
    }
    public Face SideFace1
    {
        get
        {
            return _sideFace1;
        }
    }
    public Face SideFace2
    {
        get
        {
            return _sideFace2;
        }
    }
    public Face TouchFace
    {
        get
        {
            return _touchFace;
        }
    }
    public Face TopFace
    {
        get
        {
            return _topFace;
        }
    }
    /// <summary>
    /// Возвращает тело элемента, на котором расположен паз.
    /// </summary>
    public Body Body
    {
        get
        {
            return _slotSet.Body;
        }
    }
    /// <summary>
    /// Возвращает набор граней и расстояний до них от ВГП, параллельных пазу и находящихся выше него.
    /// </summary>
    public KeyValuePair<Face, double>[] OrtFaces
    {
        get
        {
            if (_ortFacePairs == null)
            {
                FindOrtFaces();
            }
            return _ortFacePairs;
        }
    }
    /// <summary>
    /// Возвращает направление НГП.
    /// </summary>
    public double[] BottomDirection
    {
        get
        {
            return _bottomDirection;
        }
    }
    /// <summary>
    /// Возвращает спроецированную точку паза.
    /// </summary>
    public Point3d SlotPoint
    {
        get
        {
            SetSlotPoint();
            return _slotPoint;
        }
    }

    public readonly Edge EdgeLong1;
    private readonly Edge _edgeLong2;

    Config.SlotType _type;

    readonly SlotSet _slotSet;

    readonly Face _sideFace1;
    readonly Face _sideFace2;
    Face _touchFace;
    Face _topFace;

    
    readonly List<Edge> _touchEdges = new List<Edge>();
    Edge _touchEdge;

    double[] _bottomDirection;

    private Point3d _slotPoint = new Point3d(0.0, 0.0, 0.0);

    KeyValuePair<Face, double>[] _ortFacePairs;
    
    /// <summary>
    /// Инициализирует новый экземпляр класса паза.
    /// </summary>
    /// <param name="slotSet">Набор пазов.</param>
    /// <param name="edgeLong1">Первое боковое ребро паза.</param>
    /// <param name="edgeLong2">Второе боковое ребро паза.</param>
    /// <param name="type">Тип паза.</param>
    public Slot(SlotSet slotSet, Edge edgeLong1, Edge edgeLong2, Config.SlotType type)
    {
        _slotSet = slotSet;
        EdgeLong1 = edgeLong1;
        _edgeLong2 = edgeLong2;
        _type = type;

        _sideFace1 = GetNotBottomFace(edgeLong1);
        _sideFace2 = GetNotBottomFace(edgeLong2);
    }


    //public void setTouchEdge()
    //{
    //    double length;
    //    double min_len = -1.0;
    //    Edge nearestEdge = null;

    //    foreach (Edge e in this.slotSet.TouchEdges)
    //    {
    //        if (Geom.isEdgePointOnStraight(e, this.straight,
    //                                       out length, this.slotSet.SelectPoint))
    //        {
    //            this.touchEdges.Add(e);
    //            if (min_len == -1.0 || length < min_len)
    //            {
    //                min_len = length;
    //                nearestEdge = e;
    //            }
    //        }
    //    }
    //    this.touchEdge = nearestEdge;
    //}
    public void ReverseTouchEdge()
    {
        Edge otherEdge = null;

        foreach (Edge e in _touchEdges)
        {
            if (e == _touchEdge) continue;
            otherEdge = e;
            break;
        }
        _touchEdge = otherEdge;
    }

    public void SetTouchFace()
    {
        foreach (Face f in _touchEdge.GetFaces())
        {
            if (f == BottomFace) continue;
            _touchFace = f;
            break;
        }
    }

    //refactor
    void FindOrtFaces()
    {
        double[] direction1 = _bottomDirection;


        Edge[] pointEdges = _topFace.GetEdges();
        Edge pointEdge = pointEdges[0];
        Point3d point, tempPoint;
        pointEdge.GetVertices(out point, out tempPoint);

        Dictionary<Face, double> dictFaces = new Dictionary<Face, double>();

        Face[] faces = Body.GetFaces();
        foreach (Face f in faces)
        {
            double[] direction2 = Geom.GetDirection(f);

            if (!Geom.IsEqual(direction1, direction2) || f.SolidFaceType != Face.FaceType.Planar) continue;
            Platan pl = new Platan(f);

            //точка находится "под" необходимыми гранями
            double distance = - pl.GetDistanceToPoint(point);
                
            if (distance >= 0 && !dictFaces.ContainsValue(Config.Round(distance)))
            {
                dictFaces.Add(f, Config.Round(distance));
            }
        }

        SetOrtFaces(dictFaces);
    }

    void SetOrtFaces(Dictionary<Face, double> dictFaces)
    {
        _ortFacePairs = new KeyValuePair<Face, double>[dictFaces.Count];
        int i = 0;
        foreach (KeyValuePair<Face, double> pair in dictFaces)
        {
            _ortFacePairs[i] = pair;
            i++;
        }

        if (_ortFacePairs.Length > 1)
        {
            Instr.QSortPair(_ortFacePairs, 0, _ortFacePairs.Length - 1);            
        }
    }

    /*Dictionary<Edge, double> getNearestEdges(Edge[] edges, Point3d from_point)
    {
        Dictionary<Edge, double> Edges = new Dictionary<Edge, double>();

        foreach (Edge Edge in edges)
        {
            if (Edge.SolidEdgeType == Edge.EdgeType.Linear)
            {
                Point3d FirstPoint, SecondPoint;
                Edge.GetVertices(out FirstPoint, out  SecondPoint);

                Vector Vec1 = new Vector(from_point, FirstPoint);
                Vector Vec2 = new Vector(from_point, SecondPoint);

                double len1 = Vec1.getLength();
                double len2 = Vec2.getLength();

                double min_len;
                if (len1 < len2)
                {
                    min_len = len1;
                }
                else
                {
                    min_len = len2;
                }

                addInDictMinValue(Edges, Edge, min_len);

                //theUI.NXMessageBox.Show("", NXMessageBox.DialogType.Error, min_len + " " + len1 + " " + len2);
            }
        }

        return Edges;
    }*/

    public void Highlight()
    {
        EdgeLong1.Highlight();
        _edgeLong2.Highlight();
    }
    public void Unhighlight()
    {
        EdgeLong1.Unhighlight();
        _edgeLong2.Unhighlight();
    }

    Face GetNotBottomFace(Edge slotEdge)
    {
        Face[] faces = slotEdge.GetFaces();
        foreach (Face f in faces)
        {
            if (f != BottomFace)
            {
                return f;
            }
        }

        return null;
    }


    //refactor slots
    public void FindTopFace()
    {
        Face topFace = null;
        Edge topEdge = null;
        _bottomDirection = Geom.GetDirection(BottomFace);
        double[] direction;

        Edge edge = EdgeLong1;
        Face face = _sideFace1;

        if (_type == Config.SlotType.Pslot)
        {
            topEdge = GetNextEdge(face, edge, Config.PSlotHeight);
            topFace = GetNextFace(topEdge, face);
            direction = Geom.GetDirection(topFace);

            if (!Geom.IsEqual(_bottomDirection, direction))
            {
                Config.TheUi.NXMessageBox.Show("Error!", NXMessageBox.DialogType.Error, "Печаль с П-образным пазом!");
            }
        }
        else if (_type == Config.SlotType.Tslot)
        {
            foreach (double slotHeight in Config.SlotHeight1)
            {
                topEdge = GetNextEdge(face, edge, slotHeight);

                if (topEdge != null)
                {
                    break;
                }
            }
            topFace = GetNextFace(topEdge, face);
            edge = topEdge;
            face = topFace;

            topEdge = GetNextEdge(face, edge, Config.StepWidthTSlot1);

            //значит Т-образный паз второго исполнения
            if (topEdge == null)
            {
                
                topEdge = GetNextEdge(face, edge, Config.StepDownWidthTSlot2);
                topFace = GetNextFace(topEdge, face);
                edge = topEdge;
                face = topFace;
                
                foreach (double d in Config.SlotHeight3)
                {
                    topEdge = GetNextEdge(face, edge, d);

                    if (topEdge != null)
                    {
                        break;
                    }
                }
                
                topFace = GetNextFace(topEdge, face);
                edge = topEdge;
                face = topFace;


                topEdge = GetNextEdge(face, edge, Config.StepUpWidthTSlot2);
                topFace = GetNextFace(topEdge, face);
                edge = topEdge;
                face = topFace;

                topEdge = GetNextEdge(face, edge, Config.SlotHeight2);
                _type = Config.SlotType.Tslot2;
            }
            else
            {
                topFace = GetNextFace(topEdge, face);
                edge = topEdge;
                face = topFace;

                foreach (double d in Config.SlotHeight)
                {
                    topEdge = GetNextEdge(face, edge, d);

                    if (topEdge != null)
                    {
                        break;
                    }
                }
                _type = Config.SlotType.Tslot1;
            }

            topFace = GetNextFace(topEdge, face);
        }

        direction = Geom.GetDirection(topFace);

        if (!Geom.IsEqual(_bottomDirection, direction))
        {
            Config.TheUi.NXMessageBox.Show("Error!", NXMessageBox.DialogType.Error, "Печаль с T-образным пазом!");
        }


        _topFace = topFace;
    }


    static Edge GetNextEdge(Face face, Edge edge, double distance)
    {
        //поиск верхнего левого ребра
        Edge resultEdge = null;
        Vector vecEtalon = new Vector(edge);
        foreach (Edge e in face.GetEdges())
        {
            if (e == edge) continue;
            Vector vecTmp = new Vector(e);
            if (!vecEtalon.IsParallel(vecTmp)) continue;
            Straight edgeStraight = new Straight(e);
            Point3d heightStart = vecEtalon.Start;
            Point3d pointOnStraight = Geom.GetIntersectionPointStraight(heightStart, edgeStraight);

            Vector vecHeight = new Vector(heightStart, pointOnStraight);

            if (Config.Round(vecHeight.Length) != distance) continue;
            resultEdge = e;
            break;
        }

        return resultEdge;
    }

    static Face GetNextFace(Edge edge, Face face)
    {
        //поиск верхней горизонтальной поверхности
        Face resultFace = null;
        Face[] faces = edge.GetFaces();
        foreach (Face f in faces)
        {
            if (f != face)
            {
                resultFace = f;
                break;
            }
        }

        if (resultFace == null)
        {
            Config.TheUi.NXMessageBox.Show("Error!", NXMessageBox.DialogType.Error, "Верхняя горизонтальная поверхность не найдена!");
        }

        return resultFace;
    }

    /*bool isTypeTwo(Face face, Edge edge)
    {
        Vector vecEtalon = new Vector(edge);
        Edge[] edges = face.GetEdges();
        foreach (Edge e in edges)
        {
            if (e != edge)
            {
                Vector vecTmp = new Vector(e);

                if (vecEtalon.isParallel(vecTmp))
                {
                    double[,] edgeEquation = Geom.getStraitEquation(e);
                    Point3d heightStart = vecEtalon.start;
                    Point3d pointOnStraight = Geom.getIntersectionPointStraight(heightStart, edgeEquation);
                    Vector vecHeight = new Vector(heightStart, pointOnStraight);


                    if (Config.doub(vecHeight.getLength()) == Config.STEP_DOWN_WIDTH_T_SLOT_2)
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }*/

    void SetSlotPoint()
    {
        Point3d slotSetPoint = _slotSet.SelectPoint;
        Straight straight1 = new Straight(EdgeLong1);
        Straight straight2 = new Straight(_edgeLong2);
        Point3d intersection1 = Geom.GetIntersectionPointStraight(slotSetPoint, straight1);
        Point3d intersection2 = Geom.GetIntersectionPointStraight(slotSetPoint, straight2);

        _slotPoint = new Point3d((intersection1.X + intersection2.X)/2,
                                 (intersection1.Y + intersection2.Y)/2,
                                 (intersection1.Z + intersection2.Z)/2);
    }

}

