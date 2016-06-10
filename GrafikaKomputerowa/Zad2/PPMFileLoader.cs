using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GrafikaKomputerowa.Zad2
{
    public class PPMFileLoader
    {
        private Bitmap image;

        public Bitmap loadBytes(String path)
        {           
            int format = readFormat(path);
            if (format == 0)
            {
                MessageBox.Show("File don't have correct format (P3 or P6)", "Wrong file format!");
            }
            else if (format == 2)
            {
                image = ReadBitmapFromPPM3(path);
            }
            else if (format == 1)
            {
                image = ReadBitmapFromPPM(path);
            }
            return image;
        }


        private int readFormat(string path)
        {
            FileStream plik = new FileStream(path, FileMode.Open);
            var reader = new BinaryReader(plik);
            byte[] file = new byte[2];
            plik.Read(file, 0, 2);
            var a = file[0];
            var b = file[1];
            if (a == 'P' && b == '6')
            {
                reader.Close();
                plik.Close();
                return 1;
            }
            if (a == 'P' && b == '3')
            {
                reader.Close();
                plik.Close();
                return 2;
            }
            else
            {
                reader.Close();
                plik.Close();
                return 0;
            }
        }

        private Bitmap ReadBitmapFromPPM3(string path)
        {
            FileStream plik = new FileStream(path, FileMode.Open);
            long length = plik.Length;
            byte[] buffer = new byte[length];
            plik.Read(buffer, 0, (int)length);
            Bitmap tmpImg = null; //= new Bitmap(10,10,Bitmap.TYPE_INT_RGB);
            List<int> numbersList = new List<int>();
            int counter = 0;
            int colorCounter = 4;

            for (int i = 0; i < buffer.Length; i++)
            {
                i = skipComments(buffer, i);
                if (buffer[i] >= '0' && buffer[i] <= '9')
                {
                    int j = 0;
                    counter = 0;
                    int help = 0;
                    int num = 0;
                    for (j = i; j < buffer.Length && buffer[j] >= '0' && buffer[j] <= '9'; j++)
                    {
                        counter++;
                    }
                    help = counter;
                    for (j = i; j < buffer.Length && buffer[j] >= '0' && buffer[j] <= '9' && counter > 0; j++)
                    {
                        if (counter == 1)
                        {
                            num = num + (buffer[j] - 48);
                        }
                        else
                        {
                            num = num + (int)Math.Pow(10, counter - 1) * (buffer[j] - 48);
                        }
                        counter--;
                    }

                    numbersList.Add(num);
                    i += help;
                }
            }

            if ((numbersList.Count - 4) / 3 != numbersList.ElementAt(1) * numbersList.ElementAt(2))
            {
                MessageBox.Show("File have " + (numbersList.Count - 4) / 3 + " pixels loaded but expect " + numbersList.ElementAt(1) * numbersList.ElementAt(2) + "!", "Crash in file!");
                return null;
            }
            else
            {
                tmpImg = new Bitmap(numbersList.ElementAt(1), numbersList.ElementAt(2),PixelFormat.Format24bppRgb);

                for (int i = 0; i < tmpImg.Height; i++)
                {
                    for (int j = 0; j < tmpImg.Width; j++)
                    {
                        try
                        {
                            Color color = Color.FromArgb((numbersList.ElementAt(colorCounter) * 255) / numbersList.ElementAt(3), (numbersList.ElementAt(colorCounter + 1) * 255) / numbersList.ElementAt(3), (numbersList.ElementAt(colorCounter + 2) * 255) / numbersList.ElementAt(3));
                            tmpImg.SetPixel(j, i, color);
                            colorCounter += 3;
                        }
                        catch (ArgumentException e)
                        {
                            MessageBox.Show("Crash in file! " + e.Message);
                            return null;
                        }
                    }
                }               
                return tmpImg;
            }
        }

        private Bitmap ReadBitmapFromPPM(string file)
        {
            FileStream plik = new FileStream(file, FileMode.Open);
            BinaryReader reader = new BinaryReader(plik);
            //reader = skipCommentsAndSpace(reader);
            if (reader.ReadChar() != 'P' || reader.ReadChar() != '6')
                return null;
            reader = skipCommentsAndSpace(reader);
            //reader.ReadChar(); //Eat newline
            string widths = "", heights = "";
            char temp;
            while ((temp = reader.ReadChar()) != ' ')
                widths += temp;
            while ((temp = reader.ReadChar()) >= '0' && temp <= '9')
                heights += temp;
            if (reader.ReadChar() != '2' || reader.ReadChar() != '5' || reader.ReadChar() != '5')
                return null;
            //reader = skipCommentsAndSpace(reader);
            reader.ReadChar(); //Eat the last newline
            int width = int.Parse(widths),
                height = int.Parse(heights);
            Bitmap bitmap = new Bitmap(width, height,PixelFormat.Format24bppRgb);
            //Read in the pixels
            try {
                for (int y = 0; y < height; y++)
                    for (int x = 0; x < width; x++)
                    {
                        bitmap.SetPixel(x, y, Color.FromArgb(
                            reader.ReadByte(),
                            reader.ReadByte(),
                            reader.ReadByte()
                        ));
                    }
            }
            catch(System.IO.EndOfStreamException e)
            {
                plik.Close();
                reader.Close();
                MessageBox.Show("Plik niepoprawny: " + e.Message);
            }
            plik.Close();
            reader.Close();
            return bitmap;
        }
    private BinaryReader skipCommentsAndSpace(BinaryReader reader)
        {
            if (reader.PeekChar() == ' ')
            {
                reader.ReadChar();
            }
            if (reader.PeekChar() == '\n')
            {
                reader.ReadChar();
            }

            if (reader.PeekChar() == '#')
            {
                while (reader.PeekChar() != '\n')
                {
                    reader.ReadChar();
                }
                reader.ReadChar();
            }
            return reader;
        }
        private int skipComments(byte[] buffer, int i)
        {
            if (buffer[i] == '#')
            {
                while (buffer[i] != '\n' && i < buffer.Length)
                {
                    i++;
                }
            }
            return i;
        }
    }
}
