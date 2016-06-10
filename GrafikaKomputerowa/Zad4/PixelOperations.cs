using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GrafikaKomputerowa.Zad4
{
    class PixelOperations
    {
        private int valueValidator (int a)
        {
            if (a > 255)
                a = 255;
            if (a < 0)
                a = 0;
            return a;
        }
        public Color add (Color pixel,int value)
        {
            int r, g, b;
            r = valueValidator(pixel.R + value);
            g = valueValidator(pixel.G + value);
            b = valueValidator(pixel.B + value);
            return Color.FromArgb(r, g, b);
        }
        public Bitmap add(Bitmap bitmap, int value)
        {
            BitmapData bmpData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, bitmap.PixelFormat);
            byte[] pixelValues = new byte[Math.Abs(bmpData.Stride) * bitmap.Height];
            Marshal.Copy(bmpData.Scan0, pixelValues, 0, pixelValues.Length);
            for (int i = 0; i < pixelValues.Length; i++)
            {
                pixelValues[i] = (byte)(valueValidator(pixelValues[i] + value));
            }
            Marshal.Copy(pixelValues, 0, bmpData.Scan0, pixelValues.Length);
            bitmap.UnlockBits(bmpData);
            return bitmap;
        }
        public Color substract(Color pixel, int value)
        {
            int r, g, b;
            r = valueValidator(pixel.R - value);
            g = valueValidator(pixel.G - value);
            b = valueValidator(pixel.B - value);
            return Color.FromArgb(r, g, b);
        }
        public Bitmap substract(Bitmap bitmap, int value)
        {
            //value = -value;
            return add(bitmap, -value);
        }
        public Color multiple(Color pixel, int value)
        {
            int r, g, b;
            r = valueValidator(pixel.R * value);
            g = valueValidator(pixel.G * value);
            b = valueValidator(pixel.B * value);
            return Color.FromArgb(r, g, b);
        }
        public Bitmap multiple(Bitmap bitmap, int value)
        {
            BitmapData bmpData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, bitmap.PixelFormat);
            byte[] pixelValues = new byte[Math.Abs(bmpData.Stride) * bitmap.Height];
            Marshal.Copy(bmpData.Scan0, pixelValues, 0, pixelValues.Length);
            for (int i = 0; i < pixelValues.Length; i++)
            {
                pixelValues[i] = (byte)(valueValidator(pixelValues[i] * value));
            }
            Marshal.Copy(pixelValues, 0, bmpData.Scan0, pixelValues.Length);
            bmpData.PixelFormat = PixelFormat.Format24bppRgb;
            bitmap.UnlockBits(bmpData);
            return bitmap;
        }
        public Color divide(Color pixel, int value)
        {
            int r, g, b;
            r = valueValidator(pixel.R / value);
            g = valueValidator(pixel.G / value);
            b = valueValidator(pixel.B / value);
            return Color.FromArgb(r, g, b);
        }
        public Bitmap divide(Bitmap bitmap, int value)
        {
            BitmapData bmpData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, bitmap.PixelFormat);
            byte[] pixelValues = new byte[Math.Abs(bmpData.Stride) * bitmap.Height];
            Marshal.Copy(bmpData.Scan0, pixelValues, 0, pixelValues.Length);
            for (int i = 0; i < pixelValues.Length; i++)
            {
                pixelValues[i] = (byte)(valueValidator(pixelValues[i] / value));
            }
            Marshal.Copy(pixelValues, 0, bmpData.Scan0, pixelValues.Length);
            bmpData.PixelFormat = PixelFormat.Format24bppRgb;
            bitmap.UnlockBits(bmpData);
            return bitmap;
        }
        public Color bightness(Color pixel, int value)//bightness
        {
            return add(pixel, value);
        }
        public Bitmap bightness(Bitmap bitmap, int value)
        {
            byte[] LUT = new byte[256];
            for (int i = 0; i < 256; i++)
            {
                if ((value + i) > 255)
                {
                    LUT[i] = 255;
                }
                else if ((value + i) < 0)
                {
                    LUT[i] = 0;
                }
                else
                {
                    LUT[i] = (byte)(value + i);
                }
            }
            BitmapData bmpData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, bitmap.PixelFormat);
            byte[] pixelValues = new byte[Math.Abs(bmpData.Stride) * bitmap.Height];
            Marshal.Copy(bmpData.Scan0, pixelValues, 0, pixelValues.Length);
            for (int i = 0; i < pixelValues.Length; i++)
            {
                pixelValues[i] = (byte)(LUT[pixelValues[i]]);
            }
            Marshal.Copy(pixelValues, 0, bmpData.Scan0, pixelValues.Length);
            bmpData.PixelFormat = PixelFormat.Format24bppRgb;
            bitmap.UnlockBits(bmpData);
            return bitmap;
        }
        public Color grayscale1(Color pixel)
        {
            int grey;
            grey = (pixel.R + pixel.G + pixel.B)/3;         
            return Color.FromArgb(grey, grey, grey);
        }
        public Bitmap grayscale1(Bitmap bitmap)
        {

                BitmapData bmpData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, bitmap.PixelFormat);        
            byte[] pixelValues = new byte[Math.Abs(bmpData.Stride) * bitmap.Height];
            Marshal.Copy(bmpData.Scan0, pixelValues, 0, pixelValues.Length);
            for (int i = 0; i < pixelValues.Length - 3; i += 3)
            {
                byte grey = (byte)((pixelValues[i] + pixelValues[i + 1] + pixelValues[i + 2]) / 3);
                pixelValues[i] = grey;
                pixelValues[i + 1] = grey;
                pixelValues[i + 2] = grey;
            }
            Marshal.Copy(pixelValues, 0, bmpData.Scan0, pixelValues.Length);
            bmpData.PixelFormat = PixelFormat.Format24bppRgb;
            bitmap.UnlockBits(bmpData);
            return bitmap;
        }
        public Color grayscale2(Color pixel)//bightness
        {
            float grey;
            grey = (float)(0.299 * pixel.R + 0.587 * pixel.G + 0.114 * pixel.B);
            return Color.FromArgb((int)grey, (int)grey, (int)grey);
        }
        public Bitmap grayscale2(Bitmap bitmap)
        {
            CustomBitmapProcessing data = new CustomBitmapProcessing(bitmap);
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
            return bitmap;
        }
        public Bitmap grayscale3(Bitmap bitmap)
        {
            CustomBitmapProcessing data = new CustomBitmapProcessing(bitmap);
            data.LockBits();
            for (int i = 0; i < data.Height; i++)
            {
                for (int j = 0; j < data.Width; j++)
                {
                    int rgb = data.GetPixel(i, j);
                    if (!((rgb & 0xFF) == 0 && ((rgb >> 8) & 0xFF) == 0 && ((rgb >> 16) & 0xFF) == 0))
                     {
                     byte grey = (byte)(Math.Ceiling((decimal)(0.45 * (rgb & 0xFF) + 0.1 * ((rgb >> 8) & 0xFF) + 0.45 * ((rgb >> 16) & 0xFF))));
                        int rgbOut = grey + (grey << 8) + (grey << 16);
                        data.SetPixel(i, j, rgbOut);
                     }
                }
            }
            data.UnlockBits();
            return bitmap;
        }
    }
}
