using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GrafikaKomputerowa.Zad5
{
    public partial class Kuwahara : Form
    {
        Form1 mainForm;
        public int value { get; set; }
        public Kuwahara(Form1 form)
        {
            InitializeComponent();
            mainForm = form;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            value = int.Parse(int32TextBox1.Text);
            //PhotoFilters filter = new PhotoFilters(mainForm);
            //mainForm.picture = filter.KuwaharaBlur(new Bitmap(mainForm.picture), value);
            //mainForm.pictureBox1.Image = mainForm.picture;
            this.Close();
        }

        private void Kuwahara_FormClosed(object sender, FormClosedEventArgs e)
        {
            mainForm.savedBitmap.Push(mainForm.Picture);
            if (mainForm.savedBitmap.Count() >= 0)
                mainForm.button1.Enabled = true;
            PhotoFilters filter = new PhotoFilters(mainForm);
            filter.ApplyKuwaharaBlur(new Bitmap(mainForm.Picture), value);
            
        }
    }
}
