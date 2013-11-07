//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Text;
//using System.Diagnostics;
//using System.Windows.Forms;

//namespace img_gallery
//{


//    public class SelectionAlgorihtm
//    {
//        List<byte>[,] parents;
//        byte[,] counts;
//        int[] heights;
//        int maxHeight;
//        List<Element> elements;

//        const int MULTIPLY_CONST = 20;

//        ///// <summary>
//        ///// 
//        ///// </summary>
//        ///// <param name="availableElements"></param>
//        ///// <param name="maxHeight"></param>
//        //public SelectionAlgorihtm(List<Element> availableElements, int maxHeight)
//        //{
//        //    maxHeight *= MULTIPLY_CONST;
//        //    this.maxHeight = maxHeight;
//        //    elements = availableElements;
//        //    parents = new List<byte>[elements.Count + 1, maxHeight + 1];
//        //    counts  = new byte[elements.Count + 1, maxHeight + 1];
//        //    heights = new int[elements.Count];
//        //    for (int i = 0; i <= elements.Count; ++i)
//        //    {
//        //        for (int j = 0; j <= maxHeight; ++j)
//        //        {
//        //            counts[i, j] = 100;
//        //        }
//        //    }
//        //    int index = 0;
//        //    foreach (Element el in availableElements)
//        //    {
//        //        heights[index++] = (int)(el.Height * MULTIPLY_CONST);
//        //    }
//        //}

//        public Solution solve(double needHeight, bool ignoreInStock)
//        {
//            int H = (int)(needHeight * MULTIPLY_CONST) + 20;
//            counts[0, 0] = 0;
//            for (int i = 0; i < heights.Length; ++i)
//            {
//                for (int h = 0; h <= H; ++h)
//                {
//                    if (counts[i, h] > 99) continue;
//                    int limit = 8;
//                    if (!ignoreInStock)
//                    {
//                        limit = Math.Min(limit, elements[i].StockCount);
//                    }
//                    for (byte k = 0; k <= limit; ++k)
//                    {
//                        int nH = k * heights[i] + h;
//                        if (nH > H ) break;
//                        int newValue = counts[i,h] + k;
//                        if (counts[i + 1,nH] > newValue)
//                        {
//                            parents[i + 1,nH] = new List<byte>();
//                            parents[i + 1, nH].Add(k);
//                            counts[i + 1,nH] = (byte)(newValue);
//                        }
//                        else if (newValue == counts[i + 1, nH])
//                        {
//                            parents[i + 1, nH].Add(k);
//                        }
//                    }
//                }
//            }
//            H -= 20;
//            double mainAnswer = (counts[heights.Length, H] < 100 ? needHeight : -1.0);
//            double lower = -1.0, upper = -1.0;
//            for (int curH = H+1; curH <= maxHeight; ++curH)
//            {
//                if (counts[heights.Length, curH] < 100)
//                {
//                    upper = ((double)curH) / MULTIPLY_CONST;
//                    break;
//                }
//            }
//            for (int curH = H-1; curH > 0; --curH)
//            {
//                if (counts[heights.Length, curH] < 100)
//                {
//                    lower = ((double)curH) / MULTIPLY_CONST;
//                    break;
//                }
//            }
//            return new Solution(this, mainAnswer, lower, upper);
//        }

//        public Dictionary<Element, byte> getSolution(double h, int solutionNumber)
//        {
//            int H = (int)(h * MULTIPLY_CONST);
//            solutionCount = -1;
//            if (counts[elements.Count, H] < 100)
//            {
//                return getAnswer(H, elements.Count, solutionNumber);
//            }
//            else
//            {
//                return null;
//            }
//        }

//        private Dictionary<Element, byte> getAnswer(int h, int column, int solutionNumber)
//        {
//            if (h == 0)
//            {
//                ++solutionCount;
//                return (solutionCount == solutionNumber ? new Dictionary<Element, byte>() : null);
//            }
//            foreach (byte parent in parents[column, h])
//            {
//                Dictionary<Element, byte> result = getAnswer(h - parent * heights[column - 1], column - 1, solutionNumber);
//                if (result != null)
//                {
//                    if (parent > 0)
//                    {
//                        result[elements[column - 1]] = parent;
//                    }
//                    return result;
//                }
//            }
//            return null;
//        }

//        int solutionCount = 0;
//    }    

//    public class Solution
//    {
//        private SelectionAlgorihtm source;
//        public double mainAnswer;
//        public double lowerBound;
//        public double upperBound;

//        public Solution(SelectionAlgorihtm source, double main, double lower, double upper)
//        {
//            this.source = source;
//            mainAnswer = main;
//            lowerBound = lower;
//            upperBound = upper;
//        }

//        public Dictionary<Element, byte> getMainSolution(int solutionNumber)
//        {
//            if (mainAnswer == -1.0) return null;
//            return source.getSolution(mainAnswer, solutionNumber);
//        }

//        public Dictionary<Element, byte> getLowerSolution(int solutionNumber)
//        {
//            if (lowerBound == -1.0) return null;
//            return source.getSolution(lowerBound, solutionNumber);
//        }

//        public Dictionary<Element, byte> getUpperSolution(int solutionNumber)
//        {
//            if (upperBound == -1.0) return null;
//            return source.getSolution(upperBound, solutionNumber);
//        }
//    }

//    public class Element
//    {
//        public double Height;
//        public int StockCount;
//        public string ElementName;
//        public string Obozn;

//        public Element(string name, string obozn, double height, int stockCount)
//        {
//            ElementName = name;
//            Height = height;
//            StockCount = stockCount;
//            this.Obozn = obozn;
//        }
//    }

//    public class DatabaseUtils
//    {
//        public static List<Element> loadFromDb(ElementType elementType, bool ignoreInStock)
//        {
//            List<Element> result = new List<Element>();
//            DataSet elementLinks = SQLOracle.getDS("SELECT * FROM " + ElementTypeInfo.getTableName(elementType));
//            foreach (DataRow row in elementLinks.Tables[0].Rows)
//            {
//                string paramH = row["PARAM_H"].ToString();
//                DataSet subElements = SQLOracle.getDS("SELECT NAME, OBOZN, " + paramH + ", NALICHI FROM DB_DATA"
//                    + " WHERE GOST='" + row["GOST"] + "'"
//                    + (ignoreInStock ? "" : " AND NALICHI > 0"));
//                foreach (DataRow elementRow in subElements.Tables[0].Rows)
//                {
//                    result.Add(new Element(elementRow["NAME"].ToString(),elementRow["OBOZN"].ToString(),
//                        double.Parse(elementRow[paramH].ToString()), int.Parse(elementRow["NALICHI"].ToString())));
//                    Trace.WriteLine(double.Parse(elementRow[paramH].ToString()));
//                }
//            }
//            return result;
//        }
//    }

//    public enum ElementType
//    {
//        None,
//        HeightBySquare,
//        HeightByRectangle,
//        HeightByCircle
//    }

//    public static class ElementTypeInfo 
//    {
//        private static Dictionary<ElementType, String> tableNames;
//        private static Dictionary<ElementType, String> propertyNames;

//        static ElementTypeInfo()
//        {
//            tableNames = new Dictionary<ElementType, string>();
//            propertyNames = new Dictionary<ElementType, string>();
//            tableNames[ElementType.HeightByCircle]    = "USP_VISOTA_1OTV_KRUG";
//            tableNames[ElementType.HeightBySquare]    = "USP_VISOTA_1OTV_KV";
//            tableNames[ElementType.HeightByRectangle] = "USP_VISOTA_1OTV_PR";
//            propertyNames[ElementType.HeightByCircle]    = "Круглые элементы";
//            propertyNames[ElementType.HeightBySquare]    = "Квадратные элементы";
//            propertyNames[ElementType.HeightByRectangle] = "Прямоугольные элементы";
//            propertyNames[ElementType.None] = "Не выбрано";
//        }

//        public static string getTableName(ElementType type) {
//            return tableNames[type];
//        }

//        public static string getPropertyName(ElementType type)
//        {
//            return propertyNames[type];
//        }
//    }
//}