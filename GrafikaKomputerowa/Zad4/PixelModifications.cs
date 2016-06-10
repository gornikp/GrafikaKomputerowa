using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GrafikaKomputerowa.Zad4
{
    public partial class PixelModifications : Form
    {
        Form1 mainForm;
        public PixelModifications()
        {
            InitializeComponent();
        }
        public PixelModifications(Form1 form)
        {
            mainForm = form;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PixelOperations pixelmod = new PixelOperations();
            Color tempPoint;
            if (!radioButton6.Checked && !radioButton7.Checked)
            {
                if (textBoxNotEmpty())
                {
                    mainForm.savedBitmap.Push(new Bitmap(mainForm.Picture));
                    if (mainForm.savedBitmap.Count() >= 0)
                    {
                        mainForm.button1.Enabled = true;
                    }
                    if (radioButton1.Checked)
                    {
                        if (checkBox1.Checked)
                        {
                            mainForm.Picture = pixelmod.add(mainForm.Picture, int.Parse(textBox1.Text));
                            
                        }
                        else
                        {
                            Color tempColor = mainForm.Picture.GetPixel(mainForm.tempPoint.X, mainForm.tempPoint.Y);
                            tempPoint = pixelmod.add(tempColor, int.Parse(textBox1.Text));
                            mainForm.Picture.SetPixel(mainForm.tempPoint.X, mainForm.tempPoint.Y, tempPoint);
                            
                        }
                    }
                    if (radioButton2.Checked)
                    {
                        if (checkBox1.Checked)
                        {
                            mainForm.Picture = pixelmod.substract(mainForm.Picture, int.Parse(textBox1.Text));
                            
                        }
                        else
                        {
                            Color tempColor = mainForm.Picture.GetPixel(mainForm.tempPoint.X, mainForm.tempPoint.Y);
                            tempPoint = pixelmod.substract(tempColor, int.Parse(textBox1.Text));
                            mainForm.Picture.SetPixel(mainForm.tempPoint.X, mainForm.tempPoint.Y, tempPoint);
                            
                        }
                    }
                    if (radioButton3.Checked)
                    {
                        if (checkBox1.Checked)
                        {
                            mainForm.Picture = pixelmod.multiple(mainForm.Picture, int.Parse(textBox1.Text));
                            
                        }
                        else
                        {
                            Color tempColor = mainForm.Picture.GetPixel(mainForm.tempPoint.X, mainForm.tempPoint.Y);
                            tempPoint = pixelmod.multiple(tempColor, int.Parse(textBox1.Text));
                            mainForm.Picture.SetPixel(mainForm.tempPoint.X, mainForm.tempPoint.Y, tempPoint);
                            
                        }
                    }
                    if (radioButton4.Checked)
                    {
                        if (checkBox1.Checked)
                        {
                            mainForm.Picture = pixelmod.divide(mainForm.Picture, int.Parse(textBox1.Text));
                            
                        }
                        else
                        {
                            Color tempColor = mainForm.Picture.GetPixel(mainForm.tempPoint.X, mainForm.tempPoint.Y);
                            tempPoint = pixelmod.divide(tempColor, int.Parse(textBox1.Text));
                            mainForm.Picture.SetPixel(mainForm.tempPoint.X, mainForm.tempPoint.Y, tempPoint);
                            
                        }
                    }
                    if (radioButton5.Checked)
                    {
                        if (checkBox1.Checked)
                        {
                            mainForm.Picture = pixelmod.bightness(mainForm.Picture, int.Parse(textBox1.Text));
                            
                        }
                        else
                        {
                            Color tempColor = mainForm.Picture.GetPixel(mainForm.tempPoint.X, mainForm.tempPoint.Y);
                            tempPoint = pixelmod.bightness(tempColor, int.Parse(textBox1.Text));
                            mainForm.Picture.SetPixel(mainForm.tempPoint.X, mainForm.tempPoint.Y, tempPoint);
                            
                        }
                    }
                }
            }
            else
            {
                mainForm.savedBitmap.Push((Bitmap)mainForm.Picture.Clone());
                if (mainForm.savedBitmap.Count() >= 0)
                {
                    mainForm.button1.Enabled = true;
                }
                if (radioButton6.Checked)
                {
                    if (checkBox1.Checked)
                    {
                        mainForm.Picture = (Bitmap)pixelmod.grayscale1(mainForm.Picture).Clone();
                        
                    }
                    else
                    {
                        Color tempColor = mainForm.Picture.GetPixel(mainForm.tempPoint.X, mainForm.tempPoint.Y);
                        tempPoint = pixelmod.grayscale1(tempColor);
                        mainForm.Picture.SetPixel(mainForm.tempPoint.X, mainForm.tempPoint.Y, tempPoint);
                        //
                    }
                }
                if (radioButton7.Checked)
                {
                    if (checkBox1.Checked)
                    {
                        mainForm.Picture = (Bitmap)pixelmod.grayscale2(mainForm.Picture).Clone();
                        //
                    }
                    else
                    {
                        Color tempColor = mainForm.Picture.GetPixel(mainForm.tempPoint.X, mainForm.tempPoint.Y);
                        tempPoint = pixelmod.grayscale2(tempColor);
                        mainForm.Picture.SetPixel(mainForm.tempPoint.X, mainForm.tempPoint.Y, tempPoint);
                        //
                    }
                }
            }
            this.Close();
        }    
        private bool textBoxNotEmpty()
        {
            if (textBox1.Text.Length == 0)
            {
                MessageBox.Show("Brak danych wejsciowych");
                return false;
            }
            else return true;
    }
    }
}
