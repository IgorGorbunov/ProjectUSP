using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Katalog2005.Algorithm;
using Katalog2005.WinFroms.LoadPartToNX;
using NXOpen;
using NXOpen.UF;
using NXOpen.Utilities;
using NXOpenUI;
using NXOpen.Assemblies;
using NXOpen.Preferences;

namespace Katalog2005
{
    public partial class Katalog
    {
        

        /// <summary>
        /// Установка размеров формы
        /// </summary>   
        /// <returns></returns>
        void setFormSize()
        {
            this.panel1.Width = Convert.ToInt32(this.Width * 0.3);
            this.panel2.Height = Convert.ToInt32(this.panel1.Height * 0.6);

            

            /*
            if ((dataGrid2.Width / 2) > 1)
                dataGrid2.PreferredColumnWidth = (dataGrid2.Width / 2);
            else
                dataGrid2.PreferredColumnWidth = 1;*/
        }

        /// <summary>
        /// Запуск формы авторизации
        /// </summary>   
        /// <returns></returns>
        void authorization()
        {
            ConnectBD AuthForm = new ConnectBD();
            AuthForm.StartPosition = FormStartPosition.CenterScreen;
            AuthForm.ShowDialog(this);
        }

        /// <summary>
        /// Отображение информации в dgv
        /// </summary>   
        /// <returns></returns>
        public void ViewInform()
        {

            dataGridView1.DataSource = SQLOracle.getDS("SELECT NAME, OBOZN, GOST, L, B, B1, H, D, D1, D_SM_DB, D1_SM_DB, A, S, B_SM_DB, H" +
                         "0, H_SM_DB, T, N, MASSA, NALICHI, TT, YT, PR, RZ, GROUP_USP, KATALOG_USP, UG FROM DB_DATA WHERE KATALOG_USP = 0").Tables[0];

            renameDGVColumns();

            SpecialFunctions.hideUnusefulColumn(dataGridView1);

            dataGridView1.Refresh();

            CreateTreeView();

        }

        /// <summary>
        /// Переименование колонок
        /// </summary>   
        /// <returns></returns>
        public void renameDGVColumns()
        {
            dataGridView1.Columns[0].HeaderText = "Наименование";
            dataGridView1.Columns[1].HeaderText = "Обозначение";
            dataGridView1.Columns[2].HeaderText = "ГОСТ";
            dataGridView1.Columns[9].HeaderText = "d";
            dataGridView1.Columns[10].HeaderText = "d1";
            dataGridView1.Columns[11].HeaderText = "альфа";
            dataGridView1.Columns[13].HeaderText = "b";
            dataGridView1.Columns[15].HeaderText = "h";
            dataGridView1.Columns[16].HeaderText = "t";
            dataGridView1.Columns[18].HeaderText = "Масса";
            dataGridView1.Columns[19].HeaderText = "Наличие";
            dataGridView1.Columns[26].HeaderText = "Месторасположение";
        }


        /// <summary>
        /// Создание корня дерева
        /// </summary>   
        /// <returns></returns>
        public void CreateTreeView()
        {

            treeView1.BeginUpdate();

            treeView1.Nodes.Clear();

            treeView1.Nodes.Add("УСП-8");

            CreateSecondNode(0, treeView1);

            treeView1.Nodes.Add("УСП-12");

            CreateSecondNode(1, treeView1);

            treeView1.Nodes.Add("Спец. детали");

            CreateSecondNode(2, treeView1);

            CreateThirdNode(treeView1);

            treeView1.EndUpdate();

        }

        /// <summary>
        /// Создание узлов второго уровня в дереве
        /// </summary>   
        /// <returns></returns>
        public static void CreateSecondNode(int nodePosition, System.Windows.Forms.TreeView MainTree)
        {

            MainTree.Nodes[nodePosition].Nodes.Add("Базовые детали");

            MainTree.Nodes[nodePosition].Nodes.Add("Корпусные детали");

            MainTree.Nodes[nodePosition].Nodes.Add("Установочные детали");

            MainTree.Nodes[nodePosition].Nodes.Add("Направляющие детали");

            MainTree.Nodes[nodePosition].Nodes.Add("Прижимные детали");

            MainTree.Nodes[nodePosition].Nodes.Add("Крепежные детали");

            MainTree.Nodes[nodePosition].Nodes.Add("Разные детали");

            MainTree.Nodes[nodePosition].Nodes.Add("Сборочные еденицы");

        }

        /// <summary>
        /// Создание узлов третьего уровня в дереве
        /// </summary>   
        /// <returns></returns>
        public static void CreateThirdNode(System.Windows.Forms.TreeView MainTree)
        {
            System.Data.DataTable ConfigurationDataForTree;

            ConfigurationDataForTree = SQLOracle.getDS("SELECT DISTINCT NAME, GROUP_USP, KATALOG_USP FROM DB_DATA").Tables[0];

            for (int position = 0; position < ConfigurationDataForTree.Rows.Count; position++)
            {

                MainTree.Nodes[Convert.ToInt32(ConfigurationDataForTree.Rows[position][2].ToString())].Nodes[Convert.ToInt32(ConfigurationDataForTree.Rows[position][1].ToString())].Nodes.Add(ConfigurationDataForTree.Rows[position][0].ToString());

            }

            ConfigurationDataForTree.Dispose();
        }



        static private System.Windows.Forms.DataGridView.HitTestInfo hitTest;

        /// <summary>
        /// Загрузка картинок в PictureBox
        /// </summary>   
        /// <returns></returns>
        public void loadPictureInPictureBox(System.Windows.Forms.MouseEventArgs e)
        {
            hitTest = dataGridView1.HitTest(e.X, e.Y);

            if (hitTest.RowIndex >= 0)
            {
                pictureBox1.Image = SQLOracle.getBlobImageWithBuffer("SELECT DET FROM DB_DATA WHERE NAME = :NAME AND GOST = :GOST AND OBOZN = :OBOZN ", dataGridView1[0, hitTest.RowIndex].Value.ToString(), dataGridView1[2, hitTest.RowIndex].Value.ToString(), dataGridView1[1, hitTest.RowIndex].Value.ToString());

            }
        }

        /// <summary>
        /// Загрузка картинок в PictureBox (для переключения вверх-вниз)
        /// </summary>   
        /// <returns></returns>
        public void loadPictureInPictureBoxWithKeyEvent(int RowIndex)
        {
            if (pictureBox1.Image != null)
            {
                pictureBox1.Image.Dispose();
            }

            pictureBox1.Image = SQLOracle.getBlobImageWithBuffer("SELECT DET FROM DB_DATA WHERE NAME = :NAME AND GOST = :GOST AND OBOZN = :OBOZN ", dataGridView1[0, RowIndex].Value.ToString(), dataGridView1[2, RowIndex].Value.ToString(), dataGridView1[1, RowIndex].Value.ToString());


        }


        /// <summary>
        /// Сортировка данных по дереву
        /// </summary>   
        /// <returns></returns>
        public void sortInformWithTree(System.Windows.Forms.TreeViewEventArgs e)
        {

            if (((e.Node.Parent) != null) && e.Node.Parent.GetType() == typeof(System.Windows.Forms.TreeNode))
            {
                if (((e.Node.Parent.Parent) != null) && e.Node.Parent.Parent.GetType() == typeof(System.Windows.Forms.TreeNode))
                {

                    dataGridView1.DataSource = SQLOracle.getDS("SELECT NAME , OBOZN , GOST,  L, " +
                                        " B,  B1, H,  D, D1, D_SM_DB , D1_SM_DB, " +
                                        " A,  S, B_SM_DB ,  H0,  T,  N,H_SM_DB, MASSA" +
                                        " , NALICHI , TT, YT, PR, RZ, GROUP_USP, KATALOG_USP, UG FROM DB_DATA WHERE NAME = '" + e.Node.Text.ToString() + "' AND KATALOG_USP = '" + Convert.ToString(e.Node.Parent.Parent.Index) + "' AND GROUP_USP = '" + Convert.ToString(e.Node.Parent.Index) + "' ").Tables[0];

                    SpecialFunctions.hideEmptyColumn(dataGridView1);
                }
                else
                {

                    dataGridView1.DataSource = SQLOracle.getDS("SELECT NAME , OBOZN , GOST,  L, " +
                                        " B,  B1, H,  D, D1, D_SM_DB , D1_SM_DB, " +
                                        " A,  S, B_SM_DB ,  H0,  T,  N, H_SM_DB,MASSA" +
                                        " , NALICHI , TT, YT, PR, RZ, GROUP_USP, KATALOG_USP, UG FROM DB_DATA WHERE KATALOG_USP = '" + Convert.ToString(e.Node.Parent.Index) + "' AND GROUP_USP = '" + Convert.ToString(e.Node.Index) + "'").Tables[0];

                    SpecialFunctions.hideEmptyColumn(dataGridView1);
                }
            }
            else
            {
                dataGridView1.DataSource = SQLOracle.getDS("SELECT NAME , OBOZN , GOST,  L, " +
                                       " B,  B1, H,  D, D1, D_SM_DB , D1_SM_DB, " +
                                       " A,  S, B_SM_DB ,  H0,  T,  N,H_SM_DB, MASSA" +
                                       " , NALICHI , TT, YT, PR, RZ, GROUP_USP, KATALOG_USP, UG FROM  DB_DATA WHERE KATALOG_USP = '" + Convert.ToString(e.Node.Index) + "' ").Tables[0];

                SpecialFunctions.hideEmptyColumn(dataGridView1);
            }

        }


        ///<summary>
        /// Отображение информации по элементу
        /// </summary>   
        /// <returns></returns>
        public void ViewInformationAboutElement(int i)
        {

            using (Katalog2005.WinFroms.search.InformAboutElement DispInform = new Katalog2005.WinFroms.search.InformAboutElement())
            {


                for (int j = 0; j < 19; j++)
                {
                    if (String.Compare(dataGridView1[j, i].Value.ToString(), "0") != 0)
                        DispInform.AddRowToDataGrid(dataGridView1.Columns[dataGridView1[j, i].ColumnIndex].HeaderText, dataGridView1[j, i].Value.ToString());
                }

                DispInform.AddRowToDataGrid("Технические требования", dataGridView1[20, i].Value.ToString());
                DispInform.AddRowToDataGrid("Утвердил", dataGridView1[21, i].Value.ToString());
                DispInform.AddRowToDataGrid("Проверил", dataGridView1[22, i].Value.ToString());
                DispInform.AddRowToDataGrid("Разработал", dataGridView1[23, i].Value.ToString());
                DispInform.AddRowToDataGrid("Месторасположение", dataGridView1[26, i].Value.ToString());

                DispInform.ShowDialog();


            }

        }

        ///<summary>
        /// Закрытие формы
        /// </summary>   
        /// <returns></returns>
        public void CloseKatalog()
        {
            this.Close();
        }


        /// <summary>
        /// Определение модели для загрузки в Temp
        /// </summary>   
        /// <returns></returns>
        public void defineTypeOfModel()
        {
            if (SQLOracle.exist((object)dataGridView1[1, dataGridView1.SelectedCells[0].RowIndex].Value.ToString(), "HD", "MODEL_ATTR"))
            {
                loadPartToTemp(dataGridView1[1, dataGridView1.SelectedCells[0].RowIndex].Value.ToString());
            }
            else if (SQLOracle.exist((object)dataGridView1[1, dataGridView1.SelectedCells[0].RowIndex].Value.ToString(), "HD", "MODEL_ATTR20"))
            {
                loadPartToTempSpecDet(dataGridView1[1, dataGridView1.SelectedCells[0].RowIndex].Value.ToString());
            }
        }

        /// <summary>
        /// Загрузка стандартной модели в Temp
        /// </summary>   
        /// <returns></returns>
         public void loadPartToTemp(string oboznachenie)
        {
            string curname;

            string openPart = SQLOracle.UnloadPartToTEMPFolder(oboznachenie);

            System.Collections.Generic.List<string> childComponents = SQLOracle.GetInformationListWithParamQuery("NMF", "MODEL_STRUCT21", "PARENT", (oboznachenie + ".prt"));

            for (int i = 0; i < childComponents.Count; i++)
            {
                curname = System.IO.Path.GetFileNameWithoutExtension(childComponents[i]);
                SQLOracle.UnloadPartToTEMPFolder(curname);
            }

            if (String.Compare(openPart, "0") != 0)
            {
                if (string.Compare(dataGridView1["NALICHI", dataGridView1.SelectedCells[0].RowIndex].Value.ToString(), "0") != 0)
                {
                    SpecialFunctions.loadPartToNX(oboznachenie + ".prt");
                }
                else
                {
                    MessageBox.Show("Данной детали нет в наличии на складе");
                }
            }
        }

        /// <summary>
        /// Загрузка не стандартной модели в Temp
        /// </summary>   
        /// <returns></returns>
        public void loadPartToTempSpecDet(string oboznachenie)
        {

            string NMF = SQLOracle.ParamQuerySelect("SELECT NMF FROM KTC.MODEL_ATTR20 WHERE HD = :HD", "HD", oboznachenie);

            string openPart = SQLOracle.UnloadOsnasToTEMPFolderFile20(NMF.Trim());

            
            if (String.Compare(openPart, "0") != 0)
            {
                if (string.Compare(dataGridView1["NALICHI", dataGridView1.SelectedCells[0].RowIndex].Value.ToString(), "0") != 0)
                {
                    SpecialFunctions.loadPartToNX(oboznachenie + ".prt");
                }
                else
                {
                    MessageBox.Show("Данной детали нет в наличии на складе");
                }               

            }

        }





        /// <summary>
        /// Экспортирование данных в Excel
        /// </summary>   
        /// <param name="dgv">DataGridView</param>
        /// <returns></returns>
        void ExportDGVToExcel(DataGridView dgv)
        {
            
                ExcelClass InformationAboutElements = new ExcelClass();

                System.Drawing.Font HeadFont = new System.Drawing.Font(" Times New Roman ", 12.0f, FontStyle.Bold);


                int iterator = 0;

                try
                {
                    char[] alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
                    int CurrentCell = 0;
                    InformationAboutElements.NewDocument();
                    InformationAboutElements.AddNewPageAtTheStart("Данные");

                    for (int i = 0; i < dgv.Columns.Count; i++)
                    {
                        if (dgv.Columns[i].Visible == true)
                        {

                            InformationAboutElements.SelectCells(alpha[iterator] + (1).ToString(), Type.Missing);
                            InformationAboutElements.SetFont(HeadFont, 0);
                            InformationAboutElements.SetBorderStyle(0, Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous, Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin);

                            InformationAboutElements.WriteDataToCell(dgv.Columns[i].HeaderText);

                            for (int j = 0; j < dgv.Rows.Count; j++)
                            {

                                InformationAboutElements.SelectCells(alpha[iterator] + (j + 2).ToString(), Type.Missing);
                                InformationAboutElements.SetFont(HeadFont, 0);
                                InformationAboutElements.SetBorderStyle(0, Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous, Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin);
                                InformationAboutElements.setAutoFit(alpha[iterator] + (j + 2).ToString());
                                InformationAboutElements.WriteDataToCell(dgv[i, j].Value.ToString());
                            }

                            if (dgv[i, 0].Value.ToString().Length > dgv.Columns[i].HeaderText.Length)
                            {
                                InformationAboutElements.setAutoFit(alpha[iterator] + (2).ToString());
                            }
                            else
                            {
                                InformationAboutElements.setAutoFit(alpha[iterator] + (1).ToString());
                            }


                            iterator++;
                        }



                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");

                }
                finally
                {

                    InformationAboutElements.Visible = true;
                    InformationAboutElements.Dispose();
                    HeadFont.Dispose();

                }

        }


        /// <summary>
        /// Создание таблицы данных по элементам сборки
        /// </summary>   
        /// <param name="dgv">DataGridView</param>
        /// <returns></returns>
        void createDataTableOfDetailes()
        {
            Algorithm.SpecialFunctions.initUGData();

            System.Collections.Generic.List<string> NameOfElements = new System.Collections.Generic.List<string>();

            System.Collections.Generic.List<string> RootName = new System.Collections.Generic.List<string>();
            
            int numberOfParts = 0;

            Part Part_Specification = Algorithm.SpecialFunctions.theSession.Parts.Display;

            dataGridView2.DataSource = null;

            DataTable DetailsOfAssembly = new DataTable("Dannie");

            DataColumn ColumnOboznachenie = DetailsOfAssembly.Columns.Add("Обозначение детали");

            DataColumn ColunmDetailsCaunt = DetailsOfAssembly.Columns.Add("Количество деталей");

            DataColumn ColumnPlacment = DetailsOfAssembly.Columns.Add("Месторасположение");

            //создание листа элементов 
            try
            {
                if (Algorithm.SpecialFunctions.theSession.Parts.Display != null)
                {
                    NXOpen.Assemblies.Component rootComponent = Part_Specification.ComponentAssembly.RootComponent;

                    if (rootComponent != null)
                    {
                        NameOfElements.Add(Part_Specification.ComponentAssembly.RootComponent.DisplayName);

                        RootName.Add(Part_Specification.ComponentAssembly.RootComponent.DisplayName);
                                              
                        EnumerationOfTree(NameOfElements, rootComponent);
                    }
                }
                                               
                bool duplicate = false;

                System.Collections.IEnumerator NameOfElemEnum = NameOfElements.GetEnumerator();
                //исключение повторного внесения корневого элемента
                while (NameOfElemEnum.MoveNext())
                {
                    System.Collections.IEnumerator RootEnum = RootName.GetEnumerator();

                    while (RootEnum.MoveNext())
                    {
                        if (String.Compare(NameOfElemEnum.Current.ToString(), RootEnum.Current.ToString()) == 0)
                        {
                            duplicate = true;
                            break;
                        }

                    }
                    if (duplicate == false)
                    {
                        //формирование списка
                        RootName.Add(NameOfElemEnum.Current.ToString());

                        System.Collections.IEnumerator NameOfElementsSecondEnum = NameOfElements.GetEnumerator();
                        while (NameOfElementsSecondEnum.MoveNext())
                        {
                            if (String.Compare(NameOfElemEnum.Current.ToString(), NameOfElementsSecondEnum.Current.ToString()) == 0)
                            {
                                numberOfParts++;
                            }
                        }

                        AddRowToTable(DetailsOfAssembly, NameOfElemEnum.Current.ToString(), Convert.ToString(numberOfParts), "0");

                        numberOfParts = 0;
                    }
                    else
                    {
                        duplicate = false;
                    }
                }
               

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

            dataGridView2.DataSource = DetailsOfAssembly;
        }


        /// <summary>
        /// Рекурсивный перебор узлов дерева
        /// </summary>   
        /// <param name="dgv">DataGridView</param>
        /// <returns></returns>
        public void EnumerationOfTree(System.Collections.Generic.List<string> ListOfElements,NXOpen.Assemblies.Component CurrentNode)
        {
            NXOpen.Assemblies.Component[] childComponents = CurrentNode.GetChildren();

            for (int i = 0; i < childComponents.Length; i++)
            {
                ListOfElements.Add(childComponents[i].Name);
                 
                EnumerationOfTree(ListOfElements, childComponents[i]);
            }

        }

        /// <summary>
        /// Формирование таблицы данных для вывода на экран
        /// </summary>   
        /// <param name="dgv">DataGridView</param>
        /// <returns></returns>
        private void AddRowToTable(DataTable MainTable, string TheFirstColumn, string TheSecondColumn, string TheThirdColumn)
        {

            DataRow NewRow;
            NewRow = MainTable.NewRow();

            NewRow["Обозначение детали"] = TheFirstColumn;
            NewRow["Количество деталей"] = TheSecondColumn;
            NewRow["Месторасположение"] = TheThirdColumn;

            MainTable.Rows.Add(NewRow);
        }

    }

}
