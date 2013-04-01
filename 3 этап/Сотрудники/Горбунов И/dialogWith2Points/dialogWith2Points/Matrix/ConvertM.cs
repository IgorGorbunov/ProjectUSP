
namespace ExMatrix
{
    /// <summary>
    /// Класс служит для преобразования Matrix<int> в Matrix<double> и обратно.
    /// </summary>
    public static class ConvertM
    {
        /// <summary>
        /// Возвращает Matrix<int> преобразованную в Matrix<double>
        /// </summary>
        public static Matrix<double> ToDouble(Matrix<int> mA)
        {
            Matrix<double> convertMatrix = new Matrix<double>(mA.Size.X, mA.Size.Y);
            double[] array = new double[mA.Size.X * mA.Size.Y];
            
            int i = 0;
            foreach (var item in mA)
                array[i++] = (double)item;

            convertMatrix.SetArray(array);
            return convertMatrix;
        }

        /// <summary>
        /// Возвращает Matrix<double> преобразованную в Matrix<int>
        /// </summary>
        public static Matrix<int> ToInt32(Matrix<double> mA)
        {
            Matrix<int> matrix = new Matrix<int>(mA.Size.X);
            int[] arrayint = new int[mA.Size.X * mA.Size.Y];

            int i = 0;
            foreach (var item in mA)
                arrayint[i++] = (int)item;

            matrix.SetArray(arrayint);

            return matrix;
        }
    }
}
