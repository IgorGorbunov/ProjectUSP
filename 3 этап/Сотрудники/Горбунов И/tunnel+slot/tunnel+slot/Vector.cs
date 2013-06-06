﻿using System;
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
    public Point3d Start
    {
        get { return _start; }
    }
    /// <summary>
    /// Задаёт и возвращает конец вектора.
    /// </summary>
    public Point3d End
    {
        get { return _end; }
    }
    /// <summary>
    /// Возвращает длину вектора.
    /// </summary>
    public double Length
    {
        get
        {
            if (this._length == -1.0)
	        {
        		 this._length = this.GetLength();
	        }
            return this._length;
        }
    }
    /// <summary>
    /// Возвращает направляющие косинусы вектора.
    /// </summary>
    public Point3d Direction
    {
        get
        {
            if (Geom.isEqual(this._direction, new Point3d(0.0, 0.0, 0.0)))
            {
                this.SetDirection();
            }

            return this._direction;
        }
    }

    public double X
    {
        get
        {

            if (Geom.isEqual(this._direction, new Point3d(0.0, 0.0, 0.0)))
            {
                this.SetDirection();
            }
            return _direction.X;
        }
    }
    public double Y
    {
        get
        {
            if (Geom.isEqual(this._direction, new Point3d(0.0, 0.0, 0.0)))
            {
                this.SetDirection();
            }
            return _direction.Y;
        }
    }
    public double Z
    {
        get
        {
            if (Geom.isEqual(this._direction, new Point3d(0.0, 0.0, 0.0)))
            {
                this.SetDirection();
            }
            return _direction.Z;
        }
    }

    double _length = -1.0;
    Point3d _direction = new Point3d(0.0, 0.0, 0.0);
    private Point3d _start, _end;

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
        InitPoints(start, end);
    }
    /// <summary>
    /// Инициализирует новый экземпляр класса вектора для заданного ребра.
    /// </summary>
    /// <param name="edg">Ребро элемента.</param>
    public Vector(Edge edg)
    {
        Point3d start, end;
        edg.GetVertices(out start, out end);

        this.InitPoints(start, end);
    }


    public override string ToString()
    {
        string st = "";
        st += "{" + Environment.NewLine;
        st += "Start: " + _start + Environment.NewLine;
        st += "End: " + _end + Environment.NewLine;
        st += "Coordinates: " + GetCoords() + Environment.NewLine;
        st += "Direction: " + Direction + "\t}";
        return st;
    }

    

    /// <summary>
    /// Возвращает координаты вектора.
    /// </summary>
    /// <returns></returns>
    public Point3d GetCoords()
    {
        return new Point3d(_end.X - _start.X, _end.Y - _start.Y, _end.Z - _start.Z);
    }
    /// <summary>
    /// Возвращает вектор.
    /// </summary>
    /// <returns></returns>
    public Vector3d GetVector3D()
    {
        return new Vector3d(_end.X - _start.X, _end.Y - _start.Y, _end.Z - _start.Z);
    }

    /// <summary>
    /// Возвращает угол между текущим вектором и заданным (вторым).
    /// </summary>
    /// <param name="vec">Второй вектор.</param>
    /// <returns></returns>
    public double GetAngle(Vector vec)
    {
        UFSession theUFSession = UFSession.GetUFSession();

        double[] Line_Vec1 = new double[3];
        double[] Line_Vec2 = new double[3];
        double angle;
        double[] vec_ccw = new double[3];

        Point3d cord1 = this.GetCoords();
        Line_Vec1[0] = cord1.X;
        Line_Vec1[1] = cord1.Y;
        Line_Vec1[2] = cord1.Z;

        Point3d cord2 = vec.GetCoords();
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
    public bool IsNormal(Vector vec)
    {
        double angle = this.GetAngle(vec);

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
    public bool IsParallel(Vector vec)
    {
        double angle = this.GetAngle(vec);

        if (Math.Round((Math.Abs(angle))) == 0.0 || Math.Round(Math.Abs(angle)) == 180.0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    
    void InitPoints(Point3d start, Point3d end)
    {
        this._start = new Point3d();
        this._start.X = start.X;
        this._start.Y = start.Y;
        this._start.Z = start.Z;

        this._end = new Point3d();
        this._end.X = end.X;
        this._end.Y = end.Y;
        this._end.Z = end.Z;
    }

    double GetLength()
    {
        Point3d Coords = GetCoords();

        return Math.Sqrt(Math.Pow(Coords.X, 2) +
                         Math.Pow(Coords.Y, 2) +
                         Math.Pow(Coords.Z, 2));
    }

    void SetDirection()
    {
        double cosA = (_end.X - _start.X) / Length;
        double cosB = (_end.Y - _start.Y) / Length;
        double cosC = (_end.Z - _start.Z) / Length;

        _direction = new Point3d(cosA, cosB, cosC);
    }
}

