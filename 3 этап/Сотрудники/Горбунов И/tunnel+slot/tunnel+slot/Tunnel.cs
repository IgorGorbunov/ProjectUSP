using System;
using System.Collections.Generic;
using System.Text;
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
            return this.element.ElementComponent;
        }
    }
    /// <summary>
    /// Возвращает тело элемента УСП с данным базовым отверстием.
    /// </summary>
    public Body Body
    {
        get
        {
            return this.element.Body;
        }
    }
    /// <summary>
    /// Возвращает грань отверстия.
    /// </summary>
    public Face TunnelFace
    {
        get
        {
            return face;
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
            this.setDirectionAndPoint();
            return this.direction;
        }
    }
    /// <summary>
    /// Возвращает "центральную точку" базового отверстия.
    /// </summary>
    public Point3d CentralPoint
    {
        get
        {
            return this.point;
        }
    }

    Face face;
    Face[] normalFaces = new Face[2];
    KeyValuePair<Face, double>[] ortFacePairs = null;
    UspElement element;

    double radius1, radius2;
    double[] direction = new double[3];
    Point3d point;
    int rev;

    Slot slot;

    /// <summary>
    /// Инициализирует новый экземпляр класса отверстия для базирования для данной грани 
    /// данного элемента УСП.
    /// </summary>
    /// <param name="face">Грань для базирования.</param>
    /// <param name="element">Элемент УСП для данной грани.</param>
    public Tunnel(Face face, UspElement element)
    {
        this.face = face;
        this.element = element;

        this.setNormalFaces();
        this.setDirectionAndPoint();
    }
    /// <summary>
    /// Возвращает пару (Грань-Расстояние)ортогональных базовому отверстию граней с расстоянием до них.
    /// </summary>
    /// <param name="reverse">True, если необходимо изменить направление поиска граней.</param>
    /// <returns></returns>
    public KeyValuePair<Face, double>[] getOrtFacePairs(bool reverse)
    {
        if (this.ortFacePairs == null || reverse)
        {
            this.findOrtFaces(reverse);
        }
        return this.ortFacePairs;
    }
    /// <summary>
    /// Возвращает центр окружности, находящейся по направлению предполагаемой
    /// ВЕРНОЙ нормали базового отверстия.
    /// </summary>
    /// <returns></returns>
    public Point3d getEndRightDirection()
    {
        Edge[] edges = this.face.GetEdges();
        Point3d point1, point2, tempPoint;

        edges[0].GetVertices(out point1, out tempPoint);
        edges[1].GetVertices(out point2, out tempPoint);

        Vector vec1 = new Vector(point1, point2);
        if (Geom.IsEqual(this.Direction, vec1.Direction))
        {
            return point2;
        }
        else
        {
            return point1;
        }
    }
    /// <summary>
    /// Возвращает центр окружности, находящейся противоположно направлению предполагаемой
    /// ВЕРНОЙ нормали базового отверстия.
    /// </summary>
    /// <returns></returns>
    public Point3d getBeginRightDirection()
    {
        Edge[] edges = this.face.GetEdges();
        Point3d point1, point2, tempPoint;

        edges[0].GetVertices(out point1, out tempPoint);
        edges[1].GetVertices(out point2, out tempPoint);

        Vector vec1 = new Vector(point1, point2);
        if (Geom.IsEqual(this.Direction, vec1.Direction))
        {
            return point1;
        }
        else
        {
            return point2;
        }
    }

    public void setSlot(Slot slot)
    {
        this.slot = slot;
    }

    //refactor
    void findOrtFaces(bool reverse)
    {
        double[] direction1;
        if (reverse)
        {
            direction1 = this.reverseDirection();
        }
        else
        {
            this.rev = 1;
            direction1 = this.slot.BottomDirection;
        }
        
        Point3d point = this.CentralPoint;

        Dictionary<Face, double> dictFaces = new Dictionary<Face, double>();

        Face[] faces = this.Body.GetFaces();
        foreach (Face f in faces)
        {
            double[] direction2 = Geom.GetDirection(f);

            if (Geom.IsEqual(direction1, direction2) && f.SolidFaceType == Face.FaceType.Planar)
            {
                Platan pl = new Platan(f);

                //точка находится "под" необходимыми гранями
                double distance = - pl.GetDistanceToPoint(point);

                if (distance >= 0 && !dictFaces.ContainsValue(Config.Round(distance)))
                {
                    dictFaces.Add(f, Config.Round(distance));
                }
            }  
        }

        this.setOrtFaces(dictFaces);
    }

    void setOrtFaces(Dictionary<Face, double> dictFaces)
    {
        this.ortFacePairs = new KeyValuePair<Face, double>[dictFaces.Count];
        int i = 0;
        foreach (KeyValuePair<Face, double> pair in dictFaces)
        {
            this.ortFacePairs[i] = pair;
            i++;
        }

        //TODO проверка на пустоту массива
        Instr.QSortPair(this.ortFacePairs, 0, this.ortFacePairs.Length - 1);
    }

    void setNormalFaces()
    {
        Edge[] edges = face.GetEdges();

        radius1 = edges[0].GetLength() / (2 * Math.PI);
        radius2 = edges[1].GetLength() / (2 * Math.PI);

        for (int i = 0; i < edges.Length; i++)
		{
            Face[] faces = edges[i].GetFaces();
            foreach (Face f in faces)
            {
                if (f != this.face)
                {
                    normalFaces[i] = f;
                    break;
                }
            }
		}
    }

    void setDirectionAndPoint()
    {
        int voidInt;
        double voidDouble;
        double[] box = new double[6];
        double[] voidPoint = new double[3];

        Config.TheUfSession.Modl.AskFaceData(this.face.Tag, out voidInt, voidPoint, this.direction, box, out voidDouble, out voidDouble, out voidInt);

        Edge[] edges = face.GetEdges();
        Point3d point1, point2;
        edges[0].GetVertices(out point1, out point2);
        this.point = point1;
    }

    double[] reverseDirection()
    {
        double[] dir = this.Direction;
        this.rev *= -1;

        for (int i = 0; i < dir.Length; i++)
        {
            dir[i] = this.rev * dir[i];
        }

        return dir;
    }
}

