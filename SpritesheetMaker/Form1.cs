using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpritesheetMaker
{
    public partial class Form1 : Form
    {
        private string[] _files;
        private int _index;
        Bitmap _target;
        private int _tileXPosition = 1;
        private int _tileOffset = 10;
        private int _tileRow = 0;

        public Form1()
        {
            InitializeComponent();
            LoadDefaults();
        }

        private void LoadDefaults()
        {
            txtLeft.Text = @"71";
            txtTop.Text = @"65";
            txtWidth.Text = @"79";
            txtHeight.Text = @"70";
            txtTPR.Text = @"10";
            txtDirectory.Text = @"C:\Users\maxxd_000\Downloads\animget\shots\borg\borg_intro";
            _target = new Bitmap(pictureBox1.Width, pictureBox1.Height);
        }

        private void btnDirectory_Click(object sender, EventArgs e)
        {
            string folderPath = "";
            using(FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog())
            { 
                if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                    {
                        folderPath = folderBrowserDialog1.SelectedPath;
                    }
            }
            txtDirectory.Text = folderPath;
            if(!String.IsNullOrEmpty(txtDirectory.Text))
                _files = Directory.GetFiles(txtDirectory.Text);
            _index = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtDirectory.Text) && _files==null)
                _files = Directory.GetFiles(txtDirectory.Text);
            if (_index < _files.Length)
            LoadNextTile();
        }

        private void LoadNextTile()
        {
            var file = _files[_index];
            lblFile.Text = "Current file: " + file;
            Point point1 = new Point(((Convert.ToInt16(txtLeft.Text)) * _tileXPosition) + _tileOffset, (Convert.ToInt16(txtTop.Text) * _tileRow) + _tileOffset);
            Size size1 = new Size(Convert.ToInt16(txtWidth.Text), Convert.ToInt16(txtHeight.Text));
            Rectangle rect2 = new Rectangle(point1, size1);

            pictureBox1.Image = _target;
             pictureBox1.Height = _target.Height;
            pictureBox1.Width = _target.Width;

            panel1.AutoScroll = true;
            using (var source = new Bitmap(file))
            {
                Point point2 = new Point(Convert.ToInt16(txtLeft.Text),Convert.ToInt16(txtTop.Text));
                Size size2 = new Size(Convert.ToInt16(txtWidth.Text),Convert.ToInt16(txtHeight.Text));
                Rectangle rect = new Rectangle(point2, size2);
                BitmapFunctions.CopyRegionIntoImage(source, rect, ref _target, rect2);
            }
            pictureBox1.Image = _target;
            _tileXPosition++;
            if (_tileXPosition > Convert.ToInt16(txtTPR.Text))
            {
                _tileXPosition = 1;
                _tileRow++;
            }
            int tilesHeight = _tileRow * ((Convert.ToInt16(txtHeight.Text)+_tileOffset));
            if (tilesHeight > _target.Height)
                _target = BitmapFunctions.ExpandTargetBitmap(_target, pictureBox1.Width,pictureBox1.Height+Convert.ToInt16(txtHeight.Text)+Convert.ToInt16(_tileOffset));
            _index++;
            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtDirectory.Text) && _files == null)
                _files = Directory.GetFiles(txtDirectory.Text);
            foreach (var file in _files)
            {
                LoadNextTile();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var path = !String.IsNullOrEmpty(txtFile.Text)? txtFile.Text : Environment.CurrentDirectory + "\\test.png";
            _target.Save(path);
            Process.Start(Path.GetDirectoryName(path));
        }

 

    }
}

