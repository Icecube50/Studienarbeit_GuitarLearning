using GuitarLearning_TabulatorGenerator.CSS_Constants;
using GuitarLearning_TabulatorGenerator.HTML_Serializing;
using GuitarLearning_TabulatorGenerator.MusicalNotes;
using GuitarLearning_TabulatorGenerator.Storage;
using GuitarLearning_TabulatorGenerator.Storage.GuitarStrings;
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
            cbString.Items.Add(GuitarStringType.E);
            cbString.Items.Add(GuitarStringType.A);
            cbString.Items.Add(GuitarStringType.D);
            cbString.Items.Add(GuitarStringType.G);
            cbString.Items.Add(GuitarStringType.B);
            cbString.Items.Add(GuitarStringType.e);
            cbString.SelectedItem = GuitarStringType.E;

            cbSingleLength.Items.Clear();
            cbSingleLength.Items.Add(NoteTypes.Quarter);
            cbSingleLength.SelectedItem = NoteTypes.Quarter;
        }

        private int IdCounter = 0;
        private void btnAddSingle_Click(object sender, EventArgs e)
        {
            GuitarStringType stringType = (GuitarStringType)cbString.SelectedItem;
            uint track = Convert.ToUInt32(txtBund.Text);
            NoteTypes noteType = (NoteTypes)cbSingleLength.SelectedItem;

            if (noteType == NoteTypes.Quarter) MusicalStorage.AddNote(new MusicalNote_Quarter(stringType, track, IdCounter.ToString()));

            IdCounter++;
            txtID.Text = IdCounter.ToString();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                SongOptions.SongTitle = txtSongName.Text;
                SongOptions.SetBPM(Convert.ToUInt32(txtBPM.Text));

                StyleOptions.SizeOfQuarter = Convert.ToUInt32(txtDistanceBeat.Text);
                StyleOptions.HeaderLength = Convert.ToUInt32(txtHeaderLength.Text);
                StyleOptions.ContentLength = Convert.ToUInt32(txtChordLength.Text);

                btnAddSingle.Enabled = true;
                btnGenerate.Enabled = true;
            }
            catch
            {
                MessageBox.Show("Please enter valid numbers");
            }
        }

        private string PathToHTML { get; set; } = string.Empty;
        private string PathToCss { get; set; } = string.Empty;
        private void btnGenerate_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sdlg = new SaveFileDialog())
            {
                var result = sdlg.ShowDialog();
                if (result == DialogResult.OK)
                {
                    //HTML file
                    string file = sdlg.FileName;
                    if (File.Exists(file)) File.Delete(file);

                    //CSS file
                    string fileCSS = Path.GetFileNameWithoutExtension(file) + ".css";
                    string cssPath = Path.Combine(Path.GetDirectoryName(file), fileCSS);
                    if (File.Exists(cssPath)) File.Delete(cssPath);

                    PathToHTML = file;
                    PathToCss = cssPath;

                    WriteToHTML();
                    WriteToCSS();
                }
            }
        }

        private void WriteToHTML()
        {
            HTML_Document htmlDocument = new HTML_Document(SongOptions.SongTitle, Path.GetFileName(PathToCss));

            //Header
            HTML_Div divHeader = new HTML_Div(CSS_Header.ClassName, string.Empty);
            HTML_P pSongTitel = new HTML_P(SongOptions.SongTitle, CSS_SongTitle.ClassName, string.Empty);
            HTML_P pBPM = new HTML_P(SongOptions.BPM, CSS_BPM.ClassName, string.Empty);
            divHeader.AddContent(pSongTitel);
            divHeader.AddContent(pBPM);
            htmlDocument.AddContent(divHeader);

            //Tabulator
            HTML_Div divTabulator = new HTML_Div(CSS_Tabulator.ClassName, "AnimatedDiv");
            HTML_Div divStringE = new HTML_Div(CSS_StringE.ClassName, string.Empty);
            HTML_Div divStringA = new HTML_Div(CSS_StringA.ClassName, string.Empty);
            HTML_Div divStringD = new HTML_Div(CSS_StringD.ClassName, string.Empty);
            HTML_Div divStringG = new HTML_Div(CSS_StringG.ClassName, string.Empty);
            HTML_Div divStringB = new HTML_Div(CSS_StringB.ClassName, string.Empty);
            HTML_Div div_StringHighE = new HTML_Div(CSS_StringHighE.ClassName, string.Empty);
            divTabulator.AddContent(divStringE);
            divTabulator.AddContent(divStringA);
            divTabulator.AddContent(divStringD);
            divTabulator.AddContent(divStringG);
            divTabulator.AddContent(divStringB);
            divTabulator.AddContent(div_StringHighE);


            //Musical Notes
            foreach(MusicalNote musicalNote in MusicalStorage.Melodie)
            {
                divTabulator.AddContent(musicalNote.ToHTML());
            }
            htmlDocument.AddContent(divTabulator);

            //Writing
            File.WriteAllText(PathToHTML, htmlDocument.Serialize());
        }

        private void WriteToCSS()
        {
            File.WriteAllText(PathToCss, CSS_Storage.SerializeCss());
        }
    }
}
