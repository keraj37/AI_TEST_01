using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleJSON;
using System.Windows.Forms;

namespace AI_TEST_01
{
    public class LearningController
    {
        private DBController db;
        private string currentWord = "";
        private WebBrowser webBrowser;
        private AI controller;
        private string currentSentance = "";
        private int currentIndex = 0;

        public event Action<string> onLearnFinished;

        public LearningController(AI controller, DBController db, WebBrowser webBrowser)
        {
            this.db = db;
            this.webBrowser = webBrowser;
            this.controller = controller;
        }

        public void LearnWord(string word)
        {
            currentWord = word;
            webBrowser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(OnDocumentLoaded);
            webBrowser.Navigate(string.Format("https://www.google.com/search?q={0}", word));
        }

        private void OnDocumentLoaded(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            webBrowser.Document.ExecCommand("SelectAll", false, null);
            webBrowser.Document.ExecCommand("Copy", false, null);

            string searchResult = Clipboard.GetText();          

            PrepareAndAnalizeTexts(searchResult);
        }

        private void PrepareAndAnalizeTexts(string searchResult)
        {
            string[] ss = searchResult.Split("\n"[0]);

            List<string> cleanTexts = new List<string>();

            foreach (string s in ss)
            {
                if (s.Contains(currentWord + " ") || s.Contains(" " + currentWord))
                {
                    cleanTexts.Add(s);
                }
            }

            foreach (string s in cleanTexts)
            {
                if (controller.onToShowOnConsole != null)
                    controller.onToShowOnConsole(s);

                AnalizeText(s);
            }

            LearnNextWord();
        }

        public string GetAnswerFirstWordAlghoritm(string text)
        {
            text = CleanText(text);
            string[] texts = text.Split(" "[0]);

            foreach (string t in texts)
            {
                KeyValuePair<int, string> kvp = db.GetWordStatus(t);
                if (kvp.Value != "{}")
                {
                    return GetHighestScoreFullText(t);
                }
            }

            return "";
        }

        private string GetHighestScoreFullText(string word)
        {
            KeyValuePair<int, string> status = db.GetWordStatus(word);
            JSONClass obj = JSON.Parse(status.Value).AsObject;

            string result = "";

            Dictionary<int, int> sentance = new Dictionary<int, int>();

            foreach(KeyValuePair<string, JSONNode> kvp in obj)
            {
                int pos = int.Parse(kvp.Key);

                int[] highestScore = new int[2] { -1, -1 };
                foreach (KeyValuePair<string, JSONNode> kvp2 in kvp.Value.AsObject)
                {
                    int id = int.Parse(kvp2.Key);
                    int score = kvp2.Value.AsInt;
                    if (score > highestScore[1])
                    {
                        highestScore = new int[2] { id, score };
                    }
                }

                sentance[pos] = highestScore[0];
            }

            List<KeyValuePair<int, int>> sortedSentance = SortSentance(sentance);

            bool mainWordAdded = false;
            foreach (KeyValuePair<int, int> kvp in sortedSentance)
            {
                if (!mainWordAdded &&  kvp.Key == 1)
                {
                    result += word + " ";
                    mainWordAdded = true;
                }

                result += db.GetWordById(kvp.Value) + " ";

                if (!mainWordAdded && kvp.Key == -1)
                {
                    result += word + " ";
                    mainWordAdded = true;
                }
            }

            return result;
        }

        private List<KeyValuePair<int, int>> SortSentance(Dictionary<int, int> sentance)
        {
            List<KeyValuePair<int, int>> sortedSentance = new List<KeyValuePair<int, int>>();
            foreach (KeyValuePair<int, int> kvp in sentance)
            {
                sortedSentance.Add(kvp);
            }

            sortedSentance.Sort(SortSentanceFunction);

            return sortedSentance;
        }

        private int SortSentanceFunction(KeyValuePair<int, int> a, KeyValuePair<int, int> b)
        {
            if (a.Key > b.Key)
                return 1;
            else if (a.Key < b.Key)
                return -1;

            return 0;
        }

        public void LearnSentance(string text)
        {
            currentSentance = text;
            currentIndex = -1;

            LearnNextWord();
        }

        private void LearnNextWord()
        {
            currentIndex++;
            string text = CleanText(currentSentance);
            string[] ss = text.Split(" "[0]);

            if(currentIndex < ss.Length)
                LearnWord(ss[currentIndex]);
            else
            {
                if (onLearnFinished != null)
                    onLearnFinished(currentSentance);
            }
        }

        public void AnalizeText(string text)
        {
            text = CleanText(text);
            string[] ss = text.Split(" "[0]);

            Dictionary<int, JSONClass> statuses = new Dictionary<int, JSONClass>();
            foreach (string s in ss)
            {
                KeyValuePair<int, string> kvp = db.GetWordStatus(s);
                statuses[kvp.Key] = JSON.Parse(kvp.Value).AsObject;
            }

            foreach (KeyValuePair<int, JSONClass> kvp in statuses)
            {
                int id = kvp.Key;
                JSONClass obj = kvp.Value;

                foreach (KeyValuePair<int, JSONClass> kvp2 in statuses)
                {
                    int id2 = kvp2.Key;
                    int position = GiveWordPosition(statuses, obj, kvp2.Value);

                    if (kvp.Value == kvp2.Value)
                        continue;

                    if (!obj.ContainsKey(position.ToString()))
                        obj.Add(position.ToString(), new JSONClass());

                    JSONClass score = obj[position.ToString()].AsObject;

                    if (!score.ContainsKey(id2.ToString()))
                        score.Add(id2.ToString(), new JSONData(0));

                    int currentScore = score[id2.ToString()].AsInt;
                    currentScore++;
                    score[id2.ToString()] = new JSONData(currentScore);
                }
            }

            foreach (KeyValuePair<int, JSONClass> kvp in statuses)
            {
                int id = kvp.Key;
                JSONClass obj = kvp.Value;

                db.UpdateWord(id, obj.ToString());
            }
        }

        private int GiveWordPosition(Dictionary<int, JSONClass> statuses, JSONClass a, JSONClass b)
        {
            int posa = int.MaxValue;
            int posb = int.MaxValue;

            int i = 0;
            foreach (KeyValuePair<int, JSONClass> kvp in statuses)
            {
                if (kvp.Value == a)
                    posa = i;
                else if (kvp.Value == b)
                    posb = i;

                if (posa != int.MaxValue && posb != int.MaxValue)
                    break;

                i++;
            }

            return posb - posa;
        }

        private string CleanText(string text)
        {
            text = text.Replace(".", "");
            text = text.Replace("?", "");
            text = text.Replace("!", "");
            text = text.Replace(":", "");
            text = text.Replace("'", "");

            return text;
        }
    }
}
