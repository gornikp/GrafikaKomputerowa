using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GrafikaKomputerowa.Zad7
{
    public partial class ManualThreshold : Form
    {
        Form1 mainForm;
        Bitmap picture;
        public ManualThreshold(Form1 mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            picture = (Bitmap)mainForm.Picture.Clone();
        }       

        private void button1_Click(object sender, EventArgs e)
        {
            int threshold = int.Parse(int32TextBox1.Text);
            if (threshold > 255 || threshold < 0)
            {
                MessageBox.Show("Zła wartość progu. Prawidłowa jest pomedzy 0 a 255");
            }
            else
            {
                mainForm.savedBitmap.Push((Bitmap)mainForm.Picture.Clone());
                if (mainForm.savedBitmap.Count() >= 0)
                    mainForm.button1.Enabled = true;
                BinarizationComponent binary = new BinarizationComponent(mainForm);
                binary.ManualThreshold(new Bitmap (picture), threshold);
                this.Close();
            }
        }
    }
}
