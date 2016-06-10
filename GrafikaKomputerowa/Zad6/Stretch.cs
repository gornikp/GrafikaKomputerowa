using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GrafikaKomputerowa.Zad6
{
    public partial class Stretch : Form
    {
        private Form1 mainForm;
        public Stretch(Form1 form)
        {
            InitializeComponent();
            mainForm = form;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int minValue = int.Parse(int32TextBox1.Text);
            int maxValue = int.Parse(int32TextBox2.Text);
            //int32TextBox1.
            if (maxValue<minValue)
            {
                MessageBox.Show("wartość maksymalna nie moze być mniejsza od minimalnej");
            }
            mainForm.savedBitmap.Push(new Bitmap(mainForm.Picture));
            if (mainForm.savedBitmap.Count() >= 0)
                mainForm.button1.Enabled = true;
            HistogramOperations newHist = new HistogramOperations(mainForm);          
            newHist.StretchHistogram(new Bitmap(mainForm.Picture), int.Parse(int32TextBox1.Text), int.Parse(int32TextBox2.Text));
            this.Close();
        }
    }
}
