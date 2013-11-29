using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

//namespace img_gallery.Controls
//{
    public partial class ImageLabelBox : UserControl
    {
        public Image Image;

        public ImageLabelBox(Image image, string vppNum, string tzNum, string partTitle)
        {
            InitializeComponent();
            this.Image = pictureBox1.Image = image;
            pictureBox1.BorderStyle = BorderStyle.FixedSingle;
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            string labelText = "";
            labelText = ConcatString(labelText, "ÂÏÏ", vppNum);
            labelText = ConcatString(labelText, "ÒÇ", tzNum);
            labelText = ConcatString(labelText, "Äåòàëü", partTitle);
            label1.Text = labelText;
        }

        public ImageLabelBox(Image image, string title)
        {
            InitializeComponent();
            this.Image = pictureBox1.Image = image;
            pictureBox1.BorderStyle = BorderStyle.FixedSingle;
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            label1.Text = title;
        }

        public ImageLabelBox(ImageInfo info)
        {
            InitializeComponent();
            this.Image = pictureBox1.Image = info.Image;
            pictureBox1.BorderStyle = BorderStyle.FixedSingle;
            if (info.Selected)
            {
                pictureBox1.BackColor = Color.LightGreen;
            }
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            label1.Text = info.Name;
            lblCount.Text += info.ElementCount;
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
//}
