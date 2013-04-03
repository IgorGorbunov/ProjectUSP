using System;
using System.Collections.Generic;
using System.Text;
using NXOpen;

/// <summary>
/// Класс математических прямых.
/// </summary>
public class Straight
{
    /// <summary>
    /// Возвращает 6 коэффициентов из нормального уравнения прямой вида:
    ///  x - x2    y - y2    z - z2
    /// ------- = ------- = --------
    /// x2 - x1   y2 - y1   z2 - z1
    /// </summary>
    public double[,] Equation
    {
        get
        {
            return this.equation;
        }
    }
    /// <summary>
    /// Возвращает коэффициент слагаемого Х2 в числителе.
    /// </summary>
    public double NumX
    {
        get
        {
            return this.equation[0, 0];
        }
    }
    /// <summary>
    /// Возвращает коэффициент слагаемого Y2 в числителе.
    /// </summary>
    public double NumY
    {
        get
        {
            return this.equation[0, 1];
        }
    }
    /// <summary>
    /// Возвращает коэффициент слагаемого Z2 в числителе.
    /// </summary>
    public double NumZ
    {
        get
        {
            return this.equation[0, 2];
        }
    }
    /// <summary>
    /// Возвращает коэффициент Х в знаменателе.
    /// </summary>
    public double DenX
    {
        get
        {
            return this.equation[1, 0];
        }
    }
    /// <summary>
    /// Возвращает коэффициент Y в знаменателе.
    /// </summary>
    public double DenY
    {
        get
        {
            return this.equation[1, 1];
        }
    }
    /// <summary>
    /// Возвращает коэффициент Z в знаменателе.
    /// </summary>
    public double DenZ
    {
        get
        {
            return this.equation[1, 2];
        }
    }

    /// <summary>
    /// Возвращает 2 математические плоскости для образования текущей прямой.
    /// </summary>
    public Platan[] Platanes
    {
        get
        {
            if (firstPlatane == null || secondPlatane == null)
            {
                this.setPlatanes();
            }
            return new Platan[] { firstPlatane, secondPlatane };
        }
    }

    //дробь представлена 2мя значениями - числителем и знаменателем
    int equationRank = 2;

    double[,] equation;
    Platan firstPlatane, secondPlatane;

    /// <summary>
    /// Инициализирует новый экземпляр класса для математической прямой, проходящей через две
    /// заданных точки.
    /// </summary>
    /// <param name="point1">Первая точка.</param>
    /// <param name="point2">Вторая точка.</param>
    public Straight(Point3d point1, Point3d point2)
    {
        this.setEquation(point1, point2);
    }
    /// <summary>
    /// Инициализирует новый экземпляр класса для математической прямой,проходящей через
    /// заданное ребро.
    /// </summary>
    /// <param name="edge">Ребро элемента.</param>
    public Straight(Edge edge)
    {
        Point3d start, end;
        edge.GetVertices(out start, out end);

        this.setEquation(start, end);
    }
    /// <summary>
    /// Инициализирует новый экземпляр класса для математической прямой, проходящей через
    /// заданный вектор.
    /// </summary>
    /// <param name="vector">Вектор.</param>
    public Straight(Vector vector)
        : this(vector.start, vector.end)
    {

    }

    void setEquation(Point3d firstPoint, Point3d secondPoint)
    {
        this.equation = new double[equationRank, Geom.DIMENSIONS];
        this.equation[0, 0] = -secondPoint.X;
        this.equation[0, 1] = -secondPoint.Y;
        this.equation[0, 2] = -secondPoint.Z;
        this.equation[1, 0] = secondPoint.X - firstPoint.X;
        this.equation[1, 1] = secondPoint.Y - firstPoint.Y;
        this.equation[1, 2] = secondPoint.Z - firstPoint.Z;
    }
    void setPlatanes()
    {
        int nPlatanes = 2;
        int nCoefficients = 4;
        double[,] matrix = new double[nPlatanes, nCoefficients];

        int k = 0;
        if (this.DenZ == 0)
        {
            matrix[k, 0] = -this.DenX;//-straight_equation[1, 0];
            matrix[k, 1] = this.DenY;//straight_equation[1, 1];
            matrix[k, 2] = 0;
            matrix[k, 3] = this.DenY * this.NumX - this.DenX * this.NumY;
                //straight_equation[1, 1] * straight_equation[0, 0] -
                //straight_equation[1, 0] * straight_equation[0, 1];

            k++;
        }

        if (this.DenY == 0)
        {
            matrix[k, 0] = -this.DenX;//-straight_equation[1, 0];
            matrix[k, 1] = 0;
            matrix[k, 2] = this.DenZ;//straight_equation[1, 2];
            matrix[k, 3] = this.DenZ * this.NumX - this.DenX * this.NumZ;
                //straight_equation[1, 2] * straight_equation[0, 0] -
                //straight_equation[1, 0] * straight_equation[0, 2];

            k++;
        }

        if (k < 2)
        {
            matrix[k, 0] = 0;
            matrix[k, 1] = -this.DenY;//-straight_equation[1, 1];
            matrix[k, 2] = this.DenZ;//straight_equation[1, 2];
            matrix[k, 3] = this.DenZ * this.NumY - this.DenY * this.NumZ;
                //straight_equation[1, 2] * straight_equation[0, 1] -
                //straight_equation[1, 1] * straight_equation[0, 2];
        }

        Platan[] platans = new Platan[nPlatanes];
        for (int i = 0; i < nPlatanes; i++)
        {
            double[] row = new double[nCoefficients];
            for (int j = 0; j < nCoefficients; j++)
            {
                row[j] = matrix[i, j];
            }
            platans[i] = new Platan(row);
        }

        this.firstPlatane = platans[0];
        this.secondPlatane = platans[1];
    }


}

