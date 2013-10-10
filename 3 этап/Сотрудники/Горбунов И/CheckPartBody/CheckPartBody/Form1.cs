using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using NXOpen;
using NXOpen.Assemblies;

namespace CheckPartBody
{
    public partial class Form1 : Form
    {
        private StreamWriter _stream;
        private StreamWriter _stream2;
        private Component _component;

        Session.UndoMarkId _markId1;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Session theSession = Session.GetSession();
            List<string> elementTitles = new List<string>();
            string query = "select OBOZN from DB_DATA";
            if (SqlOracle.Sel(query, new Dictionary<string, string>(), out elementTitles))
            {
                _stream = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "elements.txt");
                _stream2 = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "elements2.txt");
                _stream.WriteLine("Детали, с количеством тел больше двух!" + Environment.NewLine);
                _stream2.WriteLine("Детали, которых нет в каталоге!" + Environment.NewLine);
                int count = elementTitles.Count;
                int n = 1;
                foreach (string elementTitle in elementTitles)
                {
                    SetPart(elementTitle);
            //        Session.UndoMarkId markId =
            //theSession.SetUndoMark(Session.MarkVisibility.Invisible, "Delete");

            //        theSession.UpdateManager.AddToDeleteList(_component);
            //        theSession.UpdateManager.DoUpdate(markId);
                    double procent = 100 * n / count;
                    Application.DoEvents();
                    label1.Text = Math.Round(procent, 3) + " %";
                    Application.DoEvents();
                    n++;
                }
                _stream2.Close();
                _stream.Close();
            }
            else
            {
                MessageBox.Show("Печаль с БД");
            }
        }

        private void SetPart(string title)
        {
            _markId1 = Config.TheSession.SetUndoMark(Session.MarkVisibility.Invisible,
                                                                "SetTouch");
            Katalog2005.Algorithm.SpecialFunctions.LoadPart(title, false);
            _component = Katalog2005.Algorithm.SpecialFunctions.LoadedPart;
            if (_component == null)
            {
                _stream2.WriteLine(title);
                _stream2.Flush();
                return;
            }
            BodyCollection bc = ((Part)_component.Prototype).Bodies;
            if (bc.ToArray().Length > 1)
            {
                _stream.WriteLine(title);
                _stream.Flush();
            }
            Config.TheSession.UndoToMark(_markId1, "SetTouch");
        }
    }
}