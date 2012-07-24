using System;
using System.Windows.Forms;
using System.Drawing;
using Emgu.CV;
using Emgu.Util;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;

namespace FaceDetection
{
    partial class Form1
    {
        private Capture cap;
        private HaarCascade haar;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::FaceDetection.Properties.Resources.pic1;
            this.pictureBox1.Location = new System.Drawing.Point(26, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(406, 334);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 10000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(446, 445);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            

        }

        #endregion

        private void timer1_Tick(object sender, EventArgs e)
        {
           // try
           // {
                
              // 
           
            //}
           // catch (Exception exp)
          //  {
        //    }

          //  cap = new Capture();
            //Bitmap b1 = new Bitmap(@"C:\Users\Shahar\Documents\Visual Studio 2010\Projects\FaceDetection\FaceDetection\Resources\pic1.bmp");
            Bitmap b1 = new Bitmap(@"..\\..\\Resources\pic1.bmp");
            using (Image<Bgr, byte> nextFrame = new Image<Bgr, byte>(b1)) 
                //.QueryFrame())
           
            {
                if (nextFrame != null)
                {
                    // there's only one channel (greyscale), hence the zero index
                    //var faces = nextFrame.DetectHaarCascade(haar)[0];
                    Image<Gray, byte> grayframe = nextFrame.Convert<Gray, byte>();
                    var faces =
                            grayframe.DetectHaarCascade(
                                    haar, 1.4, 4,
                                    HAAR_DETECTION_TYPE.DO_CANNY_PRUNING,
                                    new Size(nextFrame.Width / 8, nextFrame.Height / 8)
                                    )[0];

                    foreach (var face in faces)
                    {
                        nextFrame.Draw(face.rect, new Bgr(0, double.MaxValue, 0), 3);
                    }
                    pictureBox1.Image = nextFrame.ToBitmap();
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // passing 0 gets zeroth webcam
            cap = new Capture(0);
            // adjust path to find your xml
            haar = new HaarCascade("..\\..\\..\\..\\lib\\haarcascade_frontalface_alt2.xml");
        }

        private PictureBox pictureBox1;
        private Timer timer1;
        private object nextFrame;
    }
}