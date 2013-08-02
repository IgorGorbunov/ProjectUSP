using System.Collections.Generic;
using NXOpen;
using NXOpen.Assemblies;

/// <summary>
/// Класс содержащий отверстие для базирования.
/// </summary>
public class Tunnel
{
    /// <summary>
    /// Возвращает компонент отверстия.
    /// </summary>
    public Component ParentComponent
    {
        get
        {
            return _element.ElementComponent;
        }
    }
    /// <summary>
    /// Возвращает тело элемента УСП с данным базовым отверстием.
    /// </summary>
    public Body Body
    {
        get
        {
            return _element.Body;
        }
    }
    /// <summary>
    /// Возвращает грань отверстия.
    /// </summary>
    public Face TunnelFace
    {
        get
        {
            return _face;
        }
    }
    /// <summary>
    /// Возвращает направление базового отверстия.
    /// </summary>
    public double[] Direction
    {
        get
        {
            //считаем каждый раз заново, ибо может поменяться
            SetDirectionAndPoint();
            return _direction;
        }
    }
    /// <summary>
    /// Возвращает "центральную точку" базового отверстия.
    /// </summary>
    public Point3d CentralPoint
    {
        get
        {
            SetDirectionAndPoint();
            return _point;
        }
    }
    /// <summary>
    /// Возвращает паз для данного базового отверстия.
    /// </summary>
    public Slot Slot
    {
        get { return _slot; }
    }
    /// <summary>
    /// Возвращает диаметр отверстия.
    /// </summary>
    public double Diametr
    {
        get { return _diametr; }
    }
    /// <summary>
    /// Возвращает пару (Грань-Расстояние)ортогональных базовому отверстию граней с расстоянием до них.
    /// </summary>
    /// <returns></returns>
    public KeyValuePair<Face, double>[] GetOrtFacePairs()
    {
        return _slot.OrtFaces;
    }

    
    KeyValuePair<Face, double>[] _ortFacePairs;
    
    Point3d _point;
    int _rev;

    Slot _slot;

    Face _face;
    readonly UspElement _element;

    readonly double[] _direction = new double[3];

    private double _diametr;

    /// <summary>
    /// Инициализирует новый экземпляр класса отверстия для базирования для данной грани 
    /// данного элемента УСП.
    /// </summary>
    /// <param name="face">Грань для базирования.</param>
    /// <param name="element">Элемент УСП для данной грани.</param>
    public Tunnel(Face face, UspElement element)
    {
        _face = face;
        _element = element;

        SetOccurenceFace();

        //SetDirectionAndPoint();
    }
    
    /// <summary>
    /// Возвращает центр окружности, находящейся по направлению предполагаемой
    /// ВЕРНОЙ нормали базового отверстия.
    /// </summary>
    /// <returns></returns>
    public Point3d GetEndRightDirection()
    {
        Edge[] edges = _face.GetEdges();
        Point3d point1, point2, tempPoint;

        edges[0].GetVertices(out point1, out tempPoint);
        edges[1].GetVertices(out point2, out tempPoint);

        Vector vec1 = new Vector(point1, point2);
        return Geom.IsEqual(Direction, vec1.Direction) ? point2 : point1;
    }
    /// <summary>
    /// Возвращает центр окружности, находящейся противоположно направлению предполагаемой
    /// ВЕРНОЙ нормали базового отверстия.
    /// </summary>
    /// <returns></returns>
    public Point3d GetBeginRightDirection()
    {
        Edge[] edges = _face.GetEdges();
        Point3d point1, point2, tempPoint;

        edges[0].GetVertices(out point1, out tempPoint);
        edges[1].GetVertices(out point2, out tempPoint);

        Vector vec1 = new Vector(point1, point2);
        return Geom.IsEqual(Direction, vec1.Direction) ? point1 : point2;
    }
    /// <summary>
    /// Прявязка базового отверстия к пазу.
    /// </summary>
    /// <param name="slot">Паз.</param>
    public void SetSlot(Slot slot)
    {
        _slot = slot;
    }

    void SetOccurenceFace()
    {
        Edge[] edges1 = _face.GetEdges();
        _diametr = Geom.GetDiametr(edges1[0]);

        Point3d point1, tmpPoint;
        edges1[0].GetVertices(out point1, out tmpPoint);

        Face[] faces = _element.Body.GetFaces();
        foreach (Face face in faces)
        {
            Edge[] edges2 = face.GetEdges();
            foreach (Edge edge2 in edges2)
            {
                Point3d point2;
                edge2.GetVertices(out point2, out tmpPoint);

                Vector vec = new Vector(point1, point2);
                if (Config.Round(vec.Length) == 0.0 &&
                    face.SolidFaceType == Face.FaceType.Cylindrical)
                {
                    _face = face;
                    goto End;
                }
            }
        }
        End:{}
    }

    void SetDirectionAndPoint()
    {
        int voidInt;
        double voidDouble;
        double[] box = new double[6];
        double[] voidPoint = new double[3];

        Config.TheUfSession.Modl.AskFaceData(_face.Tag, out voidInt, voidPoint, _direction, 
            box, out voidDouble, out voidDouble, out voidInt);

        //попробую так
        //Edge[] edges = _face.GetEdges();
        //Point3d point1, point2;
        //edges[0].GetVertices(out point1, out point2);
        _point = new Point3d(voidPoint[0], voidPoint[1], voidPoint[2]);
    }
}

