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
                color = 0xFFFFFF;
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
                CustomBitmapProcessing data2 = new CustomBitmapProcessing(new Bitmap(tempo));
                CustomBitmapProcessing data3 = new CustomBitmapProcessing(new Bitmap(tempo));
                CustomBitmapProcessing data4 = new CustomBitmapProcessing(new Bitmap(tempo));
                data1.LockBits();              
                data2.LockBits();              
                data3.LockBits();                
                data4.LockBits();

                for (int h = 1; h < dataReadOnly.Height - 1; ++h)
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
                            data2.SetPixel(h, w, 0);
                        }
                        else { data2.SetPixel(h, w, 0xFFFFFF); }

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
                            data3.SetPixel(h, w, 0);
                        }
                        else { data3.SetPixel(h, w, 0xFFFFFF); }
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
                            data4.SetPixel(h, w, 0);
                        }
                        else { data4.SetPixel(h, w, 0xFFFFFF); }

                        int pix1 = (dataReadOnly.GetPixel(h, w));
                        if ((data1.GetPixel(h, w) & 0xFF) == 0
                            || (data2.GetPixel(h, w) & 0xFF) == 0
                            || (data3.GetPixel(h, w) & 0xFF) == 0
                            || (data4.GetPixel(h, w) & 0xFF) == 0)
                        {
                            pix1 = 0;
                        }
                        dataOut.SetPixel(h, w, pix1);
                    }
                }                            
                data1.UnlockBits();
                data2.UnlockBits();
                data3.UnlockBits();
                data4.UnlockBits();
            }
            dataReadOnly.UnlockBits();
            dataOut.UnlockBits();
            mainForm.Picture = new Bitmap(inputBitmap);
        }
        public void imageThinning(Bitmap inputBitmap, int iterations)
        {
            CustomBitmapProcessing data = new CustomBitmapProcessing(inputBitmap);
            Bitmap tempo = new Bitmap(inputBitmap);           
            data.LockBits();
            p.X = data.Height;
            p.Y = data.Width;
            for (int i = 0; i < iterations; ++i)
            {
                Bitmap imageCopy = new Bitmap(tempo);
                CustomBitmapProcessing data2 = new CustomBitmapProcessing(imageCopy);
                data2.LockBits();
                for (int h = 1; h < data.Height - 1; ++h)
                {
                    for (int w = 1; w < data.Width - 1; ++w)
                    {                        

                        if (data.GetPixel(mod1(h - 1), mod2(w - 1)) == 0 
                            && data.GetPixel(mod1(h - 1), mod2(w)) == 0 
                            && data.GetPixel(mod1(h - 1), mod2(w + 1)) == 0 
                            && data.GetPixel(mod1(h), mod2(w - 1)) == 0 
                            && data.GetPixel(mod1(h), mod2(w)) == 0 
                            && data.GetPixel(mod1(h), mod2(w + 1)) == 0 
                            && data.GetPixel(mod1(h + 1), mod2(w - 1)) == 0 
                            && data.GetPixel(mod1(h + 1), mod2(w)) == 0 
                            && data.GetPixel(mod1(h + 1), mod2(w + 1)) == 0)
                        {
                            data2.SetPixel(h, w, 0xFFFFFF);
                        }   
                    }
                }

                for (int h = 0; h < data.Height; ++h)
                {
                    for (int w = 0; w < data.Width; ++w)
                    {
                        //int pixel1 = data.GetPixel(w, h);
                        int pixel2 = data2.GetPixel(h, w);

                        if (pixel2 == 0xFFFFFF)
                        {
                            data.SetPixel(h, w, 0xFFFFFF);
                        }
                    }
                }
                data2.UnlockBits();
            }            
            data.UnlockBits();
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
