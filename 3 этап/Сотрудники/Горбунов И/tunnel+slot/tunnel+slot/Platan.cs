using System;
using System.Collections.Generic;
using System.Text;
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
            return this.equation;
        }
    }
    /// <summary>
    /// Возвращает коэффициент при аргументе X из общего уравнения плоскости.
    /// </summary>
    public double X
    {
        get
        {
            return this.equation[0];
        }
    }
    /// <summary>
    /// Возвращает коэффициент при аргументе Y из общего уравнения плоскости.
    /// </summary>
    public double Y
    {
        get
        {
            return this.equation[1];
        }
    }
    /// <summary>
    /// Возвращает коэффициент при аргументе Z из общего уравнения плоскости.
    /// </summary>
    public double Z
    {
        get
        {
            return this.equation[2];
        }
    }
    /// <summary>
    /// Возвращает свободный аргумент из общего уравнения плоскости.
    /// </summary>
    public double FreeArg
    {
        get
        {
            return this.equation[3];
        }
    }
    

    double[] equation;

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

        this.equation = new double[4] { xArg, yArg, zArg, freeArg };
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
        this.equation = new double[4] { xArg, yArg, zArg, freeArg };
    }
    /// <summary>
    /// Инициализирует новый экземпляр класса для плоскости, заданной общим уравнением
    /// (Ax + By + Cz + D = 0).
    /// </summary>
    /// <param name="equation">Массив c коэффициентами А, B, C, D</param>
    public Platan(double[] equation)
    {
        this.equation = equation;
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
        Config.theUFSession.Modl.AskFaceData(face.Tag, out tmpInt, point, dir, box,
                                             out tmpDouble, out tmpDouble, out tmpInt);

        double A = dir[0];
        double B = dir[1];
        double C = dir[2];
        double D = A * -point[0] + B * -point[1] + C * -point[2];
        
        this.equation = new double[4] { A, B, C, D };
    }

    /// <summary>
    /// Возвращает строку, которая представляет текущий объект.
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        string mess = "";

        mess += "{ ";
        mess += "A: " + X.ToString() + " ";
        mess += "B: " + Y.ToString() + " ";
        mess += "C: " + Z.ToString() + " ";
        mess += "D: " + FreeArg.ToString();
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
    public double getDistanceToPoint(Point3d point)
    {
        double numerator = this.X * point.X +
                           this.Y * point.Y +
                           this.Z * point.Z +
                           this.FreeArg;

        double denominator = Math.Sqrt(this.X * this.X +
                                       this.Y * this.Y +
                                       this.Z * this.Z);

        return numerator / denominator;
    }

    public Point3d getProection(Point3d point)
    {
        Straight straight = new Straight(point, this);

        return Geom.solveSLAE(straight, this);
    }
}
