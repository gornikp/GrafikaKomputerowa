using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GrafikaKomputerowa.Zad5
{
    class PhotoFilters
    {
        Form1 mainForm;
        public PhotoFilters(Form1 form)
        {
            mainForm = form;
        }
        #region Zmienne / filtry
        public Point size = new Point();
        private static readonly int[,] circleMask = { { 0, 1, 1, 1, 0 },
                                                      { 1, 1, 1, 1, 1 }, 
                                                      { 1, 1, 1, 1, 1 }, 
                                                      { 1, 1, 1, 1, 1 }, 
                                                      { 0, 1, 1, 1, 0 } };

        private static readonly int[,] gaus5Mask = { { 1, 1, 2, 2, 2, 1, 1 },
                                                     { 1, 2, 2, 4, 2, 2, 1 },
                                                     { 2, 2, 4, 8, 4, 2, 2 },
                                                     { 2, 4, 8, 16, 8, 4, 2 },
                                                     { 2, 2, 4, 8, 4, 2, 2 },
                                                     { 1, 2, 2, 4, 2, 2, 1 },
                                                     { 1, 1, 2, 2, 2, 1, 1 }};

        private static readonly int[,] meanMask = { { 1, 1, 1 }, 
                                                    { 1, 1, 1 }, 
                                                    { 1, 1, 1 } };
                                         
        private static readonly int[,] meanRemoval1Mask = { { -1, -1, -1 },
                                                            { -1, 9, -1 },
                                                            { -1, -1, -1 } };

        private static readonly int[,] meanRemoval2Mask = { { 0, -1, 0 },
                                                            { -1, 20, -1 },
                                                            { 0, -1, 0 } };
                                                   
        private static readonly int[,] prewittMaskVertical = { { -1, -1, -1 },
                                                               { 0, 0, 0 },
                                                               { 1, 1, 1 } };
                                                       
        private static readonly int[,] prewittMaskHorizontal = { { 1, 0, -1 },
                                                                 { 1, 0, -1 },
                                                                 { 1, 0, -1 } };

        private static readonly int[,] sobelMaskVertical = { { -1, 0, 1 },
                                                             { -2, 0, 2 },
                                                             { -1, 0, 1 } };

        private static readonly int[,] sobelMaskHorizontal = { { 1, 2, 1 },
                                                               { 0, 0, 0 },
                                                               { -1, -2, -1 } };
        private static readonly int[,] LaplaceMask1 = { { 0, -1, 0 },
                                                        { -1, 4, -1 },
                                                        { 0, -1, 0 } };
                                                     
        private static readonly int[,] LaplaceMask2 = { { -1, -1, -1 },
                                                        { -1, 8, -1 },
                                                        { -1, -1, -1 } };
                                                 
        private static readonly int[,] LaplaceMask3 = { { 1, -2, 1 },
                                                        { -2, 4, -2 },
                                                        { 1, -2, 1 } };
                                                  
        private static readonly int[,] LaplaceSlopingMask = { { -1, 0, -1 },
                                                              { 0, 4, 0 },
                                                              { -1, 0, -1 } };
        #endregion
        #region walidatory
        private bool CheckIfCornerMask(int[,] mask)
        {
            if (mask.Equals(prewittMaskVertical) || mask.Equals(prewittMaskHorizontal) || mask.Equals(sobelMaskVertical) || mask.Equals(sobelMaskHorizontal) ||
                mask.Equals(LaplaceMask1) || mask.Equals(LaplaceMask2) || mask.Equals(LaplaceMask3) || mask.Equals(LaplaceSlopingMask))
            {
                return true;
            }
            else return false;
        }
        private int mod1(int a, int b)
        {
            int c = a + b;
            if (c < 0)
            {
                c += size.X;
            }
            return c % size.X;
        }
        private int mod2(int a, int b)
        {
            int c = a + b;
            if (c < 0)
            {
                c += size.Y;
            }
            return c % size.Y;
        }
        private int RgbVal(int a)
        {
            //a += 128;
            if (a > 255)
                return 255;
            if (a < 0)
                return 0;
            else return a;
        }
        private int Val(int a, int b)
        {
            if (a + b < 0)
            {
                return 0;
            }
            if (a + b >= size.X)
            {
                return size.X - 1;
            }
            else return (a + b);
        }
        private int Val2(int a, int b)
        {
            if (a + b < 0)
            {
                return 0;
            }
            if (a + b >= size.Y)
            {
                return size.Y - 1;
            }
            else return (a + b);
        }
        #endregion
        #region wywołania konkrenych masek
        public void ApplyKuwaharaBlur(Bitmap bitmap, int value)
        {
            Thread t = new Thread(() =>
            {
                KuwaharaBlur(new Bitmap(mainForm.Picture), value);
            });
            t.Start();
        }
        public void ApplyCustomMask(Bitmap inputBitmap, int[,] mask)
        {
            ThreadingApplyMask(inputBitmap, mask);
        }
        public void ApplyMedianMask3(Bitmap bmp)
        {
            ThreadingApplyMask(bmp, 3);
        }
        public void ApplyMedianMask5(Bitmap bmp)
        {
            ThreadingApplyMask(bmp, 5);
        }
        public void ApplyLaplaceSlopingMask(Bitmap bmp)
        {
            ThreadingApplyMask(bmp, LaplaceSlopingMask);
        }
        public void ApplyLaplaceMask3(Bitmap bmp)
        {
            ThreadingApplyMask(bmp, LaplaceMask3);
        }
        public void ApplyLaplaceMask2(Bitmap bmp)
        {
            ThreadingApplyMask(bmp, LaplaceMask2);
        }
        public void ApplyLaplaceMask1(Bitmap bmp)
        {
            ThreadingApplyMask(bmp, LaplaceMask1);
        }
        public void ApplyPrewittHorizontalMask(Bitmap bmp)
        {
            ThreadingApplyMask(bmp, prewittMaskHorizontal);
        }
        public void ApplyPrewittVerticalMask(Bitmap bmp)
        {
            ThreadingApplyMask(bmp, prewittMaskVertical);
        }
        public void ApplyMeanRemovalMask(Bitmap bmp)
        {
            ThreadingApplyMask(bmp, meanRemoval1Mask);
        }
        public void ApplyMeanRemovalMask2(Bitmap bmp)
        {
            ThreadingApplyMask(bmp, meanRemoval2Mask);
        }
        public void ApplyMeanMask(Bitmap bmp)
        {
            ThreadingApplyMask(bmp, meanMask);
        }
        public void ApplySobelVerticalMask(Bitmap bmp)
        {
            ThreadingApplyMask(bmp, sobelMaskVertical);
        }
        public void ApplySobelHorizontalMask(Bitmap bmp)
        {
            ThreadingApplyMask(bmp, sobelMaskHorizontal);
        }
        public void ApplyCircleMask(Bitmap bmp)
        {
            ThreadingApplyMask(bmp, circleMask);
        }
        public void ApplyAnyMask(Bitmap bmp, int size)
        {
            int[,] tempMask = new int[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    tempMask[i, j] = 1;
                }
            }
            ThreadingApplyMask(bmp, tempMask);
        }
        public void ApplyGaus5Mask(Bitmap bmp)
        {
            ThreadingApplyMask(bmp, gaus5Mask);
        }
        #endregion
        #region Threading
        private void ThreadingApplyMask (Bitmap inputBitmap, int[,] mask)
        {
            Thread t = new Thread(() =>
            {
                ParallelInvoke(inputBitmap, mask);
            });
            t.Start();
        }
        private void ThreadingApplyMask(Bitmap inputBitmap, int size)
        {
            Thread t = new Thread(() =>
            {
                ParallelInvoke(inputBitmap, size);
            });
            t.Start();
        }
        private void ParallelInvoke(Bitmap inputBitmap, int[,] mask)
        {
            bool CornerMask = CheckIfCornerMask(mask); // sprawdzamy czy maska jest typu :wykrywanie krawędzi"
            Bitmap temorary = new Bitmap(inputBitmap.Width, inputBitmap.Height);
            CustomBitmapProcessing data = new CustomBitmapProcessing(new Bitmap(inputBitmap));
            CustomBitmapProcessing dataOut = new CustomBitmapProcessing(inputBitmap);
            data.LockBits();
            dataOut.LockBits();
            mainForm.Invoke((MethodInvoker)delegate {
                mainForm.toolStripProgressBar1.Maximum = (data.Height)*2;
            });
            Parallel.Invoke(
                () => ApplyMaskThreaded(data, dataOut, mask, 0, 0, data.Height / 2, data.Width / 2, CornerMask),
                () => ApplyMaskThreaded(data, dataOut, mask, 0, data.Width / 2, data.Height / 2, data.Width, CornerMask),
                () => ApplyMaskThreaded(data, dataOut, mask, data.Height / 2, 0, data.Height, data.Width / 2, CornerMask),
                () => ApplyMaskThreaded(data, dataOut, mask, data.Height / 2, data.Width / 2, data.Height, data.Width, CornerMask));
            data.UnlockBits();
            dataOut.UnlockBits();
            mainForm.Invoke((MethodInvoker)delegate
            {
                mainForm.toolStripProgressBar1.Maximum = 100;
                mainForm.toolStripProgressBar1.Value = 0;
                mainForm.Picture = inputBitmap;
            });
        }
        private void ParallelInvoke(Bitmap inputBitmap, int size)
        {
            CustomBitmapProcessing data = new CustomBitmapProcessing(new Bitmap(inputBitmap));
            CustomBitmapProcessing dataOut = new CustomBitmapProcessing(inputBitmap);
            data.LockBits();
            dataOut.LockBits();
            mainForm.Invoke((MethodInvoker)delegate {
                mainForm.toolStripProgressBar1.Maximum = (data.Height) * 2;
            });
            Parallel.Invoke(
                () => ApplyMedianMaskThreaded(data, dataOut, 0, 0, data.Height / 2, data.Width / 2, size),
                () => ApplyMedianMaskThreaded(data, dataOut, 0, data.Width / 2, data.Height / 2, data.Width, size),
                () => ApplyMedianMaskThreaded(data, dataOut, data.Height / 2, 0, data.Height, data.Width / 2, size),
                () => ApplyMedianMaskThreaded(data, dataOut, data.Height / 2, data.Width / 2, data.Height, data.Width, size));
            data.UnlockBits();
            dataOut.UnlockBits();
            mainForm.Invoke((MethodInvoker)delegate
            {
                mainForm.toolStripProgressBar1.Maximum = 100;
                mainForm.toolStripProgressBar1.Value = 0;
                mainForm.Picture = inputBitmap;
            });
        }
        #endregion
        private void KuwaharaBlur(Bitmap inputBitmap, int Size)
        {       
            CustomBitmapProcessing tempData = new CustomBitmapProcessing(new Bitmap(inputBitmap));
            CustomBitmapProcessing newData = new CustomBitmapProcessing(inputBitmap);
            tempData.LockBits();
            newData.LockBits();
            int[] ApetureMinX = { -(Size / 2), 0, -(Size / 2), 0 };
            int[] ApetureMaxX = { 0, (Size / 2), 0, (Size / 2) };
            int[] ApetureMinY = { -(Size / 2), -(Size / 2), 0, 0 };
            int[] ApetureMaxY = { 0, 0, (Size / 2), (Size / 2) };
            for (int y = 0; y < newData.Height; ++y)
            {
                for (int x = 0; x < newData.Width; ++x)
                {
                    int[] RValues = { 0, 0, 0, 0 };
                    int[] GValues = { 0, 0, 0, 0 };
                    int[] BValues = { 0, 0, 0, 0 };
                    int[] NumPixels = { 0, 0, 0, 0 };
                    int[] MaxRValue = { 0, 0, 0, 0 };
                    int[] MaxGValue = { 0, 0, 0, 0 };
                    int[] MaxBValue = { 0, 0, 0, 0 };
                    int[] MinRValue = { 255, 255, 255, 255 };
                    int[] MinGValue = { 255, 255, 255, 255 };
                    int[] MinBValue = { 255, 255, 255, 255 };
                    for (int i = 0; i < 4; ++i)
                    {
                        for (int x2 = ApetureMinX[i]; x2 < ApetureMaxX[i]; ++x2)
                        {
                            int TempX = x + x2;
                            if (TempX >= 0 && TempX < inputBitmap.Width)
                            {
                                for (int y2 = ApetureMinY[i]; y2 < ApetureMaxY[i]; ++y2)
                                {
                                    int TempY = y + y2;
                                    if (TempY >= 0 && TempY < inputBitmap.Height)
                                    {
                                        int TempColor = tempData.GetPixel(TempY, TempX);
                                        RValues[i] += TempColor & 0xFF;
                                        GValues[i] += (TempColor >> 8) & 0xFF;
                                        BValues[i] += (TempColor >> 16) & 0xFF;
                                        int[] tempColor = { TempColor & 0xFF, (TempColor >> 8) & 0xFF, (TempColor >> 16) & 0xFF };
                                        if (tempColor[0] > MaxRValue[i])
                                        {
                                            MaxRValue[i] = tempColor[0];
                                        }
                                        else if (tempColor[0] < MinRValue[i])
                                        {
                                            MinRValue[i] = tempColor[0];
                                        }

                                        if (tempColor[1] > MaxGValue[i])
                                        {
                                            MaxGValue[i] = tempColor[1];
                                        }
                                        else if (tempColor[1] < MinGValue[i])
                                        {
                                            MinGValue[i] = tempColor[1];
                                        }

                                        if (tempColor[2] > MaxBValue[i])
                                        {
                                            MaxBValue[i] = tempColor[2];
                                        }
                                        else if (tempColor[2] < MinBValue[i])
                                        {
                                            MinBValue[i] = tempColor[2];
                                        }
                                        ++NumPixels[i];
                                    }
                                }
                            }
                        }
                    }
                    int j = 0;
                    int MinDifference = 10000;
                    for (int i = 0; i < 4; ++i)
                    {
                        int CurrentDifference = (MaxRValue[i] - MinRValue[i]) + (MaxGValue[i] - MinGValue[i]) + (MaxBValue[i] - MinBValue[i]);
                        if (CurrentDifference < MinDifference && NumPixels[i] > 0)
                        {
                            j = i;
                            MinDifference = CurrentDifference;
                        }
                    }
                    int rgbOut = (RValues[j] / NumPixels[j]) + ((GValues[j] / NumPixels[j]) << 8) + ((BValues[j] / NumPixels[j]) << 16);
                    newData.SetPixel(y, x, rgbOut);
                }
                mainForm.Invoke((MethodInvoker)delegate {
                    mainForm.toolStripProgressBar1.Value = (y * 100 / newData.Height);
                });
                
            }
            tempData.UnlockBits();
            newData.UnlockBits();
            mainForm.Invoke((MethodInvoker)delegate {
                mainForm.toolStripProgressBar1.Value = 0;
                //mainForm.pictureBox1.Image = inputBitmap;
                mainForm.Picture = new Bitmap(inputBitmap);
            });
        }
        private void ApplyMaskThreaded(CustomBitmapProcessing data, CustomBitmapProcessing dataOut, int[,] mask, int startI, int startJ, int endI, int endJ, bool CornerMask)
        {
            size.X = data.Height;
            size.Y = data.Width;
            int MaskPositionHelper = (mask.GetLength(0) - 1) / 2;
            int maskSize = mask.GetLength(0);
            int sumMask = 0;
            foreach (var item in mask)
            {
                sumMask += item;
            }
            if (sumMask == 0)
                sumMask = 1;
            for (int i = startI; i < endI; i++)
            {
                //Parallel.For(startJ, endJ, j =>
                for (int j = startJ; j < endJ; j++)
                {
                    int r = 0, g = 0, b = 0;
                    if (i < 0 || j < 0)
                    {
                        throw new IndexOutOfRangeException();
                    }
                    for (int ii = 0; ii < maskSize; ii++)
                    {
                        for (int jj = 0; jj < maskSize; jj++)
                        {
                            int rgb = data.GetPixel(mod1(i, ii - MaskPositionHelper), mod2(j, jj - MaskPositionHelper));
                            r += (rgb & 0x0000FF) * mask[ii, jj];
                            g += ((rgb >> 8) & 0x0000FF) * mask[ii, jj];
                            b += ((rgb >> 16) & 0x0000FF) * mask[ii, jj];
                           // MaskPositionHelper2--;
                        }
                        //MaskPositionHelper1--;
                    }
                    int rgbOut = 0;
                    if (CornerMask)
                    {
                        rgbOut = RgbVal((r / sumMask) +128) + (RgbVal((g / sumMask) + 128) << 8) + (RgbVal((b / sumMask) + 128) << 16);
                    }
                    else
                    {
                        rgbOut = RgbVal(r / sumMask) + (RgbVal(g / sumMask) << 8) + (RgbVal(b / sumMask) << 16);
                    }
                    dataOut.SetPixel(i, j, rgbOut);
                }//);
                mainForm.Invoke((MethodInvoker)delegate {
                    mainForm.toolStripProgressBar1.Value++;
                });
            }
        }
        private void ApplyMedianMaskThreaded(CustomBitmapProcessing data, CustomBitmapProcessing dataOut, int startI, int startJ, int endI, int endJ, int maskSize)
        {           
            int MaskPositionHelper = (maskSize - 1) / 2;
            size.X = data.Height;
            size.Y = data.Width;
            for (int i = startI; i < endI; i++)
            {
                //Parallel.For(startJ, endJ, j =>
                for (int j = startJ; j < endJ; j++)
                {
                    List<int> r = new List<int>();
                    List<int> g = new List<int>();
                    List<int> b = new List<int>();
                    for (int ii = 0; ii < maskSize; ii++)
                    {
                        for (int jj = 0; jj < maskSize; jj++)
                        {
                            int rgb = data.GetPixel(Val(i, ii - MaskPositionHelper), Val2(j, jj - MaskPositionHelper));
                            r.Add(rgb & 0xFF);
                            g.Add((rgb >> 8) & 0xFF);
                            b.Add((rgb >> 16) & 0xFF);
                        }
                    }
                    r.Sort();
                    g.Sort();
                    b.Sort();
                    int rgbOut = RgbVal(r[r.Count / 2]) + (RgbVal(g[r.Count / 2]) << 8) + (RgbVal(b[r.Count / 2]) << 16);
                    dataOut.SetPixel(i, j, rgbOut);
                }//);
                mainForm.Invoke((MethodInvoker)delegate {
                    mainForm.toolStripProgressBar1.Value++;
                });
            }         
        }
        

        //private void ApplyMedianMask(Bitmap inputBitmap, int maskSize)
        //{
        //    CustomBitmapProcessing data = new CustomBitmapProcessing(new Bitmap(inputBitmap));
        //    CustomBitmapProcessing dataOut = new CustomBitmapProcessing(inputBitmap);
        //    data.LockBits();
        //    dataOut.LockBits();
        //    int MaskPositionHelper = (maskSize - 1) / 2;
        //    size.X = data.Height;
        //    size.Y = data.Width;
        //    for (int i = 0; i < data.Height; i++)
        //    {
        //        for (int j = 0; j < data.Width; j++)             
        //        {
        //            List<int> r = new List<int>();
        //            List<int> g = new List<int>();
        //            List<int> b = new List<int>();
        //            int MaskPositionHelper1 = MaskPositionHelper;
        //            for (int ii = 0; ii < maskSize; ii++)
        //            {
        //                int MaskPositionHelper2 = MaskPositionHelper;
        //                for (int jj = 0; jj < maskSize; jj++)
        //                {
        //                    int rgb = data.GetPixel(Val(i, ii - MaskPositionHelper1), Val2(j, jj - MaskPositionHelper2));
        //                    r.Add(rgb & 0xFF);
        //                    g.Add((rgb >> 8) & 0xFF);
        //                    b.Add((rgb >> 16) & 0xFF);
        //                    MaskPositionHelper2--;
        //                }
        //                MaskPositionHelper1--;
        //            }
        //            r.Sort();
        //            g.Sort();
        //            b.Sort();
        //            int rgbOut = RgbVal(r[r.Count / 2]) + (RgbVal(g[r.Count / 2]) << 8) + (RgbVal(b[r.Count / 2]) << 16);
        //            dataOut.SetPixel(i, j, rgbOut);
        //        }
        //        mainForm.Invoke((MethodInvoker)delegate {
        //            mainForm.toolStripProgressBar1.Value = (i * 100 / data.Height);
        //        });
        //    }
        //    data.UnlockBits();
        //    dataOut.UnlockBits();
        //    mainForm.toolStripProgressBar1.Value = 0;
        //    mainForm.Invoke((MethodInvoker)delegate {
        //        mainForm.toolStripProgressBar1.Value = 0;
        //        mainForm.pictureBox1.Image = inputBitmap;
        //        mainForm.Picture = inputBitmap;
        //    });
        //}
        //private Bitmap ApplyMask(Bitmap inputBitmap, int[,] mask)
        //{
        //    bool CornerMask = CheckIfCornerMask(mask); // sprawdzamy czy maska jest typu :wykrywanie krawędzi"
        //    CustomBitmapProcessing data = new CustomBitmapProcessing(new Bitmap(inputBitmap));
        //    CustomBitmapProcessing dataOut = new CustomBitmapProcessing(inputBitmap);
        //    data.LockBits();
        //    dataOut.LockBits();
        //    size.X = data.Height;
        //    size.Y = data.Width;
        //    int MaskPositionHelper = (mask.GetLength(0) - 1) / 2;
        //    int maskSize = mask.GetLength(0);
        //    int sumMask = 0;
        //    foreach (var item in mask)
        //    {
        //        sumMask += item;
        //    }
        //    if (sumMask == 0)
        //        sumMask = 1;
        //    for (int i = 0; i < data.Height; i++)
        //    {
        //        //for (int j = 0; j < data.Width; j++)
        //        Parallel.For(0, data.Width, j =>
        //        {
        //            int r = 0, g = 0, b = 0;
        //            int MaskPositionHelper1 = MaskPositionHelper;
        //            for (int ii = 0; ii < maskSize; ii++)
        //            {
        //                int MaskPositionHelper2 = MaskPositionHelper;
        //                for (int jj = 0; jj < maskSize; jj++)
        //                {
        //                    int rgb = data.GetPixel(Val(i, ii - MaskPositionHelper1), Val2(j, jj - MaskPositionHelper2));
        //                    r += (rgb & 0x0000FF) * mask[ii, jj];
        //                    g += ((rgb >> 8) & 0x0000FF) * mask[ii, jj];
        //                    b += ((rgb >> 16) & 0x0000FF) * mask[ii, jj];
        //                    MaskPositionHelper2--;
        //                }
        //                MaskPositionHelper1--;
        //            }
        //            int rgbOut = 0;
        //            if (CornerMask)
        //            {
        //                rgbOut = RgbVal((r / sumMask) + 128) + (RgbVal((g / sumMask) + 128) << 8) + (RgbVal((b / sumMask) + 128) << 16);
        //            }
        //            else
        //            {
        //                rgbOut = RgbVal(r / sumMask) + (RgbVal(g / sumMask) << 8) + (RgbVal(b / sumMask) << 16);
        //            }
        //            dataOut.SetPixel(i, j, rgbOut);
        //        });
        //        mainForm.Invoke((MethodInvoker)delegate {
        //            mainForm.toolStripProgressBar1.Value = (i * 100 / data.Height);
        //        });
        //    }
        //    data.UnlockBits();
        //    dataOut.UnlockBits();
        //    mainForm.toolStripProgressBar1.Value = 0;
        //    mainForm.Invoke((MethodInvoker)delegate {
        //        mainForm.toolStripProgressBar1.Value = 0;
        //        //mainForm.pictureBox1.Image = inputBitmap;
        //        mainForm.Picture = inputBitmap;
        //    });
        //    return inputBitmap;
        //}
    }
}
