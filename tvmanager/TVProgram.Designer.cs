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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TVProgram));
            this.lvTvProgram = new System.Windows.Forms.ListView();
            this.txbOpis = new System.Windows.Forms.TextBox();
            this.btnCenzura = new System.Windows.Forms.Button();
            this.btnIzvanrednaSituacija = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.statusStripDani = new System.Windows.Forms.StatusStrip();
            this.tsslPon = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslUto = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslSri = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslCet = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslPet = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslSub = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslNed = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStripDani.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvTvProgram
            // 
            this.lvTvProgram.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lvTvProgram.FullRowSelect = true;
            this.lvTvProgram.HideSelection = false;
            this.lvTvProgram.Location = new System.Drawing.Point(21, 58);
            this.lvTvProgram.Name = "lvTvProgram";
            this.lvTvProgram.Size = new System.Drawing.Size(315, 415);
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
            this.btnCenzura.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnCenzura.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnCenzura.Location = new System.Drawing.Point(425, 301);
            this.btnCenzura.Name = "btnCenzura";
            this.btnCenzura.Size = new System.Drawing.Size(233, 77);
            this.btnCenzura.TabIndex = 3;
            this.btnCenzura.Text = "Cenzuriraj";
            this.btnCenzura.UseVisualStyleBackColor = false;
            this.btnCenzura.Click += new System.EventHandler(this.btnCenzura_Click);
            // 
            // btnIzvanrednaSituacija
            // 
            this.btnIzvanrednaSituacija.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnIzvanrednaSituacija.FlatAppearance.BorderColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnIzvanrednaSituacija.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnIzvanrednaSituacija.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnIzvanrednaSituacija.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnIzvanrednaSituacija.Location = new System.Drawing.Point(425, 396);
            this.btnIzvanrednaSituacija.Name = "btnIzvanrednaSituacija";
            this.btnIzvanrednaSituacija.Size = new System.Drawing.Size(233, 77);
            this.btnIzvanrednaSituacija.TabIndex = 4;
            this.btnIzvanrednaSituacija.Text = "Izvanredna situacija";
            this.btnIzvanrednaSituacija.UseVisualStyleBackColor = false;
            this.btnIzvanrednaSituacija.Click += new System.EventHandler(this.btnIzvanrednaSituacija_Click);
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
            // statusStripDani
            // 
            this.statusStripDani.Dock = System.Windows.Forms.DockStyle.Top;
            this.statusStripDani.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusStripDani.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStripDani.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslPon,
            this.tsslUto,
            this.tsslSri,
            this.tsslCet,
            this.tsslPet,
            this.tsslSub,
            this.tsslNed});
            this.statusStripDani.Location = new System.Drawing.Point(0, 0);
            this.statusStripDani.Name = "statusStripDani";
            this.statusStripDani.Size = new System.Drawing.Size(694, 29);
            this.statusStripDani.TabIndex = 7;
            this.statusStripDani.Text = "statusStripDani";
            // 
            // tsslPon
            // 
            this.tsslPon.Name = "tsslPon";
            this.tsslPon.Size = new System.Drawing.Size(47, 24);
            this.tsslPon.Text = "Pon";
            this.tsslPon.Click += new System.EventHandler(this.tsslPon_Click);
            // 
            // tsslUto
            // 
            this.tsslUto.Name = "tsslUto";
            this.tsslUto.Size = new System.Drawing.Size(41, 24);
            this.tsslUto.Text = "Uto";
            this.tsslUto.Click += new System.EventHandler(this.tsslUto_Click);
            // 
            // tsslSri
            // 
            this.tsslSri.Name = "tsslSri";
            this.tsslSri.Size = new System.Drawing.Size(35, 24);
            this.tsslSri.Text = "Sri";
            this.tsslSri.Click += new System.EventHandler(this.tsslSri_Click);
            // 
            // tsslCet
            // 
            this.tsslCet.Name = "tsslCet";
            this.tsslCet.Size = new System.Drawing.Size(41, 24);
            this.tsslCet.Text = "Čet";
            this.tsslCet.Click += new System.EventHandler(this.tsslCet_Click);
            // 
            // tsslPet
            // 
            this.tsslPet.Name = "tsslPet";
            this.tsslPet.Size = new System.Drawing.Size(40, 24);
            this.tsslPet.Text = "Pet";
            this.tsslPet.Click += new System.EventHandler(this.tsslPet_Click);
            // 
            // tsslSub
            // 
            this.tsslSub.Name = "tsslSub";
            this.tsslSub.Size = new System.Drawing.Size(47, 24);
            this.tsslSub.Text = "Sub";
            this.tsslSub.Click += new System.EventHandler(this.tsslSub_Click);
            // 
            // tsslNed
            // 
            this.tsslNed.Name = "tsslNed";
            this.tsslNed.Size = new System.Drawing.Size(49, 24);
            this.tsslNed.Text = "Ned";
            this.tsslNed.Click += new System.EventHandler(this.tsslNed_Click);
            // 
            // TVProgram
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(694, 502);
            this.Controls.Add(this.statusStripDani);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnIzvanrednaSituacija);
            this.Controls.Add(this.btnCenzura);
            this.Controls.Add(this.txbOpis);
            this.Controls.Add(this.lvTvProgram);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(50, 50);
            this.Name = "TVProgram";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Monitor program";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.statusStripDani.ResumeLayout(false);
            this.statusStripDani.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnCenzura;
        private System.Windows.Forms.Button btnIzvanrednaSituacija;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.StatusStrip statusStripDani;
        private System.Windows.Forms.ToolStripStatusLabel tsslPon;
        private System.Windows.Forms.ToolStripStatusLabel tsslUto;
        private System.Windows.Forms.ToolStripStatusLabel tsslSri;
        private System.Windows.Forms.ToolStripStatusLabel tsslCet;
        private System.Windows.Forms.ToolStripStatusLabel tsslPet;
        private System.Windows.Forms.ToolStripStatusLabel tsslSub;
        private System.Windows.Forms.ToolStripStatusLabel tsslNed;
        public System.Windows.Forms.ListView lvTvProgram;
        public System.Windows.Forms.TextBox txbOpis;
    }
}

