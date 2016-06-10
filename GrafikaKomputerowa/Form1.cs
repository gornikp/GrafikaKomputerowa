using GrafikaKomputerowa.Zad10;
using GrafikaKomputerowa.Zad2;
using GrafikaKomputerowa.Zad3;
using GrafikaKomputerowa.Zad4;
using GrafikaKomputerowa.Zad5;
using GrafikaKomputerowa.Zad6;
using GrafikaKomputerowa.Zad7;
using GrafikaKomputerowa.Zad8;
using GrafikaKomputerowa.Zad9;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace GrafikaKomputerowa
{
    public partial class Form1 : Form
    {
        Graphics elo;
        private List<double> ptList = new List<double>();
        private BezierCurve bc = new BezierCurve();
        Pen px = new Pen(Brushes.Red);
        Pen newpx = new Pen(Brushes.Magenta);
        Graphics g;
        private int fieldofPB;
        float midX = 0;
        float midY = 0;
        bool DrawBool;
        bool DrawBool2;
        private List<PointF> pointList = new List<PointF>();
        private List<PointF> kwadrat = new List<PointF>(4);
        public Bitmap Picture{ get { return picture; } set {
                picture = value;
                //this.Invoke((MethodInvoker)delegate {
                    pictureBox1.Image = new Bitmap(picture, (int)(picture.Width * zoomValue), (int)(picture.Height * zoomValue));
               // });
               
            } }
        private Bitmap picture;      
        public CustomAlmostStack<Bitmap> savedBitmap = new CustomAlmostStack<Bitmap>(10);
        private Colors imageForm;
        private Value askForValueForm;
        private PixelModifications pixelForm;
        public Point tempPoint { get; set; }
        private float zoomValue { get; set; }
        string name;
        PhotoFilters filter;
        public Form1()
        {
            InitializeComponent();
            filter = new PhotoFilters(this);
            zoomValue = 1;
            Picture = new Bitmap(pictureBox1.Image);
            DrawBool = false;
            DrawBool2 = false;
            elo = pictureBox1.CreateGraphics();
            Console.WriteLine(Math.Sin(0.25 * Math.PI));
        }
        #region Zadanie1
        private void openPPM_menu_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            //openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "PPM files (*.ppm)|*.ppm|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                PPMFileLoader elo = new PPMFileLoader();
                picture = elo.loadBytes(openFileDialog1.FileName);
                pictureBox1.Image = picture;
                string[] split = openFileDialog1.FileName.ToString().Split('\\');
                name = split[split.Length - 1];
            }
            openFileDialog1.Dispose();
        }


        private void zoomToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.Image = picture;
        }

        private void autoSizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBox1.Image = picture;
        }
        private void stretchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.Image = picture;
        }

        private void centerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            pictureBox1.Image = picture;
        }

        private void normalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.Normal;
            pictureBox1.Image = picture;
        }

        private void quality100ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //using (MagickImage sprite = new MagickImage(picture))
            //{
            //    sprite.Format = MagickFormat.Jpeg;
            //    sprite.Quality = 10;
            //    //sprite.Resize(40, 40);
            //    picture = sprite.ToBitmap();
            //}
            using (Image img = new Bitmap(picture))
            {
                ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
                ImageCodecInfo ici = null;

                foreach (ImageCodecInfo codec in codecs)
                {
                    if (codec.MimeType == "image/jpeg")
                        ici = codec;
                }

                EncoderParameters ep = new EncoderParameters();
                ep.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, (long)100);
                picture.Save( name + " quality 100.jpg", ici, ep);
            }
        }

        private void quatily50ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (Image img = new Bitmap(picture))
            {
                ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
                ImageCodecInfo ici = null;

                foreach (ImageCodecInfo codec in codecs)
                {
                    if (codec.MimeType == "image/jpeg")
                        ici = codec;
                }

                EncoderParameters ep = new EncoderParameters();
                ep.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, (long)50);
                picture.Save(name + " quality 50.jpg", ici, ep);
            }
        }

        private void quality25ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (Image img = new Bitmap(picture))
            {
                ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
                ImageCodecInfo ici = null;

                foreach (ImageCodecInfo codec in codecs)
                {
                    if (codec.MimeType == "image/jpeg")
                        ici = codec;
                }

                EncoderParameters ep = new EncoderParameters();
                ep.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, (long)25);
                picture.Save(name + " quality 25.jpg", ici, ep);
            }
        }

        private void quality5ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            using (Image img = new Bitmap(picture))
            {
                ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
                ImageCodecInfo ici = null;

                foreach (ImageCodecInfo codec in codecs)
                {
                    if (codec.MimeType == "image/jpeg")
                        ici = codec;
                }

                EncoderParameters ep = new EncoderParameters();
                ep.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, (long)5);
                picture.Save(name + " quality 5.jpg", ici, ep);
            }
        }
        #endregion

        private void rGBCMYKToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Colors imageForm = new Colors();
            this.imageForm = imageForm;
            imageForm.Show();
        }

        private void działaniaNaPixToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PixelModifications pixelForm = new PixelModifications(this);
            this.pixelForm = pixelForm;
            pixelForm.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            pictureBox1.Cursor = Cursors.Hand;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MouseEventArgs em = (MouseEventArgs)e;
            if (pictureBox1.Cursor == Cursors.Hand)
            {
                menuStrip1.RenderMode = ToolStripRenderMode.System;           
                tempPoint = new Point((int)(em.X/zoomValue), (int)(em.Y/zoomValue));
                toolStripStatusLabelXCoordinate.Text = em.X.ToString();
                toolStripStatusLabelYCoordinate.Text = em.Y.ToString();
                Color temp = picture.GetPixel(tempPoint.X, tempPoint.Y);

                if (temp.R < 200)
                    toolStripStatusLabelColorR.ForeColor = Color.White;
                else
                    toolStripStatusLabelColorR.ForeColor = Color.Black;

                if (temp.G < 200)
                    toolStripStatusLabelCologG.ForeColor = Color.White;
                else
                    toolStripStatusLabelCologG.ForeColor = Color.Black;

                if (temp.B < 200)
                    toolStripStatusLabelColorB.ForeColor = Color.White;
                else
                    toolStripStatusLabelColorB.ForeColor = Color.Black;

                toolStripStatusLabelColorR.Text = temp.R.ToString();
                toolStripStatusLabelColorR.BackColor = Color.FromArgb(255, temp.R, 0, 0);
                toolStripStatusLabelCologG.Text = temp.G.ToString();
                toolStripStatusLabelCologG.BackColor = Color.FromArgb(255, 0, temp.G, 0);
                toolStripStatusLabelColorB.Text = temp.B.ToString();
                toolStripStatusLabelColorB.BackColor = Color.FromArgb(255, 0, 0, temp.B);
                pictureBox1.Cursor = Cursors.Default;
            }
            else if (DrawBool)
            {
                ptList.Add(em.X);
                ptList.Add(em.Y);
                g.DrawRectangle(px, new Rectangle(em.X, em.Y, 1, 1));
            }
            else if (DrawBool2)
            {
                pointList.Add(new PointF(em.X,em.Y));
                g.DrawRectangle(px, new Rectangle(em.X, em.Y, 1, 1));              
            }
        }

        private void openFileMenu_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "Otwórz obraz";
            dlg.Filter = "pliki jpg (*.jpg)|*.jpg|pliki png (*.png)|*.png|wszystkie pliki (*.*)|*.*";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                picture = new Bitmap(dlg.OpenFile());
                pictureBox1.Image = picture;
                pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
                string[] split = dlg.FileName.ToString().Split('\\');
                name = split[split.Length - 1];
            }
            dlg.Dispose();
        }
        private void pictureBox1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void pictureBox1_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            try
            {
                picture = new Bitmap(files[0]);
                pictureBox1.Image = picture;
            }
            catch (IndexOutOfRangeException ex)
            {
                MessageBox.Show("błąd pliku?");
                ex.ToString();
            }
        }



        private void pionowyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            savedBitmap.Push(new Bitmap(picture));
            if (savedBitmap.Count() >= 0)
                button1.Enabled = true;
            filter.ApplySobelVerticalMask(new Bitmap(picture));
        }
        private void poziomyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            savedBitmap.Push(new Bitmap(picture));
            if (savedBitmap.Count() >= 0)
                button1.Enabled = true;
            filter.ApplySobelHorizontalMask(new Bitmap(picture));
        }
        private void wygładzanieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            savedBitmap.Push(new Bitmap(picture));
            if (savedBitmap.Count() >= 0)
                button1.Enabled = true;
            filter.ApplyMeanMask(new Bitmap(picture));
        }

        private void rozmycieGausowskieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            savedBitmap.Push(new Bitmap(picture));
            if (savedBitmap.Count() >= 0)
                button1.Enabled = true;
            filter.ApplyGaus5Mask(new Bitmap(picture));
        }

        private void splotMaskiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            savedBitmap.Push(new Bitmap(picture));
            if (savedBitmap.Count() >= 0)
                button1.Enabled = true;
            Value askValueForm = new Value(this);
            this.askForValueForm = askValueForm;
            askForValueForm.Show();           
        }

        private void standardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            savedBitmap.Push(new Bitmap(picture));
            if (savedBitmap.Count() >= 0)
                button1.Enabled = true;
            filter.ApplyMeanRemovalMask(new Bitmap(picture));
        }

        private void hP3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            savedBitmap.Push(new Bitmap(picture));
            if (savedBitmap.Count() >= 0)
                button1.Enabled = true;
            filter.ApplyMeanRemovalMask2(new Bitmap(picture));
        }

        private void x5ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            savedBitmap.Push(new Bitmap(picture));
            if (savedBitmap.Count() >= 0)
                button1.Enabled = true;
            filter.ApplyMedianMask5(new Bitmap(picture));
        }

        private void x3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            savedBitmap.Push(new Bitmap(picture));
            if (savedBitmap.Count() >= 0)
                button1.Enabled = true;
            filter.ApplyMedianMask3(new Bitmap(picture));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (savedBitmap.Count() != 0)
            {
                Picture = (Bitmap)savedBitmap.Pop().Clone();
                //pictureBox1.Image = new Bitmap (picture,picture;
            }
            if (savedBitmap.Count() == 0)
                button1.Enabled = false;
        }

        private void prewittPionowyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            savedBitmap.Push(new Bitmap(picture));
            if (savedBitmap.Count() >= 0)
                button1.Enabled = true;
            filter.ApplyPrewittVerticalMask(new Bitmap(picture));
        }

        private void prewittPoziomyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            savedBitmap.Push(new Bitmap(picture));
            if (savedBitmap.Count() >= 0)
                button1.Enabled = true;
            filter.ApplyPrewittHorizontalMask(new Bitmap(picture));
        }

        private void lAPL1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            savedBitmap.Push(new Bitmap(picture));
            if (savedBitmap.Count() >= 0)
                button1.Enabled = true;
            filter.ApplyLaplaceMask1(new Bitmap(picture));
        }

        private void lAPL2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            savedBitmap.Push(new Bitmap(picture));
            if (savedBitmap.Count() >= 0)
                button1.Enabled = true;
            filter.ApplyLaplaceMask2(new Bitmap(picture));
        }

        private void lAPL3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            savedBitmap.Push(new Bitmap(picture));
            if (savedBitmap.Count() >= 0)
                button1.Enabled = true;
            filter.ApplyLaplaceMask3(new Bitmap(picture));
        }

        private void ukośnyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            savedBitmap.Push(new Bitmap(picture));
            if (savedBitmap.Count() >= 0)
                button1.Enabled = true;
            filter.ApplyLaplaceSlopingMask(new Bitmap(picture));           
        }

        private void własnaMaskaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CustomMask customMask = new CustomMask(this);
            customMask.Show();
        }

        private void kuwaharaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Kuwahara customMask = new Kuwahara(this);
            customMask.Show();
        }

        private void rozciagniecieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stretch stretchApplyForm = new Stretch(this);
            stretchApplyForm.Show();
        }

        private void wyrównaieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            savedBitmap.Push(new Bitmap(picture));
            if (savedBitmap.Count() >= 0)
                button1.Enabled = true;
            HistogramOperations newHist = new HistogramOperations(this);
            newHist.HistogramEqualization(new Bitmap(picture));
        }



        private void tESTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            savedBitmap.Push(new Bitmap(picture));
            if (savedBitmap.Count() >= 0)
                button1.Enabled = true;
            filter.ApplyCircleMask(new Bitmap(picture));
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            zoomValue = zoomValue * 2;
            pictureBox1.Image = new Bitmap(picture, (int)(picture.Width * zoomValue), (int)(picture.Height* zoomValue));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            zoomValue = zoomValue / 2;
            pictureBox1.Image = new Bitmap(picture, (int)(picture.Width * zoomValue), (int)(picture.Height * zoomValue));
        }

        private void manualnaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManualThreshold manualThresholdForm = new ManualThreshold(this);
            manualThresholdForm.Show();
        }

        private void selekcjaProcentowaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PercentageSelection percentageSelectionForm = new PercentageSelection(this);
            percentageSelectionForm.Show();
        }

        private void otsuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            savedBitmap.Push(new Bitmap(picture));
            if (savedBitmap.Count() >= 0)
                button1.Enabled = true;
            BinarizationComponent binary = new BinarizationComponent(this);
            binary.TransformOtsu(new Bitmap(picture));            
        }

    private void niblackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //savedBitmap.Push(new Bitmap(picture));
            //if (savedBitmap.Count() >= 0)
            //    button1.Enabled = true;            
            Niblack NiblackForm = new Niblack(this);
            NiblackForm.Show();
        }

        private void autoWyrównaieToolStripMenuItem_Click(object sender, EventArgs e)
        {

            
        }

        private void wykrywanieTekstuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            savedBitmap.Push(new Bitmap(picture));
            if (savedBitmap.Count() >= 0)
                button1.Enabled = true;
            BinarizationComponent newHist = new BinarizationComponent(this);
            newHist.HistogramNormalization(new Bitmap(picture));
        }

        private void selekcjaIteratywnaŚredniejToolStripMenuItem_Click(object sender, EventArgs e)
        {
            savedBitmap.Push(new Bitmap(picture));
            if (savedBitmap.Count() >= 0)
                button1.Enabled = true;
            BinarizationComponent newHist = new BinarizationComponent(this);
            newHist.MeanIterativeSelection(new Bitmap(picture));
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            g = Graphics.FromHwnd(pictureBox1.Handle);
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (DrawBool)
            {
                const int POINTS_ON_CURVE = 1000;
                double[] ptind = new double[ptList.Count];
                double[] p = new double[POINTS_ON_CURVE];
                ptList.CopyTo(ptind, 0);
                bc.Bezier2D(ptind, (POINTS_ON_CURVE) / 2, p);
                //pictureBox1.Refresh();
                // draw points
                for (int i = 1; i != POINTS_ON_CURVE - 1; i += 2)
                {
                    g.DrawRectangle(newpx, new Rectangle((int)p[i + 1], (int)p[i], 1, 1));
                    g.Flush();
                    Application.DoEvents();
                }
                DrawBool = false;
                ptList.Clear();               
            }
            else { DrawBool = true; }

        }
        private void button8_Click(object sender, EventArgs e)
        {                          
            if (DrawBool2 == true)
            {
                kwadrat.Clear();
                foreach (var item in pointList)
                {
                    kwadrat.Add(item);
                }
                midX = 0;
                midY = 0;
                foreach (var item in kwadrat)
                {
                    midX += item.X;
                    midY += item.Y;
                }
                midX /= kwadrat.Count;
                midY /= kwadrat.Count;
                kwadrat.Sort((new Comparison<PointF>(SortCornersClockwise)));
                DrawBool2 = false;
                pointList.Clear();
                savedBitmap.Push(new Bitmap(picture));
                if (savedBitmap.Count() >= 0)
                    button1.Enabled = true;
                Graphics g = Graphics.FromImage(picture);
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.DrawPolygon(Pens.Black, kwadrat.ToArray());
                g.Flush();
                Picture = picture;
            }
            else { DrawBool2 = true; }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            pictureBox1.Invalidate();
        }

        private void ścienianieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            savedBitmap.Push(new Bitmap(picture));
            if (savedBitmap.Count() >= 0)
                button1.Enabled = true;
            ThinningLibrary thin = new ThinningLibrary(this);
            thin.ZhangSuenAlgorithm(new Bitmap(picture));
        }

        private void entropiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            savedBitmap.Push(new Bitmap(picture));
            if (savedBitmap.Count() >= 0)
                button1.Enabled = true;
            BinarizationComponent binaryzation = new BinarizationComponent(this);
            binaryzation.EntropySelection(new Bitmap(picture));
        }

        private void zielonyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            savedBitmap.Push(new Bitmap(picture));
            if (savedBitmap.Count() >= 0)
                button1.Enabled = true;
            PixelOperations pixelmod = new PixelOperations();                      
            Picture = pixelmod.grayscale3(new Bitmap(picture));          
        }

        private void błądMinimalnyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            savedBitmap.Push(new Bitmap(picture));
            if (savedBitmap.Count() >= 0)
                button1.Enabled = true;
            fieldofPB = 0;
            for (int i = 0; i < picture.Height; i++)
            {
                for (int j = 0; j < picture.Width; j++)
                {
                    Color a = picture.GetPixel(j, i);
                    if (a.A == 0)
                    {
                        picture.SetPixel(j, i, Color.White);
                        fieldofPB++;
                    }
                }
            }
            fieldofPB = picture.Height * picture.Width - fieldofPB;
            BinarizationComponent binaryzation = new BinarizationComponent(this);
            binaryzation.GreenSearch(new Bitmap(picture));
        }

        private void ądMinimalnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            savedBitmap.Push(new Bitmap(picture));
            if (savedBitmap.Count() >= 0)
                button1.Enabled = true;
            BinarizationComponent binaryzation = new BinarizationComponent(this);
            binaryzation.MinimalError(new Bitmap(picture));            
        }

        private void dylatacjaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            savedBitmap.Push(new Bitmap(picture));
            if (savedBitmap.Count() >= 0)
                button1.Enabled = true;
            MorfologyOperators morfology = new MorfologyOperators(this);
            morfology.Dylation(new Bitmap(picture));
        }

        private void erozjaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            savedBitmap.Push(new Bitmap(picture));
            if (savedBitmap.Count() >= 0)
                button1.Enabled = true;
            MorfologyOperators morfology = new MorfologyOperators(this);
            morfology.Erosin(new Bitmap(picture));
        }

        private void otwarcieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            savedBitmap.Push(new Bitmap(picture));
            if (savedBitmap.Count() >= 0)
                button1.Enabled = true;
            MorfologyOperators morfology = new MorfologyOperators(this);
            morfology.OpeningFilter(new Bitmap(picture));
        }

        private void domknięcieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            savedBitmap.Push(new Bitmap(picture));
            if (savedBitmap.Count() >= 0)
                button1.Enabled = true;
            MorfologyOperators morfology = new MorfologyOperators(this);
            morfology.ClosingFilter(new Bitmap(picture));
        }

        private void zgrubianieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            savedBitmap.Push(new Bitmap(picture));
            if (savedBitmap.Count() >= 0)
                button1.Enabled = true;
            MorfologyOperators morfology = new MorfologyOperators(this);
            morfology.imageThickening(new Bitmap(picture),2);
        }

        private void scienianieHOMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            savedBitmap.Push(new Bitmap(picture));
            if (savedBitmap.Count() >= 0)
                button1.Enabled = true;
            MorfologyOperators morfology = new MorfologyOperators(this);
            morfology.imageThinning(new Bitmap(picture), 2);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //double a = 0, b = 359368;
            double a = 0, b = fieldofPB;
            for (int i = 0; i < picture.Height; i++)
            {
                for (int j = 0; j < picture.Width; j++)
                {
                    Color elo = picture.GetPixel(j, i);                  
                    if (elo.R == 0)
                    {
                        a++;
                    }
                }
            }
            double c = a/b * 100;
            MessageBox.Show("to jest ta liczba: " + c);
        }

        
        private PointF scaling(PointF elo)
        {
            return new PointF(elo.X + pictureBox1.Image.Width/2 ,elo.Y + pictureBox1.Image.Height/2);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            savedBitmap.Push(new Bitmap(picture));
            if (savedBitmap.Count() >= 0)
                button1.Enabled = true;
            Graphics g = Graphics.FromImage(picture);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;

            string[] parametrers = textBox1.Text.Split('|');
            PointF wektor = new PointF(int.Parse(parametrers[0]), int.Parse(parametrers[1]));
            List<PointF> figure = new List<PointF>();
            figure = PointOperations.MovePointList(kwadrat, wektor);
            midX = 0;
            midY = 0;
            foreach (var item in figure)
            {
                midX += item.X;
                midY += item.Y;
            }
            midX /= figure.Count;
            midY /= figure.Count;
            figure.Sort((new Comparison<PointF>(SortCornersClockwise)));
            g.DrawPolygon(Pens.Black, figure.ToArray());
            Picture = picture;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            savedBitmap.Push(new Bitmap(picture));
            if (savedBitmap.Count() >= 0)
                button1.Enabled = true;
            Graphics g = Graphics.FromImage(picture);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;

            string[] parametrers = textBox1.Text.Split('|');
            PointF wektor = new PointF(int.Parse(parametrers[0]), int.Parse(parametrers[1]));
            int alpha = int.Parse(parametrers[2]);
            List<PointF> figure = new List<PointF>();
            figure = PointOperations.RotatePointList(kwadrat, wektor,alpha);
            midX = 0;
            midY = 0;
            foreach (var item in figure)
            {
                midX += item.X;
                midY += item.Y;
            }
            midX /= figure.Count;
            midY /= figure.Count;

            figure.Sort((new Comparison<PointF>(SortCornersClockwise)));
            g.DrawPolygon(Pens.Black, figure.ToArray());
            Picture = picture;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            savedBitmap.Push(new Bitmap(picture));
            if (savedBitmap.Count() >= 0)
                button1.Enabled = true;
            Graphics g = Graphics.FromImage(picture);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;

            string[] parametrers = textBox1.Text.Split('|');
            PointF wektor = new PointF(int.Parse(parametrers[0]), int.Parse(parametrers[1]));
            float k = float.Parse(parametrers[2]);
            List<PointF> figure = new List<PointF>();
            figure = PointOperations.ScalePointList(kwadrat, wektor, k);
            midX = 0;
            midY = 0;
            foreach (var item in figure)
            {
                midX += item.X;
                midY += item.Y;
            }
            midX /= figure.Count;
            midY /= figure.Count;

            figure.Sort((new Comparison<PointF>(SortCornersClockwise)));
            g.DrawPolygon(Pens.Black, figure.ToArray());
            Picture = picture;
        }
        public int SortCornersClockwise(PointF A, PointF B)
        {
            
            double aTanA, aTanB;


            aTanA = Math.Atan2(A.Y - midY, A.X - midX);
            aTanB = Math.Atan2(B.Y - midY, B.X - midX);

            if (aTanA < aTanB) return -1;
            else if (aTanB < aTanA) return 1;
            return 0;
        }
    }
}
  
