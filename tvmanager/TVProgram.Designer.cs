namespace tvmanager
{
    partial class TVProgram
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TVProgram));
            this.lvTvProgram = new System.Windows.Forms.ListView();
            this.txbOpis = new System.Windows.Forms.TextBox();
            this.btnCenzura = new System.Windows.Forms.Button();
            this.btnIzvanrednaSituacija = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.SuspendLayout();
            // 
            // lvTvProgram
            // 
            this.lvTvProgram.Font = new System.Drawing.Font("Arial Narrow", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lvTvProgram.FullRowSelect = true;
            this.lvTvProgram.Location = new System.Drawing.Point(21, 58);
            this.lvTvProgram.Name = "lvTvProgram";
            this.lvTvProgram.Size = new System.Drawing.Size(264, 415);
            this.lvTvProgram.TabIndex = 0;
            this.lvTvProgram.UseCompatibleStateImageBehavior = false;
            this.lvTvProgram.View = System.Windows.Forms.View.Details;
            this.lvTvProgram.SelectedIndexChanged += new System.EventHandler(this.lvTvProgram_SelectedIndexChanged);
            // 
            // txbOpis
            // 
            this.txbOpis.Location = new System.Drawing.Point(425, 73);
            this.txbOpis.Multiline = true;
            this.txbOpis.Name = "txbOpis";
            this.txbOpis.Size = new System.Drawing.Size(196, 208);
            this.txbOpis.TabIndex = 2;
            // 
            // btnCenzura
            // 
            this.btnCenzura.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnCenzura.FlatAppearance.BorderColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnCenzura.FlatAppearance.BorderSize = 0;
            this.btnCenzura.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCenzura.Font = new System.Drawing.Font("Arial Narrow", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnCenzura.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnCenzura.Location = new System.Drawing.Point(425, 301);
            this.btnCenzura.Name = "btnCenzura";
            this.btnCenzura.Size = new System.Drawing.Size(233, 77);
            this.btnCenzura.TabIndex = 3;
            this.btnCenzura.Text = "Cenzuriraj";
            this.btnCenzura.UseVisualStyleBackColor = false;
            // 
            // btnIzvanrednaSituacija
            // 
            this.btnIzvanrednaSituacija.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnIzvanrednaSituacija.FlatAppearance.BorderColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnIzvanrednaSituacija.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnIzvanrednaSituacija.Font = new System.Drawing.Font("Arial Narrow", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnIzvanrednaSituacija.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnIzvanrednaSituacija.Location = new System.Drawing.Point(425, 396);
            this.btnIzvanrednaSituacija.Name = "btnIzvanrednaSituacija";
            this.btnIzvanrednaSituacija.Size = new System.Drawing.Size(233, 77);
            this.btnIzvanrednaSituacija.TabIndex = 4;
            this.btnIzvanrednaSituacija.Text = "Izvanredna situacija";
            this.btnIzvanrednaSituacija.UseVisualStyleBackColor = false;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(31, 28);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(135, 24);
            this.comboBox1.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(422, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 17);
            this.label1.TabIndex = 6;
            this.label1.Text = "Kratki opis";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // TVProgram
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(694, 502);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.btnIzvanrednaSituacija);
            this.Controls.Add(this.btnCenzura);
            this.Controls.Add(this.txbOpis);
            this.Controls.Add(this.lvTvProgram);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TVProgram";
            this.Text = "Monitor program";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lvTvProgram;
        private System.Windows.Forms.TextBox txbOpis;
        private System.Windows.Forms.Button btnCenzura;
        private System.Windows.Forms.Button btnIzvanrednaSituacija;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    }
}

