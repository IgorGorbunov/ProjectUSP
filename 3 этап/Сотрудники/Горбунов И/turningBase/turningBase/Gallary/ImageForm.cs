using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;


    public partial class ImageForm : Form
    {
        public List<ImageInfo> Images; 
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

        public ImageForm(List<ImageInfo> images, MouseEventHandler mouseClickEventHandler)
        {
            InitializeComponent();
            Images = images;
            MouseClickEventHandler = mouseClickEventHandler;
        }

        protected ImageForm()
        {
            InitializeComponent();
        }

        protected ImageForm(MouseEventHandler mouseClickEventHandler)
        {
            InitializeComponent();
            MouseClickEventHandler = mouseClickEventHandler;
        }

        private const int _HEIGHT = 220;
        private const int _WIDTH = 180;
        private const int _DIFF = 10;

        /// <summary>
        /// ��������� �������� �� �����.
        /// </summary>
        public void DrawItems()
        {
            int y = _DIFF;
            int x = _DIFF;
            _itemAtRow = mainPanel.Width / (_WIDTH + _DIFF + 6);

            mainPanel.Controls.Clear();

            if (Images == null)
                return;

            foreach (ImageInfo info in Images)
            {
                ImageBox pb = new ImageBox(info.Image, info.Name);

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