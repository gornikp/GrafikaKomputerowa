namespace GrafikaKomputerowa.Zad5
{
    partial class CustomMask
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tb00 = new GrafikaKomputerowa.Zad4.Int32TextBox();
            this.tb10 = new GrafikaKomputerowa.Zad4.Int32TextBox();
            this.tb20 = new GrafikaKomputerowa.Zad4.Int32TextBox();
            this.tb01 = new GrafikaKomputerowa.Zad4.Int32TextBox();
            this.tb02 = new GrafikaKomputerowa.Zad4.Int32TextBox();
            this.tb11 = new GrafikaKomputerowa.Zad4.Int32TextBox();
            this.tb12 = new GrafikaKomputerowa.Zad4.Int32TextBox();
            this.tb21 = new GrafikaKomputerowa.Zad4.Int32TextBox();
            this.tb22 = new GrafikaKomputerowa.Zad4.Int32TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tb00
            // 
            this.tb00.AllowNegativeNumber = false;
            this.tb00.Location = new System.Drawing.Point(14, 10);
            this.tb00.Name = "tb00";
            this.tb00.Size = new System.Drawing.Size(26, 20);
            this.tb00.TabIndex = 0;
            this.tb00.AllowNegativeNumber = true;
            // 
            // tb10
            // 
            this.tb10.AllowNegativeNumber = false;
            this.tb10.Location = new System.Drawing.Point(14, 36);
            this.tb10.Name = "tb10";
            this.tb10.Size = new System.Drawing.Size(26, 20);
            this.tb10.TabIndex = 1;
            this.tb10.AllowNegativeNumber = true;
            // 
            // tb20
            // 
            this.tb20.AllowNegativeNumber = false;
            this.tb20.Location = new System.Drawing.Point(14, 62);
            this.tb20.Name = "tb20";
            this.tb20.Size = new System.Drawing.Size(26, 20);
            this.tb20.TabIndex = 2;
            this.tb20.AllowNegativeNumber = true;
            // 
            // tb01
            // 
            this.tb01.AllowNegativeNumber = false;
            this.tb01.Location = new System.Drawing.Point(46, 10);
            this.tb01.Name = "tb01";
            this.tb01.Size = new System.Drawing.Size(26, 20);
            this.tb01.TabIndex = 3;
            this.tb01.AllowNegativeNumber = true;
            // 
            // tb02
            // 
            this.tb02.AllowNegativeNumber = false;
            this.tb02.Location = new System.Drawing.Point(78, 10);
            this.tb02.Name = "tb02";
            this.tb02.Size = new System.Drawing.Size(26, 20);
            this.tb02.TabIndex = 4;
            this.tb02.AllowNegativeNumber = true;
            // 
            // tb11
            // 
            this.tb11.AllowNegativeNumber = false;
            this.tb11.Location = new System.Drawing.Point(46, 36);
            this.tb11.Name = "tb11";
            this.tb11.Size = new System.Drawing.Size(26, 20);
            this.tb11.TabIndex = 5;
            this.tb11.AllowNegativeNumber = true;
            // 
            // tb12
            // 
            this.tb12.AllowNegativeNumber = false;
            this.tb12.Location = new System.Drawing.Point(78, 36);
            this.tb12.Name = "tb12";
            this.tb12.Size = new System.Drawing.Size(26, 20);
            this.tb12.TabIndex = 6;
            this.tb12.AllowNegativeNumber = true;
            // 
            // tb21
            // 
            this.tb21.AllowNegativeNumber = false;
            this.tb21.Location = new System.Drawing.Point(46, 62);
            this.tb21.Name = "tb21";
            this.tb21.Size = new System.Drawing.Size(26, 20);
            this.tb21.TabIndex = 7;
            this.tb21.AllowNegativeNumber = true;
            // 
            // tb22
            // 
            this.tb22.AllowNegativeNumber = false;
            this.tb22.Location = new System.Drawing.Point(78, 62);
            this.tb22.Name = "tb22";
            this.tb22.Size = new System.Drawing.Size(26, 20);
            this.tb22.TabIndex = 8;
            this.tb22.AllowNegativeNumber = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(14, 88);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(90, 23);
            this.button1.TabIndex = 9;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // CustomMask
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 137);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tb22);
            this.Controls.Add(this.tb21);
            this.Controls.Add(this.tb12);
            this.Controls.Add(this.tb11);
            this.Controls.Add(this.tb02);
            this.Controls.Add(this.tb01);
            this.Controls.Add(this.tb20);
            this.Controls.Add(this.tb10);
            this.Controls.Add(this.tb00);
            this.Name = "CustomMask";
            this.Text = "CustomMask";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Zad4.Int32TextBox tb00;
        private Zad4.Int32TextBox tb10;
        private Zad4.Int32TextBox tb20;
        private Zad4.Int32TextBox tb01;
        private Zad4.Int32TextBox tb02;
        private Zad4.Int32TextBox tb11;
        private Zad4.Int32TextBox tb12;
        private Zad4.Int32TextBox tb21;
        private Zad4.Int32TextBox tb22;
        private System.Windows.Forms.Button button1;
    }
}