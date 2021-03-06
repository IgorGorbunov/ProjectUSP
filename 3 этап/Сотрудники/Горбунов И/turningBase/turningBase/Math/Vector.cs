﻿using System;
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
    /// Возвращает середину вектора.
    /// </summary>
    public Point3d Center
    {
        get
        {
            double x = (Start.X + End.X) / 2;
            double y = (Start.Y + End.Y) / 2;
            double z = (Start.Z + End.Z) / 2;
            return new Point3d(x, y, z);
        }
    }
    /// <summary>
    /// Возвращает окрестностную точку начальной точки вектора.
    /// </summary>
    public Point3d SurroundingPoint
    {
        get
        {
            double x = Direction1.X * (Length / 50) + Start.X;
            double y = Direction1.Y * (Length / 50) + Start.Y;
            double z = Direction1.Z * (Length / 50) + Start.Z;
            return new Point3d(x, y, z);
        }
    }
    /// <summary>
    /// Возвращает 4 точки вдоль вектора.
    /// </summary>
    public List<Point3d> SurroundingPoints
    {
        get
        {
            List<Point3d> points = new List<Point3d>();
            Point3d tmpPoint = new Point3d();
            tmpPoint.X = Direction1.X * (Length / 5) + Start.X;
            tmpPoint.Y = Direction1.Y * (Length / 5) + Start.Y;
            tmpPoint.Z = Direction1.Z * (Length / 5) + Start.Z;
            points.Add(tmpPoint);
            for (int i = 0; i < 3; i++)
            {
                double x = Direction1.X * (Length / 5) + tmpPoint.X;
                double y = Direction1.Y * (Length / 5) + tmpPoint.Y;
                double z = Direction1.Z * (Length / 5) + tmpPoint.Z;
                tmpPoint = new Point3d(x, y, z);
                points.Add(tmpPoint);
            }

            return points;
        }
    }
    /// <summary>
    /// Возвращает длину вектора.
    /// </summary>
    public double Length
    {
        get
        {
            if (_length == -1.0)
	        {
        		 _length = GetLength();
	        }
            return _length;
        }
    }
    /// <summary>
    /// Возвращает направляющие косинусы вектора.
    /// </summary>
    public Point3d Direction1
    {
        get
        {
            SetDirection();
            return _direction;
        }
    }
    /// <summary>
    /// Возвращает направляющие косинусы вектора.
    /// </summary>
    public double[] Direction2
    {
        get
        {
            SetDirection();
            double[] direction = {_direction.X, _direction.Y, _direction.Z};
            return direction;
        }
    }
    /// <summary>
    /// Возвращает направляющие косинусы вектора.
    /// </summary>
    public Vector3d Direction3
    {
        get
        {
            SetDirection();
            Vector3d direction = new Vector3d(_direction.X, _direction.Y, _direction.Z);
            return direction;
        }
    }

    double _length = -1.0;
    Point3d _direction = new Point3d(0.0, 0.0, 0.0);
    private Point3d _start, _end;

    /// <summary>
    /// Инициализирует новый экземпляр класса вектора по координатам начальной и конечной точки.
    /// </summary>
    /// <param name="start">Начальная точка вектора.</param>
    /// <param name="end">Конечная точка вектора.</param>
    internal Vector(Point3d start, Point3d end)
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

        InitPoints(start, end);
    }
    /// <summary>
    /// Инициализирует новый экземпляр класса вектора для заданных цилиндрических рёбер.
    /// </summary>
    /// <param name="edge1">Первое цилиндрическое ребро.</param>
    /// <param name="edge2">Второе цилиндрическое ребро.</param>
    public Vector(Edge edge1, Edge edge2)
    {
        double[] firstCenter = Geom.GetCenter(edge1);
        double[] secondCenter = Geom.GetCenter(edge2);
        Point3d start, end;
        start.X = firstCenter[0];
        start.Y = firstCenter[1];
        start.Z = firstCenter[2];

        end.X = secondCenter[0];
        end.Y = secondCenter[1];
        end.Z = secondCenter[2];

        InitPoints(start, end);
    }
    /// <summary>
    /// Инициализирует новый экземпляр класса вектора для заданной координатной оси.
    /// </summary>
    /// <param name="axe">Координатная ось.</param>
    public Vector(CoordinateAxe axe)
    {
        Point3d point = new Point3d();
        switch (axe.Type)
        {
            case CoordinateConfig.Type.X:
                point = new Point3d(1.0, 0.0, 0.0);
                break;
            case CoordinateConfig.Type.Y:
                point = new Point3d(0.0, 1.0, 0.0);
                break;
            case CoordinateConfig.Type.Z:
                point = new Point3d(0.0, 0.0, 1.0);
                break;
        }
        InitPoints(new Point3d(), point);
    }

    public Vector(Face face)
    {
        int voidInt;
        double voidDouble;
        double rad;
        double[] dir = new double[3];
        double[] box = new double[6];
        double[] point = new double[3];

        Config.TheUfSession.Modl.AskFaceData(face.Tag, out voidInt, point, dir, box, out rad, out voidDouble, out voidInt);
        Point3d start, end;
        start.X = point[0];
        start.Y = point[1];
        start.Z = point[2];

        end.X = start.X + dir[0];
        end.Y = start.Y + dir[1];
        end.Z = start.Z + dir[2];
        InitPoints(start, end);
    }
    /// <summary>
    /// Инициализирует новый экземпляр класса вектора для заданного направления и начальной точки.
    /// </summary>
    /// <param name="point">Начальная точка.</param>
    /// <param name="direction">Направление.</param>
    public Vector(Point3d point, double[] direction)
    {
        Point3d start, end;
        start.X = point.X;
        start.Y = point.Y;
        start.Z = point.Z;

        end.X = start.X + direction[0];
        end.Y = start.Y + direction[1];
        end.Z = start.Z + direction[2];
        InitPoints(start, end);
    }
    /// <summary>
    /// Инициализирует новый экземпляр класса вектора для заданного направления.
    /// </summary>
    /// <param name="direction">Направление</param>
    public Vector(double[] direction)
    {
        Point3d start, end;
        start.X = 0;
        start.Y = 0;
        start.Z = 0;

        end.X = start.X + direction[0];
        end.Y = start.Y + direction[1];
        end.Z = start.Z + direction[2];
        InitPoints(start, end);
    }

    /// <summary>
    /// Возвращает строку, которая представляет текущий объект.
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        string st = "";
        st += "{" + Environment.NewLine;
        st += "Start: " + _start + Environment.NewLine;
        st += "End: " + _end + Environment.NewLine;
        st += "Coordinates: " + GetCoords1() + Environment.NewLine;
        st += "Direction: " + Direction1 + "\t}";
        return st;
    }
    /// <summary>
    /// Изменяет направление вектора.
    /// </summary>
    public void Reverse()
    {
        Point3d tmpPoint = _start;
        _start = _end;
        _end = tmpPoint;
        Initialize();
    }

    

    /// <summary>
    /// Возвращает координаты вектора.
    /// </summary>
    /// <returns></returns>
    public Point3d GetCoords1()
    {
        return new Point3d(_end.X - _start.X, _end.Y - _start.Y, _end.Z - _start.Z);
    }
    /// <summary>
    /// Возвращает вектор.
    /// </summary>
    /// <returns></returns>
    public Vector3d GetCoords2()
    {
        return new Vector3d(_end.X - _start.X, _end.Y - _start.Y, _end.Z - _start.Z);
    }

    /// <summary>
    /// Возвращает точку лежащей на прямой, проходящей через данный вектор и лежащей на заданном
    /// расстоянии от конца вектора.
    /// </summary>
    /// <param name="length">Расстояние от конца вектора.</param>
    /// <returns></returns>
    public Point3d GetPoint(double length)
    {
        return GetPoint(_end, length);
    }

    /// <summary>
    /// Возвращает точку лежащей на прямой, проходящей через заданную точку по направлению вектора 
    /// и лежащей на заданном расстоянии от заданной точки.
    /// </summary>
    /// <param name="point">За</param>
    /// <param name="length">Расстояние от конца вектора.</param>
    /// <returns></returns>
    public Point3d GetPoint(Point3d point, double length)
    {
        double xD = point.X + Direction1.X * length;
        double yD = point.Y + Direction1.Y * length;
        double zD = point.Z + Direction1.Z * length;

        return new Point3d(xD, yD, zD);
    }
    /// <summary>
    /// Возвращает угол между текущим вектором и заданным.
    /// </summary>
    /// <param name="vec">Заданный вектор.</param>
    /// <returns></returns>
    public double GetAngle(Vector vec)
    {
        UFSession theUfSession = UFSession.GetUFSession();

        double[] lineVec1 = new double[3];
        double[] lineVec2 = new double[3];
        double angle;
        double[] vecCcw = new double[3];

        Point3d cord1 = GetCoords1();
        lineVec1[0] = cord1.X;
        lineVec1[1] = cord1.Y;
        lineVec1[2] = cord1.Z;

        Point3d cord2 = vec.GetCoords1();
        lineVec2[0] = cord2.X;
        lineVec2[1] = cord2.Y;
        lineVec2[2] = cord2.Z;

        theUfSession.Vec3.AngleBetween(lineVec1, lineVec2, vecCcw, out angle);

        return angle * 180 / Math.PI;
    }

    /// <summary>
    /// Возвращает значение, определяющее является ли второй (заданный) вектор перпендикулярным
    /// текущему.
    /// </summary>
    /// <param name="vec">Второй вектор.</param>
    /// <returns></returns>
    internal bool IsNormal(Vector vec)
    {
        double angle = GetAngle(vec);

        return Math.Round(Math.Abs(angle)) == 90.0 || Math.Round(Math.Abs(angle)) == 270.0;
    }
    /// <summary>
    /// Возвращает значение, определяющее является ли второй (заданный) вектор параллельным
    /// текущему.
    /// </summary>
    /// <param name="vec">Второй вектор.</param>
    /// <returns></returns>
    internal bool IsParallel(Vector vec)
    {
        double angle = GetAngle(vec);

        return Math.Round((Math.Abs(angle))) == 0.0 || Math.Round(Math.Abs(angle)) == 180.0;
    }
    /// <summary>
    /// Возвращает true, если заданный вектор сонаправлен текущему.
    /// </summary>
    /// <param name="vector">Вектор.</param>
    /// <returns></returns>
    internal bool IsCoDirectional(Vector vector)
    {
        double angle = GetAngle(vector);
        return Config.Round(angle) == 0.0;
    }
    /// <summary>
    /// Возвращает true, если заданный вектор сонаправлен текущему в плоскости проекции текущего.
    /// </summary>
    /// <param name="vector">Вектор.</param>
    /// <returns></returns>
    public bool IsCoDirectionalInProject(Vector vector)
    {
        Straight rStraight = new Straight(this);
        Point3d start = rStraight.GetProjectPoint(vector.Start);
        Point3d end = rStraight.GetProjectPoint(vector.End);
        Vector trueSlotDirection = new Vector(start, end);

        return IsCoDirectional(trueSlotDirection);
    }

    public double GetStartsLength(Vector vector)
    {
        Vector vectorBetween = new Vector(Start, vector.Start);
        return vectorBetween.Length;
    }

    /// <summary>
    /// Возвращает координаты точки смещения заданной точки по текущему векору.
    /// </summary>
    /// <param name="point">Заданная точка.</param>
    /// <returns></returns>
    private Point3d GetOffsetPoint(Point3d point)
    {
        Point3d offsetPoint = new Point3d();

        offsetPoint.X = End.X - (Start.X - point.X);
        offsetPoint.Y = End.Y - (Start.Y - point.Y);
        offsetPoint.Z = End.Z - (Start.Z - point.Z);
        return offsetPoint;
    }
    
    /// <summary>
    /// Возвращает точку повёрнутую вокруг данного вектора на определённый угол.
    /// </summary>
    /// <param name="point">Начальная точка.</param>
    /// <param name="angle">Угол в градусах.</param>
    /// <returns></returns>
    public Point3d GetRotatePoint(Point3d point, double angle)
    {
        Vector offsetVector = new Vector(Start, new Point3d(0.0, 0.0, 0.0));
        Point3d offsetPoint = offsetVector.GetOffsetPoint(point);

        Matrix3x3 matrix = GetRotateMatrix(angle);
        MathUtils mathUtils = Config.TheSession.MathUtils;
        Point3d offsetNewPoint = mathUtils.Multiply(matrix, offsetPoint);
        offsetVector.Reverse();
        Point3d newPoint = offsetVector.GetOffsetPoint(offsetNewPoint);
        offsetVector.Reverse();
        return newPoint;
    }

    private Matrix3x3 GetRotateMatrix(double a)
    {
        Matrix3x3 matrix;
        if (Geom.IsEqual(_direction, new Point3d(0.0, 0.0, 0.0)))
        {
            SetDirection();
        }

        a = Geom.Rad(a);
        matrix.Xx = Math.Cos(a) + (1 - Math.Cos(a)) * _direction.X * _direction.X;
        matrix.Xy = (1 - Math.Cos(a)) * _direction.X * _direction.Y - Math.Sin(a) * _direction.Z;
        matrix.Xz = (1 - Math.Cos(a)) * _direction.X * _direction.Z + Math.Sin(a) * _direction.Y;
        matrix.Yx = (1 - Math.Cos(a)) * _direction.Y * _direction.X + Math.Sin(a) * _direction.Z;
        matrix.Yy = Math.Cos(a) + (1 - Math.Cos(a)) * _direction.Y * _direction.Y;
        matrix.Yz = (1 - Math.Cos(a)) * _direction.Y * _direction.Z - Math.Sin(a) * _direction.X;
        matrix.Zx = (1 - Math.Cos(a)) * _direction.Z * _direction.X - Math.Sin(a) * _direction.Y;
        matrix.Zy = (1 - Math.Cos(a)) * _direction.Z * _direction.Y + Math.Sin(a) * _direction.X;
        matrix.Zz = Math.Cos(a) + (1 - Math.Cos(a)) * _direction.Z * _direction.Z;

        return matrix;
    }


    void InitPoints(Point3d start, Point3d end)
    {
        _start = new Point3d();
        _start.X = start.X;
        _start.Y = start.Y;
        _start.Z = start.Z;

        _end = new Point3d();
        _end.X = end.X;
        _end.Y = end.Y;
        _end.Z = end.Z;
        Initialize();
    }

    void Initialize()
    {
        _direction = new Point3d(0.0, 0.0, 0.0);
    }

    double GetLength()
    {
        Point3d coords = GetCoords1();

        return Math.Sqrt(Math.Pow(coords.X, 2) +
                         Math.Pow(coords.Y, 2) +
                         Math.Pow(coords.Z, 2));
    }

    void SetDirection()
    {
        double cosA = (_end.X - _start.X) / Length;
        double cosB = (_end.Y - _start.Y) / Length;
        double cosC = (_end.Z - _start.Z) / Length;

        _direction = new Point3d(cosA, cosB, cosC);
    }
}

