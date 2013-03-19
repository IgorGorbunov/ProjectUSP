using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Katalog2005
{
    public partial class ConnectBD : Form
    {
        public ConnectBD()
        {
            InitializeComponent();
        }

        private void button1_KeyDown(object sender, KeyEventArgs e)
        {
            if (EnterDownFilter(e))
            {
                connectAction();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            connectAction();
        }

        private void ConnectBD_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

       
        


       

    

      
    }
}