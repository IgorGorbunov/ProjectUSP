using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

    public partial class PlanksForm : Form
    {
        int katalogUsp;

        public PlanksForm(int katalogUsp)
        {
            this.katalogUsp = katalogUsp;
            InitializeComponent();
            //splitContainer1.BackColor = Color.Red;
            splitContainer1.BorderStyle = BorderStyle.FixedSingle;
            loadForm();
        }

        List<string> gosts = new List<string>();
        List<string> gostNames = new List<string>();
        List<Image> gostImages = new List<Image>();
        DataView view;

        private void loadForm()
        {
            DataSet ds = SqlOracle1.getDS("SELECT * FROM KTC.USP_PLANKS_DATA WHERE KATALOG_USP = " + katalogUsp);
#if(DEBUG)
            ds = SqlOracle1.getDS("SELECT * FROM KTC.USP_PLANKS_DATA_DEBUG WHERE KATALOG_USP = " + katalogUsp);
#endif

            view = new DataView(ds.Tables[0]);
            dgvPlanks.DataSource = view;
            dgvPlanks.Columns["GOST"].Visible = false;
            dgvPlanks.Columns["KATALOG_USP"].Visible = false;
            dgvPlanks.Columns["NAME"].HeaderText = "Обозначение";
            int colWidth = 30;
            dgvPlanks.Columns["L"].MinimumWidth = colWidth;
            dgvPlanks.Columns["B"].MinimumWidth = colWidth;
            dgvPlanks.Columns["H"].MinimumWidth = colWidth;
            dgvPlanks.Columns["NAME"].MinimumWidth = 80;
            
            //dgvPlanks.Columns["NAME"].Width = dgvPlanks.Width * 3 / 4;
            Dictionary<string, bool> tgosts = new Dictionary<string, bool>();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                tgosts[row["GOST"].ToString()] = true;
            }
            foreach(string s in tgosts.Keys) {
                gosts.Add(s);
                Image image = null;
                string gostName = "ГОСТ " + s;
                try
                {
                    image = SqlUspElement.GetImage(s);
                    gostName += " " + SqlUspElement.GetName(s); 
                }
                catch (TimeoutException ex) { continue; }
                gostImages.Add(image);
                gostNames.Add(gostName);
            }
            view.RowFilter = "GOST LIKE '%" + gosts[0] + "%'";
            dgvPlanks.Refresh();
            dgvPlanks.Visible = false;
            DrawItems();
        }

        private const int _HEIGHT = 240;
        private const int _WIDTH = 180;
        private const int _DIFF = 10;

        public int ItemAtRow
        {
            get { return _itemAtRow; }
            set
            {
                if (value == _itemAtRow)
                    return;
                _itemAtRow = value;
                DrawItems();
            }
        }
        private int _itemAtRow;

        /// <summary>
        /// Добавляет элементы на форму.
        /// </summary>
        public void DrawItems()
        {
            int y;
            int x = y = _DIFF;
            _itemAtRow = splitContainer1.Panel1.Width / (_WIDTH + _DIFF + 6);

            splitContainer1.Panel1.Controls.Clear();

            for(int i = 0; i < gosts.Count; ++i) 
            {
                
                ImageBox pb = new ImageBox(gostImages[i], gostNames[i]);

                pb.Margin = new Padding(3);
                pb.pictureBox1.MouseClick += new MouseEventHandler(pb_MouseClick);
                pb.Location = new Point(x, y);

                if (x + _WIDTH + _DIFF + 6 > ItemAtRow * (_DIFF + 6) + ItemAtRow * _WIDTH)
                {
                    x = 0;
                    y += _HEIGHT + _DIFF;
                }
                else
                {
                    x += _WIDTH + _DIFF;
                }
                x += _DIFF;

                splitContainer1.Panel1.Controls.Add(pb);
            }
        }

        void pb_MouseClick(object sender, MouseEventArgs e)
        {
            int index = gostImages.IndexOf((sender as PictureBox).Image);
            view.RowFilter = "GOST LIKE '%" + gosts[index] + "%'";
            dgvPlanks.Refresh();
            dgvPlanks.Visible = true;
        }

        private void ImageForm_SizeChanged(object sender, EventArgs e)
        {
            ItemAtRow = splitContainer1.Panel1.Width / (_WIDTH + _DIFF + 6);
        }

        private void btSelect_Click(object sender, EventArgs e)
        {
            if (dgvPlanks.SelectedRows.Count == 1)
            {
                eventHandler(dgvPlanks.SelectedRows[0].Index);
            }
            else if (dgvPlanks.SelectedCells.Count > 0)
            {
                eventHandler(dgvPlanks.SelectedCells[0].RowIndex);
            }
            else
            {
                eventHandler(-1);
            }
        }

        private void dgvPlanks_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            eventHandler(e.RowIndex);    
        }

        private void eventHandler(int selectedIndex)
        {
            if (selectedIndex >= 0)
            {
                LoadPart((dgvPlanks.Rows[selectedIndex].Cells["NAME"]).Value.ToString());
            }
            else
            {
                Message.ShowError("Модель детали не выбрана!");
            }
        }

        private void dgvPlanks_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dgvPlanks.SelectedCells.Count > 0)
            {
                btSelect.Enabled = true;
            }
            else
            {
                btSelect.Enabled = false;
            }
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void LoadPart(string title)
        {
            try
            {
                Katalog2005.Algorithm.SpecialFunctions.LoadPart(title, false);
                Jig.FPlank = new FoldingPlank(Katalog2005.Algorithm.SpecialFunctions.LoadedPart);
                Close();
            }
            catch (TimeoutException)
            {
                Message.Timeout();
            }
            catch (PartNotFoundExeption ex)
            {
                Message.ShowError("Модель детали '" + ex.Message + "' не загружена в базу данных!");
            }
        }
    }