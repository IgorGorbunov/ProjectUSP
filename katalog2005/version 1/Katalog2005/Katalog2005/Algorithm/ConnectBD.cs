using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Katalog2005.Algorithm;


namespace Katalog2005
{
    public partial class ConnectBD 
    {
        /// <summary>
        /// Передает строку в SQL класс
        /// </summary>     
        /// <returns></returns>         
        void connectAction()
        {
            if ((!SpecialFunctions.IsEmpty(textBox1)) && (!SpecialFunctions.IsEmpty(textBox2)) && (!SpecialFunctions.IsEmpty(textBox3)))
            {                
                SQLOracle.BuildConnectionString(this.textBox1.Text.ToString(), this.textBox2.Text.ToString(), this.textBox3.Text.ToString());

                if (SQLOracle.CheckConnection())
                {
                    this.Visible = false;
                    //отображение информации на форме
                    ((Katalog)this.Owner).ViewInform();
                }
                 
            }
            else
            {
                MessageBox.Show("Не все строки заполнены.");
            }
            
        }

        /// <summary>
        /// Проверяет нажатие Enter
        /// </summary>
        /// <param name="e">       
        /// событие</param>
        /// <returns>true - верное значение
        /// false - не верное значение</returns> 
        static bool EnterDownFilter(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                return true;
            }
            else {

                return false;
            }
        }

    }
}