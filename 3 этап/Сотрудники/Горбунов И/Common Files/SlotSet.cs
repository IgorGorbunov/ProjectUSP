using System;
using System.Collections.Generic;
using NXOpen;
using NXOpen.Assemblies;

/// <summary>
/// Класс содержащий набор пазов, имеющих одну общую нижнюю плоскость.
/// </summary>
public sealed class SlotSet
{
    /// <summary>
    /// Возвращает тело компонента, на котором расположены пазы.
    /// </summary>
    public Body Body
    {
        get
        {
            return _element.Body;
        }
    }
    /// <summary>
    /// Возвращает компонент, на котором располагается данный набор пазов
    /// </summary>
    public Component ParentComponent
    {
        get
        {
            return _element.ElementComponent;
        }
    }
    /// <summary>
    /// Возвращает точку, по которой выбрался паз.
    /// </summary>
    public Point3d SelectPoint
    {
        get
        {
            return _selectPoint;
        }
    }
    /// <summary>
    /// Возвращает НГП.
    /// </summary>
    public Face BottomFace
    {
        get
        {
            return _bottomFace;
        }
    }

    Face _bottomFace;
    private Edge _someEdgeOnBottom;

    Dictionary<Edge, double> _nearestEdges;

    Point3d _selectPoint;

    readonly UspElement _element;
    
    /// <summary>
    /// Инициализирует новый экземпляр класса для заданного элемента УСП.
    /// </summary>
    /// <param name="element">Элемент УСП, на котором существует набор пазов.</param>
    public SlotSet(UspElement element)
    {
        _element = element;
    }

    /// <summary>
    /// Метод записывает координаты точки.
    /// </summary>
    /// <param name="point">Точка.</param>
    public void SetPoint(Point3d point)
    {
        _selectPoint = point;

        Logger.WriteLine("Координаты точки - " + _selectPoint);
    }

    //REFACTOR
    public bool HaveNearestBottomFace()
    {
        List<Face> nearFaces = new List<Face>();

        bool isFound = false;
        foreach (Face face in _element.SlotFaces)
	    {
            nearFaces.Add(face);
	        isFound = true;
	    }

        if (!isFound)
        {
            return false;
        }

        double minLenAmongFaces = double.MaxValue;

        foreach (Face face in nearFaces)
        {
            Edge[] edges = face.GetEdges();
            foreach (Edge edge in edges)
            {
                if (edge.SolidEdgeType != Edge.EdgeType.Linear) continue;

                Point3d point1, point2;
                edge.GetVertices(out point1, out point2);
                Straight straight = new Straight(edge);

                Point3d projection = Geom.GetIntersectionPointStraight(_selectPoint, straight);
                double length;
                if (Geom.IsOnSegment(projection, edge))
                {
                    Vector vec = new Vector(projection, _selectPoint);
                    length = vec.Length;
                }
                else
                {
                    Vector vec1 = new Vector(_selectPoint, point1);
                    Vector vec2 = new Vector(_selectPoint, point2);

                    length = vec1.Length < vec2.Length ? vec1.Length : vec2.Length;
                }

                if (length > minLenAmongFaces) continue;

                minLenAmongFaces = length;
                _someEdgeOnBottom = edge;
            }
        }

        //_edges = _bottomFace.GetEdges();
        
        return true;
    }

    public void SetNearestEdges()
    {
        Face[] faces = _someEdgeOnBottom.GetFaces();
        Dictionary<Edge, double>[] edgesList = new Dictionary<Edge, double>[2];

        int numberOfNearestSlots = 0;
        bool[] isSlotFace = {false, false};
        for (int i = 0; i < edgesList.Length; i++)
        {
            if (faces[i].Name != Config.SlotBottomName) continue;
            
            isSlotFace[i] = true;
            numberOfNearestSlots++;

            edgesList[i] = new Dictionary<Edge, double>();
            Edge[] edges = faces[i].GetEdges();

            for (int j = 0; j < edges.Length; j++)
            {
                edgesList[i].Add(edges[j], 0.0);
            }
        }

        int start = 0;
        int end = edgesList.Length;
        if (numberOfNearestSlots < 2)
        {
            if (isSlotFace[0])
            {
                start = 0;
                end = 1;
            }
            else
            {
               start = 1;
               end = 2; 
            }
        }

        double[] nProjections = new double[2];
        for (int i = start; i < end; i++)
        {
            nProjections[i] = 0;

            foreach (Edge edge in edgesList[i].Keys)
            {
                if (edge.SolidEdgeType != Edge.EdgeType.Linear) continue;

                Point3d firstPoint, secondPoint;
                edge.GetVertices(out firstPoint, out  secondPoint);
                Straight straight = new Straight(edge);

                Point3d projection = Geom.GetIntersectionPointStraight(_selectPoint, straight);
                double length;
                if (Geom.IsOnSegment(projection, edge))
                {
                    nProjections[i]++;
                    length = (new Vector(projection, _selectPoint)).Length;
                }
                else
                {
                    Vector vec1 = new Vector(_selectPoint, firstPoint);
                    Vector vec2 = new Vector(_selectPoint, secondPoint);

                    length = vec1.Length < vec2.Length ? vec1.Length : vec2.Length;
                }

                AddInDictMinValue(edgesList[i], edge, length);
            }
        }

        //если по одному ребру есть две НГП
        if (end == 2)
        {
            int n = nProjections[0] > nProjections[1] ? 0 : 1;

            Platan pl1 = new Platan(faces[0]);
            Platan pl2 = new Platan(faces[1]);
            double d1 = pl1.GetDistanceToPoint(_selectPoint);
            double d2 = pl2.GetDistanceToPoint(_selectPoint);
            if (Config.Round(d1) == 0.0)
            {
                n = 0;
            }
            if (Config.Round(d2) == 0.0)
            {
                n = 1;
            }
            _bottomFace = faces[n];
            _nearestEdges = edgesList[n];
        }
        else
        {
            _bottomFace = faces[start];
            _nearestEdges = edgesList[start];
        }
    }

    public bool HasNearestSlot(out Slot slot)
    {
        slot = null;
        double minLen = -1;
        double minSlotWidth = 0;
        Edge edge1 = null, edge2 = null;

        List<Edge> edges = new List<Edge>(_nearestEdges.Keys);
        bool isFound = false;
        for (int i = 0; i < edges.Count; i++)
        {
            for (int j = i + 1; j < edges.Count; j++)
            {
                if (edges[i].SolidEdgeType != Edge.EdgeType.Linear ||
                    edges[j].SolidEdgeType != Edge.EdgeType.Linear)
                    continue;

                Vector vec1 = new Vector(edges[i]);
                Vector vec2 = new Vector(edges[j]);

                double slotWidth;
                if (!IsSlot(vec1, vec2, out slotWidth, edges[i], edges[j])) continue;

                Edge edgeLong1 = edges[i];
                Point3d start, end;
                edgeLong1.GetVertices(out start, out end);

                Straight firstLongStraight = new Straight(start, end);
                Point3d intersection1 = 
                    Geom.GetIntersectionPointStraight(_selectPoint, firstLongStraight);
                    

                double len1;
                if (Geom.IsOnSegment(intersection1, edgeLong1))
                {
                    Vector vecN1 = new Vector(_selectPoint, intersection1);
                    len1 = vecN1.Length;
                }
                else
                {
                    Vector vec1S = new Vector(_selectPoint, start);
                    Vector vec1L = new Vector(_selectPoint, end);

                    double lenS = vec1S.Length;
                    double lenL = vec1L.Length;

                    len1 = lenS < lenL ? lenS : lenL;
                }

                Edge edgeLong2 = edges[j];

                edgeLong2.GetVertices(out start, out end);

                Straight secondLongStraight = new Straight(start, end);
                Point3d intersection2 = Geom.GetIntersectionPointStraight(_selectPoint, secondLongStraight);

                double len2;
                if (Geom.IsOnSegment(intersection2, edgeLong2))
                {
                    Vector vecN2 = new Vector(_selectPoint, intersection2);
                    len2 = vecN2.Length;
                }
                else
                {
                    Vector vec2S = new Vector(_selectPoint, start);
                    Vector vec2L = new Vector(_selectPoint, end);

                    double lenS = vec2S.Length;
                    double lenL = vec2L.Length;

                    len2 = lenS < lenL ? lenS : lenL;
                }

                double min = len1 < len2 ? len1 : len2;

                if (minLen == -1 || min < minLen)
                {
                    
                    minLen = min;
                    edge1 = edgeLong1;
                    edge2 = edgeLong2;
                    minSlotWidth = slotWidth;
                        
                    isFound = true;
                }
            }
        }

        if (isFound)
        {
            slot = new Slot(this, edge1, edge2, Config.GetSlotType(minSlotWidth));
            return true;
        }
        return false;
    }


/*
    int checkBottom(Face f, int max, out Edge[] edges, out List<Edge> slot_edges)
    {
        edges = f.GetEdges();
        slot_edges = new List<Edge>();

        int counter = 0;
        foreach (Edge e in edges)
        {
            if (e.GetLength().ToString() == Config.SlotWidth.ToString())
            {
                slot_edges.Add(e);
                counter++;
            }
        }

        if (counter > max)
        {
            return counter;
        }
        else
        {
            return 0;
        }
    }
*/

/*
    List<Edge> getSlotEdges(Face f)
    {
        List<Edge> slotWidthEdges = new List<Edge>();

        foreach (Edge e in _edges)
        {
            if (Config.Round(e.GetLength()) == Config.SlotWidth || Config.Round(e.GetLength()) == Config.PSlotWidth)
            {
                slotWidthEdges.Add(e);
            }
        }

        return slotWidthEdges;
    }
*/

    static void AddInDictMinValue(Dictionary<Edge, double> dictionary, Edge key, double value)
    {
        if (dictionary.ContainsKey(key)) return;

        Edge minKey = null;
        double minValue = value;

        if (dictionary.Count < Config.NumberOfNearestEdges)
        {
            dictionary.Add(key, value);
        }
        else
        {
            foreach (KeyValuePair<Edge, double> p in dictionary)
            {
                if (p.Value <= minValue) continue;
                minKey = p.Key;
                minValue = p.Value;
            }
        }

        if (minKey == null) return;
        dictionary.Remove(minKey);
        dictionary.Add(key, value);
    }

    static bool IsSlot(Vector vec1, Vector vec2, out double distance, Edge e1, Edge e2)
    {
        distance = 0;
        if (vec1.IsParallel(vec2))
        {
            int nPerpendicular = 0;
            const int nVectors = 2;

            Point3d[] points = new Point3d[Config.NPointsInEdge * nVectors];
            points[0] = vec1.Start;
            points[1] = vec1.End;
            points[2] = vec2.Start;
            points[3] = vec2.End;

            for (int i = 0; i < Config.NPointsInEdge; i++)
            {
                for (int j = Config.NPointsInEdge; j < Config.NPointsInEdge * 2; j++)
                {

                    Vector tempVec = new Vector(points[i], points[j]);

                    if (vec1.IsNormal(tempVec))
                    {
                        double length = tempVec.Length;
                        if (Config.Round(length) == Config.PSlotWidth || 
                            Config.Round(length) == Config.SlotWidth)
                        {
                            distance = length;
                            nPerpendicular++;
                        }
                    }
                }
            }

            return nPerpendicular > 0 && HaveNormals(vec1, vec2, e1, e2);
        }
        return false;
    }
    /*bool isPslot(Vector vec1, Vector vec2)
    {

    }*/

    static bool HaveNormals(Vector vec1, Vector vec2, Edge e1, Edge e2)
    {
        //для первого вектора
        Point3d[] points1 = new Point3d[Config.NPointsInEdge];
        points1[0] = vec1.Start;
        points1[1] = vec1.End;
        Straight straight1 = new Straight(vec1);

        //для второго вектора
        Point3d[] points2 = new Point3d[Config.NPointsInEdge];
        points2[0] = vec2.Start;
        points2[1] = vec2.End;
        Straight straight2 = new Straight(vec2);

        int alignment = 0;

        //первое ребро
        for (int i = 0; i < Config.NPointsInEdge; i++)
        {
            bool onPoint = false;
            Point3d intersect1 = Geom.GetIntersectionPointStraight(points1[i], straight2);

            for (int j = 0; j < Config.NPointsInEdge; j++)
            {
                if (Geom.IsEqual(intersect1, points2[j]))
                {
                    alignment++;
                    onPoint = true;
                    break;
                }
            }

            if (!onPoint && Geom.IsOnSegment(intersect1, vec2))
            {
                return true;
            }
        }

        //второе ребро
        for (int i = 0; i < Config.NPointsInEdge; i++)
        {
            bool onPoint = false;
            Point3d intersect1 = Geom.GetIntersectionPointStraight(points2[i], straight1);

            for (int j = 0; j < Config.NPointsInEdge; j++)
            {
                if (!Geom.IsEqual(intersect1, points1[j])) continue;
                alignment++;
                onPoint = true;
                break;
            }

            if (!onPoint && Geom.IsOnSegment(intersect1, vec1))
            {
                return true;
            }
        }

        return alignment > 2;
    }

    //не используется
    /*bool hasCommonEdge(Vector v1, Vector v2, out Edge edge, out int k, out int n)
    {
        Point3d[] points1, points2;
        points1[0] = v1.start;
        points1[1] = v1.end;
        points2[0] = v2.start;
        points2[1] = v2.end;

        foreach (Edge edg in this.bottomFace.GetEdges())
        {
            Point3d start, end;
            edg.GetVertices(start, end);

            for (int i = 0; i < points1.Length; i++)
            {
                for (int j = 0; j < points2.Length; j++)
                {
                    if ((start == points1[i] && end == points2[j]) || (start == points2[j] && end == points1[i]))
                    {
                        edge = edg;
                        k = i;
                        n = j;
                        return true;
                    }
                }
            }
  
        }

        return false;
    }*/
}