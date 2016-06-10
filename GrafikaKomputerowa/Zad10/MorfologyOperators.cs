using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafikaKomputerowa.Zad10
{
    class MorfologyOperators
    {
        private enum Morfology { Dylation, Erosin }
        Form1 mainForm;
        Point p = new Point();
        public MorfologyOperators(Form1 mainForm)
        {
            this.mainForm = mainForm;
        }
        private int[,] mask = { { 0, 1, 0 }, { 1, 0, 1 }, { 0, 1, 0 } };
        public void Dylation(Bitmap inputBitmap)
        {
            MorfologyFilter(inputBitmap, Morfology.Dylation);
        }
        public void Erosin(Bitmap inputBitmap)
        {
            MorfologyFilter(inputBitmap, Morfology.Erosin);
        }
        public void OpeningFilter(Bitmap inputBitmap)
        {
            MorfologyFilter(new Bitmap(MorfologyFilter(inputBitmap, Morfology.Erosin)),Morfology.Dylation);
        }
        public void ClosingFilter(Bitmap inputBitmap)
        {
            MorfologyFilter(new Bitmap (MorfologyFilter(inputBitmap, Morfology.Dylation)), Morfology.Erosin);
        }
        private Bitmap MorfologyFilter(Bitmap inputBitmap, Morfology type)
        {
            CustomBitmapProcessing dataOut = new CustomBitmapProcessing(inputBitmap);
            CustomBitmapProcessing dataRead = new CustomBitmapProcessing(new Bitmap(inputBitmap));
            dataOut.LockBits();
            dataRead.LockBits();
            p.X = dataOut.Height;
            p.Y = dataOut.Width;
            int color;
            if (type == Morfology.Dylation)
            {
                color = 0;
            }
            else
            {
                color = 0xFFFFFF;
            }

            for (int i = 0; i < p.X; i++)
            {
                for (int j = 0; j < p.Y; j++)
                {
                    if (dataRead.GetPixel(mod1(i - 1), j) == color || dataRead.GetPixel(i , j) == color || dataRead.GetPixel(i, mod2(j-1)) == color || dataRead.GetPixel(i, mod2(j + 1)) == color || dataRead.GetPixel(mod1(i + 1), j) == color)
                    {
                        dataOut.SetPixel(i, j, color);
                    }
                } 
            }
            dataOut.UnlockBits();
            dataRead.UnlockBits();
            mainForm.Picture = new Bitmap(inputBitmap);
            return new Bitmap(inputBitmap);
        }
        public void imageThickening(Bitmap inputBitmap, int iterations)
        {
            CustomBitmapProcessing dataReadOnly = new CustomBitmapProcessing(new Bitmap(inputBitmap)); // data read
            CustomBitmapProcessing dataOut = new CustomBitmapProcessing(inputBitmap);
            Bitmap tempo = new Bitmap(inputBitmap);
            dataReadOnly.LockBits();
            dataOut.LockBits();
            p.X = dataReadOnly.Height;
            p.Y = dataReadOnly.Width;
            for (int i = 0; i < iterations; ++i)
            {
                CustomBitmapProcessing data1 = new CustomBitmapProcessing(new Bitmap(tempo));
                data1.LockBits();              

                for (int h = 0; h < dataReadOnly.Height ; ++h)
                {
                    for (int w = 1; w < dataReadOnly.Width - 1; ++w)
                    {
                        //Pierwsza maska                 
                        if ((dataReadOnly.GetPixel(mod1(h - 1), mod2(w - 1)) == 0 
                            && dataReadOnly.GetPixel(mod1(h - 1), w) == 0 
                            && dataReadOnly.GetPixel(mod1(h - 1), mod2(w + 1)) == 0 
                            && dataReadOnly.GetPixel(h, mod2(w - 1)) == 0 
                            && dataReadOnly.GetPixel(h, w) == 0xFFFFFF 
                            && dataReadOnly.GetPixel(mod1(h + 1), mod2(w + 1)) == 0xFFFFFF)
                            
                            ||(dataReadOnly.GetPixel(mod1(h), mod2(w - 1)) == 0 
                            && dataReadOnly.GetPixel(mod1(h + 1), mod2(w - 1)) == 0 
                            && dataReadOnly.GetPixel(mod1(h + 1), mod2(w)) == 0 
                            && dataReadOnly.GetPixel(mod1(h + 1), mod2(w + 1)) == 0 
                            && dataReadOnly.GetPixel(h, w) == 0xFFFFFF 
                            && dataReadOnly.GetPixel(mod1(h - 1), mod2(w + 1)) == 0xFFFFFF) )
                        {
                            data1.SetPixel(h, w, 0);
                        }
                        else { data1.SetPixel(h, w, 0xFFFFFF); }

                        //Druga maska
                        if ((dataReadOnly.GetPixel(mod1(h - 1), mod2(w - 1)) == 0
                            && dataReadOnly.GetPixel(mod1(h), mod2(w - 1)) == 0
                            && dataReadOnly.GetPixel(mod1(h + 1), mod2(w - 1)) == 0
                            && dataReadOnly.GetPixel(mod1(h + 1), mod2(w)) == 0
                            && dataReadOnly.GetPixel(h, w) == 0xFFFFFF
                            && dataReadOnly.GetPixel(mod1(h - 1), mod2(w + 1)) == 0xFFFFFF)

                            || (dataReadOnly.GetPixel(mod1(h + 1), mod2(w)) == 0
                            && dataReadOnly.GetPixel(mod1(h + 1), mod2(w + 1)) == 0
                            && dataReadOnly.GetPixel(mod1(h), mod2(w + 1)) == 0
                            && dataReadOnly.GetPixel(mod1(h - 1), mod2(w + 1)) == 0
                            && dataReadOnly.GetPixel(h, w) == 0xFFFFFF
                            && dataReadOnly.GetPixel(mod1(h - 1), mod2(w - 1)) == 0xFFFFFF))
                        {
                                data1.SetPixel(h, w, 0);
                        }
                        else if (data1.GetPixel(h, w) != 0)
                            data1.SetPixel(h, w, 0xFFFFFF);

                        //Trzecia maska
                        if ((dataReadOnly.GetPixel(mod1(h + 1), mod2(w - 1)) == 0
                            && dataReadOnly.GetPixel(mod1(h + 1), mod2(w)) == 0
                            && dataReadOnly.GetPixel(mod1(h + 1), mod2(w + 1)) == 0
                            && dataReadOnly.GetPixel(mod1(h), mod2(w + 1)) == 0
                            && dataReadOnly.GetPixel(h, w) == 0xFFFFFF
                            && dataReadOnly.GetPixel(mod1(h - 1), mod2(w - 1)) == 0xFFFFFF)

                            || (dataReadOnly.GetPixel(mod1(h - 1), mod2(w - 1)) == 0
                            && dataReadOnly.GetPixel(mod1(h - 1), mod2(w)) == 0
                            && dataReadOnly.GetPixel(mod1(h - 1), mod2(w + 1)) == 0
                            && dataReadOnly.GetPixel(mod1(h), mod2(w + 1)) == 0
                            && dataReadOnly.GetPixel(h, w) == 0xFFFFFF
                            && dataReadOnly.GetPixel(mod1(h + 1), mod2(w - 1)) == 0xFFFFFF))
                        {
                                data1.SetPixel(h, w, 0);
                        }
                        else if (data1.GetPixel(h, w) != 0)
                            data1.SetPixel(h, w, 0xFFFFFF);
                        //Czwarta maska
                        if ((dataReadOnly.GetPixel(mod1(h - 1), mod2(w)) == 0
                            && dataReadOnly.GetPixel(mod1(h - 1), mod2(w + 1)) == 0
                            && dataReadOnly.GetPixel(mod1(h), mod2(w + 1)) == 0
                            && dataReadOnly.GetPixel(mod1(h + 1), mod2(w + 1)) == 0
                            && dataReadOnly.GetPixel(h, w) == 0xFFFFFF
                            && dataReadOnly.GetPixel(mod1(h + 1), mod2(w - 1)) == 0xFFFFFF)

                            || (dataReadOnly.GetPixel(mod1(h - 1), mod2(w - 1)) == 0
                            && dataReadOnly.GetPixel(mod1(h), mod2(w - 1)) == 0
                            && dataReadOnly.GetPixel(mod1(h + 1), mod2(w - 1)) == 0
                            && dataReadOnly.GetPixel(mod1(h - 1), mod2(w)) == 0
                            && dataReadOnly.GetPixel(h, w) == 0xFFFFFF
                            && dataReadOnly.GetPixel(mod1(h + 1), mod2(w + 1)) == 0xFFFFFF))
                        {
                                data1.SetPixel(h, w, 0);
                        }
                        else if (data1.GetPixel(h,w) != 0)
                            data1.SetPixel(h, w, 0xFFFFFF);

                        int pix1 = (dataReadOnly.GetPixel(h, w));
                        if ((data1.GetPixel(h, w) & 0xFF) == 0)
                            pix1 = 0;
                        dataOut.SetPixel(h, w, pix1);
                    }
                }                            
                data1.UnlockBits();
            }
            dataReadOnly.UnlockBits();
            dataOut.UnlockBits();
            mainForm.Picture = new Bitmap(inputBitmap);
        }
        public void imageThinning(Bitmap inputBitmap, int iterations)
        {
            CustomBitmapProcessing dataRead = new CustomBitmapProcessing(new Bitmap(inputBitmap));
            CustomBitmapProcessing dataOut = new CustomBitmapProcessing(inputBitmap);
            dataRead.LockBits();
            dataOut.LockBits();
            p.X = dataRead.Height;
            p.Y = dataRead.Width;
            for (int h = 0; h < dataRead.Height - 0; ++h)
            {
                for (int w = 0; w < dataRead.Width - 0; ++w)
                {

                    if ((dataRead.GetPixel(mod1(h - 1), mod2(w - 1)) & 0xFF) == 0
                        && (dataRead.GetPixel(mod1(h - 1), mod2(w)) & 0xFF) == 0
                        && (dataRead.GetPixel(mod1(h - 1), mod2(w + 1)) & 0xFF) == 0
                        && (dataRead.GetPixel(mod1(h), mod2(w - 1)) & 0xFF) == 0
                        && (dataRead.GetPixel(mod1(h), mod2(w)) & 0xFF) == 0
                        && (dataRead.GetPixel(mod1(h), mod2(w + 1)) & 0xFF) == 0
                        && (dataRead.GetPixel(mod1(h + 1), mod2(w - 1)) & 0xFF) == 0
                        && (dataRead.GetPixel(mod1(h + 1), mod2(w)) & 0xFF) == 0
                        && (dataRead.GetPixel(mod1(h + 1), mod2(w + 1)) & 0xFF) == 0)
                    {
                        dataOut.SetPixel(h, w, 0xFFFFFF);
                    }
                }
            }
            dataOut.UnlockBits();
            dataRead.UnlockBits();
            mainForm.Picture = new Bitmap(inputBitmap);
        }
        // Walidatory masek
        private int mod1(int a)
        {
            if (a < 0)
            {
                a += p.X;
            }
            return a % p.X;
        }
        private int mod2(int a)
        {
            if (a < 0)
            {
                a += p.Y;
            }
            return a % p.Y;
        }
    }
}
