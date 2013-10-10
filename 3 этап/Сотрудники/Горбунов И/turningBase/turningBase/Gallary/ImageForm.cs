using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using img_gallery.Controls;

namespace img_gallery
{
    public partial class ImageForm : Form
    {
        public Dictionary<Image, string> Images; 
        public readonly MouseEventHandler MouseClickEventHandler;
        
        public int ItemAtRow
        {
            get { return _itemAtRow; }
            set {
                if (value == _itemAtRow) 
                    return;
                _itemAtRow = value;
                DrawItems();
            }
        }
        private int _itemAtRow;

        public ImageForm(Dictionary<Image, string> images, MouseEventHandler mouseClickEventHandler)
        {
            InitializeComponent();
            Images = images;
            MouseClickEventHandler = mouseClickEventHandler;
        }

        protected ImageForm()
        {
            InitializeComponent();
        }

        private const int _HEIGHT = 170;
        private const int _WIDTH = 150;
        private const int _DIFF = 10;

        /// <summary>
        /// Добавляет элементы на форму.
        /// </summary>
        public void DrawItems()
        {
            int y;
            int x = y = _DIFF;
            _itemAtRow = mainPanel.Width / (_WIDTH + _DIFF + 6);

            mainPanel.Controls.Clear();

            foreach (KeyValuePair<Image, string> keyValuePair in Images)
            {
                Image img = keyValuePair.Key;
                ImageBox pb = new ImageBox(img, keyValuePair.Value);

                pb.Margin = new Padding(3);
                pb.pictureBox1.MouseClick += MouseClickEventHandler;

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

                mainPanel.Controls.Add(pb);
            }
        }

        private void ImageForm_SizeChanged(object sender, EventArgs e)
        {
            ItemAtRow = mainPanel.Width / (_WIDTH + _DIFF + 6);
        }
    }
}