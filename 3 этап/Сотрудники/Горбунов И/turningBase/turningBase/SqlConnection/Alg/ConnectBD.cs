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
        /// �������� ������ � SQL �����
        /// </summary>     
        /// <returns></returns>         
        void connectAction()
        {
            if ((!SpecialFunctions.IsEmpty(textBox1)) && (!SpecialFunctions.IsEmpty(textBox2)) && (!SpecialFunctions.IsEmpty(textBox3)) && (!SpecialFunctions.IsEmpty(textBox4)) && (!SpecialFunctions.IsEmpty(textBox5)))
            {                
                SQLOracle.BuildConnectionString(this.textBox1.Text.ToString(), this.textBox2.Text.ToString(), this.textBox3.Text.ToString(), this.textBox4.Text.ToString(), this.textBox5.Text.ToString());
                //me
                SqlOracle.BuildConnectionString(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text);


                if (SQLOracle.CheckConnection())
                {
                    this.Visible = false;
                    //����������� ���������� �� �����
                    //me
                    //((Katalog)this.Owner).ViewInform();
                    ((SootherForm) Owner).Start();
                    //Close();
                }
                 
            }
            else
            {
                MessageBox.Show("�� ��� ������ ���������.");
            }
            
        }

        /// <summary>
        /// ��������� ������� Enter
        /// </summary>
        /// <param name="e">       
        /// �������</param>
        /// <returns>true - ������ ��������
        /// false - �� ������ ��������</returns> 
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


        /// <summary>
        /// �������������� ����������
        /// </summary>     
        /// <returns></returns> 
        private void automatConnection()
        {
            String connectionString = "";
            
            if (GetServerBDparams(System.Environment.GetEnvironmentVariable("KTPP_DB_SERVER")).Length == 3)
            {
                string[] paramsBD = GetServerBDparams(System.Environment.GetEnvironmentVariable("KTPP_DB_SERVER"));

                 //��������� ������ ����������
                SQLOracle.BuildConnectionString(System.Environment.GetEnvironmentVariable("KTPP_DB_USER"),
                                    System.Environment.GetEnvironmentVariable("KTPP_DB_PASSWORD"), 
                                     paramsBD[2],
                                    paramsBD[0], 
                                    paramsBD[1] 
                                   );
                //me
                SqlOracle.BuildConnectionString(Environment.GetEnvironmentVariable("KTPP_DB_USER"), Environment.GetEnvironmentVariable("KTPP_DB_PASSWORD"), paramsBD[2], paramsBD[0], paramsBD[1]);
                //Logger.WriteLine(Environment.GetEnvironmentVariable("KTPP_DB_USER"), Environment.GetEnvironmentVariable("KTPP_DB_PASSWORD"), paramsBD[2], paramsBD[0], paramsBD[1]);
            }       
                

               

                    if (SQLOracle.CheckConnection())
                    {
                        
                        this.Visible = false;
                        //����������� ���������� �� �����
                        //me
                        //((Katalog)this.Owner).ViewInform();

                        ((SootherForm)Owner).Start();
                        //Close();
                    }
                    else
                    {

                        this.textBox1.Text = System.Environment.GetEnvironmentVariable("KTPP_DB_USER");
                        this.textBox2.Text = System.Environment.GetEnvironmentVariable("KTPP_DB_PASSWORD");
                        if (GetServerBDparams(System.Environment.GetEnvironmentVariable("KTPP_DB_SERVER")).Length == 3)
                        {
                            string[] paramsBD = GetServerBDparams(System.Environment.GetEnvironmentVariable("KTPP_DB_SERVER"));
                                                        
                            this.textBox3.Text = paramsBD[2];
                            this.textBox4.Text = paramsBD[0];
                            this.textBox5.Text = paramsBD[1];
                        }
                        

                    }
          
            
                       
           
        }


        /// <summary>
        /// ��������� ����� �������
        /// </summary>    
        /// <param name="serverString">       
        /// ������ �������</param>
        /// <returns>��� �������</returns>
        private string findCorrectServerName(String serverString)
        {
            int positionOfSym = 0, i = 0;

            String correctServerString = "";

            foreach (char findSym in serverString)
            {
                if (String.Compare((Convert.ToString(findSym)), "/") == 0)
                {
                    positionOfSym = i;
                }

                i++;

            }

            if (positionOfSym == 0)
            {
                return serverString;
            }

            i = 0;


            foreach (char findSym in serverString)
            {

                if (i > positionOfSym)
                {
                    correctServerString += findSym;
                }

                i++;

            }

            return correctServerString;

        }

        /// <summary>
        /// ��������� IP,PORT,Service Name
        /// </summary>    
        /// <param name="serverString">       
        /// ������ �������</param>
        /// <returns>��� �������</returns>
        private string[] GetServerBDparams(String serverString)
        {
            string[] paramsBD = serverString.Split(new char[] {'\\','/',':'});

            return paramsBD;

        }


    }
}