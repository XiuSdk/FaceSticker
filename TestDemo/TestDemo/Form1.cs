using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.IO;

namespace TestDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            zp = new TestDemo.TestBitmap();
            try
            {
                zp.FDInitialize();
                fdInitialize = true;
            }
            catch (Exception) { fdInitialize = false; }
            
        }

        #region  变量声明
        //图像路径
        private String curFileName = null;
        //当前图像变量
        private Bitmap curBitmap = null;
        //原始图像变量
        private Bitmap srcBitmap = null;
        TestBitmap zp = new TestBitmap();
        private bool fdInitialize = false;
        #endregion

        #region  图像打开保存模块
        //打开图像函数
        public void OpenFile()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "所有图像文件 | *.bmp; *.pcx; *.png; *.jpg; *.gif;" +
                   "*.tif; *.ico; *.dxf; *.cgm; *.cdr; *.wmf; *.eps; *.emf|" +
                   "位图( *.bmp; *.jpg; *.png;...) | *.bmp; *.pcx; *.png; *.jpg; *.gif; *.tif; *.ico|" +
                   "矢量图( *.wmf; *.eps; *.emf;...) | *.dxf; *.cgm; *.cdr; *.wmf; *.eps; *.emf";
            ofd.ShowHelp = true;
            ofd.Title = "打开图像文件";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                curFileName = ofd.FileName;
                try
                {
                    curBitmap = (Bitmap)System.Drawing.Image.FromFile(curFileName);
                    srcBitmap = new Bitmap(curBitmap);
                }
                catch (Exception exp)
                { MessageBox.Show(exp.Message); }
            }
        }
        //保存图像函数
        public void SaveFile()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = @"Bitmap文件(*.bmp)|*.bmp|Jpeg文件(*.jpg)|*.jpg|PNG文件(*.png)|*.png|所有合适文件(*.bmp,*.jpg,*.png)|*.bmp;*.jpg;*.png";
            sfd.FilterIndex = 3;
            sfd.RestoreDirectory = true;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                ImageFormat format = ImageFormat.Jpeg;
                switch (Path.GetExtension(sfd.FileName).ToLower())
                {
                    case ".jpg":
                        format = ImageFormat.Jpeg;
                        break;
                    case ".bmp":
                        format = ImageFormat.Bmp;
                        break;
                    case ".png":
                        format = ImageFormat.Png;
                        break;
                    default:
                        MessageBox.Show("Unsupported image format was specified!");
                        return;
                }
                pictureBox1.Image.Save(sfd.FileName, format);
            }

        }
        //打开图像
        private void openBtn_Click(object sender, EventArgs e)
        {
            OpenFile();
            if (curBitmap != null)
            {
                pictureBox1.Image = (Image)curBitmap;
            }
        }
        //保存图像
        private void saveBtn_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
                SaveFile();
        }
        #endregion

        //确定
        private void okBtn_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                if (!fdInitialize)
                    MessageBox.Show("FD_INITIALIZE FAILED!");
                int[] faceInfos = new int[15];
                zp.FDProcess(srcBitmap,ref faceInfos);
                if (faceInfos[0] > 0)
                {
                    Graphics g = Graphics.FromImage(curBitmap);
                    g.DrawRectangle(new Pen(Color.YellowGreen, 2), new Rectangle(faceInfos[1], faceInfos[2], faceInfos[3], faceInfos[4]));
                    for (int i = 0; i < 5; i++)
                    {
                        g.DrawRectangle(new Pen(Color.Red, 1), faceInfos[5 + 2 * i] - 1, faceInfos[5 + 2 * i + 1] - 1, 2, 2);
                    }
                    g.Dispose();
                    pictureBox1.Image = (Image)curBitmap;
                }
                else
                    MessageBox.Show("No face!");
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                if (!fdInitialize)
                    MessageBox.Show("FD_INITIALIZE FAILED!");
                int[] faceInfos = new int[15];
                zp.FDProcess(srcBitmap, ref faceInfos);
                int[] modelFacePoints = new int[] { 281, 601, 284, 665, 294, 726, 310, 788, 332, 846, 364, 898, 403, 941, 449, 980, 499, 1012, 560, 1022, 616, 1007, 662, 971, 701, 929, 736, 883, 762, 830, 781, 771, 793, 709, 799, 648, 798, 587, 315, 559, 340, 527, 377, 515, 419, 517, 460, 526, 489, 554, 452, 553, 416, 546, 381, 543, 348, 548, 596, 553, 624, 525, 664, 515, 706, 512, 742, 524, 767, 555, 734, 545, 702, 540, 667, 543, 632, 551, 375, 623, 387, 609, 404, 601, 424, 599, 446, 603, 464, 615, 477, 634, 459, 637, 441, 641, 422, 643, 402, 639, 388, 632, 618, 635, 629, 616, 647, 603, 669, 599, 689, 600, 706, 608, 719, 620, 706, 631, 691, 639, 672, 643, 653, 642, 635, 639, 512, 626, 514, 681, 507, 737, 481, 765, 495, 798, 537, 803, 566, 802, 607, 794, 617, 760, 590, 734, 579, 679, 576, 624, 468, 872, 497, 861, 529, 854, 555, 858, 580, 852, 610, 858, 639, 866, 620, 897, 595, 921, 557, 932, 518, 924, 489, 902, 478, 874, 517, 873, 555, 876, 591, 871, 628, 869, 593, 886, 555, 897, 515, 889, 419, 620, 665, 617, 545, 621, 549, 720, 552, 768, 551, 802 };
                if (faceInfos[0] > 0)
                {
                    curBitmap = zp.FaceSticker(srcBitmap, new Bitmap(Application.StartupPath + "\\mask_a.png"), new int[] { faceInfos[5], faceInfos[6], faceInfos[7], faceInfos[8], (faceInfos[11] + faceInfos[13]) / 2, (faceInfos[12] + faceInfos[14]) / 2 }, new int[] { 307, 364, 423, 364, 365, 490 }, 100);
                    pictureBox1.Image = (Image)curBitmap;
                }
                else
                    MessageBox.Show("No face!");
                
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (srcBitmap != null)
                pictureBox1.Image = srcBitmap;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (curBitmap != null)
                pictureBox1.Image = curBitmap;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                if (!fdInitialize)
                    MessageBox.Show("FD_INITIALIZE FAILED!");
                int[] faceInfos = new int[15];
                zp.FDProcess(srcBitmap, ref faceInfos);
                int[] modelFacePoints = new int[] { 281, 601, 284, 665, 294, 726, 310, 788, 332, 846, 364, 898, 403, 941, 449, 980, 499, 1012, 560, 1022, 616, 1007, 662, 971, 701, 929, 736, 883, 762, 830, 781, 771, 793, 709, 799, 648, 798, 587, 315, 559, 340, 527, 377, 515, 419, 517, 460, 526, 489, 554, 452, 553, 416, 546, 381, 543, 348, 548, 596, 553, 624, 525, 664, 515, 706, 512, 742, 524, 767, 555, 734, 545, 702, 540, 667, 543, 632, 551, 375, 623, 387, 609, 404, 601, 424, 599, 446, 603, 464, 615, 477, 634, 459, 637, 441, 641, 422, 643, 402, 639, 388, 632, 618, 635, 629, 616, 647, 603, 669, 599, 689, 600, 706, 608, 719, 620, 706, 631, 691, 639, 672, 643, 653, 642, 635, 639, 512, 626, 514, 681, 507, 737, 481, 765, 495, 798, 537, 803, 566, 802, 607, 794, 617, 760, 590, 734, 579, 679, 576, 624, 468, 872, 497, 861, 529, 854, 555, 858, 580, 852, 610, 858, 639, 866, 620, 897, 595, 921, 557, 932, 518, 924, 489, 902, 478, 874, 517, 873, 555, 876, 591, 871, 628, 869, 593, 886, 555, 897, 515, 889, 419, 620, 665, 617, 545, 621, 549, 720, 552, 768, 551, 802 };
                if (faceInfos[0] > 0)
                {
                    curBitmap = zp.FaceSticker(srcBitmap, new Bitmap(Application.StartupPath + "\\mask_b.png"), new int[] { faceInfos[5], faceInfos[6], faceInfos[7], faceInfos[8], (faceInfos[11] + faceInfos[13]) / 2, (faceInfos[12] + faceInfos[14]) / 2 }, new int[] { 307, 364, 423, 364, 365, 490 }, 100);
                    pictureBox1.Image = (Image)curBitmap;
                }
                else
                    MessageBox.Show("No face!");

            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://blog.csdn.net/Trent1985");
        }
    }
}
