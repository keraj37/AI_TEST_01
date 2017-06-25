using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AI_TEST_01
{
    public partial class AIForm : Form
    {
        private SpeechController sc;
        private DBController db;
        private ImageController img;
        private AI ai;
        private ConsoleForm console;

        public AIForm()
        {
            InitializeComponent();

            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;

            console = new ConsoleForm();
            console.Show();

            //img = new ImageController(pictureBox);
            //img.LoadImage("1.jpeg");
            sc = new SpeechController();
            sc.Init(OnSpeechRecognized);
            db = new DBController();
            db.onToShowOnConsole += OnToShowOnConsole;
            ai = new AI(db, /*img*/ null, answerTextBox, webBrowser1, checkBoxLearn);
            ai.onToShowOnConsole += OnToShowOnConsole;

            answerTextBox.ReadOnly = true;
            console.textBox.ReadOnly = true;

            inputTextBox.KeyUp += new KeyEventHandler(OnInputTextKeyUp);
            inputTextBox.GotFocus += new EventHandler(OnInputTextGotFocus);
        }

        private void OnToShowOnConsole(string s)
        {
            console.textBox.Text += "\n\n";
            console.textBox.Text += s;
        }

        private void OnInputTextKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ProcessText(inputTextBox.Text);
                inputTextBox.Text = "";
            }
        }

        private void OnInputTextGotFocus(object sender, EventArgs e)
        {
            inputTextBox.Text = "";
        }

        private void OnSpeechRecognized(string s)
        {
            //inputTextBox.Text = s;
            //ProcessText(s);
        }

        private void ProcessText(string s)
        {
            ai.ProcessText(s);
            //db.AddText(s);
        }

        private void AIForm_Load(object sender, EventArgs e)
        {

        }

        private void inputTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void buttonLearn_Click(object sender, EventArgs e)
        {
            ai.LearnTest("jerry");
        }
    }
}
