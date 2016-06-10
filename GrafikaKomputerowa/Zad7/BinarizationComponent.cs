using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GrafikaKomputerowa.Zad6;

namespace GrafikaKomputerowa.Zad7
{
    class BinarizationComponent
    {
        Point p;
        Form1 mainForm;
        private double GOAL_MEAN = 0;
        private double GOAL_VARIANCE = 1;
        public BinarizationComponent(Form1 form)
        {
            mainForm = form;
        }
        private int ValX(int a)
        {
            if (a < 0)
            {
                return 0;
            }
            if (a >= p.X)
            {
                return p.X - 1;
            }
            else return a;
        }
        private int ValY(int a)
        {
            if (a < 0)
            {
                return 0;
            }
            if (a >= p.Y)
            {
                return p.Y - 1;
            }
            else return a;
        }
        private ulong[] CreateHistogram(Bitmap inputBitmap)
        {
            ulong[] histogram = new ulong[256];
            CustomBitmapProcessing data = new CustomBitmapProcessing(inputBitmap);
            data.LockBits();
            for (int i = 0; i < data.Height; i++)
            {
                for (int j = 0; j < data.Width; j++)
                {
                    int gray = (data.GetPixel(i, j) & 0xFF);
                    histogram[gray]++;
                }
            }
            data.UnlockBits();
            return histogram;
        }
        private ulong[] CreateCumulativeHistogram(ulong[] histogram)
        {
            for (int i = 1; i < 256; i++)
            {
                histogram[i] += histogram[i - 1];
            }
            return histogram;
        }
        public void PercentOfBlackThreshold(Bitmap inputBitmap, int percentage)
        {
            inputBitmap = ApplyGrayScale(new Bitmap(inputBitmap));
            ulong[] histogram = CreateHistogram(inputBitmap);
            histogram = CreateCumulativeHistogram((ulong[])histogram.Clone());
            ulong numberOfBlackPixels = (ulong)inputBitmap.Width * (ulong)inputBitmap.Height * (ulong)percentage / 100;
            int threshold = 0;
            for (int i = 0; i < 256; i++)
            {
                if (numberOfBlackPixels <= histogram[i])
                {
                    threshold = i;
                    break;
                }
            }
            ApplyThreshold(inputBitmap, threshold);           
        }
        public void ManualThreshold(Bitmap inputBitmap, int threshold)
        {
            inputBitmap = ApplyGrayScale(new Bitmap(inputBitmap));
            ApplyThreshold(inputBitmap, threshold);
        }
        private Bitmap ApplyThreshold(Bitmap inputBitmap, int threshold)
        {
            CustomBitmapProcessing data = new CustomBitmapProcessing(inputBitmap);
            data.LockBits();
            for (int i = 0; i < data.Height; i++)
            {
                for (int j = 0; j < data.Width; j++)
                {                 
                        if ((data.GetPixel(i, j) & 0xFF) >= threshold)
                        {
                            int rgb = 255 + (255 << 8) + (255 << 16);
                            data.SetPixel(i, j, rgb);
                        }
                        else
                        {
                            data.SetPixel(i, j, 0);
                        }                  
                }
            }
            data.UnlockBits();
            mainForm.Picture = new Bitmap(inputBitmap);
            return new Bitmap(inputBitmap);
        }     
        private Bitmap ApplyGrayScale(Bitmap inputBitmap)
        {
            CustomBitmapProcessing data = new CustomBitmapProcessing(inputBitmap);
            data.LockBits();
            for (int i = 0; i < data.Height; i++)
            {
                for (int j = 0; j < data.Width; j++)
                {
                    int rgb = data.GetPixel(i, j);
                    byte grey = (byte)(Math.Ceiling((decimal)(0.299 * (rgb & 0xFF) + 0.587 * ((rgb >> 8) & 0xFF) + 0.114 * ((rgb >> 16) & 0xFF))));
                    int rgbOut = grey + (grey << 8) + (grey << 16);
                    data.SetPixel(i, j, rgbOut);
                }
            }
            data.UnlockBits();
            return inputBitmap;
        }
        public Bitmap TransformOtsu(Bitmap inputBitmap)
        {
            Bitmap renderedImage = ApplyGrayScale(new Bitmap(inputBitmap));
            ulong[] histogram = CreateHistogram(renderedImage);
            double[] variancies = new double[256];
            for (uint group = 0; group < 256; group++)
            {
                double objectDepth = 0, backgrDepth = 0;
                for (uint i = 0; i < group; i++)
                {
                    objectDepth += histogram[i];
                }
                for (uint i = group; i < 256; i++)
                {
                    backgrDepth += histogram[i];
                }
                double objectMiddleDepth = 0, backgrMiddleDepth = 0;
                for (uint i = 0; i < group; i++)
                {
                    objectMiddleDepth += (histogram[i] * i) / objectDepth;
                }
                for (uint i = group; i < 256; i++)
                {
                    backgrMiddleDepth += (histogram[i] * i) / backgrDepth;
                }

                variancies[group] = Math.Sqrt(objectDepth * backgrDepth * Math.Pow(objectMiddleDepth - backgrMiddleDepth, 2));
            }
            int boundary = Array.IndexOf(variancies, variancies.Max());
            return ApplyThreshold(renderedImage, boundary);
        }
        public void NiblackBinarization(Bitmap inputBitmap, int blockSize, double niblackConstant)
        {
            inputBitmap = ApplyGrayScale(new Bitmap(inputBitmap));
            CustomBitmapProcessing data = new CustomBitmapProcessing(inputBitmap);
            CustomBitmapProcessing data_help = new CustomBitmapProcessing(new Bitmap(inputBitmap));
            data.LockBits();
            data_help.LockBits();
            if (blockSize < 2) blockSize = 2;
            int itemCounter = blockSize * blockSize; // pomocnik do odchylenia standardowego i do średniej
            blockSize /= 2;
            int height = inputBitmap.Height;
            int width = inputBitmap.Width;
            p = new Point(height, width);
            for (int i = 0; i < height; i = i + blockSize)
            {
                for (int j = 0; j < width; j = j + blockSize)
                {
                    int maskHeightValidator = i + blockSize;// Prevents from taking pixels out of boundary 
                    int maskWidthValidator = j + blockSize;// Prevents from taking pixels out of boundary 
                    double meanVal = 0; // średnia
                    double stdVal = 0; // odchylenie standardowe                    
                    for (int x = i-blockSize; x < ValX(maskHeightValidator); x++)
                    {
                        for (int y = j- blockSize; y < ValY(maskWidthValidator); y++)
                        {
                            meanVal += (data_help.GetPixel(ValX(x), ValY(y)) & 0xFF);
                        }
                    }
                    meanVal /= itemCounter;

                    for (int x = i-blockSize; x < ValX(maskHeightValidator); x++)
                    {
                        for (int l = j-blockSize; l < ValY(maskWidthValidator); l++)
                        {
                            stdVal += Math.Pow((data_help.GetPixel(ValX(x), ValY(l)) & 0xFF) - meanVal, 2);
                        }
                    }
                    stdVal = Math.Sqrt(stdVal / itemCounter);
                    double threshold = (meanVal + niblackConstant * stdVal);
                    for (int k = i - blockSize; k < ValX(maskHeightValidator); k++)
                    {
                        for (int l = j - blockSize; l < ValY(maskWidthValidator); l++)
                        {
                            if ((data_help.GetPixel(ValX(k), ValY(l)) & 0xFF) < threshold)
                                data.SetPixel(ValX(k), ValY(l), 0);
                            else
                                data.SetPixel(ValX(k), ValY(l), (255 + (255 << 8) + (255 << 16)));
                        }
                    }
                }
            }
            data.UnlockBits();
            mainForm.Picture = inputBitmap;
        }
        public void HistogramNormalization(Bitmap inputBitmap)
        {
            inputBitmap = ApplyGrayScale(new Bitmap(inputBitmap));
            int width = inputBitmap.Width;
            int height = inputBitmap.Height;
            CustomBitmapProcessing data = new CustomBitmapProcessing(inputBitmap);
            data.LockBits();
            int[,] imageMatrix = new int[height, width];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    imageMatrix[i, j] = (data.GetPixel(i, j) & 0xFF);
                }
            }
            double mean = 0;
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    mean += imageMatrix[i, j];

                }
            }
            mean /= (width * height);
            double var = 0;
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    var += Math.Pow((imageMatrix[i, j] - mean), 2);

                }
            }
            var /= (height * width * 255); //255 for white color
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {

                    double normalizedPixel = 0;
                    double squareError = 0;

                    if (imageMatrix[i, j] > mean)
                    {
                        squareError = Math.Pow((imageMatrix[i, j] - mean), 2);
                        normalizedPixel = (GOAL_MEAN + Math.Sqrt(((GOAL_VARIANCE * squareError / var))));
                    }
                    else
                    {
                        squareError = (imageMatrix[i, j] - mean) * (imageMatrix[i, j] - mean);
                        normalizedPixel = (GOAL_MEAN - Math.Sqrt(((GOAL_VARIANCE * squareError / var))));
                    }
                    int rgb = (int)-normalizedPixel;
                    int color = rgb + (rgb << 8) + (rgb << 16);
                    data.SetPixel(i, j, color);
                }
            }
            data.UnlockBits();
            mainForm.Invoke((MethodInvoker)delegate {
                mainForm.toolStripProgressBar1.Value = 0;
                mainForm.Picture = new Bitmap(inputBitmap);
            });
        }
        public void MeanIterativeSelection(Bitmap inputBitmap)
        {
            inputBitmap = ApplyGrayScale(new Bitmap(inputBitmap));
            ulong[] histogram = new ulong[256];
            histogram = CreateHistogram(inputBitmap);
            uint oldMiddle = 0;
            uint middle = MeanGrayLevel(histogram, 0, 255);
            uint meanBrightnessBelow;
            uint meanBrightnessUpper;
            while (oldMiddle != middle)
            {
                meanBrightnessBelow = MeanGrayLevel(histogram, 0, middle);
                meanBrightnessUpper = MeanGrayLevel(histogram, middle, 256);
                oldMiddle = middle;
                middle = (meanBrightnessBelow + meanBrightnessUpper) / 2;
            }
            ApplyThreshold(inputBitmap, (int)middle);
        }
        private uint MeanGrayLevel(ulong[] pixels, uint start, uint stop)
        {
            ulong BrightnessSum = 0;
            ulong numberOfPixels = 0;
            for (ulong i = start; i < stop; i++)
            {
                BrightnessSum += pixels[i] * i;
                numberOfPixels += pixels[i];
            }       
            return (uint)(BrightnessSum / numberOfPixels);
        }
        private Bitmap histogramEqualization2(Bitmap inputBitmap)
        {
            CustomBitmapProcessing dataOut = new CustomBitmapProcessing(inputBitmap);
            dataOut.LockBits();
            uint pixels = (uint)dataOut.Height * (uint)dataOut.Width;
            decimal Const = (decimal)255 / (decimal)pixels;
            int R, G, B;
            int[] cdfR = new int[256];
            int[] cdfG = new int[256];
            int[] cdfB = new int[256];
            int progressBarMax = dataOut.Height * 2, progrsbar2nd = dataOut.Height;
            for (int i = 0; i < dataOut.Height; i++) // Tworzenie histogramu
            {
                for (int j = 0; j < dataOut.Width; j++)
                {
                    int rgb = dataOut.GetPixel(i, j);
                    int RValue = (rgb & 0xFF);
                    int GValue = ((rgb >> 8) & 0xFF);
                    int BValue = ((rgb >> 16) & 0xFF);
                    cdfR[RValue]++;
                    cdfG[GValue]++;
                    cdfB[BValue]++;
                }
                mainForm.Invoke((MethodInvoker)delegate
                {
                    mainForm.toolStripProgressBar1.Value = (i * 100) / progressBarMax;
                });
            }
            for (int r = 1; r <= 255; r++) // Tworzenie histogramu skumulowanego
            {
                cdfR[r] += cdfR[r - 1];
                cdfG[r] += cdfG[r - 1];
                cdfB[r] += cdfB[r - 1];
            }

            for (int i = 0; i < dataOut.Height; i++)
            {
                for (int j = 0; j < dataOut.Width; j++)
                //Parallel.For(0, dataOut.Width, j =>
                {
                    int pixelColor = dataOut.GetPixel(i, j);
                    R = (int)((decimal)cdfR[pixelColor & 0xFF] * Const);
                    G = (int)((decimal)cdfG[((pixelColor >> 8) & 0xFF)] * Const);
                    B = (int)((decimal)cdfB[((pixelColor >> 16) & 0xFF)] * Const);
                    pixelColor = R + (G << 8) + (B << 16);
                    dataOut.SetPixel(i, j, pixelColor);
                }
                mainForm.Invoke((MethodInvoker)delegate
                {
                    mainForm.toolStripProgressBar1.Value = ((i + progrsbar2nd) * 100) / progressBarMax;
                });
            }
            dataOut.UnlockBits();
            mainForm.Invoke((MethodInvoker)delegate {
                mainForm.toolStripProgressBar1.Value = 0;
            });
            return new Bitmap(inputBitmap);
        }
        public void EntropySelection(Bitmap inputBitmap)
        {
            inputBitmap = histogramEqualization2(new Bitmap(inputBitmap));
            inputBitmap = ApplyGrayScale(new Bitmap(inputBitmap));
            CustomBitmapProcessing data = new CustomBitmapProcessing(inputBitmap);
            ulong[] histogramTable = CreateHistogram(inputBitmap);
            int imageSize = inputBitmap.Width * inputBitmap.Height;
            int Height = inputBitmap.Height;
            int Width = inputBitmap.Width;
            double[] p = new double[256];
            double[] entropyTable = new double[256];
            data.LockBits();
 
            for (int i = 0; i< 256; i++)
            {
                p[i] = (double) histogramTable[i] / (double) imageSize;
            }
 
            for (int i = 0; i< 256; i++)
            {
                entropyTable[i] = p[i] * Math.Log10(p[i]);
            }
            
            double entropyMin = entropyTable[0];
            int index = 0;           
            for (int i = 0; i < 256; i++)
            {
                if (entropyMin > entropyTable[i])
                {
                    entropyMin = entropyTable[i];
                    index = i;
                }
            }
            for (int i = 0; i< Height; i++)
            {
                for (int j = 0; j< Width; j++)
                {
                    int grey = (data.GetPixel(i, j) & 0xFF);
                    if (grey >= index)
                    {
                        grey = 255;
                    }
                    else
                    {
                        grey = 0;
                    }
                    int rgb = grey + (grey << 8) + (grey << 16);
                    data.SetPixel(i, j, rgb);
                }
            }
            data.UnlockBits();
            mainForm.Picture = new Bitmap(inputBitmap);
        }
        
        public void GreenSearch(Bitmap inputBitmap)
        {           
            CustomBitmapProcessing data = new CustomBitmapProcessing(inputBitmap);            
            data.LockBits();
            for (int i = 0; i < data.Height; i++)
            {
                for (int j = 0; j < data.Width; j++)
                {
                    int pixel = data.GetPixel(i, j);
                    int r = data.GetPixel(i, j) & 0xFF;
                    int g = (data.GetPixel(i, j) >> 8) & 0xFF;
                    int b = (data.GetPixel(i, j) >> 16) & 0xFF;
                    if ((g>b && g>r && g>60) || (((g+r)/2)>b && ((g + r) / 2) > (r-3) && g > 60))
                    {
                        data.SetPixel(i, j, 0);
                    }
                    else
                        data.SetPixel(i, j, 0xFFFFFF);

                }
            }
            data.UnlockBits();
            mainForm.Picture = new Bitmap(inputBitmap);
        }
        public void MinimalError(Bitmap inputBitmap)
        {
            inputBitmap = ApplyGrayScale(new Bitmap(inputBitmap));
            ulong[] histogram = new ulong[256];
            histogram = CreateHistogram(inputBitmap);
            ulong[] histogram2 = new ulong[256];
            histogram2 = CreateHistogram(inputBitmap);         
            int height = inputBitmap.Height;
            int width = inputBitmap.Width;
            double meanVal = 0; // średnia
            double stdVal = 0; // odchylenie standardowe  
            double meanVal2 = 0; // średnia
            double stdVal2 = 0; // odchylenie standardowe  
                   
            for (int i = 0; i < histogram.Length/2; i++)
            {
                meanVal += (double)histogram[i];
            }
            meanVal /= ((histogram.Length / 2));

            for (int i = 0; i < histogram.Length/2; i++)
            {
                stdVal += Math.Pow(histogram[i] - meanVal, 2);
            }

            stdVal = Math.Sqrt(stdVal / (histogram.Length / 2));

            for (int i = histogram.Length / 2; i < histogram.Length; i++)
            {
                meanVal2 += (double)histogram[i];
            }
            meanVal2 /= ((histogram.Length / 2));

            for (int i = histogram.Length/2; i < histogram.Length; i++)
            {
                stdVal2 += Math.Pow(histogram[i] - meanVal2, 2);
            }
            stdVal2 = Math.Sqrt(stdVal2 / (histogram.Length / 2));

            for (int i = histogram.Length / 2; i < histogram.Length; i++)
            {
                double elo = Math.Abs((histogram[i] - meanVal2) / stdVal2);
                if (elo < 0)
                    histogram[i] = 0;
                else
                    histogram[i] = (ulong)elo;
            }
            for (int i = 0; i < histogram.Length/2; i++)
            {
                double elo = Math.Abs((histogram[i] - meanVal) / stdVal);
                if (elo < 0)
                    histogram[i] = 0;
                else
                    histogram[i] = (ulong)elo;                
            }
            int threshold = 0;
            int minimimValue = height*width;
            for (int i = 0; i < 256; i++)
            {
                int blackPixel = numberOfBlackPixelsAfterThreshold(histogram2, i);
                int blackPixelAfterNormal = numberOfBlackPixelsAfterThreshold(histogram, i);
                if (Math.Abs(blackPixel - blackPixelAfterNormal)<minimimValue)
                {
                    minimimValue = Math.Abs(blackPixel - blackPixelAfterNormal);
                    threshold = i;
                }
            }
            ApplyThreshold(inputBitmap, 120);
            //mainForm.Picture = inputBitmap;
        }
        private int numberOfBlackPixelsAfterThreshold(ulong[] histogram, int threshold)
        {
            int output = 0;
            for (int i = threshold; i < histogram.Length; i++)
            {
                output += (int)histogram[i];
            }
            return output;
        }
    }
}
