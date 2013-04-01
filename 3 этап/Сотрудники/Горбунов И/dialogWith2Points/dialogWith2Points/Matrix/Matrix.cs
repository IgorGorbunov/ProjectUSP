using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace ExMatrix
{
    /// <summary>
    /// Класс матрицы.
    /// </summary>
    public class Matrix<T> : IEnumerator<T>, IEnumerable<T>
    {
        /// <summary>
        /// Размерность матрицы. X - количество строк, Y - количество столбцов(полей)
        /// </summary>
        public SizeMatrix Size { get{} private set{} }

        /// <summary>
        /// Тип матрицы (квадратная, прямоугольная)
        /// </summary>
        public TypeMatrix TypeM { get { } private set { } }

        /// <summary>
        /// Размер матрицы создаваемой по умолчанию. Стартовое значение (5, 5)
        /// </summary>
        public static SizeMatrix DefaultSize = new SizeMatrix(5, 5);

        /// <summary>
        /// Массив представляющий матрицу. Использование не рекомендуется!!! (используйте индексаторы)
        /// </summary>
        public T[,] matrix = null;

        T current;
        int curIndex = -1;



        public Matrix()
        {
            InitMatrix(DefaultSize, TypeMatrix.Rectangle);
        }


        /// <summary>
        /// Конструктор класса. Если размерность не указана будет создана матрица размерностью DefaultSize. Если указан только один параметр будет создана квадратная матрица размерностью равной указанному параметру.
        /// </summary>
        public Matrix(int? x, int? y)
        {
            if (x != null && y == null) InitMatrix(new SizeMatrix((int)x, (int)x), TypeMatrix.Square);
            else if (x == null && y != null) InitMatrix(new SizeMatrix((int)y, (int)y), TypeMatrix.Square);
            else if (x == null && y == null) InitMatrix(DefaultSize, TypeMatrix.Rectangle);
            else
                InitMatrix(new SizeMatrix((int)x, (int)y), TypeMatrix.Rectangle);
        }

        public T this[int x, int y]
        {
            get { return matrix[x, y]; }
            set { matrix[x, y] = value; }
        }

        /// <summary>
        /// Инициализация матрицы. Вызывается при создании экземпляра класса. Ручной вызов нужен для изменения размера матрицы и заполнения значениями по-умолчанию.
        /// </summary>
        public void InitMatrix(SizeMatrix size, TypeMatrix type)
        {
            TypeM = type;
            this.Size = size;

            if (TypeM == TypeMatrix.RandomSquare || TypeM == TypeMatrix.Square)
                if (Size.X != Size.Y)
                    Size.Equalize();

            if (Size.X == Size.Y && (TypeM == TypeMatrix.RandomRectangle || TypeM == TypeMatrix.Rectangle))
                TypeM = TypeMatrix.Square;

            matrix = new T[size.X, size.Y];

            for (int i = 0; i < size.X; i++)
                for (int t = 0; t < size.Y; t++)
                    matrix[i, t] = default(T);

        }

        public override string ToString()
        {
            StringBuilder ret = new StringBuilder();
            if (this.Size == SizeMatrix.Zero) return ret.ToString();
            for (int i = 0; i < Size.X; i++)
            {
                for (int t = 0; t < Size.Y; t++)
                {
                    ret.Append(matrix[i, t]);
                    ret.Append("\t");
                }
                ret.Append("\n");
            }
            return ret.ToString();
        }

        /// <summary>
        /// Возвращает массив соответствующий указанной строке матрицы. Отсчет строк идет с 0.
        /// </summary>
        public T[] GetRow(int row)
        {
            if (row >= Size.X) throw new IndexOutOfRangeException("Индекс строки не принадлежит массиву.");
            T[] ret = new T[Size.Y];
            for (int i = 0; i < Size.Y; i++)
                ret[i] = (T)matrix[row, i];

            return ret;
        }

        /// <summary>
        /// Возвращает массив соответствующий указанному столбцу матрицы. Отсчет столбцов идет с 0.
        /// </summary>
        public T[] GetColumn(int column)
        {
            if (column >= Size.Y) throw new IndexOutOfRangeException("Индекс столбца(поля) не принадлежит массиву.");
            T[] ret = new T[Size.X];
            for (int i = 0; i < Size.X; i++)
                ret[i] = (T)matrix[i, column];

            return ret;
        }

        /// <summary>
        /// Заполняет указанную строку матрицы значениями из массива. Если размер массива и размер строки не совпадают, то строка будет - либо заполнена не полностью, либо "лишние" значения массива будут проигнорированы.
        /// </summary>
        public void SetRow(T[] rowValues, int row)
        {
            if (row >= Size.X) throw new IndexOutOfRangeException("Индекс строки не принадлежит массиву.");
            for (int i = 0; i < (Size.Y > rowValues.Length ? rowValues.Length : Size.Y); i++)
                matrix[row, i] = rowValues[i];
        }

        /// <summary>
        /// Заполняет указанный столбец значениями из массива. Если размеры столбца и массива не совпадают - столбец либо будет заполнен не полностью, либо "лишние" значения массива будут проигнорированы.
        /// </summary>
        public void SetColumn(T[] columnValues, int column)
        {
            if (column >= Size.Y) throw new IndexOutOfRangeException("Индекс столбца(поля) не принадлежит массиву.");
            for (int i = 0; i < (Size.X > columnValues.Length ? columnValues.Length : Size.X); i++)
                matrix[i, column] = columnValues[i];
        }

        #region Enumerabling...
        void IDisposable.Dispose()
        {

        }

        object IEnumerator.Current
        {
            get { return current; }
        }

        bool System.Collections.IEnumerator.MoveNext()
        {
            if (++curIndex >= Size.X * Size.Y)
            {
                ((IEnumerator)this).Reset();
                return false;
            }
            else
            {
                int x = curIndex / Size.Y;
                int y = curIndex - Size.Y * x;
                current = matrix[x, y];
            }
            return true;
        }

        void System.Collections.IEnumerator.Reset()
        {
            curIndex = -1;
        }

        T IEnumerator<T>.Current
        {
            get { return current; }
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this;
        }
        #endregion

        /// <summary>
        /// Создает копию матрицы
        /// </summary>
        public Matrix<T> Clone()
        {
            Matrix<T> ret = new Matrix<T>();
            ret.Size = new SizeMatrix(Size.X, Size.Y);
            ret.TypeM = TypeM;
            ret.matrix = (T[,])matrix.Clone();
            return ret;
        }

        /// <summary>
        /// Возвращает матрицу без указанных строки и столбца. Исходная матрица не изменяется.
        /// </summary>
        public Matrix<T> Exclude(int row, int column)
        {
            if (row > Size.X || column > Size.Y) throw new IndexOutOfRangeException("Строка или столбец не принадлежат матрице.");
            Matrix<T> ret = new Matrix<T>();
            ret.InitMatrix(new SizeMatrix(Size.X - 1, Size.Y - 1), TypeM);
            int offsetX = 0;
            for (int i = 0; i < Size.X; i++)
            {
                int offsetY = 0;
                if (i == row) { offsetX++; continue; }
                for (int t = 0; t < Size.Y; t++)
                {
                    if (t == column) { offsetY++; continue; }
                    ret[i - offsetX, t - offsetY] = this[i, t];
                }
            }
            return ret;
        }

        /// <summary>
        /// Возвращает матрицу без указанной строки. Исходная матрица не изменяется.
        /// </summary>
        public Matrix<T> ExcludeRow(params int[] row)
        {
            Matrix<T> ret = new Matrix<T>(Size.X - row.Length, Size.Y);
            int offset = 0;
            for (int i = 0; i < Size.X; i++)
            {
                if (!row.Contains(i))
                {
                    T[] rows = this.GetRow(i);
                    ret.SetRow(rows, i - offset);
                }
                else
                    offset++;
            }
            if (ret.Size.X == ret.Size.Y) ret.TypeM = TypeMatrix.Square;
            else
                ret.TypeM = TypeMatrix.Rectangle;

            return ret;
        }

        /// <summary>
        /// Возвращает матрицу без указанного столбца. Исходная матрица не изменяется.
        /// </summary>
        public Matrix<T> ExcludeColumn(params int[] column)
        {
            Matrix<T> ret = new Matrix<T>(Size.X, Size.Y - column.Length);
            int offset = 0;
            for (int i = 0; i < Size.Y; i++)
            {
                if (!column.Contains(i))
                {
                    T[] columns = this.GetColumn(i);
                    ret.SetColumn(columns, i - offset);
                }
                else
                    offset++;
            }
            if (ret.Size.X == ret.Size.Y) ret.TypeM = TypeMatrix.Square;
            else
                ret.TypeM = TypeMatrix.Rectangle;

            return ret;
        }

        /// <summary>
        /// Возвращает индекс максимального элемента в указанной строке.
        /// </summary>
        public int GetMaxRowIndex(int row)
        {
            if (row > Size.X) throw new IndexOutOfRangeException("Строка не принадлежит матрице");
            var array = this.GetRow(row);
            return Array.IndexOf(array, array.Max());
        }

        /// <summary>
        /// Возвращает индекс максимального элемента в указанном столбце.
        /// </summary>
        public int GetMaxColumnIndex(int column)
        {
            if (column > Size.Y) throw new IndexOutOfRangeException("Столбец не принадлежит матрице");
            var array = this.GetColumn(column);
            return Array.IndexOf(array, array.Max());

        }

        /// <summary>
        /// Возвращает индекс минимального элемента в указанной строке.
        /// </summary>
        public int GetMinRowIndex(int row)
        {
            if (row > Size.X) throw new IndexOutOfRangeException("Строка не принадлежит матрице");
            var array = this.GetRow(row);
            return Array.IndexOf(array, array.Min());

        }

        /// <summary>
        /// Возвращает индекс минимального элемента в указанном столбце.
        /// </summary>
        public int GetMinColumnIndex(int column)
        {
            if (column > Size.Y) throw new IndexOutOfRangeException("Столбец не принадлежит матрице");
            var array = this.GetColumn(column);
            return Array.IndexOf(array, array.Min());

        }

        /// <summary>
        /// Возвращает позицию максимального элемента в матрице.
        /// </summary>
        public Position GetMaxIndex()
        {
            Position ret = new Position();
            var array = this.ToArray();
            int index = Array.IndexOf(array, array.Max());
            ret.X = index / Size.Y;
            ret.Y = index - Size.Y * ret.X;
            return ret;
        }

        /// <summary>
        /// Возвращает позицию минимального элемента в матрице.
        /// </summary>
        /// <returns></returns>
        public Position GetMinIndex()
        {
            Position ret = new Position();
            var array = this.ToArray();
            int index = Array.IndexOf(array, array.Min());
            ret.X = index / Size.Y;
            ret.Y = index - Size.Y * ret.X;
            return ret;
        }

        /// <summary>
        /// Преобразует матрицу в одномерный массив.
        /// </summary>
        public T[] ToArray()
        {
            T[] ret = new T[Size.X * Size.Y];
            int i = -1;
            foreach (var item in this)
                ret[++i] = item;
            return ret;
        }

        /// <summary>
        /// Заполняет матрицу из одномерного массива.
        /// </summary>
        public void SetArray(T[] array)
        {
            int curIndex = 0;
            foreach (var item in array)
            {
                int x = curIndex / Size.Y;
                int y = curIndex - Size.Y * x;
                matrix[x, y] = array[curIndex++];
            }
        }

        /// <summary>
        /// Возвращает транспонированную матрицу. Исходная матрица не изменяется.
        /// </summary>
        public Matrix<T> Transpose
        {
            get 
            {
                Matrix<T> ret = new Matrix<T>(Size.Y, Size.X);
                int t = 0;
                for (int i = 0; i < Size.Y; i++)
                    ret.SetRow(GetColumn(i), t++);
                return ret;
            }
        }

        /// <summary>
        /// Меняет указанные строки местами. Исходная матрица не изменяется.
        /// </summary>
        public Matrix<T> MoveRow(int source, int target)
        {
            Matrix<T> ret = Clone();
            for (int i = 0; i < ret.Size.Y; i++)
            {
                //matrix[source, i] ^= matrix[target, i]; Неуниверсальный способ для целочисленных значений bool, int, long
                //matrix[target, i] ^= matrix[source, i];
                //matrix[source, i] ^= matrix[target, i];
                T val = ret[target, i];
                ret[target, i] = ret[source, i];
                ret[source, i] = val;
            }

            return ret;
        }

        /// <summary>
        /// Меняет местами указанные столбцы. Исходная матрица не изменяется.
        /// </summary>
        public Matrix<T> MoveColumn(int source, int target)
        {
            Matrix<T> ret = Clone();
            for (int i = 0; i < ret.Size.X; i++)
            {
                //matrix[i, source] ^= matrix[i, target]; Неуниверсальный способ для целочисленных значений bool, int, long
                //matrix[i, target] ^= matrix[i, source];
                //matrix[i, source] ^= matrix[i, target];
                T val = ret[i, target];
                ret[i, target] = ret[i, source];
                ret[i, source] = val;
            }
            return ret;
        }

        /// <summary>
        /// Меняет местами диагонали матрицы. Исходная матрица не изменяется.
        /// </summary>
        /// <returns></returns>
        public Matrix<T> ReversDiagonal()
        {
            Matrix<T> ret = Clone();
            if (ret.TypeM == TypeMatrix.RandomRectangle || ret.TypeM == TypeMatrix.Rectangle) throw new ArgumentException("Матрица не является квадратной.");

            int lenght = ret.Size.X;
            T[] tmp = new T[lenght];

            for (int i = 0; i < lenght; i++)
                tmp[i] = ret[i, i];

            for (int i = lenght - 1; i >= 0; i--)
            {
                ret[lenght - i - 1, lenght - i - 1] = ret[lenght - i - 1, i];
                ret[lenght - i - 1, i] = tmp[lenght - i - 1];
            }

            return ret;
        }
      
    }
}
