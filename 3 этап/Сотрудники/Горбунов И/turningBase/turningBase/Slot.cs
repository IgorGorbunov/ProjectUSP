﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
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
                FindOrtFaces(false);
            }
            return _ortFacePairs;
        }
    }
    /// <summary>
    /// Возвращает набор граней и расстояний до них от ВГП, параллельных пазу.
    /// </summary>
    public KeyValuePair<Face, double>[] ParallelFaces
    {
        get
        {
            if (_parallelFacePairs == null)
            {
                FindOrtFaces(true);
            }
            return _parallelFacePairs;
        }
    }
    /// <summary>
    /// Возвращает направление НГП.
    /// </summary>
    public double[] BottomDirection
    {
        get { return _bottomDirection ?? (_bottomDirection = Geom.GetDirection(BottomFace)); }
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
    /// <summary>
    /// Возвращает серединную точку паза.
    /// </summary>
    public Point3d CenterPoint
    {
        get
        {
            if (Geom.IsEqual(_centerPoint, new Point3d(0.0, 0.0, 0.0)))
            {
                SetCenterPoint();
            }
            return _centerPoint;
        }
    }
    /// <summary>
    /// Возвращает тот набор пазов, которому принадлежит паз.
    /// </summary>
    public SlotSet SlotSet
    {
        get { return _slotSet; }
    }
    /// <summary>
    /// Возвращает тип паза.
    /// </summary>
    public Config.SlotType Type
    {
        get { return _type; }
    }
    /// <summary>
    /// Первое рёберо на НГП.
    /// </summary>
    public Edge EdgeLong1
    {
        get { return _edge1; }
    }
    /// <summary>
    /// Второе рёберо на НГП.
    /// </summary>
    public Edge EdgeLong2
    {
        get { return _edge2; }
    }
    /// <summary>
    /// Возвращает направление паза.
    /// </summary>
    public double[] Direction1
    {
        get
        {
            Vector vector = new Vector(EdgeLong1);
            return vector.Direction2;
        }
    }
    /// <summary>
    /// Возвращает направление паза.
    /// </summary>
    public Vector Direction2
    {
        get
        {
            Vector vector = new Vector(EdgeLong1);
            return vector;
        }
    }

    readonly Config.SlotType _type;

    Edge _touchEdge;

    double[] _bottomDirection;
    Point3d _slotPoint = new Point3d(0.0, 0.0, 0.0);
    Point3d _centerPoint = new Point3d(0.0, 0.0, 0.0);
    KeyValuePair<Face, double>[] _ortFacePairs, _parallelFacePairs;

    readonly SlotSet _slotSet;

    readonly Face _sideFace1;
    readonly Face _sideFace2;

    readonly Edge _edge1;
    readonly Edge _edge2;

    readonly List<Edge> _touchEdges = new List<Edge>();

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
        _edge1 = edgeLong1;
        _edge2 = edgeLong2;
        _type = type;

        _sideFace1 = GetNotBottomFace(edgeLong1);
        _sideFace2 = GetNotBottomFace(edgeLong2);
    }


    /// <summary>
    /// Возвращает грань, перпендикулярную направлению паза.
    /// </summary>
    /// <returns></returns>
    public Face GetOrtDirectionFace()
    {
        Vector edgeVector = new Vector(EdgeLong1);
        Edge[] edges = _slotSet.BottomFace.GetEdges();
        foreach (Edge edge in edges)
        {
            if (Config.Round(edge.GetLength()) != SlotSet.SingleElement.UspCatalog.SlotWidthB &&
                Config.Round(edge.GetLength()) != SlotSet.SingleElement.UspCatalog.PSlotWidth) continue;
            Face[] faces = edge.GetFaces();
            foreach (Face face in faces)
            {
                if (face == _slotSet.BottomFace) continue;

                if (!Geom.IsEqual(Geom.GetDirection(face), edgeVector.Direction1)) continue;

                return face;
            }
        }

        Logger.WriteLine("Не найдена плоскость, нормальная по направлению паза для детали " + 
                        ParentComponent);
        return null;
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

    public override bool Equals(Object obj)
    {
        if (obj.GetType() != GetType()) return false;
        return Equals((Slot)obj);
    }

    public bool Equals(Slot p)
    {
        return this == p;
    }

    public override int GetHashCode()
    {
        int tag1 = int.Parse(_sideFace1.Tag.ToString());
        int tag2 = int.Parse(_sideFace2.Tag.ToString());
        return tag1 ^ tag2;
    }
    
    public static bool operator ==(Slot a, Slot b)
    {
        // If both are null, or both are same instance, return true.
        if (ReferenceEquals(a, b))
        {
            return true;
        }

        // If one is null, but not both, return false.
        if (((object)a == null) || ((object)b == null))
        {
            return false;
        }

        // Return true if the fields match:
        bool equal = false;
        if (a._sideFace1.Tag == b._sideFace1.Tag)
        {
            if (a._sideFace2.Tag == b._sideFace2.Tag)
            {
                equal = true;
            }
        }
        else
        {
            if (a._sideFace1.Tag == b._sideFace2.Tag)
            {
                if (a._sideFace2.Tag == b._sideFace1.Tag)
                {
                    equal = true;
                }
            }
        }
        return equal;
    }

    public static bool operator !=(Slot a, Slot b)
    {
        return !(a == b);
    }

    


    //refactor
    void FindOrtFaces(bool all)
    {
        //FindTopFace();
        
        double[] direction1 = Geom.GetDirection(BottomFace);

        Edge[] pointEdges = BottomFace.GetEdges();
        Edge pointEdge = pointEdges[0];
        Point3d point, tempPoint;
        pointEdge.GetVertices(out point, out tempPoint);

        Dictionary<Face, double> dictFaces = new Dictionary<Face, double>();

        Face[] faces = SlotSet.Body.GetFaces();
        foreach (Face f in faces)
        {
            double[] direction2 = Geom.GetDirection(f);

            bool dirsAreGood = all
                                   ? Geom.DirectionsAreOnStraight(direction1, direction2)
                                   : Geom.IsEqual(direction1, direction2);

            if (f.SolidFaceType != Face.FaceType.Planar ||
                !dirsAreGood) continue;

            Surface pl = new Surface(f);

            //округление для проверки нуля - added
            double distance = - Config.Round(pl.GetDistance(point));
            
            if (distance >= 0 && !dictFaces.ContainsValue(distance))
            {
                dictFaces.Add(f, distance);
            }
        }

        SetOrtFaces(dictFaces, all);
    }

    void SetOrtFaces(Dictionary<Face, double> dictFaces, bool all)
    {
        KeyValuePair<Face, double>[] pairs = new KeyValuePair<Face, double>[dictFaces.Count];
        int i = 0;
        foreach (KeyValuePair<Face, double> pair in dictFaces)
        {
            pairs[i] = pair;
            i++;
        }

        if (pairs.Length > 1)
        {
            Instr.QSortPairs(pairs, 0, pairs.Length - 1);            
        }

        string logMess = "Паралельные грани для НГП " + ParentComponent + " " +
            ParentComponent.Name + " c расстоянием до неё:";
        foreach (KeyValuePair<Face, double> keyValuePair in pairs)
        {
            logMess += Environment.NewLine + keyValuePair.Key + " - " + keyValuePair.Value + " мм";
        }
        logMess += Environment.NewLine + "=============";
        Logger.WriteLine(logMess);

        if (all)
        {
            _parallelFacePairs = pairs;
        }
        else
        {
            _ortFacePairs = pairs;
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
        _sideFace1.Highlight();
        _sideFace2.Highlight();
    }
    public void Unhighlight()
    {
        if (_sideFace1 == null || _sideFace2 == null) return;
        _sideFace1.Unhighlight();
        _sideFace2.Unhighlight();
    }

    Face GetNotBottomFace(Edge slotEdge)
    {
        Face[] faces = slotEdge.GetFaces();
        foreach (Face f in faces)
        {
            if (f == BottomFace) 
                continue;
            //проверка на скругление
            //if (f.SolidFaceType != Face.FaceType.Planar)
            //{
            //    Edge[] edges = f.GetEdges();
            //    foreach (Edge edge in edges)
            //    {
            //        if (edge.SolidEdgeType != Edge.EdgeType.Linear)
            //            continue;

            //        if (edge == slotEdge)
            //            continue;

            //        Face[] faces1 = edge.GetFaces();
            //        foreach (Face face in faces1)
            //        {
            //            if (face != f)
            //            {
            //                return face;
            //            }
            //        }
            //    }
            //}
            return f;
        }

        return null;
    }

    private void SetSlotPoint()
    {
        Point3d slotSetPoint = _slotSet.SelectPoint;
        Straight straight1 = new Straight(EdgeLong1);
        Straight straight2 = new Straight(EdgeLong2);
        Point3d intersection1 = Geom.GetIntersectionPointStraight(slotSetPoint, straight1);
        Point3d intersection2 = Geom.GetIntersectionPointStraight(slotSetPoint, straight2);

        _slotPoint = new Point3d((intersection1.X + intersection2.X)/2,
                                 (intersection1.Y + intersection2.Y)/2,
                                 (intersection1.Z + intersection2.Z)/2);
    }

    private void SetCenterPoint()
    {
        Point3d point1, point2, point3, point4;
        EdgeLong1.GetVertices(out point1, out point2);
        EdgeLong2.GetVertices(out point3, out point4);

        Vector[] diagonals = new Vector[4];
        diagonals[0] = new Vector(point1, point3);
        diagonals[1] = new Vector(point1, point4);
        diagonals[2] = new Vector(point2, point3);
        diagonals[3] = new Vector(point2, point4);

        double maxLength = double.MinValue;
        Vector maxDiagonal = null;
        foreach (Vector diagonal in diagonals)
        {
            if (diagonal.Length <= maxLength)
                continue;
            maxDiagonal = diagonal;
            maxLength = diagonal.Length;
        }

        Debug.Assert(maxDiagonal != null, "maxDiagonal != null");
        _centerPoint = maxDiagonal.Center;
    }

}

