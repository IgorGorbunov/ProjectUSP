using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Katalog2005.WinFroms.AddInformationAboutElements
{
    public partial class AddInformation : Form
    {
        private int statusOfPanel;

        public AddInformation(int status)
        {
            InitializeComponent();

            this.statusOfPanel = status;

            if (status == 1)
            {
                this.Text = "Добавить информацию УСП";
            }

        }
               

        public AddInformation(int status,System.Windows.Forms.DataGridView dgvLocal)
        {
            InitializeComponent();           

            this.statusOfPanel = status;
            
            if (status == 2)
            {
                this.Text = "Редактировать информацию УСП";
            }
            

            LoadInformationAboutSelectedItem(dgvLocal);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                box_DET.Text = openFileDialog1.FileName;
                toolStripStatusLabel1.Text = "Изображение выбрано!";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Загрузка...";

            if (this.statusOfPanel == 1)
            {
                LoadInformationToDB();
            }
            else if (this.statusOfPanel == 2)
            {
                UpdateInformationToDB();
            }
            
            toolStripStatusLabel1.Text = "";
           
        }

        private void загрузитьИнформациюToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Загрузка...";

            if (this.statusOfPanel == 1)
            {
                LoadInformationToDB();
            }
            else if (this.statusOfPanel == 2)
            {
                UpdateInformationToDB();
            }

            toolStripStatusLabel1.Text = "";
        }

        private void LoadInformationToDB()
        {

            if (string.Compare(CheckInformation(), "0") == 0)
            {
                System.Collections.Generic.List<string> Parameters = new System.Collections.Generic.List<string>();

                System.Collections.Generic.List<string> DataFromTextBox = new System.Collections.Generic.List<string>();

                System.IO.FileStream FileStreamBMP = new System.IO.FileStream(box_DET.Text, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                //int SizeOfBitmap = Convert.ToInt32(FileStreamBMP.Length);
                byte[] BMPInByte = new Byte[FileStreamBMP.Length - 1];
                FileStreamBMP.Read(BMPInByte, 0, BMPInByte.Length);
                FileStreamBMP.Close();

                Parameters.Add("NAME"); DataFromTextBox.Add(IsNullParametr(box_NAME));
                Parameters.Add("OBOZN"); DataFromTextBox.Add(box_OBOZN.Text.ToString());
                Parameters.Add("GOST"); DataFromTextBox.Add(box_GOST.Text.ToString());
                Parameters.Add("L"); DataFromTextBox.Add(IsNullParametr(box_L));
                Parameters.Add("B"); DataFromTextBox.Add(IsNullParametr(box_B));
                Parameters.Add("B1"); DataFromTextBox.Add(IsNullParametr(box_B1));
                Parameters.Add("H"); DataFromTextBox.Add(IsNullParametr(box_H));
                Parameters.Add("D"); DataFromTextBox.Add(IsNullParametr(box_D));
                Parameters.Add("D1"); DataFromTextBox.Add(IsNullParametr(box_D1));
                Parameters.Add("D_SM_DB"); DataFromTextBox.Add(IsNullParametr(box_d_sm));
                Parameters.Add("D1_SM_DB"); DataFromTextBox.Add(IsNullParametr(box_d_sm1));
                Parameters.Add("A"); DataFromTextBox.Add(IsNullParametr(box_alfa));
                Parameters.Add("S"); DataFromTextBox.Add(IsNullParametr(box_s));
                Parameters.Add("B_SM_DB"); DataFromTextBox.Add(IsNullParametr(box_b_sm));
                Parameters.Add("H0"); DataFromTextBox.Add(IsNullParametr(box_H0));
                Parameters.Add("H_SM_DB"); DataFromTextBox.Add(IsNullParametr(box_h_sm));
                Parameters.Add("T"); DataFromTextBox.Add(IsNullParametr(box_t));
                Parameters.Add("N"); DataFromTextBox.Add(IsNullParametr(box_N));
                Parameters.Add("MASSA"); DataFromTextBox.Add(IsNullParametr(box_MASSA));
                Parameters.Add("NALICHI"); DataFromTextBox.Add(box_NALICHIE.Text.ToString());
                Parameters.Add("TT"); DataFromTextBox.Add(IsNullParametr(box_TT));
                Parameters.Add("YT"); DataFromTextBox.Add(IsNullParametr(box_YT));
                Parameters.Add("PR"); DataFromTextBox.Add(IsNullParametr(box_PR));
                Parameters.Add("RZ"); DataFromTextBox.Add(IsNullParametr(box_RZ));
                Parameters.Add("GROUP_USP"); DataFromTextBox.Add(Convert.ToString(comboBox3.SelectedIndex));
                Parameters.Add("KATALOG_USP"); DataFromTextBox.Add(Convert.ToString(comboBox2.SelectedIndex));
                Parameters.Add("UG"); DataFromTextBox.Add(""/*Convert.ToString(comboBox1.SelectedIndex)*/);

                string cmd = "INSERT INTO DB_DATA(NAME, OBOZN, GOST, L, B, B1, H, D, D1, D_SM_DB, D1_SM_DB, A, " +
                        "S, B_SM_DB, H0, H_SM_DB, T, N, MASSA, NALICHI, TT, YT, PR, RZ, GROUP_USP, KATALO" +
                        "G_USP,UG,DET) VALUES (:NAME, :OBOZN, :GOST, :L, :B, :B1, :H, :D, :D1, :D_SM_DB, :D1_SM_" +
                        "DB, :A, :S, :B_SM_DB, :H0, :H_SM_DB, :T, :N, :MASSA, :NALICHI, :TT, :YT, :PR, :R" +
                        "Z, :GROUP_USP, :KATALOG_USP,:UG,:DET)";


                SQLOracle.SpecificInsertQuery(cmd, Parameters, DataFromTextBox, BMPInByte);

            }
            else
            {
                MessageBox.Show(CheckInformation(), "Ошибка!");
            }
        }

        private void UpdateInformationToDB()
        {

            if (string.Compare(CheckInformationForEditing(), "0") == 0)
            {
                System.Collections.Generic.List<string> Parameters = new System.Collections.Generic.List<string>();

                System.Collections.Generic.List<string> DataFromTextBox = new System.Collections.Generic.List<string>();

                Parameters.Add("NAME"); DataFromTextBox.Add(IsNullParametr(box_NAME));                
                Parameters.Add("GOST"); DataFromTextBox.Add(box_GOST.Text.ToString());
                Parameters.Add("L"); DataFromTextBox.Add(IsNullParametr(box_L));
                Parameters.Add("B"); DataFromTextBox.Add(IsNullParametr(box_B));
                Parameters.Add("B1"); DataFromTextBox.Add(IsNullParametr(box_B1));
                Parameters.Add("H"); DataFromTextBox.Add(IsNullParametr(box_H));
                Parameters.Add("D"); DataFromTextBox.Add(IsNullParametr(box_D));
                Parameters.Add("D1"); DataFromTextBox.Add(IsNullParametr(box_D1));
                Parameters.Add("D_SM_DB"); DataFromTextBox.Add(IsNullParametr(box_d_sm));
                Parameters.Add("D1_SM_DB"); DataFromTextBox.Add(IsNullParametr(box_d_sm1));
                Parameters.Add("A"); DataFromTextBox.Add(IsNullParametr(box_alfa));
                Parameters.Add("S"); DataFromTextBox.Add(IsNullParametr(box_s));
                Parameters.Add("B_SM_DB"); DataFromTextBox.Add(IsNullParametr(box_b_sm));
                Parameters.Add("H0"); DataFromTextBox.Add(IsNullParametr(box_H0));
                Parameters.Add("H_SM_DB"); DataFromTextBox.Add(IsNullParametr(box_h_sm));
                Parameters.Add("T"); DataFromTextBox.Add(IsNullParametr(box_t));
                Parameters.Add("N"); DataFromTextBox.Add(IsNullParametr(box_N));
                Parameters.Add("MASSA"); DataFromTextBox.Add(IsNullParametr(box_MASSA));
                Parameters.Add("NALICHI"); DataFromTextBox.Add(box_NALICHIE.Text.ToString());
                Parameters.Add("TT"); DataFromTextBox.Add(IsNullParametr(box_TT));
                Parameters.Add("YT"); DataFromTextBox.Add(IsNullParametr(box_YT));
                Parameters.Add("PR"); DataFromTextBox.Add(IsNullParametr(box_PR));
                Parameters.Add("RZ"); DataFromTextBox.Add(IsNullParametr(box_RZ));
                Parameters.Add("GROUP_USP"); DataFromTextBox.Add(Convert.ToString(comboBox3.SelectedIndex));
                Parameters.Add("KATALOG_USP"); DataFromTextBox.Add(Convert.ToString(comboBox2.SelectedIndex));
                Parameters.Add("UG"); DataFromTextBox.Add("123"/*Convert.ToString(comboBox1.SelectedIndex)*/);
                Parameters.Add("OBOZN"); DataFromTextBox.Add(box_OBOZN.Text.ToString());

                if (String.Compare(box_DET.Text.ToString(), "0") != 0)
                {
                    System.IO.FileStream FileStreamBMP = new System.IO.FileStream(box_DET.Text, System.IO.FileMode.Open, System.IO.FileAccess.Read);                   
                    byte[] BMPInByte = new Byte[FileStreamBMP.Length - 1];
                    FileStreamBMP.Read(BMPInByte, 0, BMPInByte.Length);
                    FileStreamBMP.Close();
                    IDisposable disp = (IDisposable)FileStreamBMP;
                    disp.Dispose();

                     string cmd = "UPDATE DB_DATA SET NAME = :NAME, GOST = :GOST, L=:L, B=:B, B1=:B1, H=:H, D =:D, D1 =:D1, D_SM_DB =: D_SM_DB, D1_SM_DB = :D1_SM_DB, A =:A, " +
                     "S =:S, B_SM_DB = :B_SM_DB, H0 = :H0, H_SM_DB =:H_SM_DB, T = :T, N = :N, MASSA = :MASSA, NALICHI =:NALICHI, TT = :TT, YT = :YT, PR = :PR, RZ =: RZ, GROUP_USP =:GROUP_USP ," +
                     "KATALOG_USP =:KATALOG_USP,UG =:UG ,DET =:DET WHERE OBOZN = :OBOZN";

                     SQLOracle.SpecificUpdateQuery(cmd, Parameters, DataFromTextBox, BMPInByte);

                }
                else {
                   
                     string cmd = "UPDATE DB_DATA SET NAME = :NAME, GOST = :GOST, L=:L, B=:B, B1=:B1, H=:H, D =:D, D1 =:D1, D_SM_DB =: D_SM_DB, D1_SM_DB = :D1_SM_DB, A =:A, " +
                     "S =:S, B_SM_DB = :B_SM_DB, H0 = :H0, H_SM_DB =:H_SM_DB, T = :T, N = :N, MASSA = :MASSA, NALICHI =:NALICHI, TT = :TT, YT = :YT, PR = :PR, RZ =: RZ, GROUP_USP =:GROUP_USP ," +
                     "KATALOG_USP =:KATALOG_USP,UG =:UG WHERE OBOZN = :OBOZN";

                     if (SQLOracle.UpdateQuery(cmd, Parameters, DataFromTextBox) == true)
                     {
                         System.Windows.Forms.MessageBox.Show("Данные успешно обновлены!");
                     }
                       
                }

                Parameters.Clear();
                DataFromTextBox.Clear();

            }
            else
            {
                MessageBox.Show(CheckInformation(), "Ошибка!");
            }
        }

        private string IsNullParametr(System.Windows.Forms.TextBox CheckThisBox)
        {
            string NameParametr = "";

            if ((box_NAME.Text.Length > 0))
            {
                
                foreach (char StrName in CheckThisBox.Text)
                {

                    NameParametr = String.Concat(NameParametr, Convert.ToString(StrName));
                    
                }

                return NameParametr;

            }
            else
            {
                return NameParametr = "0";
            }

        }


        private int IsGOST()
        {

            int CountLetter = 1;
            bool FindNumber = false;


            if (box_GOST.Text.Length == 8)
            {

                foreach (char StrName in box_GOST.Text)
                {
                    if (CountLetter != 6)
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            if (String.Compare((Convert.ToString(StrName)), Convert.ToString(i)) == 0)
                            {
                                FindNumber = true;
                            }
                        }

                        if (FindNumber == false)
                        {
                            return 0;

                        }
                        else
                        {
                            FindNumber = false;
                        }

                    }
                    else
                    {
                        if (String.Compare((Convert.ToString(StrName)), "-") != 0)
                        {
                            return 0;
                        }


                    }
                    CountLetter++;

                }



                return 1;

            }
            else
            {

                return 0;

            }
        }

        private int IsOBOZN()
        {

            int CountLetter = 1;
            bool FindNumber = false;



            if (box_OBOZN.Text.Length == 9)
            {
                
                foreach (char StrName in box_OBOZN.Text)
                {
                    if (CountLetter != 5)
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            if (String.Compare((Convert.ToString(StrName)), Convert.ToString(i)) == 0)
                            {
                                FindNumber = true;
                            }
                        }

                        if (FindNumber == false)
                        {
                            return 0;

                        }
                        else
                        {
                            FindNumber = false;
                        }

                    }
                    else
                    {
                        if (String.Compare((Convert.ToString(StrName)), "-") != 0)
                        {
                            return 0;
                        }


                    }
                    CountLetter++;

                }



                return 1;

            }
            else
            {

                return 0;

            }

        }




        private int is_NALICHIE()
        {
            bool FindNumber = false;

            if (box_NALICHIE.Text.Length > 0)
            {

                foreach (char StrName in box_NALICHIE.Text)
                {

                    for (int i = 0; i < 10; i++)
                    {
                        if (String.Compare((Convert.ToString(StrName)), Convert.ToString(i)) == 0)
                        {
                            FindNumber = true;
                        }
                    }

                    if (FindNumber == false)
                    {
                        return 0;

                    }
                    else
                    {
                        FindNumber = false;
                    }

                }


                return 1;
            }
            else
            {
                return 0;
            }

        }

        private void проверитьИнформациюToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (statusOfPanel == 1)
            { 
                 if (string.Compare(CheckInformation(), "0") == 0)
                {
                    MessageBox.Show("Параметры введены без ошибок","Сообщение!");
                }
                else {
                    MessageBox.Show(CheckInformation(), "Ошибка!");
                }
            }
            else if (statusOfPanel == 2)
            {
                  if (string.Compare(  CheckInformationForEditing(), "0") == 0)
                {
                    MessageBox.Show("Параметры введены без ошибок","Сообщение!");
                }
                else {
                    MessageBox.Show(  CheckInformationForEditing(), "Ошибка!");
                }
            
            }
           
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private string CheckInformation()
        {
        
            string DisplayErrors = "";

            if ((IsGOST() == 0))
            {
                DisplayErrors += "Формат ГОСТ должен быть: 12345-67\n";
            }
            
            if ((IsOBOZN() == 0))
            {
              DisplayErrors += "Формат Обозначения должен быть: 1234-1234\n";
            }
                
            if (SQLOracle.exist("DB_DATA", (string)("OBOZN = '" + box_OBOZN.Text.ToString() + "'"))==true)
            {
              DisplayErrors += "Обозначение детали было занесено ранее\n";
            }

            if (String.Compare(IsNullParametr(box_NAME), "0") == 0)
            {
                DisplayErrors += "Наименование детали не внесено!\n";
            }

            if (String.Compare(box_DET.Text.ToString(), "0") == 0)
            {
                DisplayErrors += "Не указано изображение!\n";
            }

            if (comboBox2.SelectedIndex == -1 || comboBox3.SelectedIndex == -1)
            {
                DisplayErrors += "Не выбран каталог и группа!\n";
            }

            if (is_NALICHIE() == 0)
            {
                DisplayErrors += ("Поле \"Наличие\" должно состоять из чисел\n");
            }
            //для боевой версии
            //string NMF = SQLOracle.ParamQuerySelect("SELECT NMF FROM KTC.MODEL_ATTR20 WHERE HD = :HD", "HD", box_OBOZN.Text.ToString());
            //для тестовой версии
            string NMF = SQLOracle.ParamQuerySelect("SELECT NMF FROM MODEL_ATTR20 WHERE HD = :HD", "HD", box_OBOZN.Text.ToString());

            if (SQLOracle.exist("NMF", "FILE_BLOB20", (string)("NMF = '" + NMF.Trim() +"'")) == false)
            {
                DisplayErrors += "Модели элемента УСП с таким обозначением нет в списках утвержденных!";
            }

            if (String.Compare(DisplayErrors, "") == 0)
            {
                return "0";
            }

            else {

                return DisplayErrors;              
            }
        }



        private string CheckInformationForEditing()
        {

            string DisplayErrors = "";

            if ((IsGOST() == 0))
            {
                DisplayErrors += "Формат ГОСТ должен быть: 12345-67\n";
            }

            if (String.Compare(IsNullParametr(box_NAME), "0") == 0)
            {
                DisplayErrors += "Наименование детали не внесено!\n";
            }

            if (comboBox2.SelectedIndex == -1 || comboBox3.SelectedIndex == -1)
            {
                DisplayErrors += "Не выбран каталог и группа!\n";
            }

            if (is_NALICHIE() == 0)
            {
                DisplayErrors += ("Поле \"Наличие\" должно состоять из чисел\n");
            }


            if (String.Compare(DisplayErrors, "") == 0)
            {
                return "0";
            }
            else
            {
                return DisplayErrors;
            }
        }



        private void проверитьМодельToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FindLoadedModels newSearchOfModel = new FindLoadedModels();
            
                newSearchOfModel.Show();            
            
        }

        private void box_NAME_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Введите наименование модели!";
        }

       
        private void comboBox1_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Выберите месторасположение элемента УСП на складе!";
        }

        private void box_GOST_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "ГОСТ должен быть в формате \"12345-67\" !";
        }

        private void box_OBOZN_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "обозначение должно быть в формате \"1234-5678\" !";
        }
        
        private void box_MASSA_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Введите массу элемента УСП!";
        }

              

        private void comboBox2_Click(object sender, EventArgs e)
        {
                 toolStripStatusLabel1.Text = "Выберите каталог для элемента УСП!";
        }

        private void comboBox3_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Выберите группу для элемента УСП!";
        }

        private void box_NALICHIE_TextChanged(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Введите количество элементов УСП на складе!";
        }

            

        private void box_B_Click_1(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Введите соответствующий параметр!";
        }

        private void box_NALICHIE_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Введите количество элементов на складе!";
        }

        private void box_YT_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Введите ФИО!";
        }

        private void box_TT_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Введите тех. требования!";
            
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Панель ввода информации  о элементах УСП!";
        }

        private void показатьВExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExcelClass InformationAboutElements = new ExcelClass();

            Font HeadFont = new Font(" Times New Roman ", 12.0f, FontStyle.Bold);

            try
            {

                string currentName = "";
                int countCell = 2;
                InformationAboutElements.NewDocument();

                InformationAboutElements.AddNewPageAtTheStart("Параметры элемента УСП");
                InformationAboutElements.SelectCells("A1", Type.Missing);
                InformationAboutElements.SetColumnWidth(20);
                InformationAboutElements.SetFont(HeadFont, 0);
                InformationAboutElements.SetBorderStyle(0, Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous, Microsoft.Office.Interop.Excel.XlBorderWeight.xlThick);
                InformationAboutElements.WriteDataToCell("Параметр");                
                InformationAboutElements.SelectCells("B1", Type.Missing);
                InformationAboutElements.SetColumnWidth(30);
                InformationAboutElements.SetFont(HeadFont, 0);
                InformationAboutElements.SetBorderStyle(0, Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous, Microsoft.Office.Interop.Excel.XlBorderWeight.xlThick);
                InformationAboutElements.WriteDataToCell("Значение");

                for (int i = 1; i < 28; i++)
                {
                    for (int j = 0; j < this.panel1.Controls.Count; j++)
                    {                    
                        if ((String.Compare(this.panel1.Controls[j].Tag.ToString(), i.ToString()) == 0))
                        {
                            if ((String.Compare(this.panel1.Controls[j].Text, "") != 0)&&(String.Compare(this.panel1.Controls[j].Text, "0") != 0))
                            {   
                                
                                currentName = ((System.Windows.Forms.Label)(this.Controls.Find(("label" + i.ToString()), true)[0])).Text;

                                InformationAboutElements.SelectCells((string)(("B" + countCell.ToString())), Type.Missing);
                                InformationAboutElements.WriteDataToCell(this.panel1.Controls[j].Text);
                                InformationAboutElements.SetHorisontalAlignment(2);
                                InformationAboutElements.SetBorderStyle(0, Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous, Microsoft.Office.Interop.Excel.XlBorderWeight.xlThick);


                                InformationAboutElements.SelectCells((string)(("A" + countCell.ToString())), Type.Missing);
                                InformationAboutElements.WriteDataToCell(currentName);
                                InformationAboutElements.SetHorisontalAlignment(2);
                                InformationAboutElements.SetBorderStyle(0, Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous, Microsoft.Office.Interop.Excel.XlBorderWeight.xlThick);

                                countCell++;
                            
                            }                         
                        }
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

        private void AddInformation_Load(object sender, EventArgs e)
        {

        }

        private void LoadInformationAboutSelectedItem(System.Windows.Forms.DataGridView dgv)
        {
            
            this.box_NAME.Text = dgv[0, dgv.SelectedCells[0].RowIndex].Value.ToString();
            this.box_OBOZN.Text = dgv[1, dgv.SelectedCells[0].RowIndex].Value.ToString();
            this.box_GOST.Text = dgv[2, dgv.SelectedCells[0].RowIndex].Value.ToString();
            this.box_L.Text = dgv[3, dgv.SelectedCells[0].RowIndex].Value.ToString();
            this.box_B.Text = dgv[4, dgv.SelectedCells[0].RowIndex].Value.ToString();
            this.box_B1.Text = dgv[5, dgv.SelectedCells[0].RowIndex].Value.ToString();
            this.box_H.Text = dgv[6, dgv.SelectedCells[0].RowIndex].Value.ToString();
            this.box_D.Text = dgv[7, dgv.SelectedCells[0].RowIndex].Value.ToString();
            this.box_D1.Text = dgv[8, dgv.SelectedCells[0].RowIndex].Value.ToString();
            this.box_d_sm.Text = dgv[9, dgv.SelectedCells[0].RowIndex].Value.ToString();
            this.box_d_sm1.Text = dgv[10, dgv.SelectedCells[0].RowIndex].Value.ToString();
            this.box_alfa.Text = dgv[11, dgv.SelectedCells[0].RowIndex].Value.ToString();
            this.box_s.Text = dgv[12, dgv.SelectedCells[0].RowIndex].Value.ToString();
            this.box_b_sm.Text = dgv[13, dgv.SelectedCells[0].RowIndex].Value.ToString();
            this.box_H0.Text = dgv[14, dgv.SelectedCells[0].RowIndex].Value.ToString();
            this.box_h_sm.Text = dgv[15, dgv.SelectedCells[0].RowIndex].Value.ToString();
            this.box_t.Text = dgv[16, dgv.SelectedCells[0].RowIndex].Value.ToString();
            this.box_N.Text = dgv[17, dgv.SelectedCells[0].RowIndex].Value.ToString();
            this.box_MASSA.Text = dgv[18, dgv.SelectedCells[0].RowIndex].Value.ToString();
            this.box_NALICHIE.Text = dgv[19, dgv.SelectedCells[0].RowIndex].Value.ToString();
            this.box_TT.Text = dgv[20, dgv.SelectedCells[0].RowIndex].Value.ToString();
            this.box_YT.Text = dgv[21, dgv.SelectedCells[0].RowIndex].Value.ToString();
            this.box_PR.Text = dgv[22, dgv.SelectedCells[0].RowIndex].Value.ToString();
            this.box_RZ.Text = dgv[23, dgv.SelectedCells[0].RowIndex].Value.ToString();
            this.comboBox3.SelectedIndex = Convert.ToInt32(dgv[24, dgv.SelectedCells[0].RowIndex].Value);
            this.comboBox2.SelectedIndex = Convert.ToInt32(dgv[25, dgv.SelectedCells[0].RowIndex].Value);
            this.box_OBOZN.ReadOnly = true;
            this.box_NALICHIE.ReadOnly = true;
            
            /*this.comboBox1.Text = dgv[26, dgv.SelectedCells[0].RowIndex].Value.ToString();*/
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

      

       
       
    }
}