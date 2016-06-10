using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GrafikaKomputerowa.Zad6
{
    public class HistogramOperations
    {
        Form1 mainForm;
        public HistogramOperations(Form1 form)
        {
            mainForm = form;
        }
        public void StretchHistogram(Bitmap inputBitmap, int minValue, int maxValue)
        {
            Thread t = new Thread(() =>
            {
                stretch(inputBitmap, minValue, maxValue);
            });
            t.Start();
        }
        public void HistogramEqualization(Bitmap inputBitmap)
        {
            Thread t = new Thread(() =>
            {
                histogramEqualization(inputBitmap);
            });
            t.Start();
        }
        private void stretch(Bitmap inputBitmap, int minValue, int maxValue)
        {
            CustomBitmapProcessing dataOut = new CustomBitmapProcessing(inputBitmap);
            dataOut.LockBits();
            byte[] LUT = new byte[256];
            byte vMaxR = (byte)maxValue;
            byte vMinR = (byte)minValue;
            byte iMax = 255;
            for (int i = 0; i < 256; i++)
            {
                if (i > maxValue)
                {
                    LUT[i] = 255;
                }
                else if (i < minValue)
                {
                    LUT[i] = 0;
                }
                else
                {
                    LUT[i] = (byte)(iMax / (maxValue - minValue) * (i - minValue));
                }
            }
            Parallel.Invoke(
                () =>
                {
                    Parallel.For(0, dataOut.Pixels.LongLength / 4, i =>
                      {
                          dataOut.Pixels[i] = LUT[dataOut.Pixels[i]]; // w operacji LUT nie obodzi nas które wartości zmieniamy - więc tak jest szybciej
                    });
                },
                () =>
                {
                    Parallel.For(dataOut.Pixels.LongLength / 4, dataOut.Pixels.LongLength / 2, i =>
                    {
                        dataOut.Pixels[i] = LUT[dataOut.Pixels[i]]; // w operacji LUT nie obodzi nas które wartości zmieniamy - więc tak jest szybciej
                    });

                },
                () =>
                {
                    Parallel.For(dataOut.Pixels.LongLength / 2, ((dataOut.Pixels.LongLength / 4) * 3), i =>
                        {
                            dataOut.Pixels[i] = LUT[dataOut.Pixels[i]]; // w operacji LUT nie obodzi nas które wartości zmieniamy - więc tak jest szybciej
                    });
                },
                () =>
                {
                    Parallel.For(((dataOut.Pixels.LongLength / 4) * 3), dataOut.Pixels.LongLength, i =>
                    {
                        dataOut.Pixels[i] = LUT[dataOut.Pixels[i]]; // w operacji LUT nie obodzi nas które wartości zmieniamy - więc tak jest szybciej
                    });
                });
            dataOut.UnlockBits();
            mainForm.Invoke((MethodInvoker)delegate {
                //mainForm.pictureBox1.Image = inputBitmap;
                mainForm.Picture = new Bitmap(inputBitmap);
            });
        }
        private void histogramEqualization(Bitmap inputBitmap)
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
                mainForm.Picture = new Bitmap(inputBitmap);
                //mainForm.pictureBox1.Image = inputBitmap;
            });

        }       
    }
}
