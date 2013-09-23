using System;
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
    public Point3d Direction
    {
        get
        {
            SetDirection();
            return _direction;
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
        st += "Coordinates: " + GetCoords() + Environment.NewLine;
        st += "Direction: " + Direction + "\t}";
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
    public Vector3d GetCoordsVector3D()
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
        double xD = point.X + Direction.X * length;
        double yD = point.Y + Direction.Y * length;
        double zD = point.Z + Direction.Z * length;

        return new Point3d(xD, yD, zD);
    }

    public double GetAngle(Vector vec)
    {
        UFSession theUfSession = UFSession.GetUFSession();

        double[] lineVec1 = new double[3];
        double[] lineVec2 = new double[3];
        double angle;
        double[] vecCcw = new double[3];

        Point3d cord1 = GetCoords();
        lineVec1[0] = cord1.X;
        lineVec1[1] = cord1.Y;
        lineVec1[2] = cord1.Z;

        Point3d cord2 = vec.GetCoords();
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
    }

    double GetLength()
    {
        Point3d coords = GetCoords();

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

