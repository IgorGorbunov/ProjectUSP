using System;
using NXOpen;

/// <summary>
/// Класс математических прямых.
/// </summary>
public sealed class Straight
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
            return _equation;
        }
    }
    /// <summary>
    /// Возвращает коэффициент слагаемого Х2 в числителе.
    /// </summary>
    public double NumX
    {
        get
        {
            return _equation[0, 0];
        }
    }
    /// <summary>
    /// Возвращает коэффициент слагаемого Y2 в числителе.
    /// </summary>
    public double NumY
    {
        get
        {
            return _equation[0, 1];
        }
    }
    /// <summary>
    /// Возвращает коэффициент слагаемого Z2 в числителе.
    /// </summary>
    public double NumZ
    {
        get
        {
            return _equation[0, 2];
        }
    }
    /// <summary>
    /// Возвращает коэффициент Х в знаменателе.
    /// </summary>
    public double DenX
    {
        get
        {
            return _equation[1, 0];
        }
    }
    /// <summary>
    /// Возвращает коэффициент Y в знаменателе.
    /// </summary>
    public double DenY
    {
        get
        {
            return _equation[1, 1];
        }
    }
    /// <summary>
    /// Возвращает коэффициент Z в знаменателе.
    /// </summary>
    public double DenZ
    {
        get
        {
            return _equation[1, 2];
        }
    }

    /// <summary>
    /// Возвращает 2 математические плоскости для образования текущей прямой.
    /// </summary>
    public Surface[] Platanes
    {
        get
        {
            if (_firstPlatane == null || _secondPlatane == null)
            {
                SetPlatanes();
            }
            return new Surface[] { _firstPlatane, _secondPlatane };
        }
    }
    /// <summary>
    /// Возвращает произвольную точку на прямой.
    /// </summary>
    public Point3d PointOnStraight
    {
        get
        {
            if (!_hasPointOn)
            {
                const string logMess = "Произвольной точки на прямой не существует!";
                Config.TheUi.NXMessageBox.Show("Ошибка!", NXMessageBox.DialogType.Error,
                                               logMess);
                Logger.WriteError(logMess);
            }

            return _pointOnStraight;
        }
    }

    //дробь представлена 2мя значениями - числителем и знаменателем
    private const int EquationRank = 2;

    double[,] _equation;
    Surface _firstPlatane, _secondPlatane;

    private bool _hasPointOn;
    private Point3d _pointOnStraight;

    /// <summary>
    /// Инициализирует новый экземпляр класса для математической прямой, проходящей через две
    /// заданных точки.
    /// </summary>
    /// <param name="point1">Первая точка.</param>
    /// <param name="point2">Вторая точка.</param>
    public Straight(Point3d point1, Point3d point2)
    {
        SetEquation(point1, point2);
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

        SetEquation(start, end);
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
    public Straight(Point3d p, Surface pl)
    {
        _equation = new double[EquationRank, Geom.Dimensions];

        _equation[0, 0] = - p.X;
        _equation[0, 1] = - p.Y;
        _equation[0, 2] = - p.Z;

        _equation[1, 0] = pl.X;
        _equation[1, 1] = pl.Y;
        _equation[1, 2] = pl.Z; 
    }
    /// <summary>
    /// Инициализирует новый экземпляр класса для математической прямой, заданной каноническим
    /// уравнением прямой.
    /// </summary>
    /// <param name="equation">Массив коэффициентов канонического уравнения.</param>
    public Straight(double[,] equation)
    {
        _equation = new double[EquationRank, Geom.Dimensions];
        for (int i = 0; i < EquationRank; i++)
        {
            for (int j = 0; j < Geom.Dimensions; j++)
            {
                _equation[i, j] = equation[i, j];
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
        mess += "X: " + NumX + " ";
        mess += "Y: " + NumY + " ";
        mess += "Z: " + NumZ;
        mess += " }";

        mess += Environment.NewLine;

        mess += "   { ";
        mess += "X: " + DenX + " ";
        mess += "Y: " + DenY + " ";
        mess += "Z: " + DenZ;
        mess += " }";

        mess += " }";

        return mess;
    }
    /// <summary>
    /// Возвращает расстояние от прямой до заданной точки.
    /// </summary>
    /// <param name="point">Точка.</param>
    /// <returns></returns>
    public double GetDistance(Point3d point)
    {
        _pointOnStraight = Geom.GetIntersectionPointStraight(point, this);
        _hasPointOn = true;

        Vector vec = new Vector(point, _pointOnStraight);
        return vec.Length;
    }
    /// <summary>
    /// Возвращает расстояние до параллельной прямой.
    /// </summary>
    /// <param name="straight">Параллельная для данной прямой, прямая.</param>
    /// <returns></returns>
    public double GetDistance(Straight straight)
    {
        Point3d point = straight.PointOnStraight;
        return GetDistance(point);
    }
    /// <summary>
    /// Возвращает точку пересечения перпендикуляра проведенного из заданной точки
    /// и данной прямой.
    /// </summary>
    /// <param name="point">Заданная точка.</param>
    /// <returns></returns>
    public Point3d GetProjectPoint(Point3d point)
    {
        return Geom.GetIntersectionPointStraight(point, this);
    }

    void SetEquation(Point3d firstPoint, Point3d secondPoint)
    {
        _equation = new double[EquationRank, Geom.Dimensions];
        _equation[0, 0] = -secondPoint.X;
        _equation[0, 1] = -secondPoint.Y;
        _equation[0, 2] = -secondPoint.Z;
        _equation[1, 0] = secondPoint.X - firstPoint.X;
        _equation[1, 1] = secondPoint.Y - firstPoint.Y;
        _equation[1, 2] = secondPoint.Z - firstPoint.Z;
    }
    void SetPlatanes()
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
        const int nPlatanes = 2;
        const int nCoefficients = 4;
        double[,] matrix = new double[nPlatanes, nCoefficients];

        int k = 0;
        if (Config.Round(DenX) != 0)
        {
            SetXy(matrix, k);
            k++;
            SetXz(matrix, k);
        }
        else if (Config.Round(DenY) != 0)
        {
            SetXy(matrix, k);
            k++;
            SetYz(matrix, k);
        }
        else if (Config.Round(DenZ) != 0)
        {
            SetXz(matrix, k);
            k++;
            SetYz(matrix, k);
        }

        Surface[] surfaces = new Surface[nPlatanes];
        for (int i = 0; i < nPlatanes; i++)
        {
            double[] row = new double[nCoefficients];
            for (int j = 0; j < nCoefficients; j++)
            {
                row[j] = matrix[i, j];
            }
            surfaces[i] = new Surface(row);
        }

        _firstPlatane = surfaces[0];
        _secondPlatane = surfaces[1];
    }
    void SetXy(double[,] matrix, int k)
    {
        matrix[k, 0] = DenY;//-straight_equation[1, 0];
        matrix[k, 1] = -DenX;//straight_equation[1, 1];
        matrix[k, 2] = 0;
        matrix[k, 3] = DenY * NumX - DenX * NumY;
        //straight_equation[1, 1] * straight_equation[0, 0] -
        //straight_equation[1, 0] * straight_equation[0, 1];
    }
    void SetYz(double[,] matrix, int k)
    {
        matrix[k, 0] = 0;
        matrix[k, 1] = DenZ;//-straight_equation[1, 1];
        matrix[k, 2] = -DenY;//straight_equation[1, 2];
        matrix[k, 3] = DenZ * NumY - DenY * NumZ;
        //straight_equation[1, 2] * straight_equation[0, 1] -
        //straight_equation[1, 1] * straight_equation[0, 2];
    }
    void SetXz(double[,] matrix, int k)
    {
        matrix[k, 0] = DenZ;//-straight_equation[1, 0];
        matrix[k, 1] = 0;
        matrix[k, 2] = -DenX;//straight_equation[1, 2];
        matrix[k, 3] = DenZ * NumX - DenX * NumZ;
        //straight_equation[1, 2] * straight_equation[0, 0] -
        //straight_equation[1, 0] * straight_equation[0, 2];
    }


}