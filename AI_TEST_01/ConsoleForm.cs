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
    public partial class ConsoleForm : Form
    {
        public ConsoleForm()
        {
            InitializeComponent();

            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox.Text = "";
        }
    }
}
