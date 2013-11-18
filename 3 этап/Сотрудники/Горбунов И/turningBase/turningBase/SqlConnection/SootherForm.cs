using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Katalog2005;


public partial class SootherForm : Form
    {
        private static DialogProgpam _startProgram;

        public SootherForm()
        {
            InitializeComponent();
            Visible = false;
            
        }

        /// <summary>
        /// Запуск формы авторизации
        /// </summary>   
        /// <returns></returns>
        void Authorization()
        {
            ConnectBD authForm = new ConnectBD();
            authForm.StartPosition = FormStartPosition.CenterScreen;
            authForm.Owner = this;
            authForm.ShowDialog(this);

        }

        public void start()
        {
            _startProgram = new Buttons();
            _startProgram.Show();
        }

    private void SootherForm_Load(object sender, EventArgs e)
    {
        Authorization();
    }
    }