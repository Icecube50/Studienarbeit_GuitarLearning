namespace GuitarLearning_TabulatorGenerator
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtSongName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBPM = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDistanceBeat = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtHeaderLength = new System.Windows.Forms.TextBox();
            this.txtChordLength = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnInit = new System.Windows.Forms.Button();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cckE = new System.Windows.Forms.CheckBox();
            this.cckA = new System.Windows.Forms.CheckBox();
            this.cckD = new System.Windows.Forms.CheckBox();
            this.cckG = new System.Windows.Forms.CheckBox();
            this.cckB = new System.Windows.Forms.CheckBox();
            this.cckHighE = new System.Windows.Forms.CheckBox();
            this.numValueE = new System.Windows.Forms.NumericUpDown();
            this.numValueA = new System.Windows.Forms.NumericUpDown();
            this.numValueD = new System.Windows.Forms.NumericUpDown();
            this.numValueG = new System.Windows.Forms.NumericUpDown();
            this.numValueB = new System.Windows.Forms.NumericUpDown();
            this.numValueHighE = new System.Windows.Forms.NumericUpDown();
            this.rbtnEighth = new System.Windows.Forms.RadioButton();
            this.rbtnQuarter = new System.Windows.Forms.RadioButton();
            this.rbtnPQuarter = new System.Windows.Forms.RadioButton();
            this.rbtnHalf = new System.Windows.Forms.RadioButton();
            this.rbtnWhole = new System.Windows.Forms.RadioButton();
            this.btnNewNote = new System.Windows.Forms.Button();
            this.btnNewChord = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.txtChordCounter = new System.Windows.Forms.TextBox();
            this.txtNewChordName = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numValueE)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numValueA)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numValueD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numValueG)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numValueB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numValueHighE)).BeginInit();
            this.SuspendLayout();
            // 
            // txtSongName
            // 
            this.txtSongName.Location = new System.Drawing.Point(67, 6);
            this.txtSongName.Name = "txtSongName";
            this.txtSongName.Size = new System.Drawing.Size(193, 20);
            this.txtSongName.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Song:";
            // 
            // txtBPM
            // 
            this.txtBPM.Location = new System.Drawing.Point(67, 32);
            this.txtBPM.Name = "txtBPM";
            this.txtBPM.Size = new System.Drawing.Size(193, 20);
            this.txtBPM.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Bpm:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Abstand:";
            // 
            // txtDistanceBeat
            // 
            this.txtDistanceBeat.Location = new System.Drawing.Point(67, 58);
            this.txtDistanceBeat.Name = "txtDistanceBeat";
            this.txtDistanceBeat.Size = new System.Drawing.Size(193, 20);
            this.txtDistanceBeat.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 87);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Header:";
            // 
            // txtHeaderLength
            // 
            this.txtHeaderLength.Location = new System.Drawing.Point(67, 84);
            this.txtHeaderLength.Name = "txtHeaderLength";
            this.txtHeaderLength.Size = new System.Drawing.Size(193, 20);
            this.txtHeaderLength.TabIndex = 7;
            // 
            // txtChordLength
            // 
            this.txtChordLength.Location = new System.Drawing.Point(67, 110);
            this.txtChordLength.Name = "txtChordLength";
            this.txtChordLength.Size = new System.Drawing.Size(193, 20);
            this.txtChordLength.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 113);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Chords:";
            // 
            // btnInit
            // 
            this.btnInit.Location = new System.Drawing.Point(12, 136);
            this.btnInit.Name = "btnInit";
            this.btnInit.Size = new System.Drawing.Size(112, 23);
            this.btnInit.TabIndex = 17;
            this.btnInit.Text = "Init";
            this.btnInit.UseVisualStyleBackColor = true;
            this.btnInit.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // btnGenerate
            // 
            this.btnGenerate.Enabled = false;
            this.btnGenerate.Location = new System.Drawing.Point(12, 186);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(248, 161);
            this.btnGenerate.TabIndex = 18;
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtNewChordName);
            this.groupBox1.Controls.Add(this.txtChordCounter);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.btnNewChord);
            this.groupBox1.Controls.Add(this.btnNewNote);
            this.groupBox1.Controls.Add(this.cckE);
            this.groupBox1.Controls.Add(this.cckA);
            this.groupBox1.Controls.Add(this.cckD);
            this.groupBox1.Controls.Add(this.cckG);
            this.groupBox1.Controls.Add(this.cckB);
            this.groupBox1.Controls.Add(this.cckHighE);
            this.groupBox1.Controls.Add(this.numValueE);
            this.groupBox1.Controls.Add(this.numValueA);
            this.groupBox1.Controls.Add(this.numValueD);
            this.groupBox1.Controls.Add(this.numValueG);
            this.groupBox1.Controls.Add(this.numValueB);
            this.groupBox1.Controls.Add(this.numValueHighE);
            this.groupBox1.Controls.Add(this.rbtnEighth);
            this.groupBox1.Controls.Add(this.rbtnQuarter);
            this.groupBox1.Controls.Add(this.rbtnPQuarter);
            this.groupBox1.Controls.Add(this.rbtnHalf);
            this.groupBox1.Controls.Add(this.rbtnWhole);
            this.groupBox1.Location = new System.Drawing.Point(278, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(454, 352);
            this.groupBox1.TabIndex = 48;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tabulator Input";
            // 
            // cckE
            // 
            this.cckE.AutoSize = true;
            this.cckE.Location = new System.Drawing.Point(36, 177);
            this.cckE.Name = "cckE";
            this.cckE.Size = new System.Drawing.Size(57, 17);
            this.cckE.TabIndex = 16;
            this.cckE.Text = "SaiteE";
            this.cckE.UseVisualStyleBackColor = true;
            this.cckE.CheckedChanged += new System.EventHandler(this.cckE_CheckedChanged);
            // 
            // cckA
            // 
            this.cckA.AutoSize = true;
            this.cckA.Location = new System.Drawing.Point(36, 151);
            this.cckA.Name = "cckA";
            this.cckA.Size = new System.Drawing.Size(63, 17);
            this.cckA.TabIndex = 15;
            this.cckA.Text = "Saite: A";
            this.cckA.UseVisualStyleBackColor = true;
            this.cckA.CheckedChanged += new System.EventHandler(this.cckA_CheckedChanged);
            // 
            // cckD
            // 
            this.cckD.AutoSize = true;
            this.cckD.Location = new System.Drawing.Point(36, 125);
            this.cckD.Name = "cckD";
            this.cckD.Size = new System.Drawing.Size(64, 17);
            this.cckD.TabIndex = 14;
            this.cckD.Text = "Saite: D";
            this.cckD.UseVisualStyleBackColor = true;
            this.cckD.CheckedChanged += new System.EventHandler(this.cckD_CheckedChanged);
            // 
            // cckG
            // 
            this.cckG.AutoSize = true;
            this.cckG.Location = new System.Drawing.Point(36, 99);
            this.cckG.Name = "cckG";
            this.cckG.Size = new System.Drawing.Size(64, 17);
            this.cckG.TabIndex = 13;
            this.cckG.Text = "Saite: G";
            this.cckG.UseVisualStyleBackColor = true;
            this.cckG.CheckedChanged += new System.EventHandler(this.cckG_CheckedChanged);
            // 
            // cckB
            // 
            this.cckB.AutoSize = true;
            this.cckB.Location = new System.Drawing.Point(36, 73);
            this.cckB.Name = "cckB";
            this.cckB.Size = new System.Drawing.Size(63, 17);
            this.cckB.TabIndex = 12;
            this.cckB.Text = "Saite: B";
            this.cckB.UseVisualStyleBackColor = true;
            this.cckB.CheckedChanged += new System.EventHandler(this.cckB_CheckedChanged);
            // 
            // cckHighE
            // 
            this.cckHighE.AutoSize = true;
            this.cckHighE.Location = new System.Drawing.Point(36, 48);
            this.cckHighE.Name = "cckHighE";
            this.cckHighE.Size = new System.Drawing.Size(62, 17);
            this.cckHighE.TabIndex = 11;
            this.cckHighE.Text = "Saite: e";
            this.cckHighE.UseVisualStyleBackColor = true;
            this.cckHighE.CheckedChanged += new System.EventHandler(this.cckHighE_CheckedChanged);
            // 
            // numValueE
            // 
            this.numValueE.Enabled = false;
            this.numValueE.Location = new System.Drawing.Point(122, 176);
            this.numValueE.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.numValueE.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.numValueE.Name = "numValueE";
            this.numValueE.Size = new System.Drawing.Size(120, 20);
            this.numValueE.TabIndex = 10;
            // 
            // numValueA
            // 
            this.numValueA.Enabled = false;
            this.numValueA.Location = new System.Drawing.Point(122, 150);
            this.numValueA.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.numValueA.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.numValueA.Name = "numValueA";
            this.numValueA.Size = new System.Drawing.Size(120, 20);
            this.numValueA.TabIndex = 9;
            // 
            // numValueD
            // 
            this.numValueD.Enabled = false;
            this.numValueD.Location = new System.Drawing.Point(122, 124);
            this.numValueD.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.numValueD.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.numValueD.Name = "numValueD";
            this.numValueD.Size = new System.Drawing.Size(120, 20);
            this.numValueD.TabIndex = 8;
            // 
            // numValueG
            // 
            this.numValueG.Enabled = false;
            this.numValueG.Location = new System.Drawing.Point(122, 98);
            this.numValueG.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.numValueG.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.numValueG.Name = "numValueG";
            this.numValueG.Size = new System.Drawing.Size(120, 20);
            this.numValueG.TabIndex = 7;
            // 
            // numValueB
            // 
            this.numValueB.Enabled = false;
            this.numValueB.Location = new System.Drawing.Point(122, 72);
            this.numValueB.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.numValueB.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.numValueB.Name = "numValueB";
            this.numValueB.Size = new System.Drawing.Size(120, 20);
            this.numValueB.TabIndex = 6;
            // 
            // numValueHighE
            // 
            this.numValueHighE.Enabled = false;
            this.numValueHighE.Location = new System.Drawing.Point(122, 47);
            this.numValueHighE.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.numValueHighE.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.numValueHighE.Name = "numValueHighE";
            this.numValueHighE.Size = new System.Drawing.Size(120, 20);
            this.numValueHighE.TabIndex = 5;
            // 
            // rbtnEighth
            // 
            this.rbtnEighth.AutoSize = true;
            this.rbtnEighth.Location = new System.Drawing.Point(293, 18);
            this.rbtnEighth.Name = "rbtnEighth";
            this.rbtnEighth.Size = new System.Drawing.Size(55, 17);
            this.rbtnEighth.TabIndex = 4;
            this.rbtnEighth.Text = "Achtel";
            this.rbtnEighth.UseVisualStyleBackColor = true;
            // 
            // rbtnQuarter
            // 
            this.rbtnQuarter.AutoSize = true;
            this.rbtnQuarter.Checked = true;
            this.rbtnQuarter.Location = new System.Drawing.Point(233, 18);
            this.rbtnQuarter.Name = "rbtnQuarter";
            this.rbtnQuarter.Size = new System.Drawing.Size(54, 17);
            this.rbtnQuarter.TabIndex = 3;
            this.rbtnQuarter.TabStop = true;
            this.rbtnQuarter.Text = "Viertel";
            this.rbtnQuarter.UseVisualStyleBackColor = true;
            // 
            // rbtnPQuarter
            // 
            this.rbtnPQuarter.AutoSize = true;
            this.rbtnPQuarter.Location = new System.Drawing.Point(122, 18);
            this.rbtnPQuarter.Name = "rbtnPQuarter";
            this.rbtnPQuarter.Size = new System.Drawing.Size(105, 17);
            this.rbtnPQuarter.TabIndex = 2;
            this.rbtnPQuarter.Text = "Punktierte Viertel";
            this.rbtnPQuarter.UseVisualStyleBackColor = true;
            // 
            // rbtnHalf
            // 
            this.rbtnHalf.AutoSize = true;
            this.rbtnHalf.Location = new System.Drawing.Point(69, 18);
            this.rbtnHalf.Name = "rbtnHalf";
            this.rbtnHalf.Size = new System.Drawing.Size(47, 17);
            this.rbtnHalf.TabIndex = 1;
            this.rbtnHalf.Text = "Halb";
            this.rbtnHalf.UseVisualStyleBackColor = true;
            // 
            // rbtnWhole
            // 
            this.rbtnWhole.AutoSize = true;
            this.rbtnWhole.Location = new System.Drawing.Point(7, 18);
            this.rbtnWhole.Name = "rbtnWhole";
            this.rbtnWhole.Size = new System.Drawing.Size(56, 17);
            this.rbtnWhole.TabIndex = 0;
            this.rbtnWhole.Text = "Ganze";
            this.rbtnWhole.UseVisualStyleBackColor = true;
            // 
            // btnNewNote
            // 
            this.btnNewNote.Location = new System.Drawing.Point(263, 96);
            this.btnNewNote.Name = "btnNewNote";
            this.btnNewNote.Size = new System.Drawing.Size(75, 23);
            this.btnNewNote.TabIndex = 17;
            this.btnNewNote.Text = "+ Note";
            this.btnNewNote.UseVisualStyleBackColor = true;
            this.btnNewNote.Click += new System.EventHandler(this.btnNewNote_Click);
            // 
            // btnNewChord
            // 
            this.btnNewChord.Location = new System.Drawing.Point(263, 145);
            this.btnNewChord.Name = "btnNewChord";
            this.btnNewChord.Size = new System.Drawing.Size(75, 23);
            this.btnNewChord.TabIndex = 18;
            this.btnNewChord.Text = "+ Akkord";
            this.btnNewChord.UseVisualStyleBackColor = true;
            this.btnNewChord.Click += new System.EventHandler(this.btnNewChord_Click);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(388, 20);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(47, 13);
            this.label16.TabIndex = 19;
            this.label16.Text = "Counter:";
            // 
            // txtChordCounter
            // 
            this.txtChordCounter.Enabled = false;
            this.txtChordCounter.Location = new System.Drawing.Point(384, 36);
            this.txtChordCounter.Name = "txtChordCounter";
            this.txtChordCounter.Size = new System.Drawing.Size(51, 20);
            this.txtChordCounter.TabIndex = 20;
            // 
            // txtNewChordName
            // 
            this.txtNewChordName.Location = new System.Drawing.Point(344, 145);
            this.txtNewChordName.Name = "txtNewChordName";
            this.txtNewChordName.Size = new System.Drawing.Size(100, 20);
            this.txtNewChordName.TabIndex = 21;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(747, 479);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.btnInit);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtChordLength);
            this.Controls.Add(this.txtHeaderLength);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtDistanceBeat);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtBPM);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtSongName);
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numValueE)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numValueA)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numValueD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numValueG)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numValueB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numValueHighE)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSongName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBPM;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDistanceBeat;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtHeaderLength;
        private System.Windows.Forms.TextBox txtChordLength;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnInit;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbtnEighth;
        private System.Windows.Forms.RadioButton rbtnQuarter;
        private System.Windows.Forms.RadioButton rbtnPQuarter;
        private System.Windows.Forms.RadioButton rbtnHalf;
        private System.Windows.Forms.RadioButton rbtnWhole;
        private System.Windows.Forms.CheckBox cckE;
        private System.Windows.Forms.CheckBox cckA;
        private System.Windows.Forms.CheckBox cckD;
        private System.Windows.Forms.CheckBox cckG;
        private System.Windows.Forms.CheckBox cckB;
        private System.Windows.Forms.CheckBox cckHighE;
        private System.Windows.Forms.NumericUpDown numValueE;
        private System.Windows.Forms.NumericUpDown numValueA;
        private System.Windows.Forms.NumericUpDown numValueD;
        private System.Windows.Forms.NumericUpDown numValueG;
        private System.Windows.Forms.NumericUpDown numValueB;
        private System.Windows.Forms.NumericUpDown numValueHighE;
        private System.Windows.Forms.TextBox txtChordCounter;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button btnNewChord;
        private System.Windows.Forms.Button btnNewNote;
        private System.Windows.Forms.TextBox txtNewChordName;
    }
}

