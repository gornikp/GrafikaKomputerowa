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
    public partial class Niblack : Form
    {
        Form1 mainForm;
        Bitmap picture;
        BinarizationComponent binary;
        public Niblack(Form1 form)
        {
            InitializeComponent();
            mainForm = form;
            picture = (Bitmap)mainForm.Picture.Clone();
            binary = new BinarizationComponent(mainForm);
        }
        private const int CP_NOCLOSE_BUTTON = 0x200;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
                return myCp;
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(trackBar1, trackBar1.Value.ToString());           
            binary.NiblackBinarization(new Bitmap(picture), trackBar1.Value, (((double)trackBar2.Value - 30) / 10));
            label3.Text = (((double)trackBar2.Value - 30) / 16).ToString();
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            toolTip2.SetToolTip(trackBar2, ((trackBar2.Value - 25) / 20).ToString());
            binary.NiblackBinarization(new Bitmap(picture), trackBar1.Value, (((double)trackBar2.Value - 30) / 16));
            label3.Text = (((double)trackBar2.Value - 30) / 16).ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mainForm.savedBitmap.Push(new Bitmap(picture));
            if (mainForm.savedBitmap.Count() >= 0)
                mainForm.button1.Enabled = true;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            mainForm.Picture = new Bitmap(picture);
            this.Close();
        }
    }
}
