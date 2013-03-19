using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Katalog2005.WinFroms.search
{
    public partial class InformAboutElement : Form
    {
        public InformAboutElement()
        {
            InitializeComponent();
        }

        private void InformAboutElement_Load(object sender, EventArgs e)
        {
            this.Height = DataInformTable.Rows.GetRowsHeight(DataGridViewElementStates.Visible) + 100;
            DataInformTable.Columns[0].Width = DataInformTable.Width / 2 - 20;
            DataInformTable.Columns[1].Width = DataInformTable.Width / 2 - 20; 
        }

        private void menuStrip1_SizeChanged(object sender, EventArgs e)
        {
            DataInformTable.Columns[0].Width = DataInformTable.Width / 2 - 20;
            DataInformTable.Columns[1].Width = DataInformTable.Width / 2 - 20; 
        }

        public void AddRowToDataGrid(string Parametr, string Value)
        {
            DataInformTable.Rows.Add(Parametr, Value);            
        }

   

        private void DataInformTable_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (String.Compare(DataInformTable[0, e.RowIndex].Value.ToString(), "Технические требования") == 0)
            {
                DataInformTable.Rows[e.RowIndex].Height = 50;
              
            }
        }

        private void DataInformTable_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            ((TextBox)e.Control).ReadOnly = true; 
        }

        private void отобразитьВExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExcelClass InformationAboutElements = new ExcelClass();

            Font HeadFont = new Font(" Times New Roman ", 12.0f, FontStyle.Bold);
        

            try
            {    
                
                int CurrentCell=0;
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

                for (int i = 0; i < DataInformTable.RowCount; i++)
                {
                    CurrentCell = i + 2;
                    InformationAboutElements.SelectCells(("A" + CurrentCell.ToString()), Type.Missing);
                    InformationAboutElements.WriteDataToCell(DataInformTable[0,i].Value.ToString());
                    InformationAboutElements.SetHorisontalAlignment(2);
                    InformationAboutElements.SetBorderStyle(0, Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous, Microsoft.Office.Interop.Excel.XlBorderWeight.xlThick);

                    InformationAboutElements.SelectCells(("B" + CurrentCell.ToString()), Type.Missing);
                    InformationAboutElements.WriteDataToCell(DataInformTable[1, i].Value.ToString());
                    InformationAboutElements.SetHorisontalAlignment(2);
                    InformationAboutElements.SetBorderStyle(0, Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous, Microsoft.Office.Interop.Excel.XlBorderWeight.xlThick);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Error");               

            }
            finally {

                InformationAboutElements.Visible = true;
                InformationAboutElements.Dispose();
                HeadFont.Dispose();

            }
            
                
            
        }

       

    }
}