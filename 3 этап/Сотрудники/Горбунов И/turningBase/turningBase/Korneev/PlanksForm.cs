using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


    public partial class PlanksForm : Form
    {
        public PlanksForm()
        {
            SqlOracle.BuildConnectionString("ktc", "ktc", "baseeoi");
            InitializeComponent();
            //splitContainer1.BackColor = Color.Red;
            splitContainer1.BorderStyle = BorderStyle.FixedSingle;
            loadForm();
        }

        List<string> gosts = new List<string>();

        private void loadForm()
        {
            DataSet ds = SqlOracle1.getDS("SELECT * FROM USP_PLANKS_DATA");            
            dgvPlanks.DataSource = ds.Tables[0];
            dgvPlanks.Columns["GOST"].Visible = false;
            dgvPlanks.Columns["NAME"].HeaderText = "Наименование";            
            dgvPlanks.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            //dgvPlanks.Columns["NAME"].Width = dgvPlanks.Width * 3 / 4;
            Dictionary<string, bool> tgosts = new Dictionary<string, bool>();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                tgosts[row["GOST"].ToString()] = true;
            }
            foreach(string s in tgosts.Keys) {
                gosts.Add(s);
            }
            DrawItems();
        }

        private const int _HEIGHT = 220;
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

            foreach (string gost in gosts)
            {
                Image image = null;
                try
                {
                    image = SqlUspElement.GetImage(gost);
                }
                catch (TimeoutException ex) { continue; }

                ImageBox pb = new ImageBox(image, gost);

                pb.Margin = new Padding(3);

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

        private void ImageForm_SizeChanged(object sender, EventArgs e)
        {
            ItemAtRow = splitContainer1.Panel1.Width / (_WIDTH + _DIFF + 6);
        }

        private void btSelect_Click(object sender, EventArgs e)
        {
            if (dgvPlanks.SelectedRows.Count == 1) {
                eventHandler(dgvPlanks.SelectedRows[0].Index);
            } else {
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
                MessageBox.Show((dgvPlanks.Rows[selectedIndex].Cells["GOST"]).Value.ToString());
            }
            else
            {
                MessageBox.Show("Строка не выбрана");
            }
        }
    }