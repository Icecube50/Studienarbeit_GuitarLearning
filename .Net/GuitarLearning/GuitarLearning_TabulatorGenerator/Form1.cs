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
            cbString.Items.Add(GuitarStringType.e);
            cbString.Items.Add(GuitarStringType.B);
            cbString.Items.Add(GuitarStringType.G);
            cbString.Items.Add(GuitarStringType.D);
            cbString.Items.Add(GuitarStringType.A);
            cbString.Items.Add(GuitarStringType.E);
            cbString.SelectedItem = GuitarStringType.e;

            cbSingleLength.Items.Clear();
            cbSingleLength.Items.Add(NoteTypes.Whole);
            cbSingleLength.Items.Add(NoteTypes.Half);
            cbSingleLength.Items.Add(NoteTypes.PunctedQuarter);
            cbSingleLength.Items.Add(NoteTypes.Quarter);
            cbSingleLength.Items.Add(NoteTypes.Eighth);
            cbSingleLength.SelectedItem = NoteTypes.Quarter;

            cbChordLength.Items.Clear();
            cbChordLength.Items.Add(NoteTypes.Whole);
            cbChordLength.Items.Add(NoteTypes.Half);
            cbChordLength.Items.Add(NoteTypes.PunctedQuarter);
            cbChordLength.Items.Add(NoteTypes.Quarter);
            cbChordLength.Items.Add(NoteTypes.Eighth);
            cbChordLength.SelectedItem = NoteTypes.Quarter;
        }

        private int IdCounter = 0;
        private double stroke = 0;
        private void btnAddSingle_Click(object sender, EventArgs e)
        {
            GuitarStringType stringType = (GuitarStringType)cbString.SelectedItem;
            int track = Convert.ToInt32(txtBund.Text);
            NoteTypes noteType = (NoteTypes)cbSingleLength.SelectedItem;

            if (noteType == NoteTypes.Whole)
            {
                stroke += 4;
                MusicalStorage.AddNote(new MusicalNote_Whole(stringType, track, IdCounter.ToString()));
            }
            else if (noteType == NoteTypes.Half)
            {
                stroke += 2;
                MusicalStorage.AddNote(new MusicalNote_Half(stringType, track, IdCounter.ToString()));
            }
            else if (noteType == NoteTypes.PunctedQuarter)
            {
                stroke += 1.5;
                MusicalStorage.AddNote(new MusicalNote_PunctedQuarter(stringType, track, IdCounter.ToString()));
            }
            else if (noteType == NoteTypes.Quarter)
            {
                stroke++;
                MusicalStorage.AddNote(new MusicalNote_Quarter(stringType, track, IdCounter.ToString()));
            }
            else if(noteType == NoteTypes.Eighth)
            {
                stroke += 0.5;
                MusicalStorage.AddNote(new MusicalNote_Eighth(stringType, track, IdCounter.ToString()));
            }

            if (stroke >= 4)
            {
                stroke = stroke - 4;
                MusicalStorage.AddNote(new MusicalNote_Stroke(IdCounter.ToString(), stroke));
            }

            IdCounter++;
            txtID.Text = IdCounter.ToString();
        }

        private void btnAddChord_Click(object sender, EventArgs e)
        {
            NoteTypes noteTypes = (NoteTypes)cbChordLength.SelectedItem;
            string E = txtChordHighE.Text;
            string D = txtChordG.Text;
            string A = txtChordB.Text;
            string G = txtChordD.Text;
            string B = txtChordA.Text;
            string HighE = txtChordE.Text;
            string ChordName = txtChordName.Text;

            if (noteTypes == NoteTypes.Whole) stroke += 4;
            else if (noteTypes == NoteTypes.Half) stroke += 2;
            else if (noteTypes == NoteTypes.PunctedQuarter) stroke += 1.5;
            else if (noteTypes == NoteTypes.Quarter) stroke += 1;
            else if (noteTypes == NoteTypes.Eighth) stroke += 0.5;


            var ListTupel = new List<Tuple<GuitarStringType, int>>();
            if (E != "") ListTupel.Add(new Tuple<GuitarStringType, int>(GuitarStringType.E, Convert.ToInt32(E)));
            if (A != "") ListTupel.Add(new Tuple<GuitarStringType, int>(GuitarStringType.A, Convert.ToInt32(A)));
            if (D != "") ListTupel.Add(new Tuple<GuitarStringType, int>(GuitarStringType.D, Convert.ToInt32(D)));
            if (G != "") ListTupel.Add(new Tuple<GuitarStringType, int>(GuitarStringType.G, Convert.ToInt32(G)));
            if (B != "") ListTupel.Add(new Tuple<GuitarStringType, int>(GuitarStringType.B, Convert.ToInt32(B)));
            if (HighE != "") ListTupel.Add(new Tuple<GuitarStringType, int>(GuitarStringType.e, Convert.ToInt32(HighE)));
            if (ChordName == "") ChordName = "NoName";

            MusicalStorage.AddNote(new MusicalNote_Chord(ListTupel.ToArray(), IdCounter.ToString(), noteTypes, ChordName));


            if (stroke >= 4)
            {
                stroke = stroke - 4;
                MusicalStorage.AddNote(new MusicalNote_Stroke(IdCounter.ToString(), stroke));
            }

            IdCounter++;
            txtID.Text = IdCounter.ToString();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                SongOptions.SongTitle = txtSongName.Text;
                SongOptions.SetBPM(Convert.ToInt32(txtBPM.Text));

                StyleOptions.SizeOfQuarter = Convert.ToInt32(txtDistanceBeat.Text);
                StyleOptions.HeaderLength = Convert.ToInt32(txtHeaderLength.Text);
                StyleOptions.ContentLength = Convert.ToInt32(txtChordLength.Text);

                MusicalStorage.DumpStorage();

                btnAddSingle.Enabled = true;
                btnGenerate.Enabled = true;
                btnAddChord.Enabled = true;
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
                    PathToHTML = file;

                    //CSS file
                    string fileXML = Path.GetFileNameWithoutExtension(file) + ".xml";
                    fileXML = Path.Combine(Path.GetDirectoryName(file), fileXML);
                    if (File.Exists(fileXML)) File.Delete(fileXML);

                    WriteToHTML();
                    SongOptions.SerializeJson(fileXML);

                    MessageBox.Show("DONE");
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
            HTML_Div divPointer = new HTML_Div(CSS_Pointer.ClassName, string.Empty);
            divHeader.AddContent(pSongTitel);
            divHeader.AddContent(pBPM);
            htmlDocument.AddContent(divHeader);
            htmlDocument.AddContent(divPointer);

            //Tabulator
            HTML_Div divTabulator = new HTML_Div(CSS_Tabulator.ClassName, string.Empty);

            //Tabulator - Info
            HTML_Div divTabulatorInfo = new HTML_Div(CSS_TabulatorInfo.ClassName, string.Empty);
            HTML_RythmDiv divUpperRythm = new HTML_RythmDiv(CSS_UpperRythm.ClassName, string.Empty, "4");
            HTML_Div divRythmSeperator = new HTML_Div(CSS_RythmSeperator.ClassName, string.Empty);
            HTML_RythmDiv divLowerRythm = new HTML_RythmDiv(CSS_LowerRythm.ClassName, string.Empty, "4");
            divTabulatorInfo.AddContent(divUpperRythm);
            divTabulatorInfo.AddContent(divRythmSeperator);
            divTabulatorInfo.AddContent(divLowerRythm);

            HTML_TextDiv divTextE = new HTML_TextDiv(CSS_StringNameE.ClassName, string.Empty, "E");
            HTML_TextDiv divTextA = new HTML_TextDiv(CSS_StringNameA.ClassName, string.Empty, "A");
            HTML_TextDiv divTextD = new HTML_TextDiv(CSS_StringNameD.ClassName, string.Empty, "D");
            HTML_TextDiv divTextG = new HTML_TextDiv(CSS_StringNameG.ClassName, string.Empty, "G");
            HTML_TextDiv divTextB = new HTML_TextDiv(CSS_StringNameB.ClassName, string.Empty, "B");
            HTML_TextDiv divTextHighE = new HTML_TextDiv(CSS_StringNameHighE.ClassName, string.Empty, "e");
            divTabulatorInfo.AddContent(divTextE);
            divTabulatorInfo.AddContent(divTextA);
            divTabulatorInfo.AddContent(divTextD);
            divTabulatorInfo.AddContent(divTextG);
            divTabulatorInfo.AddContent(divTextB);
            divTabulatorInfo.AddContent(divTextHighE);
            divTabulator.AddContent(divTabulatorInfo);


            //Tabulator - Strings
            HTML_Div divTabulatorChords = new HTML_Div(CSS_TabulatorChords.ClassName, StyleOptions.IdOfAnimatedDiv);
            HTML_Div divStringE = new HTML_Div(CSS_StringE.ClassName, string.Empty);
            HTML_Div divStringA = new HTML_Div(CSS_StringA.ClassName, string.Empty);
            HTML_Div divStringD = new HTML_Div(CSS_StringD.ClassName, string.Empty);
            HTML_Div divStringG = new HTML_Div(CSS_StringG.ClassName, string.Empty);
            HTML_Div divStringB = new HTML_Div(CSS_StringB.ClassName, string.Empty);
            HTML_Div div_StringHighE = new HTML_Div(CSS_StringHighE.ClassName, string.Empty);
            divTabulatorChords.AddContent(divStringE);
            divTabulatorChords.AddContent(divStringA);
            divTabulatorChords.AddContent(divStringD);
            divTabulatorChords.AddContent(divStringG);
            divTabulatorChords.AddContent(divStringB);
            divTabulatorChords.AddContent(div_StringHighE);


            //Musical Notes
            foreach(MusicalNote musicalNote in MusicalStorage.Melodie)
            {
                if (musicalNote is MusicalNote_Stroke)
                {
                    divTabulatorChords.AddContent(musicalNote.StrokeSeperatorToHTML());
                }
                else if(musicalNote is MusicalNote_Chord)
                {
                    var chord = ((MusicalNote_Chord)musicalNote).ChordToHTML();
                    foreach(var note in chord)
                    {
                        divTabulatorChords.AddContent(note);
                    }
                }
                else
                {
                    divTabulatorChords.AddContent(musicalNote.ToHTML());
                }
            }
            divTabulator.AddContent(divTabulatorChords);

            htmlDocument.AddContent(divTabulator);

            //Hack: Overlay to cover passing tabs
            HTML_Div divOverlay = new HTML_Div(CSS_TabulatorOverlay.ClassName, string.Empty);
            htmlDocument.AddContent(divOverlay);

            //Writing
            File.WriteAllText(PathToHTML, htmlDocument.Serialize());
        }
    }
}
