using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.OracleClient;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Katalog2005.Algorithm;


namespace Katalog2005.WinFroms.search
{
    public partial class search : Form
    {
        private ArrayOfComboBox newLineOfComBox;
        private ArrayOfTextBox newLineOfTextBox;
        System.Windows.Forms.DataGridView dgv;

         OracleConnection _connect = new  OracleConnection();
        public  OracleDataAdapter oracleDataAdapter1 = new  OracleDataAdapter();
        public DataSet dataSet11 = new DataSet();
        public  OracleCommand oracleSelectCommand1 = new  OracleCommand();

        public search(System.Windows.Forms.DataGridView GetDGV)
        {
            InitializeComponent();
            newLineOfComBox = new ArrayOfComboBox(this);
            newLineOfTextBox = new ArrayOfTextBox(this);
            newLineOfTextBox[0].KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBox_KeyDown);
         
            dgv = GetDGV;
        }

        private void search_Load(object sender, EventArgs e)
        {

        }


        void createNewSearchLine()
        {
            try
            {

                if (newLineOfComBox.Count < 6)
                {
                    this.Height = this.Height + 21;
                    newLineOfComBox.AddNewComboBox();
                    newLineOfTextBox.AddNewTextBox();
                    newLineOfTextBox[newLineOfTextBox.Count - 1].KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBox_KeyDown);


                    this.button1.Enabled = true;
                    this.radioAND.Enabled = true;
                    this.radioOR.Enabled = true;
                }
                else
                    MessageBox.Show("Нельзя добавлять более 5 полей!");

            }
            catch (Exception e)
            {

                MessageBox.Show(e.ToString());

            }



        }

        void deleteLastSearchLine()
        {
            try
            {


                this.Height = this.Height - 21;

                if (this.Height < 57)
                {
                    button1.Enabled = false;
                    radioAND.Enabled = false;
                    radioOR.Enabled = false;

                }
                newLineOfComBox.Remove();
                newLineOfTextBox.Remove();

            }
            catch (Exception e)
            {

                MessageBox.Show(e.ToString());

            }


        }

        private void addButtonCon_Click(object sender, EventArgs e)
        {
            createNewSearchLine();	
        }

        private void button1_Click(object sender, EventArgs e)
        {
            deleteLastSearchLine();	
        }

        private void searchBtnCon_Click(object sender, EventArgs e)
        {
            clickButtonSearchFn();
        }


        public void clickButtonSearchFn()
        {
            if (checkEmptyFields() == true)
            {
                try
                {
                    searchData(newLineOfComBox, newLineOfTextBox, this.radioAND.Checked);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());

                }
            }
            else
            {
                MessageBox.Show("Одно из полей пустое");
            }
        }

        private bool checkEmptyFields()
        {
            for (int i = 0; i < newLineOfTextBox.Count; i++)
            {
                if (newLineOfTextBox[i].Text == "")
                    return false;

            }

            return true;
        }

        private void TextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                clickButtonSearchFn();

            }
        }

        private void searchData(ArrayOfComboBox ComboBoxList, ArrayOfTextBox TextBoxList, bool AndOrSelect)
        {                     

            System.Collections.Generic.List<string> Parameters = new System.Collections.Generic.List<string>();
            System.Collections.Generic.List<string> DataFromTextBox = new System.Collections.Generic.List<string>();
          

            String searchColumn = "";
            String searchStringLine = "SELECT NAME , OBOZN , GOST,  L, " +
                " B,  B1, H,  D, D1, D_SM_DB , D1_SM_DB, " +
                " A,  S, B_SM_DB ,  H0,  T,  N,H_SM_DB, MASSA" +
                " , NALICHI , TT, YT, PR, RZ, GROUP_USP, KATALOG_USP FROM DB_DATA WHERE ";

            

            for (int i = 0; i < ComboBoxList.Count; i++)
            {
                switch (((System.Windows.Forms.ComboBox)ComboBoxList[i]).SelectedItem.ToString())
                {
                    case "Наименование":
                        searchColumn = "NAME";
                        break;
                    case "Обозначение":
                        searchColumn = "OBOZN";
                        break;
                    case "ГОСТ":
                        searchColumn = "GOST";
                        break;
                    case "d":
                        searchColumn = "D_SM_DB";
                        break;
                    case "d1":
                        searchColumn = "D1_SM_DB";
                        break;
                    case "alfa":
                        searchColumn = "A";
                        break;
                    case "b":
                        searchColumn = "B_SM_DB";
                        break;
                    case "h":
                        searchColumn = "H_SM_DB";
                        break;
                    case "t":
                        searchColumn = "T";
                        break;
                    case "Масса":
                        searchColumn = "MASSA";
                        break;
                    case "Наличие":
                        searchColumn = "NALICHI";
                        break;
                    case "Месторасположение":
                        searchColumn = "UG";
                        break;
                    default:
                        searchColumn = ((System.Windows.Forms.ComboBox)ComboBoxList[i]).SelectedItem.ToString();
                        break;
                }
              

                if (i != 0)
                {
                        if (AndOrSelect)
                        {

                            searchStringLine += " AND " + searchColumn + " LIKE :" + searchColumn + " ";
                            Parameters.Add(searchColumn);
                            DataFromTextBox.Add((string)("%" + (((System.Windows.Forms.TextBox)TextBoxList[i]).Text) + "%"));
                           
                        }
                        else
                        {
                            searchStringLine += " OR " + searchColumn + " LIKE :" + searchColumn  + " ";
                            Parameters.Add(searchColumn);
                            DataFromTextBox.Add((string)("%" + (((System.Windows.Forms.TextBox)TextBoxList[i]).Text) + "%"));
                           
                        }
                }
                else
                {

                    searchStringLine += " " + searchColumn + " LIKE :" + searchColumn + " ";
                    
                    Parameters.Add(searchColumn);
                    DataFromTextBox.Add((string)("%" + (((System.Windows.Forms.TextBox)TextBoxList[i]).Text) + "%"));
                 
                }
               

        }

        dgv.DataSource = SQLOracle.ParamQuerySelect(searchStringLine, Parameters, DataFromTextBox).Tables[0];

        SpecialFunctions.hideEmptyColumn(dgv);

        searchStringLine = "";
    }

    }
}