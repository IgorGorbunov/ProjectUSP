using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using NXOpen;
using NXOpen.UF;
using NXOpen.Utilities;
using NXOpenUI;
using NXOpen.Assemblies;
using NXOpen.Preferences;


namespace Katalog2005
{
    public partial class Katalog : Form
    {
        public Katalog()
        {
            InitializeComponent();
        }


        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            setFormSize();                
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void общаяИнформацияУСПToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            if (tabControl1.SelectedTab != this.tabPage1)
            {
                tabControl1.SelectedTab = this.tabPage1;
            }
                
        }

       

        private void компонентыСборкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab != this.tabPage2)
            {
                tabControl1.SelectedTab = this.tabPage2;
            }
        }

        private void экпортВExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage1"])
            {
                if ((dataGridView1.RowCount > 0) && ((dataGridView1.RowCount < 200)))
                {
                    ExportDGVToExcel(dataGridView1);
                }
                else
                {
                    MessageBox.Show("Выберите меньшее количество строк для экспорта");
                }
                
             }
             else if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage2"])
             {
                 if (dataGridView2.RowCount > 0)
                 { 
                    ExportDGVToExcel(dataGridView2);
                 }
                 
             }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            defineTypeOfModel();

            //GetCountOfElementInAssembly("7084-0173");
        }

        private void dataGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            loadPictureInPictureBox(e);
        }

        private void dataGridView1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                loadPictureInPictureBoxWithKeyEvent(dataGridView1.SelectedCells[0].RowIndex);

            }

            if (e.KeyCode == Keys.Up)
            {
                loadPictureInPictureBoxWithKeyEvent(dataGridView1.SelectedCells[0].RowIndex);
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            sortInformWithTree(e); 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (Katalog2005.WinFroms.search.search newSearchInBD = new Katalog2005.WinFroms.search.search(dataGridView1))
            {
                newSearchInBD.ShowDialog();
                newSearchInBD.Dispose();
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ViewInformationAboutElement(e.RowIndex);
        }

        private void загрузкаДанныхToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab != this.tabPage1)
            {
                tabControl1.SelectedTab = this.tabPage1;
            }
        }

        private void внестиИнформациюПоСуществующейМоделиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Katalog2005.WinFroms.AddInformationAboutElements.AddInformation AddForm = new Katalog2005.WinFroms.AddInformationAboutElements.AddInformation(1);
            
            AddForm.Show();
            
        }

        private void Katalog_Shown(object sender, EventArgs e)
        {
            Katalog2005.Algorithm.SpecialFunctions.initUGData();

            authorization();
        }

        private void редактироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WinFroms.AddInformationAboutElements.AddInformation AddForm = new WinFroms.AddInformationAboutElements.AddInformation(2, dataGridView1);
            
            AddForm.Show();
               
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SQLOracle.EditNumberOFElemnt(dataGridView1);
        }

        private void dToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            createDataTableOfDetailes();
        }       
    }
}