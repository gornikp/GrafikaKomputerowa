using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GrafikaKomputerowa.Zad5
{
    public partial class CustomMask : Form
    {
        
        private Form1 mainForm;
        public CustomMask(Form1 main)
        {
            InitializeComponent();
            mainForm = main;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(tb00.Text.Length==0 ||
               tb01.Text.Length == 0 ||
               tb02.Text.Length == 0 ||
               tb10.Text.Length == 0 ||
               tb11.Text.Length == 0 ||
               tb12.Text.Length == 0 ||
               tb20.Text.Length == 0 ||
               tb21.Text.Length == 0 ||
               tb22.Text.Length == 0)
            {
                MessageBox.Show("Wszystkiepola muszą być wypełnione");
            }
            else
            {
                int[,] mask = { { int.Parse(tb00.Text), int.Parse(tb01.Text), int.Parse(tb02.Text)},
                                { int.Parse(tb10.Text), int.Parse(tb11.Text), int.Parse(tb12.Text)},
                                { int.Parse(tb20.Text), int.Parse(tb21.Text), int.Parse(tb22.Text)} };
                mainForm.savedBitmap.Push(mainForm.Picture);
                if (mainForm.savedBitmap.Count() >= 0)
                    mainForm.button1.Enabled = true;
                PhotoFilters filter = new PhotoFilters(mainForm);
                filter.ApplyCustomMask(new Bitmap(mainForm.Picture), mask);
                this.Close();
            }
        }
    }
}
