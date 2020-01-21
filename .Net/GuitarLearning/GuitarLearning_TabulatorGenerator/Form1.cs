using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GuitarLearning_TabulatorGenerator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            cbString.Items.Clear();
            cbString.Items.Add(GuitarStrings.E);
            cbString.Items.Add(GuitarStrings.A);
            cbString.Items.Add(GuitarStrings.D);
            cbString.Items.Add(GuitarStrings.G);
            cbString.Items.Add(GuitarStrings.B);
            cbString.Items.Add(GuitarStrings.e);

            cbSingleLength.Items.Clear();
            cbSingleLength.Items.Add(Notes.Ganze);
            cbSingleLength.Items.Add(Notes.Halbe);
            cbSingleLength.Items.Add(Notes.Viertel);
            cbSingleLength.Items.Add(Notes.Achtel);
        }

        private int IdCounter = 0;
        private void btnAddSingle_Click(object sender, EventArgs e)
        {
            GuitarStrings guitarStrings = (GuitarStrings)cbString.SelectedItem;
            uint bund = Convert.ToUInt32(txtBund.Text);
            Notes notes = (Notes)cbSingleLength.SelectedItem;

            if (notes == Notes.Ganze) NoteContainer.Song.Add(new WholeNote(guitarStrings, bund, IdCounter.ToString()));
            if (notes == Notes.Halbe) NoteContainer.Song.Add(new HalfNote(guitarStrings, bund, IdCounter.ToString()));
            if (notes == Notes.Viertel) NoteContainer.Song.Add(new QuarterNote(guitarStrings, bund, IdCounter.ToString()));
            if (notes == Notes.Achtel) NoteContainer.Song.Add(new EighthNote(guitarStrings, bund, IdCounter.ToString()));
            IdCounter++;
            txtID.Text = IdCounter.ToString();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                SetValues.Title = txtSongName.Text;
                SetValues.QuarterDuration = Convert.ToUInt32(txtDistanceBeat.Text);
                SetValues.BPM = Convert.ToUInt32(txtBPM.Text);
                SetValues.SizeHeader = Convert.ToUInt32(txtHeaderLength.Text);
                SetValues.SizeChords = Convert.ToUInt32(txtChordLength.Text);

                btnAddSingle.Enabled = true;
                btnGenerate.Enabled = true;
            }
            catch
            {
                MessageBox.Show("Please enter valid numbers");
            }
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            SaveFileDialog sdlg = new SaveFileDialog();
            var result = sdlg.ShowDialog();
            if(result == DialogResult.OK)
            {
                string file = sdlg.FileName;

                if (File.Exists(file)) File.Delete(file);
                string fileCSS = Path.GetFileNameWithoutExtension(file) + ".css";
                string cssPath = Path.Combine(Path.GetDirectoryName(file), fileCSS);
                if (File.Exists(cssPath)) File.Delete(cssPath);
               
                CreateHTML(file);
                CreateCSS(cssPath);
            }
            sdlg.Dispose();
        }

        #region HTML
        private void CreateHTML(string file)
        {
            File.AppendAllText(file, "<!-- This code was automatically generated. Any change may result in errors -->\n");
            CreateHeader(file);
            CreateStringLines(file);
            CreateFooter(file);
        }
        private void CreateHeader(string file)
        {
            File.AppendAllText(file, "<!DOCTYPE html>\n");
            File.AppendAllText(file, "<html lang=\"de\">\n");
            File.AppendAllText(file, "<head>\n");
            File.AppendAllText(file, "<meta charset=\"utf-8\">\n");
            string fileCSS = Path.GetFileNameWithoutExtension(file) + ".css";
            File.AppendAllText(file, "<link rel=\"stylesheet\" type=\"text/css\" href=\"" + fileCSS + "\">\n");
            File.AppendAllText(file,
                "</head>\n" +
                "<body>\n" +
                "<div class=\"TabHeader\">\n" +
                "<h3>" + SetValues.Title + "</h3>\n" +
                "<p>" + SetValues.BPM.ToString() + " BPM</p>\n");
        }

        private void CreateStringLines(string file)
        {
            File.AppendAllText(file,
                "<div class=\"Sheet\" id=\"divSheet\">\n" +
                "<hr class=\"GuitarString\">\n" +
                "<hr class=\"GuitarString\">\n" +
                "<hr class=\"GuitarString\">\n" +
                "<hr class=\"GuitarString\">\n" +
                "<hr class=\"GuitarString\">\n" +
                "<hr class=\"GuitarString\">\n");

            CreateAllNotes(file);

            File.AppendAllText(file, "</div>\n");
        }

        private void CreateAllNotes(string file)
        {
            foreach(Note n in NoteContainer.Song)
            {
                File.AppendAllText(file, n.ToHTML());
            }
        }
        private void CreateFooter(string file)
        {
            File.AppendAllText(file,
                "<script>\n" +
                "</script>\n" +
                "</body>\n" +
                "</html>\n");
        }
        #endregion

        #region CSS
        private void CreateCSS(string file)
        {
            File.AppendAllText(file, "/* This code was automatically generated. Any change may result in errors */\n");

            //Header
            File.AppendAllText(file,
                ".TabHeader{\n" +
                "width: 100%;\n" +
                "height: " + SetValues.SizeHeader + "px;\n" +
                "}\n");

            //ChordLines
            File.AppendAllText(file,
                ".GuitarString{\n" +
                "border-color: lightgray;\n" +
                "margin-bottom: " + (SetValues.SizeChords / 6) + "px;\n" +
                "}\n");

            //Sheet
            File.AppendAllText(file,
               ".Sheet{\n" +
               "position: relative;\n" +
               "height: " + SetValues.SizeChords + "px;\n" +
               "min-width: 100%;\n" +
               "width: " + SetValues.GetMaxWidth() + "px;\n" +
               "}\n" );
        }
        #endregion
    }
}
