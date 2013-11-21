using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace algorithm
{
    /// <summary>
    /// Класс, реализующий алгоритм набора высоты и угла
    /// </summary>
    public class SelectionAlgorihtm
    {
        List<byte>[,] parents;
        byte[,] counts;
        int[] heights;
        int maxHeight;
        List<Element> elements;

        int MULTIPLY_CONST = 20;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="availableElements">Элементы за счет которых идет набор значения</param>
        /// <param name="maxHeight">Максимальное возможное значение</param>
        public SelectionAlgorihtm(List<Element> availableElements, int maxHeight)
            : this(availableElements, maxHeight, 20) { }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="availableElements">Элементы за счет которых идет набор значения</param>
        /// <param name="maxHeight">Максимальное возможное значение</param>
        /// <param name="multiplyConst">Константа, необходимая для представления всех значений в целых числах</param>
        public SelectionAlgorihtm(List<Element> availableElements, int maxHeight, int multiplyConst)
        {
            MULTIPLY_CONST = multiplyConst;
            maxHeight *= MULTIPLY_CONST;
            this.maxHeight = maxHeight;
            elements = availableElements;
            parents = new List<byte>[elements.Count + 1, maxHeight + 1];
            counts = new byte[elements.Count + 1, maxHeight + 1];
            heights = new int[elements.Count];
            for (int i = 0; i <= elements.Count; ++i)
            {
                for (int j = 0; j <= maxHeight; ++j)
                {
                    counts[i, j] = 100;
                }
            }
            int index = 0;
            foreach (Element el in availableElements)
            {
                heights[index++] = (int)(el.Height * MULTIPLY_CONST);
            }
        }

        /// <summary>
        /// Решение для заданной высоты/угла
        /// </summary>
        /// <param name="needHeight">Требуемое значение параметра</param>
        /// <param name="ignoreInStock">Игнорировать наличие на складе</param>
        /// <returns>Найденное решение</returns>
        public Solution solve(double needHeight, bool ignoreInStock)
        {
            int H = (int)(needHeight * MULTIPLY_CONST) + 20;
            counts[0, 0] = 0;
            for (int i = 0; i < heights.Length; ++i)
            {
                for (int h = 0; h <= H; ++h)
                {
                    if (counts[i, h] > 99) continue;
                    int limit = 8;
                    if (!ignoreInStock)
                    {
                        limit = Math.Min(limit, elements[i].StockCount);
                    }
                    for (byte k = 0; k <= limit; ++k)
                    {
                        int nH = k * heights[i] + h;
                        if (nH > H) break;
                        int newValue = counts[i, h] + k;
                        if (counts[i + 1, nH] > newValue)
                        {
                            parents[i + 1, nH] = new List<byte>();
                            parents[i + 1, nH].Add(k);
                            counts[i + 1, nH] = (byte)(newValue);
                        }
                        else if (newValue == counts[i + 1, nH])
                        {
                            parents[i + 1, nH].Add(k);
                        }
                    }
                }
            }
            H -= 20;
            double mainAnswer = (counts[heights.Length, H] < 100 ? needHeight : -1.0);
            double lower = -1.0, upper = -1.0;
            for (int curH = H + 1; curH <= maxHeight; ++curH)
            {
                if (counts[heights.Length, curH] < 100)
                {
                    upper = ((double)curH) / MULTIPLY_CONST;
                    break;
                }
            }
            for (int curH = H - 1; curH > 0; --curH)
            {
                if (counts[heights.Length, curH] < 100)
                {
                    lower = ((double)curH) / MULTIPLY_CONST;
                    break;
                }
            }
            return new Solution(this, mainAnswer, lower, upper);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="h">Требуемое значение параметра</param>
        /// <returns>Количество элементов, которым можно набрать значение h</returns>
        public byte getElementCount(double h)
        {
            return counts[heights.Length, (int)(h * MULTIPLY_CONST)];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="h">Требуемое значение параметра</param>
        /// <param name="solutionNumber">Номер решение (нумеруются с 0)</param>
        /// <returns>Список элементов, которым можно набрать значение h</returns>
        public Dictionary<Element, byte> getSolution(double h, int solutionNumber)
        {
            int H = (int)(h * MULTIPLY_CONST);
            solutionCount = -1;
            if (counts[elements.Count, H] < 100)
            {
                return getAnswer(H, elements.Count, solutionNumber);
            }
            else
            {
                return null;
            }
        }

        private Dictionary<Element, byte> getAnswer(int h, int column, int solutionNumber)
        {
            if (h == 0)
            {
                ++solutionCount;
                return (solutionCount == solutionNumber ? new Dictionary<Element, byte>() : null);
            }
            foreach (byte parent in parents[column, h])
            {
                Dictionary<Element, byte> result = getAnswer(h - parent * heights[column - 1], column - 1, solutionNumber);
                if (result != null)
                {
                    if (parent > 0)
                    {
                        result[elements[column - 1]] = parent;
                    }
                    return result;
                }
            }
            return null;
        }

        int solutionCount = 0;
    }

    /// <summary>
    /// Класс, обеспечивающий получение решения для набора угла
    /// </summary>
    static class AngleSolver
    {
        static SelectionAlgorihtm[] smallAngleTable = new SelectionAlgorihtm[2];
        static Dictionary<String, BigAngleGost>[] bigAngleGosts = new Dictionary<string,BigAngleGost>[2];
        static AngleSolver() {
            for (int i = 0; i < 2; ++i)
            {
                smallAngleTable[i] = new SelectionAlgorihtm(
                    DatabaseUtils.loadAngleElement(ElementType.SmallAngle, false, i),
                    10000, 1);
                smallAngleTable[i].solve(6000, false);
                bigAngleGosts[i] = DatabaseUtils.loadBigAngleElement(false, i);
            }
        }

        public static int maxElementInSolution = 4;

        /// <summary>
        /// Список решений для каждого ГОСТа
        /// </summary>
        /// <param name="angle">Требуемый угол</param>
        /// <param name="ignoreInStock">Игнорировать наличие на складе</param>
        /// <param name="katalog">Каталог УСП</param>
        /// <returns>Список решений для каждого ГОСТа</returns>
        public static Dictionary<String, AngleSolution> solve(int angle, bool ignoreInStock, int katalog) 
        {
            int lowerBound = (angle > 90 * 60 ? 2 : -1);
            Dictionary<String, AngleSolution> result = new Dictionary<string, AngleSolution>();
            foreach (string gost in bigAngleGosts[katalog].Keys)
            {
                BigAngleGost bigAngleGost = bigAngleGosts[katalog][gost];
                if (bigAngleGost.type >= lowerBound && bigAngleGost.minimalAngle > 0 && bigAngleGost.minimalAngle <= angle)
                {
                    Element baseElement = null;
                    int elementCount = 0;
                    foreach (Element element in bigAngleGost.elements)
                    {
                        if (element.Height <= angle)
                        {
                            int h = (int)(angle - element.Height);
                            int cn = smallAngleTable[katalog].getElementCount(h);
                            if (cn > maxElementInSolution) continue;
                            if (baseElement == null || elementCount > cn)
                            {
                                elementCount = cn;
                                baseElement = element;
                            }
                        }
                    }
                    if (baseElement != null)
                    {
                        result[gost] = new AngleSolution(elementCount, baseElement,
                                            smallAngleTable[katalog].solve(angle - baseElement.Height, ignoreInStock), gost);
                    }
                }
            }
            if (smallAngleTable[katalog].getElementCount(angle) <= maxElementInSolution)
            {
                result["Без большого элемента"] = new AngleSolution(smallAngleTable[katalog].getElementCount(angle), null,
                                            smallAngleTable[katalog].solve(angle, ignoreInStock), "Без большого элемента");
            }
            return result;
        }

        /// <summary>
        /// Получить отсортированный в порядке не убывания количества список решений
        /// </summary>
        /// <param name="solution">Список решений для каждого ГОСТа</param>
        /// <returns>Отсортированный в порядке не убывания количества список решений</returns>
        public static List<AngleSolution> GetOrderedList(Dictionary<String, AngleSolution> solution)
        {
            List<AngleSolution> result = new List<AngleSolution>(solution.Values);
            result.Sort();
            return result;
        }
    }

    /// <summary>
    /// Решение для набора заданного значения параметра
    /// </summary>
    public class Solution
    {
        private SelectionAlgorihtm source;
        public double mainAnswer;
        public double lowerBound;
        public double upperBound;

        public Solution(SelectionAlgorihtm source, double main, double lower, double upper)
        {
            this.source = source;
            mainAnswer = main;
            lowerBound = lower;
            upperBound = upper;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="solutionNumber">Номер решения</param>
        /// <returns>Список элементов, которым можно набрать требуемое</returns>
        public Dictionary<Element, byte> getMainSolution(int solutionNumber)
        {
            if (mainAnswer == -1.0) return null;
            return source.getSolution(mainAnswer, solutionNumber);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="solutionNumber">Номер решения</param>
        /// <returns>Список элементов, которым можно набрать ближайшее снизу значение параметра</returns>
        public Dictionary<Element, byte> getLowerSolution(int solutionNumber)
        {
            if (lowerBound == -1.0) return null;
            return source.getSolution(lowerBound, solutionNumber);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="solutionNumber">Номер решения</param>
        /// <returns>Список элементов, которым можно набрать ближайшее сверху значение параметра</returns>
        public Dictionary<Element, byte> getUpperSolution(int solutionNumber)
        {
            if (upperBound == -1.0) return null;
            return source.getSolution(upperBound, solutionNumber);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="h">Требуемое значение параметра</param>
        /// <returns>Количество элементов, которым можно набрать значение h</returns>
        public byte getElementCount(double h)
        {
            return source.getElementCount(h);
        }
    }

    /// <summary>
    /// Решение для набора угла
    /// </summary>
    public class AngleSolution : IComparable<AngleSolution>
    {
        /// <summary>
        /// Базовый большой элемент
        /// </summary>
        public Element baseElement;

        public string Gost;

        /// <summary>
        /// Требуемое количеств малых элементов
        /// </summary>
        public int count;

        /// <summary>
        /// Решение для набора малых элементов
        /// </summary>
        public Solution solution;

        public int CompareTo(AngleSolution other)
        {
            if (other == null)
                return 1;
            return count.CompareTo(other.count);
        }

        public AngleSolution(int count, Element baseElement, Solution solution, string gost)
        {
            this.count = count;
            this.Gost = gost;
            this.baseElement = baseElement;
            this.solution = solution;
        }
    }

    /// <summary>
    /// Гост на большие углы
    /// </summary>
    public class BigAngleGost
    {
        /// <summary>
        /// Наименование ГОСТа
        /// </summary>
        public string Gost;
        /// <summary>
        /// Большой элемент с минимальным углом
        /// </summary>
        public int minimalAngle;
        /// <summary>
        /// Тип госта (REAL_ANGLE_TYPE)
        /// </summary>
        public int type;
        /// <summary>
        /// Список элементов, принадлежащих этому ГОСТу
        /// </summary>
        public List<Element> elements;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="gost">Наименование ГОСТа</param>
        /// <param name="minAngle">Большой элемент с минимальным углом</param>
        /// <param name="type">Тип госта (REAL_ANGLE_TYPE)</param>
        /// <param name="elements">Список элементов, принадлежащих этому ГОСТу</param>
        public BigAngleGost(string gost, int minAngle, int type, List<Element> elements)
        {
            this.Gost = gost;
            this.minimalAngle = minAngle;
            this.elements = elements;
            this.type = type;
        }
    }

    /// <summary>
    /// Конвертор для угла
    /// </summary>
    public static class AngleConverter
    {
        /// <summary>
        /// Конвертирование строки в числовое значение - количество минут 
        /// </summary>
        /// <param name="s">Строка вида xx°xx'</param>
        /// <returns>Количество минут в заданном угле</returns>
        public static int StringToInt(string s)
        {
            string[] t = s.Split('°');
            int res = 0;
            if (t.Length > 1)
            {
                res = int.Parse(t[0].Trim()) * 60;
                if (!String.IsNullOrEmpty(t[1])) res += int.Parse(t[1].Split('\'')[0].Trim());
            }
            else
            {
                res = int.Parse(t[0].Split('\'')[0].Trim());
            }
            return res;
        }

        /// <summary>
        /// Конвертирование количетсво минут в строку
        /// </summary>
        /// <param name="s">Количество минут в угле</param>
        /// <returns>Строку вида xx°xx'</returns>
        public static string IntToString(int value)
        {
            int degree = value / 60;
            int minutes = value % 60;
            string min = "00'";

            return (degree > 0 ? (degree + "°") : "") +
                minutes.ToString("D2") + "'";
        }
    }

    /// <summary>
    /// Элемент для набора высоты и угла
    /// </summary>
    public class Element
    {
        /// <summary>
        /// Высота или угол
        /// </summary>
        public double Height;
        
        /// <summary>
        /// Доступное количество на складе
        /// </summary>
        public int StockCount;

        /// <summary>
        /// Имя элемента
        /// </summary>
        public string ElementName;

        /// <summary>
        /// Обозначение
        /// </summary>
        public string Obozn;

        public Element(string name, string obozn, double height, int stockCount)
        {
            ElementName = name;
            Height = height;
            StockCount = stockCount;
            this.Obozn = obozn;
        }
    }

    /// <summary>
    /// Тип элемента
    /// </summary>
    public enum ElementType
    {
        None,
        HeightBySquare,
        HeightByRectangle,
        HeightByCircle,
        BigAngle,
        SmallAngle
    }

    /// <summary>
    /// Информация о типе элемента
    /// </summary>
    public static class ElementTypeInfo
    {
        private static Dictionary<ElementType, String> tableNames;
        private static Dictionary<ElementType, String> propertyNames;

        static ElementTypeInfo()
        {
            tableNames = new Dictionary<ElementType, string>();
            propertyNames = new Dictionary<ElementType, string>();
            tableNames[ElementType.HeightByCircle] = "USP_VISOTA_1OTV_KRUG";
            tableNames[ElementType.HeightBySquare] = "USP_VISOTA_1OTV_KV";
            tableNames[ElementType.HeightByRectangle] = "USP_VISOTA_1OTV_PR";
            tableNames[ElementType.BigAngle] = "USP_ANGLE_BIG";
            tableNames[ElementType.SmallAngle] = "USP_ANGLE_SMALL";
            propertyNames[ElementType.HeightByCircle] = "Круглые элементы";
            propertyNames[ElementType.HeightBySquare] = "Квадратные элементы";
            propertyNames[ElementType.HeightByRectangle] = "Прямоугольные элементы";
            propertyNames[ElementType.BigAngle] = "Большие элементы для набора угра";
            propertyNames[ElementType.SmallAngle] = "Малые элементы для набора угла";
            propertyNames[ElementType.None] = "Не выбрано";
        }

        public static string getTableName(ElementType type)
        {
            return tableNames[type];
        }

        public static string getPropertyName(ElementType type)
        {
            return propertyNames[type];
        }
    }

    /// <summary>
    /// Класс для выборки из БД элементов для набора высоты и угла
    /// </summary>
    public class DatabaseUtils
    {
        /// <summary>
        /// Список элементов для набора высоты, удовлетворяющих заданным параметрам
        /// </summary>
        /// <param name="elementType">Тип элемента</param>
        /// <param name="ignoreInStock">Игнорировать наличие на складе</param>
        /// <param name="KatologUsp">Номер каталога УСП - 0/1</param>
        /// <returns>Список элементов для набора высоты, удовлетворяющих заданным параметрам</returns>
        public static List<Element> loadFromDb(ElementType elementType, bool ignoreInStock, int KatologUsp)
        {
            List<Element> result = new List<Element>();
            DataSet elementLinks = SqlOracle1.getDS("SELECT * FROM " + ElementTypeInfo.getTableName(elementType));
            foreach (DataRow row in elementLinks.Tables[0].Rows)
            {
                string paramH = row["PARAM_H"].ToString();
                DataSet subElements = SqlOracle1.getDS("SELECT NAME, OBOZN, " + paramH + ", NALICHI FROM DB_DATA"
                    + " WHERE GOST='" + row["GOST"] + "'"
                    + (ignoreInStock ? "" : " AND NALICHI > 0 ")
                    + " AND KATALOG_USP='" + KatologUsp + "'");
                foreach (DataRow elementRow in subElements.Tables[0].Rows)
                {
                    result.Add(new Element(elementRow["NAME"].ToString(), elementRow["OBOZN"].ToString(),
                        double.Parse(elementRow[paramH].ToString()), int.Parse(elementRow["NALICHI"].ToString())));
                    //Trace.WriteLine(double.Parse(elementRow[paramH].ToString()));
                }
            }
            return result;
        }

        /// <summary>
        /// Список малых элементов для набора угла, удовлетворяющих заданным параметрам
        /// </summary>
        /// <param name="elementType">Тип элемента</param>
        /// <param name="ignoreInStock">Игнорировать наличие на складе</param>
        /// <param name="KatologUsp">Номер каталога УСП - 0/1</param>
        /// <returns>Список малых элементов для набора угла, удовлетворяющих заданным параметрам</returns>
        public static List<Element> loadAngleElement(ElementType elementType, bool ignoreInStock, int KatologUsp)
        {
            List<Element> result = new List<Element>();
            DataSet elementLinks = SqlOracle1.getDS("SELECT * FROM " + ElementTypeInfo.getTableName(elementType));
            foreach (DataRow row in elementLinks.Tables[0].Rows)
            {
                DataSet subElements = SqlOracle1.getDS("SELECT NAME, OBOZN, A, NALICHI FROM DB_DATA"
                    + " WHERE GOST='" + row["GOST"] + "'"
                    + (ignoreInStock ? "" : " AND NALICHI > 0 AND NALICHI <> 999")
                    + " AND KATALOG_USP='" + KatologUsp + "'");
                foreach (DataRow elementRow in subElements.Tables[0].Rows)
                {
                    result.Add(new Element(elementRow["NAME"].ToString(), elementRow["OBOZN"].ToString(),
                        AngleConverter.StringToInt(elementRow["A"].ToString()), int.Parse(elementRow["NALICHI"].ToString())));
                }
            }
            return result;
        }

        /// <summary>
        /// Список больших элементов для набора угла, удовлетворяющих заданным параметрам
        /// </summary>
        /// <param name="ignoreInStock">Игнорировать наличие на складе</param>
        /// <param name="KatologUsp">Номер каталога УСП - 0/1</param>
        /// <returns>Список больших элементов для набора угла, удовлетворяющих заданным параметрам</returns>
        public static Dictionary<String, BigAngleGost> loadBigAngleElement(bool ignoreInStock, int KatologUsp)
        {
            Dictionary<String, BigAngleGost> result = new Dictionary<string, BigAngleGost>();
            DataSet elementLinks = SqlOracle1.getDS("SELECT * FROM " + ElementTypeInfo.getTableName(ElementType.BigAngle));
            foreach (DataRow row in elementLinks.Tables[0].Rows)
            {
                int type = int.Parse(row["REAL_ANGLE_TYPE"].ToString());
                string gost = row["GOST"].ToString();
                DataSet subElements = SqlOracle1.getDS("SELECT NAME, OBOZN, A, NALICHI FROM DB_DATA"
                    + " WHERE GOST='" + gost + "'"
                    + (ignoreInStock ? "" : " AND NALICHI > 0 AND NALICHI <> 999")
                    + " AND KATALOG_USP='" + KatologUsp + "'");
                List<Element> subList = new List<Element>();
                int minAngle = -1;
                foreach (DataRow elementRow in subElements.Tables[0].Rows)
                {
                    int angle = AngleConverter.StringToInt(elementRow["A"].ToString());
                    if (type == 1) angle = 90 * 60 - angle;
                    else if (type == 2) angle = 90 * 60 + angle;
                    if (minAngle == -1 || angle < minAngle)
                    {
                        minAngle = angle;
                    }
                    Element element = new Element(elementRow["NAME"].ToString(), elementRow["OBOZN"].ToString(),
                        angle, int.Parse(elementRow["NALICHI"].ToString()));
                    if (gost.EndsWith("15255-70"))
                    {
                        char c = element.Obozn[element.Obozn.Length - 2];
                        if (c < '7' || c > '8')
                        {
                            continue;
                        }
                    }
                    subList.Add(element);
                }
                result[gost] = new BigAngleGost(gost, minAngle, type, subList);
            }
            return result;
        }
    }    
}
