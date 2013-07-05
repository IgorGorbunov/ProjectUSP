using System;
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
    private double[] Direction
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

    
    KeyValuePair<Face, double>[] _ortFacePairs;
    
    Point3d _point;
    int _rev;

    Slot _slot;

    Face _face;
    readonly Face[] _normalFaces = new Face[2];
    readonly UspElement _element;

    readonly double[] _direction = new double[3];

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

        SetNormalFaces();
        SetDirectionAndPoint();
    }
    /// <summary>
    /// Возвращает пару (Грань-Расстояние)ортогональных базовому отверстию граней с расстоянием до них.
    /// </summary>
    /// <param name="reverse">True, если необходимо изменить направление поиска граней.</param>
    /// <returns></returns>
    public KeyValuePair<Face, double>[] GetOrtFacePairs(bool reverse)
    {
        if (_ortFacePairs == null || reverse)
        {
            FindOrtFaces(reverse);
        }
        return _ortFacePairs;
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
        Point3d point3D1, tmpPoint;
        edges1[0].GetVertices(out point3D1, out tmpPoint);

        Face[] faces = _element.Body.GetFaces();
        foreach (Face face in faces)
        {
            Edge[] edges2 = face.GetEdges();
            foreach (Edge edge2 in edges2)
            {
                Point3d point3D2;
                edge2.GetVertices(out point3D2, out tmpPoint);

                Vector vec = new Vector(point3D1, point3D2);
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
    //refactor
    void FindOrtFaces(bool reverse)
    {
        double[] direction1;
        if (reverse)
        {
            direction1 = ReverseDirection();
        }
        else
        {
            _rev = 1;
            direction1 = _slot.BottomDirection;
        }
        
        Point3d point = CentralPoint;

        Dictionary<Face, double> dictFaces = new Dictionary<Face, double>();

        Face[] faces = Body.GetFaces();
        foreach (Face f in faces)
        {
            double[] direction2 = Geom.GetDirection(f);

            if (Geom.IsEqual(direction1, direction2) && f.SolidFaceType == Face.FaceType.Planar)
            {
                Platan pl = new Platan(f);

                //точка находится "под" необходимыми гранями
                //округление для проверки нуля - added
                double distance = - Config.Round(pl.GetDistanceToPoint(point));

                if (distance >= 0 && !dictFaces.ContainsValue(distance))
                {
                    dictFaces.Add(f, distance);
                }
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

        string logMess = "Паралельные грани для НГП " + ParentComponent.ToString() + " " + 
            ParentComponent.Name + " c расстоянием до неё:";
        foreach (KeyValuePair<Face, double> keyValuePair in _ortFacePairs)
        {
            logMess += Environment.NewLine + keyValuePair.Key.ToString() + " - " + 
                keyValuePair.Value.ToString() + " мм";
        }
        logMess += Environment.NewLine + "=============";
        Log.WriteLine(logMess);
    }

    void SetNormalFaces()
    {
        Edge[] edges = _face.GetEdges();

        for (int i = 0; i < edges.Length; i++)
		{
            Face[] faces = edges[i].GetFaces();
            foreach (Face f in faces)
            {
                if (f == _face) continue;
                _normalFaces[i] = f;
                break;
            }
		}
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

    double[] ReverseDirection()
    {
        double[] dir = Direction;
        _rev *= -1;

        for (int i = 0; i < dir.Length; i++)
        {
            dir[i] = _rev * dir[i];
        }

        return dir;
    }
}

