using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Speech.Synthesis;

namespace AI_TEST_01
{
    public class AI
    {
        private DBController db;
        private ImageController img;
        private TextBox answerTxt;
        private WebBrowser webBrowser;
        private SpeechSynthesizer speech;
        private LearningController learn;
        private CheckBox checkBoxLearn;

        public Action<string> onToShowOnConsole;

        private string SayString
        {
            set
            {
                answerTxt.Text = value;
                speech.SpeakAsync(value);
            }
        }

        public AI(DBController db, ImageController img, TextBox answerTxt, WebBrowser webBrowser, CheckBox checkBoxLearn)
        {
            this.img = img;
            this.db = db;
            this.answerTxt = answerTxt;
            this.webBrowser = webBrowser;
            this.checkBoxLearn = checkBoxLearn;

            learn = new LearningController(this, db, webBrowser);
            learn.onLearnFinished += OnLearnFinished;
            speech = new SpeechSynthesizer();
            speech.SelectVoiceByHints(VoiceGender.Female, VoiceAge.Adult);
        }

        public void LearnTest(string word)
        {
            learn.LearnWord(word);
        }

        private void OnLearnFinished(string s)
        {
            learn.AnalizeText(s);
            SayString = learn.GetAnswerFirstWordAlghoritm(s).ToLower();
        }

        public void ProcessText(string s)
        {
            //if (db.CheckIfExisits(s))
            {
                //SayString = "you said that before!";
            }
            //else
            {
                answerTxt.Text = "";
            }

            if(!checkBoxLearn.Checked)
                OnLearnFinished(s);
            else
                learn.LearnSentance(s);

            /*
            if (s.ToLower().Contains("open"))
            {
                SayString = "As you whish!";
                string url = @"powershell.exe";
                currentProc = Process.Start(url);
            }

            if (s.ToLower().Contains("close"))
            {
                SayString = "As you whish!";

                if (currentProc != null)
                    currentProc.Close();
            }

            if (s.ToLower().Contains("jerry"))
            {
                SayString = "As you whish!";
                db.PrintAllConversations();
            }

            if (s.ToLower().Contains("detect"))
            {
                SayString = "As you whish!";
                img.DetectFaces();
            }
            */
        }
    }
}
