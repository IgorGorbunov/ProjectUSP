using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace algorithm
{
    /// <summary>
    /// �����, ����������� �������� ������ ������ � ����
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
        /// �����������
        /// </summary>
        /// <param name="availableElements">�������� �� ���� ������� ���� ����� ��������</param>
        /// <param name="maxHeight">������������ ��������� ��������</param>
        public SelectionAlgorihtm(List<Element> availableElements, int maxHeight)
            : this(availableElements, maxHeight, 20) { }

        /// <summary>
        /// �����������
        /// </summary>
        /// <param name="availableElements">�������� �� ���� ������� ���� ����� ��������</param>
        /// <param name="maxHeight">������������ ��������� ��������</param>
        /// <param name="multiplyConst">���������, ����������� ��� ������������� ���� �������� � ����� ������</param>
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
        /// ������� ��� �������� ������/����
        /// </summary>
        /// <param name="needHeight">��������� �������� ���������</param>
        /// <param name="ignoreInStock">������������ ������� �� ������</param>
        /// <returns>��������� �������</returns>
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
        /// <param name="h">��������� �������� ���������</param>
        /// <returns>���������� ���������, ������� ����� ������� �������� h</returns>
        public byte getElementCount(double h)
        {
            return counts[heights.Length, (int)(h * MULTIPLY_CONST)];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="h">��������� �������� ���������</param>
        /// <param name="solutionNumber">����� ������� (���������� � 0)</param>
        /// <returns>������ ���������, ������� ����� ������� �������� h</returns>
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
    /// �����, �������������� ��������� ������� ��� ������ ����
    /// </summary>
    static class AngleSolver
    {
        static SelectionAlgorihtm[,] smallAngleTable = new SelectionAlgorihtm[2,2];
        static Dictionary<String, BigAngleGost>[,] bigAngleGosts = new Dictionary<string,BigAngleGost>[2,2];
        static AngleSolver() {
            for (int i = 0; i < 2; ++i)
            {
                for(int j = 0; j < 2; ++j) {
                    bool ignore = j > 0;
                    smallAngleTable[i,j] = new SelectionAlgorihtm(
                        DatabaseUtils.loadAngleElement(ElementType.SmallAngle, ignore, i),
                        10000, 1);
                    smallAngleTable[i,j].solve(6000, ignore);
                    bigAngleGosts[i,j] = DatabaseUtils.loadBigAngleElement(ignore, i);
                }
            }
        }

        public static int maxElementInSolution = 4;

        /// <summary>
        /// ������ ������� ��� ������� �����
        /// </summary>
        /// <param name="angle">��������� ����</param>
        /// <param name="ignoreInStock">������������ ������� �� ������</param>
        /// <param name="katalog">������� ���</param>
        /// <returns>������ ������� ��� ������� �����</returns>
        public static Dictionary<String, AngleSolution> solve(int angle, bool ignoreInStock, int katalog) 
        {
            int ignore = ignoreInStock ? 1 : 0;
            int lowerBound = (angle > 90*60 ? 2 : -1);
            Dictionary<String, AngleSolution> result = new Dictionary<string, AngleSolution>();
            foreach (string gost in bigAngleGosts[katalog,ignore].Keys)
            {
                BigAngleGost bigAngleGost = bigAngleGosts[katalog,ignore][gost];
                if (bigAngleGost.type >= lowerBound && bigAngleGost.minimalAngle > 0 && bigAngleGost.minimalAngle <= angle)
                {
                    Element baseElement = null;
                    int elementCount = 0;
                    foreach (Element element in bigAngleGost.elements)
                    {
                        if (element.Height <= angle)
                        {
                            int h = (int)(angle - element.Height);
                            int cn = smallAngleTable[katalog,ignore].getElementCount(h);
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
                                            smallAngleTable[katalog,ignore].solve(angle - baseElement.Height, ignoreInStock), gost);
                    }
                }
            }
            if (smallAngleTable[katalog,ignore].getElementCount(angle) <= maxElementInSolution)
            {
                string smallGost = (katalog == 0 ? "14449-69" : "15229-70");
                result[smallGost] = new AngleSolution(smallAngleTable[katalog, ignore].getElementCount(angle), null,
                                            smallAngleTable[katalog, ignore].solve(angle, ignoreInStock), smallGost);
            }
            return result;
        }

        /// <summary>
        /// �������� ��������������� � ������� �� �������� ���������� ������ �������
        /// </summary>
        /// <param name="solution">������ ������� ��� ������� �����</param>
        /// <returns>��������������� � ������� �� �������� ���������� ������ �������</returns>
        public static List<AngleSolution> GetOrderedList(Dictionary<String, AngleSolution> solution)
        {
            List<AngleSolution> result = new List<AngleSolution>(solution.Values);
            result.Sort();
            return result;
        }
    }

    /// <summary>
    /// ������� ��� ������ ��������� �������� ���������
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
        /// <param name="solutionNumber">����� �������</param>
        /// <returns>������ ���������, ������� ����� ������� ���������</returns>
        public Dictionary<Element, byte> getMainSolution(int solutionNumber)
        {
            if (mainAnswer == -1.0) return null;
            return source.getSolution(mainAnswer, solutionNumber);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="solutionNumber">����� �������</param>
        /// <returns>������ ���������, ������� ����� ������� ��������� ����� �������� ���������</returns>
        public Dictionary<Element, byte> getLowerSolution(int solutionNumber)
        {
            if (lowerBound == -1.0) return null;
            return source.getSolution(lowerBound, solutionNumber);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="solutionNumber">����� �������</param>
        /// <returns>������ ���������, ������� ����� ������� ��������� ������ �������� ���������</returns>
        public Dictionary<Element, byte> getUpperSolution(int solutionNumber)
        {
            if (upperBound == -1.0) return null;
            return source.getSolution(upperBound, solutionNumber);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="h">��������� �������� ���������</param>
        /// <returns>���������� ���������, ������� ����� ������� �������� h</returns>
        public byte getElementCount(double h)
        {
            return source.getElementCount(h);
        }
    }

    /// <summary>
    /// ������� ��� ������ ����
    /// </summary>
    public class AngleSolution : IComparable<AngleSolution>
    {
        /// <summary>
        /// ������� ������� �������
        /// </summary>
        public Element baseElement;

        public string Gost;

        /// <summary>
        /// ��������� ��������� ����� ���������
        /// </summary>
        public int count;

        /// <summary>
        /// ������� ��� ������ ����� ���������
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
    /// ���� �� ������� ����
    /// </summary>
    public class BigAngleGost
    {
        /// <summary>
        /// ������������ �����
        /// </summary>
        public string Gost;
        /// <summary>
        /// ������� ������� � ����������� �����
        /// </summary>
        public int minimalAngle;
        /// <summary>
        /// ��� ����� (REAL_ANGLE_TYPE)
        /// </summary>
        public int type;
        /// <summary>
        /// ������ ���������, ������������� ����� �����
        /// </summary>
        public List<Element> elements;

        /// <summary>
        /// �����������
        /// </summary>
        /// <param name="gost">������������ �����</param>
        /// <param name="minAngle">������� ������� � ����������� �����</param>
        /// <param name="type">��� ����� (REAL_ANGLE_TYPE)</param>
        /// <param name="elements">������ ���������, ������������� ����� �����</param>
        public BigAngleGost(string gost, int minAngle, int type, List<Element> elements)
        {
            this.Gost = gost;
            this.minimalAngle = minAngle;
            this.elements = elements;
            this.type = type;
        }
    }

    /// <summary>
    /// ��������� ��� ����
    /// </summary>
    public static class AngleConverter
    {
        /// <summary>
        /// ��������������� ������ � �������� �������� - ���������� ����� 
        /// </summary>
        /// <param name="s">������ ���� xx�xx'</param>
        /// <returns>���������� ����� � �������� ����</returns>
        public static int StringToInt(string s)
        {
            string[] t = s.Split('�');
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
        /// ��������������� ���������� ����� � ������
        /// </summary>
        /// <param name="s">���������� ����� � ����</param>
        /// <returns>������ ���� xx�xx'</returns>
        public static string IntToString(int value)
        {
            int degree = value / 60;
            int minutes = value % 60;
            string min = "00'";

            return (degree > 0 ? (degree + "�") : "") +
                minutes.ToString("D2") + "'";
        }
    }

    /// <summary>
    /// ������� ��� ������ ������ � ����
    /// </summary>
    public class Element
    {
        /// <summary>
        /// ������ ��� ����
        /// </summary>
        public double Height;
        
        /// <summary>
        /// ��������� ���������� �� ������
        /// </summary>
        public int StockCount;

        /// <summary>
        /// ��� ��������
        /// </summary>
        public string ElementName;

        /// <summary>
        /// �����������
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
    /// ��� ��������
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
    /// ���������� � ���� ��������
    /// </summary>
    public static class ElementTypeInfo
    {
        private static Dictionary<ElementType, String> tableNames;
        private static Dictionary<ElementType, String> propertyNames;

        static ElementTypeInfo()
        {
            tableNames = new Dictionary<ElementType, string>();
            propertyNames = new Dictionary<ElementType, string>();
            tableNames[ElementType.HeightByCircle] = "KTC.USP_VISOTA_1OTV_KRUG";
            tableNames[ElementType.HeightBySquare] = "KTC.USP_VISOTA_1OTV_KV";
            tableNames[ElementType.HeightByRectangle] = "KTC.USP_VISOTA_1OTV_PR";
            tableNames[ElementType.BigAngle] = "KTC.USP_ANGLE_BIG";
            tableNames[ElementType.SmallAngle] = "KTC.USP_ANGLE_SMALL";
            propertyNames[ElementType.HeightByCircle] = "������� ��������";
            propertyNames[ElementType.HeightBySquare] = "���������� ��������";
            propertyNames[ElementType.HeightByRectangle] = "������������� ��������";
            propertyNames[ElementType.BigAngle] = "������� �������� ��� ������ ����";
            propertyNames[ElementType.SmallAngle] = "����� �������� ��� ������ ����";
            propertyNames[ElementType.None] = "�� �������";
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
    /// ����� ��� ������� �� �� ��������� ��� ������ ������ � ����
    /// </summary>
    public class DatabaseUtils
    {
        /// <summary>
        /// ������ ��������� ��� ������ ������, ��������������� �������� ����������
        /// </summary>
        /// <param name="elementType">��� ��������</param>
        /// <param name="ignoreInStock">������������ ������� �� ������</param>
        /// <param name="KatologUsp">����� �������� ��� - 0/1</param>
        /// <returns>������ ��������� ��� ������ ������, ��������������� �������� ����������</returns>
        public static List<Element> loadFromDb(ElementType elementType, bool ignoreInStock, int KatologUsp)
        {
            List<Element> result = new List<Element>();
            DataSet elementLinks = SqlOracle1.getDS("SELECT * FROM " + ElementTypeInfo.getTableName(elementType));
            foreach (DataRow row in elementLinks.Tables[0].Rows)
            {
                string paramH = row["PARAM_H"].ToString();
                DataSet subElements = SqlOracle1.getDS("SELECT NAME, OBOZN, " + paramH + ", NALICHI FROM KTC.DB_DATA"
                    + " WHERE GOST='" + row["GOST"] + "'"
                    + (ignoreInStock ? "" : " AND NALICHI > 0 AND NALICHI <> 999")
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
        /// ������ ����� ��������� ��� ������ ����, ��������������� �������� ����������
        /// </summary>
        /// <param name="elementType">��� ��������</param>
        /// <param name="ignoreInStock">������������ ������� �� ������</param>
        /// <param name="KatologUsp">����� �������� ��� - 0/1</param>
        /// <returns>������ ����� ��������� ��� ������ ����, ��������������� �������� ����������</returns>
        public static List<Element> loadAngleElement(ElementType elementType, bool ignoreInStock, int KatologUsp)
        {
            List<Element> result = new List<Element>();
            DataSet elementLinks = SqlOracle1.getDS("SELECT * FROM " + ElementTypeInfo.getTableName(elementType));
            foreach (DataRow row in elementLinks.Tables[0].Rows)
            {
                DataSet subElements = SqlOracle1.getDS("SELECT NAME, OBOZN, A, NALICHI FROM KTC.DB_DATA"
                    + " WHERE GOST='" + row["GOST"] + "'"
                    + (ignoreInStock ? "" : " AND NALICHI > 0 AND NALICHI <> 999")
                    + " AND KATALOG_USP='" + KatologUsp + "'");
                foreach (DataRow elementRow in subElements.Tables[0].Rows)
                {
                    //System.Diagnostics.Trace.WriteLine(elementRow["NAME"].ToString());
                    //System.Diagnostics.Trace.WriteLine(elementRow["OBOZN"].ToString());
                    //System.Diagnostics.Trace.WriteLine(elementRow["A"].ToString());
                    //System.Diagnostics.Trace.WriteLine(elementRow["NALICHI"].ToString());
                    result.Add(new Element(elementRow["NAME"].ToString(), elementRow["OBOZN"].ToString(),
                        AngleConverter.StringToInt(elementRow["A"].ToString()), 
                        (ignoreInStock ? 99 : int.Parse(elementRow["NALICHI"].ToString()))));
                }
            }
            return result;
        }

        /// <summary>
        /// ������ ������� ��������� ��� ������ ����, ��������������� �������� ����������
        /// </summary>
        /// <param name="ignoreInStock">������������ ������� �� ������</param>
        /// <param name="KatologUsp">����� �������� ��� - 0/1</param>
        /// <returns>������ ������� ��������� ��� ������ ����, ��������������� �������� ����������</returns>
        public static Dictionary<String, BigAngleGost> loadBigAngleElement(bool ignoreInStock, int KatologUsp)
        {
            Dictionary<String, BigAngleGost> result = new Dictionary<string, BigAngleGost>();
            DataSet elementLinks = SqlOracle1.getDS("SELECT * FROM " + ElementTypeInfo.getTableName(ElementType.BigAngle));
            foreach (DataRow row in elementLinks.Tables[0].Rows)
            {
                int type = int.Parse(row["REAL_ANGLE_TYPE"].ToString());
                string gost = row["GOST"].ToString();
                if (KatologUsp == 1 && type == 2)
                {
                    System.Diagnostics.Trace.WriteLine(gost + " " + type);
                    System.Diagnostics.Trace.WriteLine("SELECT NAME, OBOZN, A, NALICHI FROM KTC.DB_DATA"
                    + " WHERE GOST='" + gost + "'"
                    + (ignoreInStock ? "" : " AND NALICHI > 0 AND NALICHI <> 999")
                    + " AND KATALOG_USP='" + KatologUsp + "'");
                }
                DataSet subElements = SqlOracle1.getDS("SELECT NAME, OBOZN, A, NALICHI FROM KTC.DB_DATA"
                    + " WHERE GOST='" + gost + "'"
                    + (ignoreInStock ? "" : " AND NALICHI > 0 AND NALICHI <> 999")
                    + " AND KATALOG_USP='" + KatologUsp + "'");
                List<Element> subList = new List<Element>();
                int minAngle = -1;
                foreach (DataRow elementRow in subElements.Tables[0].Rows)
                {
                    int angle = AngleConverter.StringToInt(elementRow["A"].ToString());
                    if (type == 1) angle = 90 * 60 - angle;
                    else if (type == 2)
                    {
                        System.Diagnostics.Trace.WriteLine(KatologUsp + " " + gost + " " + elementRow["NAME"].ToString() + " " + elementRow["OBOZN"].ToString() + " " + angle);
                        angle = 90 * 60 + angle;
                    }
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
