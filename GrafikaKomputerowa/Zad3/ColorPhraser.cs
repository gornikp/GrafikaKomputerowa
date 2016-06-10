using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafikaKomputerowa.Zad3
{
    public class Cmyk
    {
        public Cmyk(float c, float m, float y, float k)
        {
            C = c;
            M = m;
            Y = y;
            K = k;
        }

        public Cmyk()
        {
        }

        public float C { get; set; }
        public float M { get; set; }
        public float Y { get; set; }
        public float K { get; set; }
    }
    class ColorPhraser
    {
        public Color SwitchCmykToRgb(Cmyk color)
        {
            Color convertedColor = new Color();
            int r, g, b;
            //r = (int)((((float)1 - color.C) * ((float)1 - color.K)) * 255);
            //g = (int)((((float)1 - color.M) * (float)(1 - color.K)) * 255);
            //b = (int)((((float)1 - color.Y) * ((float)1 - color.K)) * 255);
            r = (int)((1 - Math.Min(1, color.C * (1 - color.K) + color.K)) * 255);
            b = (int)((1 - Math.Min(1, color.Y * (1 - color.K) + color.K)) * 255);
            g = (int)((1 - Math.Min(1, color.M * (1 - color.K) + color.K)) * 255);
            convertedColor = Color.FromArgb(r, g, b);
            return convertedColor;
        }
        public Cmyk SwitchRgbToCmyk(Color color)
        {
            //Black = minimum(1 - Red, 1 - Green, 1 - Blue)
            //Cyan = (1 - Red - Black) / (1 - Black)
            //Magenta = (1 - Green - Black) / (1 - Black)
            //Yellow = (1 - Blue - Black) / (1 - Black)

            float r,g,b,c, m, y, k;
            r = (float)color.R / 255;
            g = (float)color.G / 255;
            b = (float)color.B / 255;
            k = (float)Math.Round((decimal)(Math.Min(1 - r,Math.Min(1-g,1-b))),3,MidpointRounding.AwayFromZero);
            if (1 - k != 0)
            {
                c = (float)Math.Round((decimal)((1 - (r + k)) / (1 - k)), 3, MidpointRounding.AwayFromZero);
                m = (float)Math.Round((decimal)((1 - (g + k)) / (1 - k)), 3, MidpointRounding.AwayFromZero);
                y = (float)Math.Round((decimal)((1 - (b + k)) / (1 - k)), 3, MidpointRounding.AwayFromZero);
            }
            else
            {
                c = 0;
                m = 0;
                y = 0;
            }
            if (c < 0)
                c = 0;
            if (m < 0)
                m = 0;
            if (y < 0)
                y = 0;

            Cmyk convertedColor = new Cmyk(c,m,y,k);

            return convertedColor;
        }
    }
}
