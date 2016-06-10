using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GrafikaKomputerowa.Zad3
{
    public partial class Colors : Form
    {
        public Colors()
        {
            InitializeComponent();
        }

        private void textBoxR_KeyPress(object sender, KeyPressEventArgs e)
        {        
            if (!(Char.IsDigit(e.KeyChar) || (e.KeyChar == (char)Keys.Back)))
            {
                e.Handled = true;
            }  
        }

        private void textBoxG_TextChanged(object sender, EventArgs e)
        {
            if (textBoxG.Text.Length > 0)
            {
                if (int.Parse(textBoxG.Text) > 255)
                    textBoxG.Text = "255";
                if (int.Parse(textBoxG.Text) < 0)
                    textBoxG.Text = "0";
                Color color = Color.FromArgb(int.Parse(textBoxR.Text), int.Parse(textBoxG.Text), int.Parse(textBoxB.Text));
                ColorPhraser switcher = new ColorPhraser();
                Cmyk cmykColor = switcher.SwitchRgbToCmyk(color);
                textBoxC.Text = cmykColor.C.ToString();
                textBoxM.Text = cmykColor.M.ToString();
                textBoxY.Text = cmykColor.Y.ToString();
                textBoxK.Text = cmykColor.K.ToString();
                draw(color);
            }
        }

        private void textBoxB_TextChanged(object sender, EventArgs e)
        {
            if (textBoxB.Text.Length > 0)
            {
                if (int.Parse(textBoxB.Text) > 255)
                    textBoxB.Text = "255";
                if (int.Parse(textBoxB.Text) < 0)
                    textBoxB.Text = "0";
                Color color = Color.FromArgb(int.Parse(textBoxR.Text), int.Parse(textBoxG.Text), int.Parse(textBoxB.Text));
                ColorPhraser switcher = new ColorPhraser();
                Cmyk cmykColor = switcher.SwitchRgbToCmyk(color);
                textBoxC.Text = cmykColor.C.ToString();
                textBoxM.Text = cmykColor.M.ToString();
                textBoxY.Text = cmykColor.Y.ToString();
                textBoxK.Text = cmykColor.K.ToString();
                draw(color);
            }
        }

        private void textBoxC_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
       (e.KeyChar != ','))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == ',') && ((sender as TextBox).Text.IndexOf(',') > -1))
            {
                e.Handled = true;
            }
        }

        private void textBoxR_TextChanged(object sender, EventArgs e)
        {
            if (textBoxR.Text.Length > 0)
            {
                if (int.Parse(textBoxR.Text) > 255)
                    textBoxR.Text = "255";
                if (int.Parse(textBoxR.Text) < 0)
                    textBoxR.Text = "0";
                Color color = Color.FromArgb(int.Parse(textBoxR.Text), int.Parse(textBoxG.Text), int.Parse(textBoxB.Text));
                ColorPhraser switcher = new ColorPhraser();
                Cmyk cmykColor = switcher.SwitchRgbToCmyk(color);
                textBoxC.Text = cmykColor.C.ToString();
                textBoxM.Text = cmykColor.M.ToString();
                textBoxY.Text = cmykColor.Y.ToString();
                textBoxK.Text = cmykColor.K.ToString();
                draw(color);
            }
        }

        private void textBoxC_TextChanged(object sender, EventArgs e)
        {
            if (textBoxC.Text.Length > 0)
            {
                if (float.Parse(textBoxC.Text) > 1)
                    textBoxC.Text = "1";
                if (float.Parse(textBoxC.Text) < 0)
                    textBoxC.Text = "0";
                Cmyk cmykColor = new Cmyk();
                cmykColor.C = float.Parse(textBoxC.Text);
                cmykColor.M = float.Parse(textBoxM.Text);
                cmykColor.Y = float.Parse(textBoxY.Text);
                cmykColor.K = float.Parse(textBoxK.Text);
                ColorPhraser switcher = new ColorPhraser();
                Color color = switcher.SwitchCmykToRgb(cmykColor);
                textBoxR.Text = color.R.ToString();
                textBoxG.Text = color.G.ToString();
                textBoxB.Text = color.B.ToString();
                draw(color);
            }
        }

        private void textBoxM_TextChanged(object sender, EventArgs e)
        {
            if (textBoxM.Text.Length > 0)
            {
                if (float.Parse(textBoxM.Text) > 1)
                    textBoxM.Text = "1";
                if (float.Parse(textBoxM.Text) < 0)
                    textBoxM.Text = "0";
                Cmyk cmykColor = new Cmyk();
                cmykColor.C = float.Parse(textBoxC.Text);
                cmykColor.M = float.Parse(textBoxM.Text);
                cmykColor.Y = float.Parse(textBoxY.Text);
                cmykColor.K = float.Parse(textBoxK.Text);
                ColorPhraser switcher = new ColorPhraser();
                Color color = switcher.SwitchCmykToRgb(cmykColor);
                textBoxR.Text = color.R.ToString();
                textBoxG.Text = color.G.ToString();
                textBoxB.Text = color.B.ToString();
                draw(color);
            }
        }

        private void textBoxY_TextChanged(object sender, EventArgs e)
        {
            if (textBoxY.Text.Length > 0)
            {
                if (float.Parse(textBoxY.Text) > 1)
                    textBoxY.Text = "1";
                if (float.Parse(textBoxY.Text) < 0)
                    textBoxY.Text = "0";
                Cmyk cmykColor = new Cmyk();
                cmykColor.C = float.Parse(textBoxC.Text);
                cmykColor.M = float.Parse(textBoxM.Text);
                cmykColor.Y = float.Parse(textBoxY.Text);
                cmykColor.K = float.Parse(textBoxK.Text);
                ColorPhraser switcher = new ColorPhraser();
                Color color = switcher.SwitchCmykToRgb(cmykColor);
                textBoxR.Text = color.R.ToString();
                textBoxG.Text = color.G.ToString();
                textBoxB.Text = color.B.ToString();
                draw(color);
            }
        }

        private void textBoxK_TextChanged(object sender, EventArgs e)
        {
            if (textBoxK.Text.Length > 0)
            {
                if (float.Parse(textBoxK.Text) > 1)
                    textBoxK.Text = "1";
                if (float.Parse(textBoxK.Text) < 0)
                    textBoxK.Text = "0";
                Cmyk cmykColor = new Cmyk();
                cmykColor.C = float.Parse(textBoxC.Text);
                cmykColor.M = float.Parse(textBoxM.Text);
                cmykColor.Y = float.Parse(textBoxY.Text);
                cmykColor.K = float.Parse(textBoxK.Text);
                ColorPhraser switcher = new ColorPhraser();
                Color color = switcher.SwitchCmykToRgb(cmykColor);
                textBoxR.Text = color.R.ToString();
                textBoxG.Text = color.G.ToString();
                textBoxB.Text = color.B.ToString();
                draw(color);
            }
        }

        private void draw (Color color)
        {
            SolidBrush myBrush = new System.Drawing.SolidBrush(color);
            Graphics formGraphics = pictureBox1.CreateGraphics();
            formGraphics.FillRectangle(myBrush, new Rectangle(0,0,147,147));
            myBrush.Dispose();
            formGraphics.Dispose();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            draw(Color.Yellow);
        }
        private void textBoxR_Enter(object sender, EventArgs e)
        {
            this.textBoxR.TextChanged += new System.EventHandler(this.textBoxR_TextChanged);
            this.textBoxG.TextChanged += new System.EventHandler(this.textBoxG_TextChanged);
            this.textBoxB.TextChanged += new System.EventHandler(this.textBoxB_TextChanged);
        }

        private void textBoxC_Enter(object sender, EventArgs e)
        {
            this.textBoxC.TextChanged += new System.EventHandler(this.textBoxC_TextChanged);
            this.textBoxM.TextChanged += new System.EventHandler(this.textBoxM_TextChanged);
            this.textBoxY.TextChanged += new System.EventHandler(this.textBoxY_TextChanged);
            this.textBoxK.TextChanged += new System.EventHandler(this.textBoxK_TextChanged);
        }
        private void textBoxR_Leave(object sender, EventArgs e)
        {
            this.textBoxR.TextChanged -= new System.EventHandler(this.textBoxR_TextChanged);
            this.textBoxG.TextChanged -= new System.EventHandler(this.textBoxG_TextChanged);
            this.textBoxB.TextChanged -= new System.EventHandler(this.textBoxB_TextChanged);
        }

        private void textBoxC_Leave(object sender, EventArgs e)
        {
            this.textBoxC.TextChanged -= new System.EventHandler(this.textBoxC_TextChanged);
            this.textBoxM.TextChanged -= new System.EventHandler(this.textBoxM_TextChanged);
            this.textBoxY.TextChanged -= new System.EventHandler(this.textBoxY_TextChanged);
            this.textBoxK.TextChanged -= new System.EventHandler(this.textBoxK_TextChanged);
        }

        private void Colors_Load(object sender, EventArgs e)
        {
        }
    }
}
