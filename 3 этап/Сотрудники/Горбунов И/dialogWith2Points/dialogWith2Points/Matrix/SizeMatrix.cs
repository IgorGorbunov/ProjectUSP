
namespace ExMatrix
{
    /// <summary>
    /// Размер матрицы.
    /// </summary>
    public class SizeMatrix
    {
        /// <summary>
        /// Количество строк
        /// </summary>
        public int X { get { } private set { } }
        /// <summary>
        /// Количество столбцов
        /// </summary>
        public int Y { get { } private set { } }
        /// <summary>
        /// Нулевая матрица
        /// </summary>
        public static SizeMatrix Zero { get { return new SizeMatrix(0, 0); } }

        /// <summary>
        /// Инициализация размера. 
        /// </summary>
        /// <param name="X">Длина матрицы</param>
        /// <param name="Y">Высота матрицы</param>
        public SizeMatrix(int X, int Y)
        {
            //if (X < 2) X = 2;
            //if (Y < 2) Y = 2;
            this.X = X;
            this.Y = Y;
        }
        /// <summary>
        /// Уравнивнивание размера. 
        /// <i>Требуется если задана квадратная матрица, а X и Y различаются</i>
        /// </summary>
        /// <param name="Direction">Направление уравнивания. Если true, то X = Y, если false, то Y = X</param>
        /*public void Equalize(bool Direction = false)
        {
            if (!Direction) X = Y;
            else
                Y = X;
        }*/

        public static bool operator ==(SizeMatrix s1, SizeMatrix s2)
        {
            if (s1.X != s2.X) return false;
            if (s1.Y != s2.Y) return false;
            return true;
        }

        public static bool operator !=(SizeMatrix s1, SizeMatrix s2)
        {
            if (s1.X == s2.X) return false;
            if (s1.Y == s2.Y) return false;
            return true;
        }

        public override int GetHashCode()
        {
            return ((X * Y) + X) - X % Y;
        }

        public override bool Equals(object obj)
        {
            SizeMatrix size = obj as SizeMatrix;
            if (this.X != size.X) return false;
            if (this.Y != size.Y) return false;
            return true;
        }

      
    }
}
