namespace GrafikaKomputerowa.Zad7
{
    partial class ManualThreshold
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
            this.button1 = new System.Windows.Forms.Button();
            this.int32TextBox1 = new GrafikaKomputerowa.Zad4.Int32TextBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(23, 49);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "ok";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // int32TextBox1
            // 
            this.int32TextBox1.AllowNegativeNumber = false;
            this.int32TextBox1.Location = new System.Drawing.Point(12, 12);
            this.int32TextBox1.Name = "int32TextBox1";
            this.int32TextBox1.Size = new System.Drawing.Size(100, 20);
            this.int32TextBox1.TabIndex = 0;
            // 
            // ManualThreshold
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(132, 91);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.int32TextBox1);
            this.Name = "ManualThreshold";
            this.Text = "ManualThreshold";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Zad4.Int32TextBox int32TextBox1;
        private System.Windows.Forms.Button button1;
    }
}