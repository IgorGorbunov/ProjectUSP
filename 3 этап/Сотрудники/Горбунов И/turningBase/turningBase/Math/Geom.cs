using System;
using System.Collections.Generic;
using NXOpen;
using NXOpen.UF;

/// <summary>
/// Класс с реализацией геометрических функций.
/// </summary>
static class Geom
{
    public const int Dimensions = 3;

    /// <summary>
    /// Возвращает диаметр окружности, образованной цилиндрическим ребром.
    /// </summary>
    /// <param name="edge">Циллиндрической ребро.</param>
    /// <returns></returns>
    public static double GetDiametr(Edge edge)
    {
        return edge.GetLength()/Math.PI;
    }

    /// <summary>
    /// Возвращает true, если точка находится между заданными прямыми.
    /// </summary>
    /// <param name="point">Заданная точка.</param>
    /// <param name="surface">Плоскость, в которых находится прямая.</param>
    /// <param name="straight1">Первая заданная прямая.</param>
    /// <param name="straight2">Вторая заданная прямая.</param>
    /// <returns></returns>
    public static bool PointIsBetweenStraights(Point3d point, Surface surface,
                                                Straight straight1, Straight straight2)
    {
        Point3d projectionPoint = surface.GetProection(point);

        double len1 = straight1.GetDistance(projectionPoint);
        double len2 = straight2.GetDistance(projectionPoint);
        double len = straight1.GetDistance(straight2);
        
        return !(len < len1 + len2);
    }


    //public static double[,] getStraitEquation(Point3d firstPoint, Point3d secondPoint)
    //{
    //    double[,] straight_equation =
    //        new double[straight_equation_rank, DIMENSIONS];

    //    straight_equation[0, 0] = -secondPoint.X;
    //    straight_equation[0, 1] = -secondPoint.Y;
    //    straight_equation[0, 2] = -secondPoint.Z;
    //    straight_equation[1, 0] = secondPoint.X - firstPoint.X;
    //    straight_equation[1, 1] = secondPoint.Y - firstPoint.Y;
    //    straight_equation[1, 2] =secondPoint.Z - firstPoint.Z;

    //    return straight_equation;
    //}
    //public static double[,] getStraitEquation(Edge edge)
    //{
    //    Point3d start, end;
    //    edge.GetVertices(out start, out end);

    //    return getStraitEquation(start, end);
    //}

    public static bool IsEdgePointOnStraight(Edge edge, Straight straight, 
                                                out double length, Point3d measurePoint)
    {
        length = -1.0;

        Point3d[] points = new Point3d[2];
        edge.GetVertices(out points[0], out points[1]);

        double[,] straightEquation = straight.Equation;
        foreach (Point3d p in points)
        {
            if (IsPointOnStrait(p, straightEquation, out length, measurePoint))
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Возвращает точку пересечения прямой и проведенным перпендикуляром к ней через заданную
    /// точку.
    /// </summary>
    /// <param name="p">Заданная точка, начало перпендикуляра.</param>
    /// <param name="straight">Прямая.</param>
    /// <returns></returns>
    public static Point3d GetIntersectionPointStraight(Point3d p, Straight straight)
    {
        Surface plain = new Surface(p, straight);

        return SolveSlae(straight, plain);
    }


    //public static double[] getPlainEquation(Point3d point, double[,] strain_equation, out int axeWillBeNull)
    //{
    //    double xArg = strain_equation[1, 0];
    //    double yArg = strain_equation[1, 1];
    //    double zArg = strain_equation[1, 2];
        
    //    if (xArg != 0)
    //    {
    //        axeWillBeNull = 0;
    //    }
    //    else if (yArg != 0)
    //    {
    //        axeWillBeNull = 1;
    //    }
    //    else if (zArg != 0)
    //    {
    //        axeWillBeNull = 2;
    //    }
    //    else
    //    {
    //        axeWillBeNull = -1;
    //    }

    //    double xFreeArg = strain_equation[1, 0] * -point.X;
    //    double yFreeArg = strain_equation[1, 1] * -point.Y;
    //    double zFreeArg = strain_equation[1, 2] * -point.Z;

    //    double freeArg = xFreeArg + yFreeArg + zFreeArg;

    //    double[] plain_equation = {xArg, yArg, zArg, freeArg};

    //    return plain_equation;
    //}
    //public static double[,] get2plainsFromStraight(double[,] straight_equation, int axeIsNull)
    //{
    //    double[,] matrix = new double[2, 4];

    //    int i = 0;
    //    if (axeIsNull != 2)
    //    {
    //        matrix[i, 0] = straight_equation[1, 1];
    //        matrix[i, 1] = -straight_equation[1, 0];
    //        matrix[i, 2] = 0;
    //        matrix[i, 3] = straight_equation[1, 1] * straight_equation[0, 0] -
    //            straight_equation[1, 0] * straight_equation[0, 1];

    //        i++;
    //    }

    //    if (axeIsNull != 1)
    //    {
    //        matrix[i, 0] = straight_equation[1, 2];
    //        matrix[i, 1] = 0;
    //        matrix[i, 2] = -straight_equation[1, 0];
    //        matrix[i, 3] = straight_equation[1, 2] * straight_equation[0, 0] -
    //            straight_equation[1, 0] * straight_equation[0, 2];

    //        i++;
    //    }

    //    if (i < 2)
    //    {
    //        matrix[i, 0] = 0;
    //        matrix[i, 1] = straight_equation[1, 2];
    //        matrix[i, 2] = -straight_equation[1, 1];
    //        matrix[i, 3] = straight_equation[1, 2] * straight_equation[0, 1] -
    //            straight_equation[1, 1] * straight_equation[0, 2];
    //    }

    //    return matrix;
    //}
    static double[,] GetMatrixIntersection(Straight straight, Surface surface, out double[] freeArg)
    {
        double[,] matrix = new double[Dimensions, Dimensions]; //REFACTOR
        freeArg = new double[Dimensions];

        Surface[] straightSurfaces = straight.Platanes;

        int i = 0;
        while (i < Dimensions - 1)
        {
            matrix[i, 0] = straightSurfaces[i].X;
            matrix[i, 1] = straightSurfaces[i].Y;
            matrix[i, 2] = straightSurfaces[i].Z;
            freeArg[i] = -straightSurfaces[i].FreeArg;
            i++;
        }

        matrix[i, 0] = surface.X;
        matrix[i, 1] = surface.Y;
        matrix[i, 2] = surface.Z;
        freeArg[i] = -surface.FreeArg;

        return matrix;
    }
    
    public static Point3d SolveSlae(Straight straight, Surface plain)
    {
        double[] freeArg;
        double[,] matrix = GetMatrixIntersection(straight, plain, out freeArg);

        double det = GetDet3X3(matrix);

        double[,] copyMatr = Copy3X3(matrix);
        double[,] matrixX = SetCol3X3(copyMatr, freeArg, 0);
        double detX = GetDet3X3(matrixX);

        copyMatr = Copy3X3(matrix);
        double[,] matrixY = SetCol3X3(copyMatr, freeArg, 1);
        double detY = GetDet3X3(matrixY);

        copyMatr = Copy3X3(matrix);
        double[,] matrixZ = SetCol3X3(copyMatr, freeArg, 2);
        double detZ = GetDet3X3(matrixZ);

        return new Point3d(detX / det, detY / det, detZ / det);
    }

    public static bool IsOnSegment(Point3d p, Edge e)
    {
        Point3d start, end;
        e.GetVertices(out start, out end);

        return IsOnSegment(p, start, end);
    }
    public static bool IsOnSegment(Point3d p, Vector v)
    {
        return IsOnSegment(p, v.Start, v.End);
    }

    private static bool IsOnSegment(Point3d p, Point3d vecPoint1, Point3d vecPoint2)
    {
        //AB - отрезок, Х - точка
        Vector vecAb = new Vector(vecPoint1, vecPoint2);
        Vector vecAx = new Vector(vecPoint1, p);

        Point3d aBcoords = vecAb.GetCoords();
        Point3d aXcoords = vecAx.GetCoords();
        double[] ratio = new double[Dimensions];

        ratio[0] = GetRatio(aXcoords.X, aBcoords.X);
        ratio[1] = GetRatio(aXcoords.Y, aBcoords.Y);
        ratio[2] = GetRatio(aXcoords.Z, aBcoords.Z);

        for (int i = 0; i < Dimensions; i++)
        {
            if (Config.Round(ratio[i]) < 0 || Config.Round(ratio[i]) > 1)
            {
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// Возвращает нормаль/направление для заданной грани.
    /// </summary>
    /// <param name="face">Целевая грань.</param>
    /// <returns></returns>
    public static double[] GetDirection(Face face)
    {
        int voidInt;
        double voidDouble;
        double[] dir = new double[3];
        double[] box = new double[6];
        double[] point = new double[3];

        Config.TheUfSession.Modl.AskFaceData(face.Tag, out voidInt, point, dir, box, out voidDouble, out voidDouble, out voidInt);
        return dir;
    }
    /// <summary>
    /// Возвращает "центральную" точку для грани.
    /// </summary>
    /// <param name="face">Целевая грань.</param>
    /// <returns></returns>
    public static double[] GetPoint(Face face)
    {
        int voidInt;
        double voidDouble;
        double[] dir = new double[3];
        double[] box = new double[6];
        double[] point = new double[3];

        Config.TheUfSession.Modl.AskFaceData(face.Tag, out voidInt, point, dir, box, out voidDouble, out voidDouble, out voidInt);
        return point;
    }

    /// <summary>
    /// Возвращает значение, определяющее являются ли две точки эквивалентными с учетом
    /// рабочего округления.
    /// </summary>
    /// <param name="point1">Первая точка.</param>
    /// <param name="point2">Вторая точка.</param>
    /// <returns></returns>
    public static bool IsEqual(Point3d point1, Point3d point2)
    {
        if (Config.Round(point1.X) != Config.Round(point2.X))
        {
            return false;
        }
        if (Config.Round(point1.Y) != Config.Round(point2.Y))
        {
            return false;
        }
        return Config.Round(point1.Z) == Config.Round(point2.Z);
    }
    /// <summary>
    /// Возвращает значение, определяющее являются ли две точки эквивалентными с учетом
    /// рабочего округления.
    /// </summary>
    /// <param name="vertex1">Первая точка.</param>
    /// <param name="vertex2">Вторая точка.</param>
    /// <returns></returns>
    public static bool IsEqual(Vertex vertex1, Vertex vertex2)
    {
        if (Config.Round(vertex1.Point.X) != Config.Round(vertex2.Point.X))
        {
            return false;
        }
        if (Config.Round(vertex1.Point.Y) != Config.Round(vertex2.Point.Y))
        {
            return false;
        }
        return Config.Round(vertex1.Point.Z) == Config.Round(vertex2.Point.Z);
    }
    /// <summary>
    /// Возвращает значение, определяющее являются ли два массива эквивалентными с учетом
    /// рабочего округления.
    /// </summary>
    /// <param name="d1">Первый одномерный массив.</param>
    /// <param name="d2">Второй одномерный массив.</param>
    /// <returns></returns>
    public static bool IsEqual(double[] d1, double[] d2)
    {
        if (d1.Length == d2.Length)
        {
            for (int i = 0; i < d1.Length; i++)
            {
                if (Config.Round(d1[i]) != Config.Round(d2[i]))
                {
                    return false;
                }
            }
            return true;
        }
        Config.TheUi.NXMessageBox.Show("Error!", NXMessageBox.DialogType.Error, "Массивы различны по длине!");
        return false;
    }
    /// <summary>
    /// Возвращает значение, определяющее являются ли два направления, заданные разными способами,
    /// эквивалентными с учетом рабочего округления.
    /// </summary>
    /// <param name="d1">Направление, заданное одномерным массивом.</param>
    /// <param name="d2">Направление, заданное экземпляром класса Point3D</param>
    /// <returns></returns>
    public static bool IsEqual(double[] d1, Point3d d2)
    {
        if (d1.Length == 3)
        {
            if (Config.Round(d1[0]) != Config.Round(d2.X))
            {
                return false;
            }
            if (Config.Round(d1[1]) != Config.Round(d2.Y))
            {
                return false;
            }
            return Config.Round(d1[2]) == Config.Round(d2.Z);
        }
        Config.TheUi.NXMessageBox.Show("Error!", NXMessageBox.DialogType.Error, "Длина массива не равна 3!");
        return false;
    }


    /// <summary>
    /// Возвращает true, если объект является компонентом.
    /// </summary>
    /// <param name="tO">Объект в формате TaggedObject.</param>
    /// <returns></returns>
    public static bool IsComponent(TaggedObject tO)
    {
        string strTo = tO.ToString();
        string[] split = strTo.Split(' ');

        return split[0] == "Component";
    }

    public static bool DirectionsAreOnStraight(double[] dir1, double[] dir2)
    {
        if (dir1.Length == dir2.Length)
        {
            bool notCoDirect = false;
            for (int i = 0; i < dir1.Length; i++)
            {
                if (Config.Round(dir1[i]) != Config.Round(dir2[i]))
                {
                    notCoDirect = true;
                }
                if (notCoDirect)
                {
                    break;
                }
            }
            if (notCoDirect)
            {
                for (int i = 0; i < dir1.Length; i++)
                {
                    if (Config.Round(dir1[i]) != - Config.Round(dir2[i]))
                    {
                        return false;
                    }
                }
                return true;
            }
            return true;
        }
        Config.TheUi.NXMessageBox.Show("Error!", NXMessageBox.DialogType.Error, "Массивы различны по длине!");
        return false;
    }




    static double GetDet2X2(double[,] matrix)
    {
        return matrix[0, 0] * matrix[1, 1] - matrix[1, 0] * matrix[0, 1];
    }

    static double GetDet3X3(double[,] matrix)
    {
        double[,] matr1 = new double[2, 2];
        matr1[0,0] = matrix[1,1];
        matr1[0,1] = matrix[1,2];
        matr1[1,0] = matrix[2,1];
        matr1[1,1] = matrix[2,2];
        double d1 = matrix[0, 0] * GetDet2X2(matr1);

        double[,] matr2 = new double[2, 2];
        matr2[0, 0] = matrix[1, 0];
        matr2[0, 1] = matrix[1, 2];
        matr2[1, 0] = matrix[2, 0];
        matr2[1, 1] = matrix[2, 2];
        double d2 = matrix[0, 1] * GetDet2X2(matr2);

        double[,] matr3 = new double[2, 2];
        matr3[0, 0] = matrix[1, 0];
        matr3[0, 1] = matrix[1, 1];
        matr3[1, 0] = matrix[2, 0];
        matr3[1, 1] = matrix[2, 1];
        double d3 = matrix[0, 2] * GetDet2X2(matr3);

        return (d1 - d2 + d3);
    }

    static double[,] SetCol3X3(double[,] matr, double[] colValues, int col)
    {
        for (int i = 0; i < colValues.Length; i++)
        {
            matr[i, col] = colValues[i];
        }

        return matr;
    }

    static double[,] Copy3X3(double[,] matr)
    {
        double[,] m = new double[3, 3];

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                m[i, j] = matr[i, j];
            }
        }

        return m;
    }



    static bool IsPointOnStrait(Point3d point, double[,] straightEquation, 
                                    out double length, Point3d measurePoint)
    {
        length = -1;

        const int nDimensions = Dimensions;
        

        // Массив с для обозначения осей, в перпендикулярных плоскостях которых лежит прямая
        // по уравнению straight_equation. Это важно для проверки точки на принадлежность к
        // прямой (подробнее см. "Пространственная геометрия").
        bool[] isConstantAxe = new bool[nDimensions];
        for (int i = 0; i < nDimensions; i++)
        {
            // Если знаменатель равен 0, то прямая лежит в плоскости перпендикулярной 
            // соответствующей оси по i (x - 0 | y - 1 | z - 2).
            if (Math.Round(straightEquation[1, i]) == 0.0)
            {
                isConstantAxe[i] = true;
            }
            else
            {
                isConstantAxe[i] = false;
            }
        }

        //REFACTOR
        double[] arrPoint = new double[nDimensions];
        arrPoint[0] = Config.Round(point.X);
        arrPoint[1] = Config.Round(point.Y);
        arrPoint[2] = Config.Round(point.Z);

        double[] coefficient = new double[nDimensions]; 
        for (int i = 0; i < nDimensions; i++)
        {
            // Если прямая не лежит в плоскости, перпендикулярной к одной из оси, то вычисляем
            // коэффициент по конкретному направлению
            if (!isConstantAxe[i])
	        {
        	    coefficient[i] = 
                    (arrPoint[i] + straightEquation[0, i]) / straightEquation[1, i];
	        }
        }

        List<int> axeNo = new List<int>();
        List<int> constAxeNo = new List<int>();
        for (int i = 0; i < isConstantAxe.Length; i++)
        {
            if (!isConstantAxe[i])
            {
                axeNo.Add(i);
            }
            else
            {
                constAxeNo.Add(i);
            }
        }

        int equelity = 0;
        for (int i = 1; i < axeNo.Count; i++)
        {
            if (Config.Round(coefficient[axeNo[i - 1]]) == Config.Round(coefficient[axeNo[i]]))
            {
                equelity++;
            }
        }

        for (int i = 0; i < constAxeNo.Count; i++)
        {
            int axeDirection = constAxeNo[i];
            if (Config.Round(arrPoint[axeDirection]) == 
                    Config.Round(- straightEquation[0, axeDirection]))
            {
                equelity++;
            }
        }
        if (equelity == nDimensions - 1)
        {
            Vector vecLen = new Vector(measurePoint, point);
            length = vecLen.Length;
            return true;
        }
        return false;
    }

    static double GetRatio(double d1, double d2)
    {
        if (Config.Round(d1) == 0 && Config.Round(d2) == 0)
        {
            return 0;
        }
        return d1 / d2;
    }

    /// <summary>
    /// Возвращает радиус заданной грани.
    /// </summary>
    /// <param name="face">Грань.</param>
    /// <returns></returns>
    public static double GetRadius(Face face)
    {
        int voidInt;
        double voidDouble;
        double rad;
        double[] dir = new double[3];
        double[] box = new double[6];
        double[] point = new double[3];

        Config.TheUfSession.Modl.AskFaceData(face.Tag, out voidInt, point, dir, box, out rad, out voidDouble, out voidInt);
        return rad;
    }
    /// <summary>
    /// Возвращает радиус заданного ребра.
    /// </summary>
    /// <param name="edge">Ребро.</param>
    /// <returns></returns>
    public static double GetRadius(Edge edge)
    {
        double len = edge.GetLength();
        return len/(2*Math.PI);
    }

    /// <summary>
    /// Возвращает центр цилиндрического ребра.
    /// </summary>
    /// <param name="edge">Цилиндрическое ребро.</param>
    /// <returns></returns>
    public static double[] GetCenter(Edge edge)
    {
        IntPtr arcEvaluator;
        Config.TheUfSession.Eval.Initialize(edge.Tag, out arcEvaluator);
        UFEval.Arc arcData;
        Config.TheUfSession.Eval.AskArc(arcEvaluator, out arcData);
        double[] center = arcData.center;
        Config.TheUfSession.Eval.Free(arcEvaluator);
        return center;
    }

    /// <summary>
    /// Возвращает цилиндрическое ребро заданной грани.
    /// </summary>
    /// <param name="face">Грань.</param>
    /// <returns></returns>
    public static Edge GetCyllindricalEdge(Face face)
    {
        Edge[] edges = face.GetEdges();
        foreach (Edge edge in edges)
        {
            if (edge.SolidEdgeType == Edge.EdgeType.Circular)
            {
                return edge;
            }
        }
        return null;
    }
}
