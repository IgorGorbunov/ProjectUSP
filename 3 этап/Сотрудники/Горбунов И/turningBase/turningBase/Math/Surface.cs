using System;
using NXOpen;

/// <summary>
/// Класс математических плоскостей.
/// </summary>
public class Surface
{
    /// <summary>
    /// Возвращает массив коэффициентов A, B, C, D для общего уравнения плоскости -
    /// Ax + By + Cz + D = 0.
    /// </summary>
    public double[] Equation
    {
        get
        {
            return _equation;
        }
        
    }
    /// <summary>
    /// Возвращает коэффициент при аргументе X из общего уравнения плоскости.
    /// </summary>
    public double X
    {
        get
        {
            return _equation[0];
        }
    }
    /// <summary>
    /// Возвращает коэффициент при аргументе Y из общего уравнения плоскости.
    /// </summary>
    public double Y
    {
        get
        {
            return _equation[1];
        }
    }
    /// <summary>
    /// Возвращает коэффициент при аргументе Z из общего уравнения плоскости.
    /// </summary>
    public double Z
    {
        get
        {
            return _equation[2];
        }
    }
    /// <summary>
    /// Возвращает свободный аргумент из общего уравнения плоскости.
    /// </summary>
    public double FreeArg
    {
        get
        {
            return _equation[3];
        }
    }
    /// <summary>
    /// Возвращает грань элемента.
    /// </summary>
    public Face GetFace
    {
        get { return _face; }
    }
    /// <summary>
    /// Возвращает радиус грани, заданной данной поверхностью.
    /// </summary>
    /// <returns></returns>
    public double Radius
    {
        get
        {
            return _faceRadius;
        }
    }


    readonly double[] _equation;

    private Face _face;
    private int _faceType, _faceNormDir;
    private double[] _faceCenterPoint = new double[3];
    private double[] _faceDirection = new double[3];
    private double[] _faceBox = new double[6];
    private double _faceRadius, _faceRadData;

    /// <summary>
    /// Инициализирует новый экземпляр класса для плоскости, проходящей через заданную точку и
    /// перпендикулярной заданной прямой
    /// </summary>
    /// <param name="point">Точка, через которую должна проходить плоскость.</param>
    /// <param name="normalStraight">Прямая, перпендикулярно которой должна проходить
    /// плоскость</param>
    public Surface(Point3d point, Straight normalStraight)
    {
        double xArg = normalStraight.DenX;
        double yArg = normalStraight.DenY;
        double zArg = normalStraight.DenZ;

        double xFreeArg = normalStraight.DenX * -point.X;
        double yFreeArg = normalStraight.DenY * -point.Y;
        double zFreeArg = normalStraight.DenZ * -point.Z;

        double freeArg = xFreeArg + yFreeArg + zFreeArg;

        _equation = new double[] { xArg, yArg, zArg, freeArg };
    }
    /// <summary>
    /// Инициализирует новый экземпляр класса для плоскости, заданной общим уравнением
    /// (Ax + By + Cz + D = 0).
    /// </summary>
    /// <param name="xArg">Коэффициент при аргументе Х.</param>
    /// <param name="yArg">Коэффициент при аргументе Y.</param>
    /// <param name="zArg">Коэффициент при аргументе Z.</param>
    /// <param name="freeArg">Cвободный аргумент.</param>
    public Surface(double xArg, double yArg, double zArg, double freeArg)
    {
        _equation = new double[] { xArg, yArg, zArg, freeArg };
    }
    /// <summary>
    /// Инициализирует новый экземпляр класса для плоскости, заданной общим уравнением
    /// (Ax + By + Cz + D = 0).
    /// </summary>
    /// <param name="equation">Массив c коэффициентами А, B, C, D</param>
    public Surface(double[] equation)
    {
        _equation = equation;
    }
    /// <summary>
    /// Инициализирует новый экземпляр класса для плоскости, заданной гранью элемента.
    /// </summary>
    /// <param name="face">Грань элемента.</param>
    public Surface(Face face)
    {
        _face = face;
        Config.TheUfSession.Modl.AskFaceData(face.Tag, out _faceType, _faceCenterPoint, _faceDirection, _faceBox,
                                             out _faceRadius, out _faceRadData, out _faceNormDir);

        double a = _faceDirection[0];
        double b = _faceDirection[1];
        double c = _faceDirection[2];
        double d = a * -_faceCenterPoint[0] + b * -_faceCenterPoint[1] + c * -_faceCenterPoint[2];
        
        _equation = new double[] { a, b, c, d };
    }
    /// <summary>
    /// Инициализирует новый экземпляр класса для ортогональной плоскости, заданной осью.
    /// </summary>
    /// <param name="axe"></param>
    public Surface(CoordinateAxe axe)
    {
        double a = 0, b = 0, c = 0;
        const double d = 0;
        switch (axe.Type)
        {
            case CoordinateConfig.Type.X:
                a = 1;
                break;
            case CoordinateConfig.Type.Y:
                b = 1;
                break;
            case CoordinateConfig.Type.Z:
                c = 1;
                break;
        }
        _equation = new double[] {a, b, c, d};
    }

    /// <summary>
    /// Возвращает строку, которая представляет текущий объект.
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        string mess = "";

        mess += "{ ";
        mess += "A: " + X + " ";
        mess += "B: " + Y + " ";
        mess += "C: " + Z + " ";
        mess += "D: " + FreeArg;
        mess += " }";

        return mess;
    }

    /// <summary>
    /// Возвращает минимальное по модулю расстояние (перпендикуляр) между плоскостью и заданной
    /// точкой. Ахтунг!!1 Возвращает положительное значение, если нормаль плоскости и вектор к
    /// точке сонаправлены, отрицательное значение - если противонаправлены.
    /// </summary>
    /// <param name="point">Точка.</param>
    /// <returns></returns>
    public double GetDistanceToPoint(Point3d point)
    {
        double numerator = X * point.X +
                           Y * point.Y +
                           Z * point.Z +
                           FreeArg;

        double denominator = Math.Sqrt(X * X +
                                       Y * Y +
                                       Z * Z);

        return numerator / denominator;
    }
    /// <summary>
    /// Возвращает проекцию заданной точки на текущую плоскость.
    /// </summary>
    /// <param name="point">Точка.</param>
    /// <returns></returns>
    public Point3d GetProection(Point3d point)
    {
        Straight straight = new Straight(point, this);

        return Geom.SolveSlae(straight, this);
    }
    /// <summary>
    /// Возвращает проекцию заданной точки на текущую плоскость.
    /// </summary>
    /// <param name="vertex">Точка.</param>
    /// <returns></returns>
    public Vertex GetProection(Vertex vertex)
    {
        Point3d point = vertex.Point;
        Straight straight = new Straight(point, this);

        Vertex projectVertex = new Vertex(Geom.SolveSlae(straight, this));
        return projectVertex;
    }
    /// <summary>
    /// Возвращает true, если данный экзмепляр плоскости параллелен заданной.
    /// </summary>
    /// <param name="surface">Плоскость.</param>
    /// <returns></returns>
    public bool IsParallel(Surface surface)
    {
        if (Config.Round(X) != Config.Round(surface.X))
        {
            return false;
        }
        if (Config.Round(Y) != Config.Round(surface.Y))
        {
            return false;
        }
        if (Config.Round(Z) != Config.Round(surface.Z))
        {
            return false;
        }
        return true;
    }
}
