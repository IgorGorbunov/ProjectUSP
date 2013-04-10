using System;
using System.Collections.Generic;
using System.Text;
using NXOpen;

/// <summary>
/// Класс с реализацией геометрических функций.
/// </summary>
static class Geom
{
    public const int DIMENSIONS = 3;
    
    
    

    public static double getHalfPerimetr(double a, double b, double c)
    {
        return (a + b + c) / 2;
    }
    public static double getSquare(double a, double b, double c)
    {
        double p = getHalfPerimetr(a, b, c);
        return Math.Sqrt(p * (p - a) * (p - b) * (p - c));
    }
    public static double getPerpen(double a, double b, double c)
    {
        double S = getSquare(a, b, c);

        return (2 * S) / a;
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

    public static bool isEdgePointOnStraight(Edge edge, Straight straight, 
                                                out double length, Point3d measurePoint)
    {
        length = -1.0;

        Point3d[] points = new Point3d[2];
        edge.GetVertices(out points[0], out points[1]);

        double[,] straight_equation = straight.Equation;
        foreach (Point3d p in points)
        {
            if (isPointOnStrait(p, straight_equation, out length, measurePoint))
            {
                return true;
            }
        }

        return false;
    }

    public static Point3d getIntersectionPointStraight(Point3d P, Straight straight)
    {
        Platan plain = new Platan(P, straight);

        double[] freeArg;
        double[,] matrix = getMatrixIntersection(straight, plain, out freeArg);

        double det = getDet3x3(matrix);

        double[,] copyMatr = copy3x3(matrix);
        double[,] matrixX = setCol3x3(copyMatr, freeArg, 0);
        double detX = getDet3x3(matrixX);

        copyMatr = copy3x3(matrix);
        double[,] matrixY = setCol3x3(copyMatr, freeArg, 1);
        double detY = getDet3x3(matrixY);

        copyMatr = copy3x3(matrix);
        double[,] matrixZ = setCol3x3(copyMatr, freeArg, 2);
        double detZ = getDet3x3(matrixZ);

        return new Point3d(detX / det, detY / det, detZ / det);
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
    public static double[,] getMatrixIntersection(Straight straight, Platan platan, out double[] freeArg)
    {
        double[,] matrix = new double[DIMENSIONS, DIMENSIONS]; //REFACTOR
        freeArg = new double[DIMENSIONS];

        Platan[] straightPlatans = straight.Platanes;

        int i = 0;
        while (i < DIMENSIONS - 1)
        {
            matrix[i, 0] = straightPlatans[i].X;
            matrix[i, 1] = straightPlatans[i].Y;
            matrix[i, 2] = straightPlatans[i].Z;
            freeArg[i] = -straightPlatans[i].FreeArg;
            i++;
        }

        matrix[i, 0] = platan.X;
        matrix[i, 1] = platan.Y;
        matrix[i, 2] = platan.Z;
        freeArg[i] = -platan.FreeArg;

        //matrix[0, 0] = straights[0, 0];
        //matrix[0, 1] = straights[0, 1];
        //matrix[0, 2] = straights[0, 2];

        //matrix[1, 0] = straights[1, 0];
        //matrix[1, 1] = straights[1, 1];
        //matrix[1, 2] = straights[1, 2];

        //matrix[2, 0] = plane[0];
        //matrix[2, 1] = plane[1];
        //matrix[2, 2] = plane[2];

        //freeArg[0] = -straights[0, 3];
        //freeArg[1] = -straights[1, 3];
        //freeArg[2] = -plane[3];

        return matrix;
    }

    public static bool isOnSegment(Point3d P, Edge E)
    {
        Point3d start, end;
        E.GetVertices(out start, out end);

        return isOnSegment(P, start, end);
    }
    public static bool isOnSegment(Point3d P, Vector v)
    {
        if (isOnSegment(P, v.start, v.end))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public static bool isOnSegment(Point3d P, Point3d vecPoint1, Point3d vecPoint2)
    {
        //AB - отрезок, Х - точка
        Vector vecAB = new Vector(vecPoint1, vecPoint2);
        Vector vecAX = new Vector(vecPoint1, P);

        Point3d ABcoords = vecAB.getCoords();
        Point3d AXcoords = vecAX.getCoords();

        double[] p = new double[DIMENSIONS];

        p[0] = AXcoords.X / ABcoords.X;
        p[1] = AXcoords.Y / ABcoords.Y;
        p[2] = AXcoords.Z / ABcoords.Z;

        for (int i = 0; i < DIMENSIONS; i++)
        {
            if (Config.round(p[i]) < 0 || Config.round(p[i]) > 1)
            {
                return false;
            }
        }

        return true;
    }

    public static double[] getDirection(Face face)
    {
        int voidInt;
        double voidDouble;
        double[] dir = new double[3];
        double[] box = new double[6];

        double[] point = new double[3];
        Config.theUFSession.Modl.AskFaceData(face.Tag, out voidInt, point, dir, box, out voidDouble, out voidDouble, out voidInt);
        return dir;
    }

    /// <summary>
    /// Возвращает значение, определяющее являются ли две точки эквивалентными с учетом
    /// рабочего округления.
    /// </summary>
    /// <param name="point1">Первая точка.</param>
    /// <param name="point2">Вторая точка.</param>
    /// <returns></returns>
    public static bool isEqual(Point3d point1, Point3d point2)
    {
        if (Config.round(point1.X) != Config.round(point2.X))
        {
            return false;
        }
        else if (Config.round(point1.Y) != Config.round(point2.Y))
        {
            return false;
        }
        else if (Config.round(point1.Z) != Config.round(point2.Z))
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    public static bool isEqual(double[] dir1, double[] dir2)
    {
        if (dir1.Length == dir2.Length && dir1.Length == DIMENSIONS)
        {
            for (int i = 0; i < dir1.Length; i++)
            {
                if (Config.round(dir1[i]) != Config.round(dir2[i]))
                {
                    return false;
                }
            }
            return true;
        }
        else
        {
            Config.theUI.NXMessageBox.Show("Error!", NXMessageBox.DialogType.Error, "В массиве элементов больше 3х!");
            return false;
        }
    }

    public static bool isComponent(TaggedObject tO)
    {
        string strTO = tO.ToString();
        string[] split = strTO.Split(' ');

        if (split[0] == "Component")
        {
            return true;
        }
        else
        {
            return false;
        }
    }









    static double getDet2x2(double[,] matrix)
    {
        return matrix[0, 0] * matrix[1, 1] - matrix[1, 0] * matrix[0, 1];
    }

    static double getDet3x3(double[,] matrix)
    {
        double[,] matr1 = new double[2, 2];
        matr1[0,0] = matrix[1,1];
        matr1[0,1] = matrix[1,2];
        matr1[1,0] = matrix[2,1];
        matr1[1,1] = matrix[2,2];
        double d1 = matrix[0, 0] * getDet2x2(matr1);

        double[,] matr2 = new double[2, 2];
        matr2[0, 0] = matrix[1, 0];
        matr2[0, 1] = matrix[1, 2];
        matr2[1, 0] = matrix[2, 0];
        matr2[1, 1] = matrix[2, 2];
        double d2 = matrix[0, 1] * getDet2x2(matr2);

        double[,] matr3 = new double[2, 2];
        matr3[0, 0] = matrix[1, 0];
        matr3[0, 1] = matrix[1, 1];
        matr3[1, 0] = matrix[2, 0];
        matr3[1, 1] = matrix[2, 1];
        double d3 = matrix[0, 2] * getDet2x2(matr3);

        return (d1 - d2 + d3);
    }

    static double[,] setCol3x3(double[,] matr, double[] colValues, int col)
    {
        for (int i = 0; i < colValues.Length; i++)
        {
            matr[i, col] = colValues[i];
        }

        return matr;
    }

    static double[,] copy3x3(double[,] matr)
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



    /// <summary>
    /// Возвращает массив соответствующий указанной строке матрицы. Отсчет строк идет с 0.
    /// </summary>
    static double[] getRow(double[,] matrix, int row)
    {
        int size = DIMENSIONS;
        double[] ret = new double[size];

        for (int i = 0; i < size; i++)
        {
            ret[i] = matrix[row, i];
        }

        return ret;
    }

    /// <summary>
    /// Заполняет указанную строку матрицы значениями из массива. Если размер массива и размер строки не совпадают, то строка будет - либо заполнена не полностью, либо "лишние" значения массива будут проигнорированы.
    /// </summary>
    static void SetRow(double[,] matrix, int row, double[] rowValues)
    {
        int size = DIMENSIONS;

        for (int i = 0; i < size; i++)
            matrix[row, i] = rowValues[i];
    }

    /// <summary>
    /// Поэлементное умножение массивов
    /// </summary>
    static double[] MulArrayConst(double[] array, double number)
    {
        double[] ret = (double[])array.Clone();
        for (int i = 0; i < ret.Length; i++)
            ret[i] *= number;
        return ret;
    }

    /// <summary>
    /// поэлементное вычитание массивов.
    /// </summary>
    static double[] SubArray(double[] A, double[] B)
    {
        double[] ret = (double[])A.Clone();
        for (int i = 0; i < (A.Length > B.Length ? A.Length : B.Length); i++)
            ret[i] -= B[i];
        return ret;
    }



    static bool isPointOnStrait(Point3d point, double[,] straight_equation, 
                                    out double length, Point3d measurePoint)
    {
        length = -1;

        int nDimensions = DIMENSIONS;
        int nNulls = 0;

        // Массив с для обозначения осей, в перпендикулярных плоскостях которых лежит прямая
        // по уравнению straight_equation. Это важно для проверки точки на принадлежность к
        // прямой (подробнее см. "Пространственная геометрия").
        bool[] isConstantAxe = new bool[nDimensions];
        for (int i = 0; i < nDimensions; i++)
        {
            // Если знаменатель равен 0, то прямая лежит в плоскости перпендикулярной 
            // соответствующей оси по i (x - 0 | y - 1 | z - 2).
            if (Math.Round(straight_equation[1, i]) == 0.0)
            {
                isConstantAxe[i] = true;
                nNulls++;
            }
            else
            {
                isConstantAxe[i] = false;
            }
        }

        //REFACTOR
        double[] arrPoint = new double[nDimensions];
        arrPoint[0] = Config.round(point.X);
        arrPoint[1] = Config.round(point.Y);
        arrPoint[2] = Config.round(point.Z);

        double[] coefficient = new double[nDimensions]; 
        for (int i = 0; i < nDimensions; i++)
        {
            // Если прямая не лежит в плоскости, перпендикулярной к одной из оси, то вычисляем
            // коэффициент по конкретному направлению
            if (!isConstantAxe[i])
	        {
        	    coefficient[i] = 
                    (arrPoint[i] + straight_equation[0, i]) / straight_equation[1, i];
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
            if (Config.round(coefficient[axeNo[i - 1]]) == Config.round(coefficient[axeNo[i]]))
            {
                equelity++;
            }
        }

        for (int i = 0; i < constAxeNo.Count; i++)
        {
            int axeDirection = constAxeNo[i];
            if (Config.round(arrPoint[axeDirection]) == 
                    Config.round(- straight_equation[0, axeDirection]))
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
        else
        {
            return false;
        }

    }
}
