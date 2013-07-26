using System;
using NXOpen;

/// <summary>
/// Класс математических плоскостей.
/// </summary>
public class Platan
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


    readonly double[] _equation;

    /// <summary>
    /// Инициализирует новый экземпляр класса для плоскости, проходящей через заданную точку и
    /// перпендикулярной заданной прямой
    /// </summary>
    /// <param name="point">Точка, через которую должна проходить плоскость.</param>
    /// <param name="normalStraight">Прямая, перпендикулярно которой должна проходить
    /// плоскость</param>
    public Platan(Point3d point, Straight normalStraight)
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
    public Platan(double xArg, double yArg, double zArg, double freeArg)
    {
        _equation = new double[] { xArg, yArg, zArg, freeArg };
    }
    /// <summary>
    /// Инициализирует новый экземпляр класса для плоскости, заданной общим уравнением
    /// (Ax + By + Cz + D = 0).
    /// </summary>
    /// <param name="equation">Массив c коэффициентами А, B, C, D</param>
    public Platan(double[] equation)
    {
        _equation = equation;
    }
    /// <summary>
    /// Инициализирует новый экземпляр класса для плоскости, заданной гранью элемента.
    /// </summary>
    /// <param name="face">Грань элемента.</param>
    public Platan(Face face)
    {
        int tmpInt;
        double tmpDouble;
        double[] box = new double[6];

        double[] point = new double[3];
        double[] dir = new double[3];
        Config.TheUfSession.Modl.AskFaceData(face.Tag, out tmpInt, point, dir, box,
                                             out tmpDouble, out tmpDouble, out tmpInt);

        double a = dir[0];
        double b = dir[1];
        double c = dir[2];
        double d = a * -point[0] + b * -point[1] + c * -point[2];
        
        _equation = new double[] { a, b, c, d };
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

    public Point3d GetProection(Point3d point)
    {
        Straight straight = new Straight(point, this);

        return Geom.SolveSlae(straight, this);
    }
}
