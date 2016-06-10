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
    public partial class PercentageSelection : Form
    {
        Form1 mainForm;
        Bitmap picture;
        public PercentageSelection(Form1 mainform)
        {
            InitializeComponent();
            mainForm = mainform;
            picture = (Bitmap)mainForm.Picture.Clone();
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
            BinarizationComponent binary = new BinarizationComponent(mainForm);
            binary.PercentOfBlackThreshold(new Bitmap(picture), trackBar1.Value * 2);
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
