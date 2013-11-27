using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

    public partial class ImageBox : UserControl
    {
        public Image Image;

        public ImageBox(Image image, string vppNum, string tzNum, string partTitle)
        {
            InitializeComponent();
            this.Image = pictureBox1.Image = image;
            pictureBox1.BorderStyle = BorderStyle.FixedSingle;
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            string labelText = "";
            labelText = ConcatString(labelText, "боо", vppNum);
            labelText = ConcatString(labelText, "рг", tzNum);
            if (!String.IsNullOrEmpty(partTitle))
            {
                labelText += ";\n" + partTitle.Trim();
            }
            label1.Text = labelText;
        }

        public ImageBox(Image image, string title)
        {
            InitializeComponent();
            this.Image = pictureBox1.Image = image;
            pictureBox1.BorderStyle = BorderStyle.FixedSingle;
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            label1.Text = title;
        }

        string ConcatString(string s, string key, string value)
        {
            if (value == null) return s;
            value = value.Trim();
            if (!String.IsNullOrEmpty(value))
            {
                if (!String.IsNullOrEmpty(s))
                {
                    s += "; ";
                }
                s += key + ": " + value;
            }
            return s;
        }
    }
