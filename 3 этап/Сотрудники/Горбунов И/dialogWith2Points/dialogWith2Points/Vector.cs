using System;
using System.Collections.Generic;
using NXOpen;
using NXOpen.UF;


public class Vector
{
    public Point3d start, end;


    /// <summary>
    /// Инициализация по-умолчанию
    /// </summary>
    public Vector()
        : this(new Point3d(), new Point3d())
    {

    }

    /// <summary>
    /// Инициализация точкой начала и точкой конца вектора
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    public Vector(Point3d start, Point3d end)
    {
        initPoints(start, end);
    }

    public Vector(Edge Edg)
    {
        Point3d start, end;
        Edg.GetVertices(out start, out end);

        initPoints(start, end);
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

    /// <summary>
    /// Получение координат вектора
    /// </summary>
    /// <returns></returns>
    public Point3d getCoords()
    {
        return new Point3d(end.X - start.X, end.Y - start.Y, end.Z - start.Z);
    }

    /// <summary>
    /// Получение длины вектора
    /// </summary>
    /// <returns></returns>
    public double getLength()
    {
        Point3d Coords = getCoords();
        return Math.Sqrt(Math.Abs(Math.Pow(Coords.X, 2) + Math.Pow(Coords.Y, 2) + Math.Pow(Coords.Z, 2)));
    }

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
}

