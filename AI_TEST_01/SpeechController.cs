using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Microsoft.Speech;
//using Microsoft.Speech.Text;
//using Microsoft.Speech.Synthesis;
//using Microsoft.Speech.Recognition;
using System.Speech;
using System.Speech.Recognition;
using System.Windows.Forms;

namespace AI_TEST_01
{ 
    class SpeechController
    {
        private SpeechRecognitionEngine sre;        
        private Action<string> callback;
        public int count = 0;

        public SpeechController()
        {
            sre = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("en-US"));
        }

        public void Init(Action<string> callback)
        {
            this.callback = callback;
            sre.SetInputToDefaultAudioDevice();

            //Choices colors = new Choices();
            //colors.Add(new string[] { "red", "green", "blue" });

            //GrammarBuilder gb = new GrammarBuilder();
            //gb.Append(colors);

            //Grammar g = new Grammar(gb);
            sre.LoadGrammar(new DictationGrammar());

            sre.SpeechRecognized += sre_SpeechRecognized;
            sre.RecognizeCompleted += sre_RecognizeCompleted;
            StartRecognition();
        }

        private void StartRecognition()
        {
            sre.RecognizeAsync(RecognizeMode.Multiple);
        }

        private void sre_RecognizeCompleted(object sender, RecognizeCompletedEventArgs e)
        {
            StartRecognition();
        }

        private void sre_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            if (callback != null)
                callback(e.Result.Text);
        }

        #region DEBUG
        public string GetAllLanguages()
        {
            string result = "";

            foreach (RecognizerInfo ri in SpeechRecognitionEngine.InstalledRecognizers())
            {
                result += ri.Id + " | " + ri.Name;
            }

            return result;
        }
        #endregion
    }
}
