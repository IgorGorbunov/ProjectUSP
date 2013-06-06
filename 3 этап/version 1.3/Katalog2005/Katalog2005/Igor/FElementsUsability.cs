using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

    //Igor
    public partial class FElementsUsability : Form
    {
        bool isCanceled = true;

        int nFree;
        int nAll;
        int nInProject;

        int warningOffset = 63;
        int alternativeOffset = 55;

        string currentTitle;

        /// <summary>
        /// Конструктор создания окна по 
        /// </summary>
        /// <param name="elementTitle">Обозначение элемента</param>
        /// <param name="nFree">Количество свободных</param>
        /// <param name="nAll">Общее количество</param>
        /// <param name="nInProject">Кол-во элементов в проекте</param>
        public FElementsUsability(string elementTitle, int nFree, int nAll, int nInProject)
        {
            InitializeComponent();

            this.nFree = nFree;
            lFreeElems.Text = nFree.ToString();
            this.nAll = nAll;
            lAllElems.Text = nAll.ToString();
            this.nInProject = nInProject;

            if (nFree == 0)
            {
                makeOffset();
                bCurrent.Enabled = false;
            }

            currentTitle = elementTitle;
        }

        /// <summary>
        /// Отмена формы
        /// </summary>
        void cancel()
        {
            
        }

        /// <summary>
        /// смещение контролов при нулевом количестве элементов
        /// </summary>
        void makeOffset()
        {
            lWarning.Visible = false;

            int x, y;
            x = lStats.Location.X;
            y = lStats.Location.Y;
            lStats.Location = new Point(x, y - warningOffset);

            x = bStats.Location.X;
            y = bStats.Location.Y;
            bStats.Location = new Point(x, y - warningOffset);

            lAlternative.Visible = false;

            x = this.Size.Width;
            y = this.Size.Height;
            this.Size = new Size(x, y - (warningOffset + alternativeOffset));
        }

        private void bttbCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FElementsUsability_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (isCanceled)
            {
                cancel();
                Element.addElemIsCanceled = true;
            }
            else
            {
                Element.addElemIsCanceled = false;
            }
        }

        private void bttnAlternative_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Test - запуск формы с альтернативным выбором элемента");
            //TODO
            bool altIsSelected = false;

            if (altIsSelected)
            {
                Element.titleToAdd = currentTitle;
                isCanceled = false;
            }
            else
            {
                isCanceled = true;
            }

            this.Close();
            
        }
        private void bttnCurrent_Click(object sender, EventArgs e)
        {
            Element.titleToAdd = currentTitle;
            isCanceled = false;
            this.Close();
        }

        private void bStats_Click(object sender, EventArgs e)
        {
            StatsForm Fstat = new StatsForm(currentTitle, nInProject);
            Fstat.ShowDialog();
        }


    }