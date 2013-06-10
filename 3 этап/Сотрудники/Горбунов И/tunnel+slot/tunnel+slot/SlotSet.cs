using System;
using System.Collections.Generic;
using NXOpen;
using NXOpen.Assemblies;

/// <summary>
/// Класс содержащий набор пазов, имеющих одну общую нижнюю плоскость.
/// </summary>
public class SlotSet
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

    Edge[] _edges;
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

        Log.WriteLine("Координаты точки - " + _selectPoint);
    }

    public bool HaveNearestBottomFace()
    {
        double minLen = double.MaxValue;
        Face nearFace = null;
        bool isFound = false;
        foreach (Face face in _element.SlotFaces)
	    {
            
            Platan platan = new Platan(face);
            double len = Math.Abs(platan.GetDistanceToPoint(_selectPoint));

	        if (Config.Round(len) > minLen) continue;
	        nearFace = face;
	        minLen = len;
	        isFound = true;
	    }

        if (!isFound)
        {
            return false;
        }
        _bottomFace = nearFace;
        _edges = nearFace.GetEdges();
        return true;
    }

    public void SetNearestEdges()
    {
        Dictionary<Edge, double> edges = new Dictionary<Edge, double>();

        foreach (Edge edge in _edges)
        {
            if (edge.SolidEdgeType != Edge.EdgeType.Linear) continue;
            Point3d firstPoint, secondPoint;
            edge.GetVertices(out firstPoint, out  secondPoint);

            Vector vec1 = new Vector(_selectPoint, firstPoint);
            Vector vec2 = new Vector(_selectPoint, secondPoint);

            double len1 = vec1.Length;
            double len2 = vec2.Length;

            double minLen = len1 < len2 ? len1 : len2;

            AddInDictMinValue(edges, edge, minLen);
        }
        _nearestEdges = edges;
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
                Vector vec1 = new Vector(edges[i]);
                Vector vec2 = new Vector(edges[j]);

                double slotWidth;
                if (!IsSlot(vec1, vec2, out slotWidth)) continue;
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

    static bool IsSlot(Vector vec1, Vector vec2, out double distance)
    {
        distance = 0;
        if (vec1.IsParallel(vec2))
        {
            int nPerpendicular = 0;
            const int nPointsInEdge = 2;
            const int nVectors = 2;

            Point3d[] points = new Point3d[nPointsInEdge * nVectors];
            points[0] = vec1.Start;
            points[1] = vec1.End;
            points[2] = vec2.Start;
            points[3] = vec2.End;

            for (int i = 1; i <= nPointsInEdge; i++)
            {
                for (int j = nPointsInEdge + 1; j <= nPointsInEdge * 2; j++)
                {

                    Vector tempVec = new Vector(points[i - 1], points[j - 1]);

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

            return nPerpendicular > 0 && HaveNormals(vec1, vec2);
        }
        return false;
    }
    /*bool isPslot(Vector vec1, Vector vec2)
    {

    }*/

    static bool HaveNormals(Vector vec1, Vector vec2)
    {
        //для первого вектора
        Point3d[] points1 = new Point3d[Config.NPointsInEdge];
        points1[0] = vec1.Start;
        points1[1] = vec1.End;
        //double[,] straight1 = Geom.getStraitEquation(points1[0], points1[1]);
        Straight straight1 = new Straight(vec1);

        //для второго вектора
        Point3d[] points2 = new Point3d[Config.NPointsInEdge];
        points2[0] = vec2.Start;
        points2[1] = vec2.End;
        //double[,] straight2 = Geom.getStraitEquation(points2[0], points2[1]);
        Straight straight2 = new Straight(vec2);

        int alignment = 0;

        //первое ребро
        bool onPoint = false;
        for (int i = 0; i < Config.NPointsInEdge; i++)
        {
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
        onPoint = false;
        for (int i = 0; i < Config.NPointsInEdge; i++)
        {
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