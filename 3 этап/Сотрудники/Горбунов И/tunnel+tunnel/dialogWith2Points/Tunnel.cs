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
            return new Point3d(point[0], point[1], point[2]);
        }
    }
    /// <summary>
    /// Возвращает пару (Грань-Расстояние)ортогональных базовому отверстию граней с расстоянием до них.
    /// </summary>
    public KeyValuePair<Face, double>[] OrtFacePairs
    {
        get
        {
            if (this.ortFacePairs == null)
            {
                this.findOrtFaces();
            }
            return this.ortFacePairs;
        }
    }


    Face face;
    Face[] normalFaces = new Face[2];
    KeyValuePair<Face, double>[] ortFacePairs = null;
    UspElement element;

    double radius1, radius2;
    double[] direction = new double[3];
    double[] point = new double[3];

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

    public void findOrtFaces()
    {
        Face[] faces = this.Body.GetFaces();
        double[] direction1 = this.Direction;
        Point3d point = this.CentralPoint;

        Dictionary<Face, double> dictFaces = new Dictionary<Face, double>();
 
        foreach (Face f in faces)
        {
            double[] direction2 = Geom.getDirection(f);

            if (Geom.isEqual(direction1, direction2) && f.SolidFaceType == Face.FaceType.Planar)
            {
                Platan pl = new Platan(f);

                //точка находится "под" необходимыми гранями
                double distance = - pl.getDistanceToPoint(point);

                if (distance >= 0)
                {
                    dictFaces.Add(f, distance);
                }
            }  
        }

        this.ortFacePairs = new KeyValuePair<Face, double>[dictFaces.Count];
        int i = 0;
        foreach (KeyValuePair<Face, double> pair in dictFaces)
        {
            this.ortFacePairs[i] = pair;
            i++;
        }

        Instr.qSortPair(this.ortFacePairs, 0, this.ortFacePairs.Length - 1);
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

        Config.theUFSession.Modl.AskFaceData(this.face.Tag, out voidInt, this.point, this.direction, box, out voidDouble, out voidDouble, out voidInt);
    }
}

