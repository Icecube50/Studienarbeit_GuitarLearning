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
            this.btnAddSingle = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.cbSingleLength = new System.Windows.Forms.ComboBox();
            this.cbString = new System.Windows.Forms.ComboBox();
            this.txtBund = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btnInit = new System.Windows.Forms.Button();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.txtID = new System.Windows.Forms.TextBox();
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
            // btnAddSingle
            // 
            this.btnAddSingle.Enabled = false;
            this.btnAddSingle.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnAddSingle.Location = new System.Drawing.Point(401, 87);
            this.btnAddSingle.Name = "btnAddSingle";
            this.btnAddSingle.Size = new System.Drawing.Size(102, 23);
            this.btnAddSingle.TabIndex = 10;
            this.btnAddSingle.Text = "+ Note";
            this.btnAddSingle.UseVisualStyleBackColor = true;
            this.btnAddSingle.Click += new System.EventHandler(this.btnAddSingle_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(398, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(33, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Note:";
            // 
            // cbSingleLength
            // 
            this.cbSingleLength.FormattingEnabled = true;
            this.cbSingleLength.Items.AddRange(new object[] {
            "Ganze",
            "Halbe",
            "Viertel",
            "Achtel"});
            this.cbSingleLength.Location = new System.Drawing.Point(437, 6);
            this.cbSingleLength.Name = "cbSingleLength";
            this.cbSingleLength.Size = new System.Drawing.Size(194, 21);
            this.cbSingleLength.TabIndex = 12;
            // 
            // cbString
            // 
            this.cbString.FormattingEnabled = true;
            this.cbString.Items.AddRange(new object[] {
            "E",
            "A",
            "D",
            "G",
            "B",
            "e"});
            this.cbString.Location = new System.Drawing.Point(437, 35);
            this.cbString.Name = "cbString";
            this.cbString.Size = new System.Drawing.Size(194, 21);
            this.cbString.TabIndex = 13;
            // 
            // txtBund
            // 
            this.txtBund.Location = new System.Drawing.Point(437, 62);
            this.txtBund.Name = "txtBund";
            this.txtBund.Size = new System.Drawing.Size(193, 20);
            this.txtBund.TabIndex = 14;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(398, 38);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(34, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "Saite:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(398, 65);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(32, 13);
            this.label8.TabIndex = 16;
            this.label8.Text = "Bund";
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
            this.btnGenerate.Location = new System.Drawing.Point(676, 415);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(112, 23);
            this.btnGenerate.TabIndex = 18;
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // txtID
            // 
            this.txtID.Enabled = false;
            this.txtID.Location = new System.Drawing.Point(509, 89);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(122, 20);
            this.txtID.TabIndex = 19;
            this.txtID.Text = "0";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.btnInit);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtBund);
            this.Controls.Add(this.cbString);
            this.Controls.Add(this.cbSingleLength);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnAddSingle);
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
        private System.Windows.Forms.Button btnAddSingle;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbSingleLength;
        private System.Windows.Forms.ComboBox cbString;
        private System.Windows.Forms.TextBox txtBund;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnInit;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.TextBox txtID;
    }
}

