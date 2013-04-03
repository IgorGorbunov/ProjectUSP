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
    /// Возвращает массив коэффициентов A, B, C, D для нормального уравнения плоскости -
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
    /// Возвращает коэффициент при аргументе X из нормального уравнения плоскости.
    /// </summary>
    public double X
    {
        get
        {
            return this.equation[0];
        }
    }
    /// <summary>
    /// Возвращает коэффициент при аргументе Y из нормального уравнения плоскости.
    /// </summary>
    public double Y
    {
        get
        {
            return this.equation[1];
        }
    }
    /// <summary>
    /// Возвращает коэффициент при аргументе Z из нормального уравнения плоскости.
    /// </summary>
    public double Z
    {
        get
        {
            return this.equation[2];
        }
    }
    /// <summary>
    /// Возвращает свободный аргумент из нормального уравнения плоскости.
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
    /// Инициализирует новый экземпляр класса для плоскости, заданной нормальным уравнением
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
    /// Инициализирует новый экземпляр класса для плоскости, заданной нормальным уравнением
    /// (Ax + By + Cz + D = 0).
    /// </summary>
    /// <param name="equation">Массив c коэффициентами А, B, C, D</param>
    public Platan(double[] equation)
    {
        this.equation = equation;
    }

    public double getDistanceToPoint(Point3d point)
    {
        return 0.0;
    }
}
