using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Katalog2005.WinFroms.Common
{
    public partial class InputBox : Form
    {
        public InputBox(string Caption,string ButtonName,string LabelName)
        {
            InitializeComponent();
            this.label1.Text = LabelName;
            this.button1.Text = ButtonName;
            this.Text = Caption;
        }

        private void InputBox_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private bool nonNumberEntered = false;

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode < Keys.D0 || e.KeyCode > Keys.D9)
            {
                
                if (e.KeyCode < Keys.NumPad0 || e.KeyCode > Keys.NumPad9)
                {
                  
                    if (e.KeyCode != Keys.Back)
                    {                        
                        nonNumberEntered = true;
                    }
                }
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (nonNumberEntered == true)
            {                
                e.Handled = true;
                nonNumberEntered = false;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}