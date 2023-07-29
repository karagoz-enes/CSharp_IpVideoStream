using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Accord.Video.FFMPEG;
using Accord.Video.VFW;
using AForge.Video;

namespace videoStream
{
    public partial class Form1 : Form
    {
        MJPEGStream streamVideo;
        string url;
        int ImgNumber = 1;
        string ImgPath;
        public Form1()
        {
            InitializeComponent();
        }
        public void button1_Click(object sender, EventArgs e)
        {
            url = textBox1.Text;
            if (url == "")
            {
                MessageBox.Show("there is no url");
            }
            else if (url != "")
            {
                streamVideo = new MJPEGStream(url);
                streamVideo.NewFrame += GetNewFram;
                streamVideo.Start();
            }
        }
        private void GetNewFram(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap bmp = (Bitmap)eventArgs.Frame.Clone();
            pictureBox1.Image = bmp;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (streamVideo.IsRunning)
            {
                streamVideo.Stop();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Text = "http://158.58.130.148:80/mjpg/video.mjpg";
        }

        public object Image { get; private set; }

        private void save_Click(object sender, EventArgs e)
        {
            if (ImgNumber > 0)
            {
                ImgPath = "image folder path here" + ImgNumber + ".jpg";
                pictureBox1.Image.Save(ImgPath);
                ImgNumber++;
                pictureBox2.Image = pictureBox1.Image;
            }
        }
    }
}