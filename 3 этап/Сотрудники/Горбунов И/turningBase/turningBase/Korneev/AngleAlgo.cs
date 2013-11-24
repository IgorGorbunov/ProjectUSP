using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using algorithm;


    public partial class AngleAlgo : Form
    {

        List<Element> bigAngleElements;

        public AngleAlgo()
        {
            InitializeComponent();
            bigAngleElements = DatabaseUtils.loadAngleElement(ElementType.BigAngle, false, 0);
            /*foreach(Element element in bigAngleElements) {
                lstBigAngle.Items.Add(element.ElementName + " " + AngleConverter.IntToString((int)element.Height));
            }*/
        }

        List<Element> currentList;

        private void btFind_Click(object sender, EventArgs e)
        {
            List<Element> optimalElements = tryAllSolution();
            if (optimalElements != null)
            {
                foreach (Element element in optimalElements)
                {
                    lstBigAngle.Items.Add(element.ElementName + " " + AngleConverter.IntToString((int)element.Height));
                }
            }
            currentList = optimalElements;
        }

        private List<Element> tryAllSolution()
        {
            double h = AngleConverter.StringToInt(txtHeight.Text);
            answer = new SelectionAlgorihtm(
                DatabaseUtils.loadAngleElement(ElementType.SmallAngle, chkInStock.Checked, 0),
                11000, 1).solve(h, chkInStock.Checked);
            byte minCount = 99;
            foreach (Element element in bigAngleElements)
            {
                minCount = Math.Min(minCount, answer.getElementCount(h - element.Height));
            }
            if (minCount == 99)
            {
                return null;
            }
            else
            {
                List<Element> optimalElements = new List<Element>();
                foreach (Element element in bigAngleElements)
                {
                    if (answer.getElementCount(h - element.Height) == minCount)
                    {
                        optimalElements.Add(element);
                    }
                }
                return optimalElements;
            }
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
                        + "Угол: " + AngleConverter.IntToString((int)element.Height)
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

        private void button4_Click(object sender, EventArgs e)
        {
            double h = AngleConverter.StringToInt(txtHeight.Text);
            int index = lstBigAngle.SelectedIndex;
            if (index >= 0)
            {
                h -= currentList[index].Height;
            }
            answer = new SelectionAlgorihtm(
                DatabaseUtils.loadAngleElement(ElementType.SmallAngle, chkInStock.Checked, 0),
                11000, 1).solve(h, chkInStock.Checked);
            button1.Text = AngleConverter.IntToString((int)answer.lowerBound);
            button2.Text = AngleConverter.IntToString((int)answer.mainAnswer);
            button3.Text = AngleConverter.IntToString((int)answer.upperBound);
        }
    }