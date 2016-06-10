namespace GrafikaKomputerowa.Zad6
{
    partial class Stretch
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
            this.int32TextBox1 = new GrafikaKomputerowa.Zad4.Int32TextBox();
            this.int32TextBox2 = new GrafikaKomputerowa.Zad4.Int32TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // int32TextBox1
            // 
            this.int32TextBox1.Location = new System.Drawing.Point(12, 41);
            this.int32TextBox1.Name = "int32TextBox1";
            this.int32TextBox1.Size = new System.Drawing.Size(58, 20);
            this.int32TextBox1.TabIndex = 0;
            this.int32TextBox1.AllowNegativeNumber = false;
            // 
            // int32TextBox2
            // 
            this.int32TextBox2.Location = new System.Drawing.Point(106, 41);
            this.int32TextBox2.Name = "int32TextBox2";
            this.int32TextBox2.Size = new System.Drawing.Size(59, 20);
            this.int32TextBox2.TabIndex = 1;
            this.int32TextBox2.AllowNegativeNumber = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Donly próg:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(103, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Górny próg:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(48, 69);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Stretch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(177, 104);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.int32TextBox2);
            this.Controls.Add(this.int32TextBox1);
            this.Name = "Stretch";
            this.Text = "Stretch";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Zad4.Int32TextBox int32TextBox1;
        private Zad4.Int32TextBox int32TextBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
    }
}