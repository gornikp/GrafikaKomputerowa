using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace GrafikaKomputerowa
{
    public class ThinningLibrary
    {
        public ThinningLibrary(Form1 mainForm)
        {
            this.mainForm = mainForm;   
        }
        //Kamil surmacz - minucje
        private Form1 mainForm;
        private int[,] imageM;
        private int width;
        private int height;
        private Bitmap originalImage;
        private Bitmap filteredImage;
        private int BinaryValidator(int a)
        {
            if (a == 255) return 0;
            if (a == 1) return 0;
            if (a == 0) return 1;
            else return 1;
        }
        private int BinaryValidator2(int a)
        {
            if (a != 0) return 0;
            else return 255;
        }             
        public void ZhangSuenAlgorithm(Bitmap image)
        {
            Zad7.BinarizationComponent binarize = new Zad7.BinarizationComponent(mainForm);
            originalImage = new Bitmap (binarize.TransformOtsu(new Bitmap(image)));
            width = originalImage.Width;
            height = originalImage.Height;
            filteredImage = new Bitmap(image);
            CustomBitmapProcessing data = new CustomBitmapProcessing(originalImage);
            CustomBitmapProcessing dataOut = new CustomBitmapProcessing(filteredImage);
            data.LockBits();
            dataOut.LockBits();

            imageM = new int[height, width];

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {

                    int col = (data.GetPixel(i, j) & 0xFF);
                    imageM[i, j] = BinaryValidator(col);
                }
            }
            while (true)
            {

                int[,] start = new int[height, width];

                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        start[i, j] = imageM[i, j];
                    }
                }
                thiningIteration(0);
                thiningIteration(1);
                bool same = true;
                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        if (start[i, j] != imageM[i, j])
                        {
                            same = false;
                            goto MainforLoop;
                        }

                    }
                }
                MainforLoop:
                if (same)
                {
                    break;
                }
            }
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    int col;
                    col = BinaryValidator2(imageM[i, j]);
                    int rgb = col + (col << 8) + (col << 16);
                    dataOut.SetPixel(i, j, rgb);
                }
            }
            data.UnlockBits();
            dataOut.UnlockBits();
            mainForm.Picture = (Bitmap)filteredImage.Clone();
            mainForm.pictureBox1.Image = (Bitmap)filteredImage.Clone();
        }
        public void thiningIteration(int iter)
        {
            int[,] marker = new int[height, width];

            for (int i = 1; i < height - 1; i++)
            {
                for (int j = 1; j < width - 1; j++)
                {

                    int p2 = imageM[i - 1, j], p3 = imageM[i - 1, j + 1], p4 = imageM[i, j + 1], p5 = imageM[i + 1, j + 1], p6 = imageM[i + 1, j],
                        p7 = imageM[i + 1, j - 1], p8 = imageM[i, j - 1], p9 = imageM[i - 1, j - 1];
                    int c1 = 0; //p2 == 0 && p3 == 1
                    int c2 = 0; //p3 == 0 && p4 == 1
                    int c3 = 0; //p4 == 0 && p5 == 1
                    int c4 = 0; //p5 == 0 && p6 == 1
                    int c5 = 0; //p6 == 0 && p7 == 1
                    int c6 = 0; //p7 == 0 && p8 == 1
                    int c7 = 0; //p8 == 0 && p9 == 1
                    int c8 = 0; //p9 == 0 && p2 == 1

                    if (p2 == 0 && p3 == 1)
                    {
                        c1 = 1;
                    }
                    if (p3 == 0 && p4 == 1)
                    {
                        c2 = 1;
                    }
                    if (p4 == 0 && p5 == 1)
                    {
                        c3 = 1;
                    }
                    if (p5 == 0 && p6 == 1)
                    {
                        c4 = 1;
                    }
                    if (p6 == 0 && p7 == 1)
                    {
                        c5 = 1;
                    }
                    if (p7 == 0 && p8 == 1)
                    {
                        c6 = 1;
                    }
                    if (p8 == 0 && p9 == 1)
                    {
                        c7 = 1;
                    }
                    if (p9 == 0 && p2 == 1)
                    {
                        c8 = 1;
                    }

                    int A = c1 + c2 + c3 + c4 + c5 + c6 + c7 + c8;
                    int B = p2 + p3 + p4 + p5 + p6 + p7 + p8 + p9;

                    int m1 = iter == 0 ? (p2 * p4 * p6) : (p2 * p4 * p8);
                    int m2 = iter == 0 ? (p4 * p6 * p8) : (p2 * p6 * p8);

                    if (A == 1 && (B >= 2 && B <= 6) && m1 == 0 && m2 == 0)
                    {
                        marker[i, j] = 1;
                    }
                }
            }
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {

                    int tmp = 1 - marker[i, j];
                    if (imageM[i, j] == tmp && imageM[i, j] == 1)
                    {
                        imageM[i, j] = 1;
                    }
                    else
                    {
                        imageM[i, j] = 0;
                    }

                }
            }

        }

        public void Dilatation(Bitmap inputImage)
        {

        }
        public void Erode(Bitmap inputImage)
        {

        }
        public void Closing(Bitmap inputImage)
        {

        }
        public void Opening(Bitmap inputImage)
        {

        }
    }
}
