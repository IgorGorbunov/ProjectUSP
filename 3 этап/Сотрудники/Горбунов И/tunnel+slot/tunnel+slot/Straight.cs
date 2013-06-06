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
        : this(vector.Start, vector.End)
    {

    }
    /// <summary>
    /// Инициализирует новый экземпляр класса для математической прямой, проходящей через заданную 
    /// точку, перпендикулярно заданной плоскости.
    /// </summary>
    /// <param name="p">Точка, через которую проходит прямая.</param>
    /// <param name="pl">Плоскость, перпендикулярно которой проходит прямая.</param>
    public Straight(Point3d p, Platan pl)
    {
        this.equation = new double[this.equationRank, Geom.DIMENSIONS];

        this.equation[0, 0] = - p.X;
        this.equation[0, 1] = - p.Y;
        this.equation[0, 2] = - p.Z;

        this.equation[1, 0] = pl.X;
        this.equation[1, 1] = pl.Y;
        this.equation[1, 2] = pl.Z; 
    }
    /// <summary>
    /// Инициализирует новый экземпляр класса для математической прямой, заданной каноническим
    /// уравнением прямой.
    /// </summary>
    /// <param name="equation">Массив коэффициентов канонического уравнения.</param>
    public Straight(double[,] equation)
    {
        this.equation = new double[this.equationRank, Geom.DIMENSIONS];
        for (int i = 0; i < this.equationRank; i++)
        {
            for (int j = 0; j < Geom.DIMENSIONS; j++)
            {
                this.equation[i, j] = equation[i, j];
            }
        }
    }

    /// <summary>
    /// Возвращает строку, которая представляет текущий объект.
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        string mess = "";
        mess += "{ ";

        mess += "{ ";
        mess += "X: " + NumX.ToString() + " ";
        mess += "Y: " + NumY.ToString() + " ";
        mess += "Z: " + NumZ.ToString();
        mess += " }";

        mess += Environment.NewLine;

        mess += "   { ";
        mess += "X: " + DenX.ToString() + " ";
        mess += "Y: " + DenY.ToString() + " ";
        mess += "Z: " + DenZ.ToString();
        mess += " }";

        mess += " }";

        return mess;
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
        //if (Config.round(this.DenX) != 0 || Config.round(this.DenY) != 0)
        //{
        //    matrix[k, 0] = this.DenY;//-straight_equation[1, 0];
        //    matrix[k, 1] = -this.DenX;//straight_equation[1, 1];
        //    matrix[k, 2] = 0;
        //    matrix[k, 3] = this.DenY * this.NumX - this.DenX * this.NumY;
        //        //straight_equation[1, 1] * straight_equation[0, 0] -
        //        //straight_equation[1, 0] * straight_equation[0, 1];

        //    k++;
        //}

        //if (Config.round(this.DenX) != 0 || Config.round(this.DenZ) != 0)
        //{
        //    matrix[k, 0] = this.DenZ;//-straight_equation[1, 0];
        //    matrix[k, 1] = 0;
        //    matrix[k, 2] = -this.DenX;//straight_equation[1, 2];
        //    matrix[k, 3] = this.DenZ * this.NumX - this.DenX * this.NumZ;
        //        //straight_equation[1, 2] * straight_equation[0, 0] -
        //        //straight_equation[1, 0] * straight_equation[0, 2];

        //    k++;
        //}

        //if (k < 2)
        //{
        //    matrix[k, 0] = 0;
        //    matrix[k, 1] = this.DenZ;//-straight_equation[1, 1];
        //    matrix[k, 2] = -this.DenY;//straight_equation[1, 2];
        //    matrix[k, 3] = this.DenZ * this.NumY - this.DenY * this.NumZ;
        //        //straight_equation[1, 2] * straight_equation[0, 1] -
        //        //straight_equation[1, 1] * straight_equation[0, 2];
        //}
        int nPlatanes = 2;
        int nCoefficients = 4;
        double[,] matrix = new double[nPlatanes, nCoefficients];

        int k = 0;
        if (Config.round(this.DenX) != 0)
        {
            this.setXY(matrix, k);
            k++;
            this.setXZ(matrix, k);
        }
        else if (Config.round(this.DenY) != 0)
        {
            this.setXY(matrix, k);
            k++;
            this.setYZ(matrix, k);
        }
        else if (Config.round(this.DenZ) != 0)
        {
            this.setXZ(matrix, k);
            k++;
            this.setYZ(matrix, k);
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
    void setXY(double[,] matrix, int k)
    {
        matrix[k, 0] = this.DenY;//-straight_equation[1, 0];
        matrix[k, 1] = -this.DenX;//straight_equation[1, 1];
        matrix[k, 2] = 0;
        matrix[k, 3] = this.DenY * this.NumX - this.DenX * this.NumY;
        //straight_equation[1, 1] * straight_equation[0, 0] -
        //straight_equation[1, 0] * straight_equation[0, 1];
    }
    void setYZ(double[,] matrix, int k)
    {
        matrix[k, 0] = 0;
        matrix[k, 1] = this.DenZ;//-straight_equation[1, 1];
        matrix[k, 2] = -this.DenY;//straight_equation[1, 2];
        matrix[k, 3] = this.DenZ * this.NumY - this.DenY * this.NumZ;
        //straight_equation[1, 2] * straight_equation[0, 1] -
        //straight_equation[1, 1] * straight_equation[0, 2];
    }
    void setXZ(double[,] matrix, int k)
    {
        matrix[k, 0] = this.DenZ;//-straight_equation[1, 0];
        matrix[k, 1] = 0;
        matrix[k, 2] = -this.DenX;//straight_equation[1, 2];
        matrix[k, 3] = this.DenZ * this.NumX - this.DenX * this.NumZ;
        //straight_equation[1, 2] * straight_equation[0, 0] -
        //straight_equation[1, 0] * straight_equation[0, 2];
    }


}