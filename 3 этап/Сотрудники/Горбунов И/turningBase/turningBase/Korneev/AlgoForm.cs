using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;
using algorithm;


    public partial class AlgoForm : Form
    {
        public AlgoForm()
        {
            InitializeComponent();
            int rbH = 25;
            for (int i = 0; i < 4; ++i)
            {
                ElementType type = (ElementType)i;
                RadioButton rb = new RadioButton();
                rb.Text = ElementTypeInfo.getPropertyName(type);
                rb.Location = new Point(5, i * rbH + rbH);
                grbElementType.Controls.Add(rb);
            }
        }

        private void btFind_Click(object sender, EventArgs e)
        {
            double h = double.Parse(txtHeight.Text);
            ElementType type = ElementType.None;
            for (int i = 1; i < grbElementType.Controls.Count; ++i )
            {
                if ((grbElementType.Controls[i] as RadioButton).Checked)
                {
                    type = (ElementType)i;
                    break;
                }
            }
            answer = new SelectionAlgorihtm(
                DatabaseUtils.loadFromDb(type, chkInStock.Checked, 0),
                1000).solve(h, chkInStock.Checked);
            button1.Text = answer.lowerBound.ToString();
            button2.Text = answer.mainAnswer.ToString();
            button3.Text = answer.upperBound.ToString();
        }

        Solution answer;

        void PrintAnswer(Dictionary<Element, byte> answer)
        {
            rtbAnswer.Text = "";
            if (answer != null)
            {

                foreach (Element element in answer.Keys)
                {
                    rtbAnswer.Text += element.ElementName + "\n"
                        + "высота: " + element.Height
                        + "\nколичество: " + answer[element] + "\n\n";
                }
            }
            else
            {
                rtbAnswer.Text = "Нет решений";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            PrintAnswer(answer.getUpperSolution(int.Parse(txtSolutionNumber.Text)));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PrintAnswer(answer.getMainSolution(int.Parse(txtSolutionNumber.Text)));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PrintAnswer(answer.getLowerSolution(int.Parse(txtSolutionNumber.Text)));
        }
    }
