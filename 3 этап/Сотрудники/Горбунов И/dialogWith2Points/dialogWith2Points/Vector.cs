using System;
using System.Collections.Generic;
using NXOpen;
using NXOpen.UF;

/// <summary>
/// Класс математического вектора.
/// </summary>
public class Vector
{
    /// <summary>
    /// Задаёт и возвращает начало вектора.
    /// </summary>
    public Point3d start;
    /// <summary>
    /// Задаёт и возвращает конец вектора.
    /// </summary>
    public Point3d end;
    /// <summary>
    /// Возвращает длину вектора.
    /// </summary>
    public double Length
    {
        get
        {
            if (this.length == -1.0)
	        {
        		 this.length = this.getLength();
	        }
            return this.length;
        }
    }
    /// <summary>
    /// Возвращает направляющие косинусы вектора.
    /// </summary>
    public Point3d Direction
    {
        get
        {
            if (Geom.isEqual(this.direction, new Point3d(0.0, 0.0, 0.0)))
            {
                this.setDirection();
            }

            return this.direction;
        }
    }

    double length = -1.0;
    Point3d direction = new Point3d(0.0, 0.0, 0.0);


    /// <summary>
    /// Инициализирует новый путой экземпляр класса.
    /// </summary>
    public Vector()
        : this(new Point3d(), new Point3d())
    {

    }
    /// <summary>
    /// Инициализирует новый экземпляр класса вектора по координатам начальной и конечной точки.
    /// </summary>
    /// <param name="start">Начальная точка вектора.</param>
    /// <param name="end">Конечная точка вектора.</param>
    public Vector(Point3d start, Point3d end)
    {
        initPoints(start, end);
    }
    /// <summary>
    /// Инициализирует новый экземпляр класса вектора для заданного ребра.
    /// </summary>
    /// <param name="Edg">Ребро элемента.</param>
    public Vector(Edge Edg)
    {
        Point3d start, end;
        Edg.GetVertices(out start, out end);

        initPoints(start, end);
    }

    

    /// <summary>
    /// Возвращает координаты вектора.
    /// </summary>
    /// <returns></returns>
    public Point3d getCoords()
    {
        return new Point3d(end.X - start.X, end.Y - start.Y, end.Z - start.Z);
    }

    /// <summary>
    /// Возвращает угол между текущим вектором и заданным (вторым).
    /// </summary>
    /// <param name="vec">Второй вектор.</param>
    /// <returns></returns>
    public double getAngle(Vector vec)
    {
        UFSession theUFSession = UFSession.GetUFSession();

        double[] Line_Vec1 = new double[3];
        double[] Line_Vec2 = new double[3];
        double angle;
        double[] vec_ccw = new double[3];

        Point3d cord1 = this.getCoords();
        Line_Vec1[0] = cord1.X;
        Line_Vec1[1] = cord1.Y;
        Line_Vec1[2] = cord1.Z;

        Point3d cord2 = vec.getCoords();
        Line_Vec2[0] = cord2.X;
        Line_Vec2[1] = cord2.Y;
        Line_Vec2[2] = cord2.Z;

        theUFSession.Vec3.AngleBetween(Line_Vec1, Line_Vec2, vec_ccw, out angle);

        return angle * 180 / Math.PI;
    }

    /// <summary>
    /// Возвращает значение, определяющее является ли второй (заданный) вектор перпендикулярным
    /// текущему.
    /// </summary>
    /// <param name="vec">Второй вектор.</param>
    /// <returns></returns>
    public bool isNormal(Vector vec)
    {
        double angle = this.getAngle(vec);

        if (Math.Round(Math.Abs(angle)) == 90.0 || Math.Round(Math.Abs(angle)) == 270.0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    /// <summary>
    /// Возвращает значение, определяющее является ли второй (заданный) вектор параллельным
    /// текущему.
    /// </summary>
    /// <param name="vec">Второй вектор.</param>
    /// <returns></returns>
    public bool isParallel(Vector vec)
    {
        double angle = this.getAngle(vec);

        if (Math.Round((Math.Abs(angle))) == 0.0 || Math.Round(Math.Abs(angle)) == 180.0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    
    void initPoints(Point3d start, Point3d end)
    {
        this.start = new Point3d();
        this.start.X = Config.doub(start.X);
        this.start.Y = Config.doub(start.Y);
        this.start.Z = Config.doub(start.Z);

        this.end = new Point3d();
        this.end.X = Config.doub(end.X);
        this.end.Y = Config.doub(end.Y);
        this.end.Z = Config.doub(end.Z);
    }

    double getLength()
    {
        Point3d Coords = getCoords();
        return Math.Sqrt(Math.Abs(Math.Pow(Coords.X, 2) +
                                  Math.Pow(Coords.Y, 2) +
                                  Math.Pow(Coords.Z, 2)));
    }

    void setDirection()
    {
        double cosA = (end.X - start.X) / this.Length;
        double cosB = (end.Y - start.Y) / this.Length;
        double cosC = (end.Z - start.Z) / this.Length;

        this.direction = new Point3d(cosA, cosB, cosC);
    }
}

