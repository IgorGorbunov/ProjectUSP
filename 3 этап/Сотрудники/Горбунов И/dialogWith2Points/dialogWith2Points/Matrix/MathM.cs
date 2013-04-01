using System;

namespace ExMatrix
{
    /// <summary>
    /// Математические операции с матрицами типа int, double.
    /// </summary>
    public class MathM
    {
        #region Sum - int, double, bool (копипаст форевер)

        /// <summary>
        /// Возвращает сумму матриц.
        /// </summary>
        public static Matrix<int> Sum(Matrix<int> mA, Matrix<int> mB)
        {
            if (mA.Size != mB.Size) throw new ArgumentException("Матрицы должны быть одинаковых размеров.");
            Matrix<int> ret = new Matrix<int>();
            ret.InitMatrix(mA.Size, mA.TypeM);

            int size = mA.Size.X;
            for (int i = 0; i < mA.Size.X * mA.Size.Y; i++)
            {
                int x = i / size;
                int y = i - size * x;

                ret[x, y] = mA[x, y] + mB[x, y];
            }
            return ret;
        }

        /// <summary>
        /// Возвращает сумму матриц.
        /// </summary>
        public static Matrix<double> Sum(Matrix<double> mA, Matrix<double> mB)
        {
            if (mA.Size != mB.Size) throw new ArgumentException("Матрицы должны быть одинаковых размеров.");
            Matrix<double> ret = new Matrix<double>();
            ret.InitMatrix(mA.Size, mA.TypeM);

            int size = mA.Size.X;
            for (int i = 0; i < mA.Size.X * mA.Size.Y; i++)
            {
                int x = i / size;
                int y = i - size * x;

                ret[x, y] = mA[x, y] + mB[x, y];
            }
            return ret;
        }

        /// <summary>
        /// Возвращает сумму матриц.
        /// </summary>
        public static Matrix<bool> Sum(Matrix<bool> mA, Matrix<bool> mB)
        {
            if (mA.Size != mB.Size) throw new ArgumentException("Матрицы должны быть одинаковых размеров.");
            Matrix<bool> ret = new Matrix<bool>();
            ret.InitMatrix(mA.Size, mA.TypeM);

            int size = mA.Size.X;
            for (int i = 0; i < mA.Size.X * mA.Size.Y; i++)
            {
                int x = i / size;
                int y = i - size * x;

                ret[x, y] = mA[x, y] | mB[x, y];
            }
            return ret;
        }
        #endregion

        #region MulConst (умножение матрицы на число) - int, double
        /// <summary>
        /// Возвращает матрицу умноженную на константу.
        /// </summary>
        public static Matrix<int> MulConst(Matrix<int> mA, int number)
        {
            Matrix<int> ret = mA.Clone();
            int size = mA.Size.X;
            for (int i = 0; i < mA.Size.X * mA.Size.Y; i++)
            {
                int x = i / size;
                int y = i - size * x;

                ret[x, y] = mA[x, y] * number;
            }
            return ret;
        }

        /// <summary>
        /// Возвращает матрицу умноженную на константу.
        /// </summary>
        public static Matrix<double> MulConst(Matrix<double> mA, int number)
        {
            Matrix<double> ret = mA.Clone();
            int size = mA.Size.X;
            for (int i = 0; i < mA.Size.X * mA.Size.Y; i++)
            {
                int x = i / size;
                int y = i - size * x;

                ret[x, y] = mA[x, y] * number;
            }
            return ret;
        }

        /// <summary>
        /// Возвращает матрицу умноженную на константу.
        /// </summary>
        public static Matrix<double> MulConst(Matrix<double> mA, double number)
        {
            Matrix<double> ret = mA.Clone();
            int size = mA.Size.X;
            for (int i = 0; i < mA.Size.X * mA.Size.Y; i++)
            {
                int x = i / size;
                int y = i - size * x;

                ret[x, y] = mA[x, y] * number;
            }
            return ret;
        }

        #endregion

        #region Reverse (только для bool-матриц, возвращает матрицу со значениями обратными исходным)
        /// <summary>
        /// Только для bool-матриц, возвращает матрицу со значениями обратными исходным
        /// </summary>
        public static Matrix<bool> Reverse(Matrix<bool> mA)
        {
            Matrix<bool> ret = mA.Clone();
            int size = mA.Size.X;
            for (int i = 0; i < mA.Size.X * mA.Size.Y; i++)
            {
                int x = i / size;
                int y = i - size * x;

                ret[x, y] = !mA[x, y];
            }
            return ret;

        }
        #endregion

        #region Sub (вычитание матриц) - int, double
        /// <summary>
        /// Возвращает разность матриц.
        /// </summary>
        public static Matrix<int> Sub(Matrix<int> mA, Matrix<int> mB)
        {
             if (mA.Size != mB.Size) throw new ArgumentException("Матрицы должны быть одинаковых размеров.");
             Matrix<int> reversB = MulConst(mB, -1);
             return Sum(mA, reversB);
        }

        /// <summary>
        /// Возвращает разность матриц.
        /// </summary>
        public static Matrix<double> Sub(Matrix<double> mA, Matrix<double> mB)
        {
            if (mA.Size != mB.Size) throw new ArgumentException("Матрицы должны быть одинаковых размеров.");
            Matrix<double> reversB = MulConst(mB, -1);
            return Sum(mA, reversB);
        }
        #endregion

        #region Multiplication (умножение классическим способом) - int, double
        /// <summary>
        /// Возвращает результат умножения матриц. Умножение классическим способом, сложность алгоритма O(n^3). Число столбцов матрицы А должно быть равно числу строк матрицы В.
        /// </summary>
        public static Matrix<int> Multiplication(Matrix<int> mA, Matrix<int> mB)
        {
            if (mA.Size.Y != mB.Size.X) throw new ArgumentException("Число столбцов матрицы А не равно числу строк матрицы В.");
            Matrix<int> ret = new Matrix<int>();
            ret.InitMatrix(new SizeMatrix(mA.Size.X, mB.Size.Y), TypeMatrix.Rectangle);
            for (int i = 0; i < mA.Size.X; i++)
                for (int j = 0; j < mB.Size.Y; j++)
                    for (int k = 0; k < mB.Size.X; k++)
                        ret[i, j] += mA[i, k] * mB[k, j];

            return ret;
        }

        /// <summary>
        /// Возвращает результат умножения матриц. Умножение классическим способом, сложность алгоритма O(n^3). Число столбцов матрицы А должно быть равно числу строк матрицы В.
        /// </summary>
        /*public static Matrix<double> Multiplication(Matrix<double> mA, Matrix<double> mB, uint round = 0)
        {
            if (mA.Size.Y != mB.Size.X) throw new ArgumentException("Число столбцов матрицы А не равно числу строк матрицы В.");
            Matrix<double> ret = new Matrix<double>();
            ret.InitMatrix(new SizeMatrix(mA.Size.X, mB.Size.Y), TypeMatrix.Rectangle);
            for (int i = 0; i < mA.Size.X; i++)
                for (int j = 0; j < mB.Size.Y; j++)
                    for (int k = 0; k < mB.Size.X; k++)
                    {
                        ret[i, j] += mA[i, k] * mB[k, j];
                        if (round != 0) ret[i, j] = Math.Round(ret[i, j], (int)round);
                    }

            return ret;
        }*/
        #endregion

        #region Determinant (вычисление определителя матрицы по Гауссу) - int, double
        /// <summary>
        /// Вычисление определителя матрицы методом Гаусса.
        /// </summary>
        public static int Determinant(Matrix<int> mA)
        {
            if (mA.Size.X != mA.Size.Y) throw new ArgumentException("Вычисление определителя возможно только для квадратных матриц.");

            Matrix<double> convertMatrix = ConvertM.ToDouble(mA);
            return (int)MathM.Determinant(convertMatrix);
        }

        /// <summary>
        /// Вычисление определителя матрицы методом Гаусса (Приводим матрицу к треугольному виду и перемножаем главную диагональ).
        /// </summary>
        public static double Determinant(Matrix<double> mA)
        {
            if (mA.Size.X != mA.Size.Y) throw new ArgumentException("Вычисление определителя возможно только для квадратных матриц.");
            Matrix<double> matrix = mA.Clone();
            double det = 1;
            int order = mA.Size.X;

            for (int i = 0; i < order - 1; i++)
            {
                double[] masterRow = matrix.GetRow(i);
                det *= masterRow[i];
                if (det == 0) return 0;
                for (int t = i + 1; t < order; t++)
                {
                    double[] slaveRow = matrix.GetRow(t);
                    double[] tmp = MulArrayConst(masterRow, slaveRow[i] / masterRow[i]);
                    double[] source = matrix.GetRow(t);
                    matrix.SetRow(SubArray(source, tmp), t);
                }
            }
            det *= matrix[order - 1, order - 1];

            return det;
        }
        #endregion

        #region Inverse (вычисление обратной матрицы) - int, double
        /// <summary>
        /// Возвращает матрицу обратную данной. Обратная матрица существует только для квадратных, невырожденных, матриц.
        /// </summary>
        public static Matrix<int> Inverse(Matrix<int> mA)
        {
            if (mA.Size.X != mA.Size.Y) throw new ArgumentException("Обратная матрица существует только для квадратных, невырожденных, матриц.");

            Matrix<double> convertMatrix = ConvertM.ToDouble(mA);
            convertMatrix = Inverse(convertMatrix);

            return ConvertM.ToInt32(convertMatrix);
        }

        /// <summary>
        /// Возвращает матрицу обратную данной. Обратная матрица существует только для квадратных, невырожденных, матриц.
        /// </summary>
        /*public static Matrix<double> Inverse(Matrix<double> mA, uint round = 0)
        {
            if (mA.Size.X != mA.Size.Y) throw new ArgumentException("Обратная матрица существует только для квадратных, невырожденных, матриц.");
            Matrix<double> matrix = new Matrix<double>(mA.Size.X);
            double determinant = Determinant(mA);

            if (determinant == 0) return matrix; //Если определитель == 0 - матрица вырожденная

            for (int i = 0; i < mA.Size.X; i++)
            {
                for (int t = 0; t < mA.Size.Y; t++)
                {
                    Matrix<double> tmp = mA.Exclude(i, t);
                    matrix[t, i] = round == 0 ? (1 / determinant) * Determinant(tmp) : Math.Round(((1 / determinant) * Determinant(tmp)), (int)round, MidpointRounding.ToEven);
                }
            }
            return matrix;
        }*/
        #endregion

        #region Transpose (транспонирование матрицы) - int, double
        /// <summary>
        /// Возвращает транспонированную матрицу
        /// </summary>
        public static Matrix<int> Transpose(Matrix<int> mA)
        {
            Matrix<int> matrix = new Matrix<int>(mA.Size.Y, mA.Size.X);
            int t = 0;
            for (int i = 0; i < mA.Size.Y; i++)
                matrix.SetRow(mA.GetColumn(i), t++);

            return matrix;
        }

        /// <summary>
        /// Возвращает транспонированную матрицу
        /// </summary>
        public static Matrix<double> Transpose(Matrix<double> mA)
        {
            Matrix<double> matrix = new Matrix<double>(mA.Size.Y, mA.Size.X);
            int t = 0;
            for (int i = 0; i < mA.Size.Y; i++)
                matrix.SetRow(mA.GetColumn(i), t++);

            return matrix;
        }

        #endregion

        #region Rank (вычисление ранга матрицы методом приведения матрицы к треугольному виду) - int, double
        /// <summary>
        /// Возвращает ранг матрицы.
        /// </summary>
        public static int Rank(Matrix<double> mA)
        {
            Matrix<double> matrix = mA.Clone();
            int order = mA.Size.X;
            //Приводим к треугольному виду
            for (int i = 0; i < order - 1; i++)
            {
                double[] masterRow = matrix.GetRow(i);
                double[] slaveRow = null;
                for (int t = i + 1; t < order; t++)
                {
                    slaveRow = matrix.GetRow(t);
                    double[] tmp = MulArrayConst(masterRow, slaveRow[i] / masterRow[i]);
                    double[] source = matrix.GetRow(t);
                    matrix.SetRow(SubArray(source, tmp), t);
                }
            }
            //Вычитаем количество строк нулей из общего количества строк
            for (int i = 0; i < matrix.Size.X; i++)
            {
                double[] row = matrix.GetRow(i);
                int countZero = 1;
                for (int t = 0; t < row.Length; t++)
                {
                    if (row[t] != 0) break;
                    countZero++;
                }
                if (countZero == row.Length) order--;
            }

            return order;
        }

        /// <summary>
        /// Возвращает ранг матрицы.
        /// </summary>
        public static int Rank(Matrix<int> mA)
        {
            Matrix<double> convertMatrix = ConvertM.ToDouble(mA);
            return Rank(convertMatrix);
        }
        #endregion


        #region SupportMethods
        /// <summary>
        /// поэлементное вычитание массивов.
        /// </summary>
        public static double[] SubArray(double[] A, double[] B)
        {
            double[] ret = (double[])A.Clone();
            for (int i = 0; i < (A.Length > B.Length ? A.Length : B.Length); i++)
                ret[i] -= B[i];
            return ret;
        }
        /// <summary>
        /// поэлементное вычитание массивов.
        /// </summary>
        public static int[] SubArray(int[] A, int[] B)
        {
            int[] ret = (int[])A.Clone();
            for (int i = 0; i < (A.Length > B.Length ? A.Length : B.Length); i++)
                ret[i] -= B[i];
            return ret;
        }
        /// <summary>
        /// поэлементное деление массивов.
        /// </summary>
        public static double[] DivArrayConst(double[] array, double number)
        {
            double[] ret = (double[])array.Clone();
            for (int i = 0; i < ret.Length; i++)
                ret[i] /= number;
            return ret;
        }
        /// <summary>
        /// поэлементное деление массива на константу.
        /// </summary>
        public static int[] DivArrayConst(int[] array, double number)
        {
            int[] ret = (int[])array.Clone();
            for (int i = 0; i < ret.Length; i++)
                ret[i] = (int)(ret[i] / number);
            return ret;
        }
        /// <summary>
        /// поэлементное сложение массива с константой
        /// </summary>
        public static double[] SumArrayConst(double[] array, double number)
        {
            double[] ret = (double[])array.Clone();
            for (int i = 0; i < ret.Length; i++)
                ret[i] += number;
            return ret;
        }
        /// <summary>
        /// поэлементное сложение массива с константой
        /// </summary>
        public static int[] SumArrayConst(int[] array, double number)
        {
            int[] ret = (int[])array.Clone();
            for (int i = 0; i < ret.Length; i++)
                ret[i] = (int)(ret[i] + number);
            return ret;
        }
        /// <summary>
        /// Поэлементное умножение массивов
        /// </summary>
        public static double[] MulArrayConst(double[] array, double number)
        {
            double[] ret = (double[])array.Clone();
            for (int i = 0; i < ret.Length; i++)
                ret[i] *= number;
            return ret;
        }
        /// <summary>
        /// Поэлементное умножение массива на константу
        /// </summary>
        public static int[] MulArrayConst(int[] array, double number)
        {
            int[] ret = (int[])array.Clone();
            for (int i = 0; i < ret.Length; i++)
                ret[i] = (int)(ret[i] * number);
            return ret;
        }

        /// <summary>
        /// Поэлементоное сравнение массивов
        /// </summary>
        public static bool Equals(int[] A, int[] B)
        {
            if (A.Length != B.Length) return false;
            for (int i = 0; i < A.Length; i++)
                if (A[i] != B[i]) return false;
            return true;
 
        }
        #endregion
    }
}
