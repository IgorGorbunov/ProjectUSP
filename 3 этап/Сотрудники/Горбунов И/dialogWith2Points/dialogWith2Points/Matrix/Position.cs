
namespace ExMatrix
{
    /// <summary>
    /// Позиция элемента матрицы
    /// </summary>
    public struct Position
    {
        /// <summary>
        /// Номер строки
        /// </summary>
        public int X { get { } set { } }

        /// <summary>
        /// Номер столбца(поля)
        /// </summary>
        public int Y { get { } set { } }

        public override string ToString()
        {
            return string.Format("{0},{1}", X, Y);
        }
    }
}
