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
    public partial class Value : Form
    {
        public int value { get; set; }
        private Form1 mainForm;
        public Value()
        {
            InitializeComponent();
        }
        public Value(Form1 mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            value = int.Parse(int32TextBox1.Text);
            if (value%2 == 0)
            {
                value += 1;
            }
            mainForm.savedBitmap.Push(mainForm.Picture);
            if (mainForm.savedBitmap.Count() >= 0)
                mainForm.button1.Enabled = true;
            PhotoFilters filter = new PhotoFilters(mainForm);
            filter.ApplyAnyMask(new Bitmap(mainForm.Picture), value);
            this.Close();
        }
    }
}
