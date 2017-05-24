using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        string _strFileText = string.Empty;
        PdfReader _pdfReader = null;
        SpeechSynthesizer _speechReader = null;

        public Form1()
        {
            InitializeComponent();
            btnPlay.Enabled = btnPause.Enabled = btnResume.Enabled = btnStop.Enabled = false;
        }

        private void btnUploadFile_Click(object sender, EventArgs e)
        {
            //Create instance of file dialog to select file.
            OpenFileDialog _fileDialog = new OpenFileDialog();
            string _strFilePath = string.Empty;
            //Filters of the files to be selected.
            _fileDialog.Filter = "PDF Files(*.pdf)|*.pdf|All Files(*.*)|*.*";

            if (_fileDialog.ShowDialog().Equals(DialogResult.OK))//if file is selected
            {
                _strFilePath = _fileDialog.FileName; //Get the file name or complete path

                if (!string.IsNullOrEmpty(_strFilePath))
                {
                    try
                    {
                        lblFileName.Text = $"Reading file '{_strFilePath}'";
                        using (_pdfReader = new PdfReader(_strFilePath))//open pdfreader
                        {
                            for (int _pageNumber = 1; _pageNumber <= _pdfReader.NumberOfPages; _pageNumber++)
                            {
                                ITextExtractionStrategy _strategy = new iTextSharp.text.pdf.parser.LocationTextExtractionStrategy();

                                //Read text
                                string _pageText = PdfTextExtractor.GetTextFromPage(_pdfReader, _pageNumber, _strategy);
                                _pageText = Encoding.UTF8.GetString(Encoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(_pageText)));
                                _strFileText += _pageText;
                            }

                            //once reading is done close the reader.
                            _pdfReader.Close();
                        }

                        //If the text reading is successful enable play button
                        if (!string.IsNullOrEmpty(_strFileText))
                            btnPlay.Enabled = true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            try
            {
                btnPause.Enabled = btnStop.Enabled = true;
                btnResume.Enabled = btnPlay.Enabled = false;

                _speechReader = new SpeechSynthesizer();
                _speechReader.SelectVoiceByHints(VoiceGender.Female, VoiceAge.Teen);
                _speechReader.SpeakAsync(_strFileText);
                _speechReader.SpeakCompleted += SpeechReader_SpeakCompleted;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void SpeechReader_SpeakCompleted(object sender, SpeakCompletedEventArgs e)
        {
            btnStop.Enabled = btnPause.Enabled = btnResume.Enabled = false;
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            if (_speechReader != null)
            {
                if (_speechReader.State.Equals(SynthesizerState.Speaking))
                {
                    _speechReader.Pause();
                    btnPause.Enabled = false;
                    btnResume.Enabled = true;
                }
            }
        }

        private void btnResume_Click(object sender, EventArgs e)
        {
            if (_speechReader != null)
            {
                if (_speechReader.State.Equals(SynthesizerState.Paused))
                {
                    _speechReader.Resume();
                    btnResume.Enabled = false;
                    btnPause.Enabled = true;
                }
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (_speechReader != null)
            {
                if (_speechReader.State.Equals(SynthesizerState.Speaking) || _speechReader.State.Equals(SynthesizerState.Paused))
                {
                    _speechReader.Dispose();
                    btnPause.Enabled = btnResume.Enabled = btnStop.Enabled = false;
                    btnPlay.Enabled = true;
                }
            }
        }
    }
}
