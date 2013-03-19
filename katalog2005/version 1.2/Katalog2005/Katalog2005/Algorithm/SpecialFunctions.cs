using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

using NXOpen;
using NXOpen.UF;
using NXOpen.Utilities;
using NXOpenUI;
using NXOpen.Assemblies;
using NXOpen.Preferences;


namespace Katalog2005.Algorithm
{
    partial class SpecialFunctions
    {
        public static string IsNullParametr(System.Windows.Forms.TextBox CheckThisBox)
        {
            string NameParametr = "";

            if ((CheckThisBox.Text.Length > 0))
            {

                foreach (char StrName in CheckThisBox.Text)
                {

                    NameParametr = String.Concat(NameParametr, Convert.ToString(StrName));

                }

                return NameParametr;

            }
            else
            {
                return NameParametr = " ";
            }
        }



      
        public static string IsNullParametr(System.Windows.Forms.ComboBox CheckThisBox)
        {
            string NameParametr = "";

            if ((CheckThisBox.Text.Length > 0))
            {

                foreach (char StrName in CheckThisBox.Text)
                {

                    NameParametr = String.Concat(NameParametr, Convert.ToString(StrName));

                }

                return NameParametr;

            }
            else
            {
                return NameParametr = " ";
            }
        }

        /// <summary>
        /// Функция проверки данных из TextBox
        /// </summary>
        /// <param name="CheckThisBox">       
        ///CheckThisBox.Text == "значение" - Возвращает это значение.
        ///CheckThisBox.Text == "" - возвращает 0 для использования данных в арифметических операциях.
        ///CheckThisBox.Text == " " - возвращает 0 для использования данных в арифметических операциях (такой вариант возможн при выгрузки из БД пустых значений).</param>
        /// <returns></returns>   
        public static string IsEmptyParametr(System.Windows.Forms.TextBox CheckThisBox)
        {
            string NameParametr = "";

            if ((CheckThisBox.Text.Length > 0) && (String.Compare(CheckThisBox.Text," ")!=0))
            {

                foreach (char StrName in CheckThisBox.Text)
                {

                    NameParametr = String.Concat(NameParametr, Convert.ToString(StrName));

                }

                return NameParametr;

            }
            else if ((CheckThisBox.Text.Length > 0) && ((String.Compare(CheckThisBox.Text, "") == 0)))
            {
                return NameParametr = "0";
            }
            else
            {
                return NameParametr = "0";
            }
        }

        public static bool IsNumber(string num)
        {
            System.Text.RegularExpressions.Regex rxNums = new System.Text.RegularExpressions.Regex(@"^\d+$");

            if (rxNums.IsMatch(num))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void blockKeyNotNumber(KeyPressEventArgs e, System.Windows.Forms.TextBox textBox)
        {
            if (((int)e.KeyChar) == 22)
            {
                int resulDouble = 0;

                bool isdouble = Int32.TryParse(Clipboard.GetText(TextDataFormat.Text), out resulDouble);

                if (isdouble)
                {
                                        
                    return;

                }
                else
                {

                    MessageBox.Show("Скопированные данные не соответствуют числовому формату!");

                }
            }

            //ctrl+copy
            if (((int)e.KeyChar) == 3)
            {
                return;
            }

            if (e.KeyChar == (char)Keys.Back)
            {
                return;
            }

            if ((e.KeyChar >= '0' && e.KeyChar <= '9'))
            {
                return;
            }
            e.Handled = true;

        }


        public static void blockKeyNotNumberExceptPercent(KeyPressEventArgs e, System.Windows.Forms.TextBox textBox)
        {
            if (((int)e.KeyChar) == 22)
            {
                int resulDouble = 0;

                bool isdouble = Int32.TryParse(Clipboard.GetText(TextDataFormat.Text), out resulDouble);

                if (isdouble)
                {

                    return;

                }
                else
                {

                    MessageBox.Show("Скопированные данные не соответствуют числовому формату!");

                }
            }

            //shift+5 (%)
            if (((int)e.KeyChar) == 37)
            {
                return;
            }

            //ctrl+copy
            if (((int)e.KeyChar) == 3)
            {
                return;
            }

            if (e.KeyChar == (char)Keys.Back)
            {
                return;
            }

            if ((e.KeyChar >= '0' && e.KeyChar <= '9'))
            {
                return;
            }
            e.Handled = true;

        }

        public static void blockKeyNotNumber(KeyPressEventArgs e, System.Windows.Forms.ComboBox comboBox)
        {
            if (((int)e.KeyChar) == 22)
            {
                int resulDouble = 0;

                bool isdouble = Int32.TryParse(Clipboard.GetText(TextDataFormat.Text), out resulDouble);

                if (isdouble)
                {

                    return;

                }
                else
                {

                    MessageBox.Show("Скопированные данные не соответствуют числовому формату!");

                }
            }

            //ctrl+copy
            if (((int)e.KeyChar) == 3)
            {
                return;
            }

            if (e.KeyChar == (char)Keys.Back)
            {
                return;
            }

            if ((e.KeyChar >= '0' && e.KeyChar <= '9'))
            {
                return;
            }
            e.Handled = true;

        }


        public static void blockKeyNotMoney(KeyPressEventArgs e, System.Windows.Forms.TextBox textBox)
        {
            int counDot = 0;

            if (((int)e.KeyChar) == 22)
            {  
                double resulDouble=0;

                bool isdouble = double.TryParse( Clipboard.GetText(TextDataFormat.Text),out resulDouble);

                if (isdouble)
                {

                    textBox.Clear();
                    Clipboard.SetText(Convert.ToString(Math.Round(resulDouble, 2)));
                    return;

                }
                else {

                    MessageBox.Show("Скопированные данные не соответствуют денежному формату!");
       
                 }
            }
            //ctrl+copy
            if (((int)e.KeyChar) == 3)
            {
                return;
            }

            if(e.KeyChar == ',')
            {
                
                foreach (char symb in textBox.Text)
                { 
                    if(symb==',')
                    {
                        counDot++;
                    }
                        
                }

                if (counDot >= 1)
                {
                    MessageBox.Show("Нельзя вводить больше одной запятой для цены!");
                }
                else {
                        return;
                    
                }

                counDot = 0;
            }
                

            if (e.KeyChar == (char)Keys.Back)
            {
                return;
               
            }

            if ((e.KeyChar >= '0' && e.KeyChar <= '9'))
            {
                return; 
            }

            e.Handled = true;

        }


        public static void blockKeyNotMoneyLeaveEvent(System.Windows.Forms.TextBox textBox)
        {
    
             double resulDouble = 0;

                bool isdouble = double.TryParse(textBox.Text, out resulDouble);
               
                if (isdouble)
                {
                    textBox.Clear();
                    textBox.Text = Convert.ToString(Math.Round(resulDouble, 2));
                    
                }   
        }

        /// <summary>
        /// Функция проверки данных из TextBox
        /// </summary>
        /// <param name="CheckThisBox">       
        /// CheckThisBox  - TextBox для проверки</param>
        /// <returns>true - пустое значение
        /// false - не пустое значение</returns>  
        public static bool IsEmpty(System.Windows.Forms.TextBox CheckThisBox)
        {
            if (CheckThisBox.Text.Length == 0)
            {    
                return true;
            }
            else
            {
                return false;
            }
        }
        

        /// <summary>
        /// Скрытие пустых колонок
        /// </summary>   
        /// <returns></returns>
        public static void hideEmptyColumn(System.Windows.Forms.DataGridView dgv)
        {
            bool columnIsEmpty = true;

           
            for (int i = 0; i < dgv.ColumnCount; i++)
            {
                dgv.Columns[i].Visible = true;
            }

            SpecialFunctions.hideUnusefulColumn(dgv);

            for (int i = 0; i < dgv.ColumnCount; i++)
            {
                for (int j = 0; j < dgv.RowCount; j++)
                {
                    columnIsEmpty = true;

                    if (String.Compare(dgv.Rows[j].Cells[i].Value.ToString(), "0") != 0)
                    {
                        columnIsEmpty = false;
                        break;

                    }

                }

                if (columnIsEmpty == true)
                {
                    dgv.Columns[i].Visible = false;
                }
            }
        }


        /// <summary>
        /// Скрытие неиспользуемой информации
        /// </summary>   
        /// <returns></returns>
        public static void hideUnusefulColumn(System.Windows.Forms.DataGridView dgv)
        {
            dgv.Columns["TT"].Visible = false;
            dgv.Columns["YT"].Visible = false;
            dgv.Columns["PR"].Visible = false;
            dgv.Columns["RZ"].Visible = false;
            dgv.Columns["GROUP_USP"].Visible = false;
            dgv.Columns["KATALOG_USP"].Visible = false;

        }


       
    }


}
