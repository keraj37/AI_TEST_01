namespace AI_TEST_01
{
    partial class AIForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.answerTextBox = new System.Windows.Forms.TextBox();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.inputTextBox = new System.Windows.Forms.TextBox();
            this.buttonLearn = new System.Windows.Forms.Button();
            this.checkBoxLearn = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // answerTextBox
            // 
            this.answerTextBox.BackColor = System.Drawing.SystemColors.Control;
            this.answerTextBox.Location = new System.Drawing.Point(12, 475);
            this.answerTextBox.Multiline = true;
            this.answerTextBox.Name = "answerTextBox";
            this.answerTextBox.Size = new System.Drawing.Size(948, 56);
            this.answerTextBox.TabIndex = 1;
            // 
            // webBrowser1
            // 
            this.webBrowser1.Location = new System.Drawing.Point(13, 12);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(1029, 457);
            this.webBrowser1.TabIndex = 4;
            // 
            // inputTextBox
            // 
            this.inputTextBox.BackColor = System.Drawing.SystemColors.WindowText;
            this.inputTextBox.ForeColor = System.Drawing.Color.Lime;
            this.inputTextBox.Location = new System.Drawing.Point(13, 537);
            this.inputTextBox.Name = "inputTextBox";
            this.inputTextBox.Size = new System.Drawing.Size(1022, 20);
            this.inputTextBox.TabIndex = 5;
            this.inputTextBox.TextChanged += new System.EventHandler(this.inputTextBox_TextChanged);
            // 
            // buttonLearn
            // 
            this.buttonLearn.Location = new System.Drawing.Point(967, 475);
            this.buttonLearn.Name = "buttonLearn";
            this.buttonLearn.Size = new System.Drawing.Size(75, 29);
            this.buttonLearn.TabIndex = 7;
            this.buttonLearn.Text = "Learn";
            this.buttonLearn.UseVisualStyleBackColor = true;
            this.buttonLearn.Click += new System.EventHandler(this.buttonLearn_Click);
            // 
            // checkBoxLearn
            // 
            this.checkBoxLearn.AutoSize = true;
            this.checkBoxLearn.Location = new System.Drawing.Point(967, 511);
            this.checkBoxLearn.Name = "checkBoxLearn";
            this.checkBoxLearn.Size = new System.Drawing.Size(59, 17);
            this.checkBoxLearn.TabIndex = 8;
            this.checkBoxLearn.Text = "Learn?";
            this.checkBoxLearn.UseVisualStyleBackColor = true;
            // 
            // AIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(1045, 569);
            this.Controls.Add(this.checkBoxLearn);
            this.Controls.Add(this.buttonLearn);
            this.Controls.Add(this.inputTextBox);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.answerTextBox);
            this.Name = "AIForm";
            this.Text = "AI";
            this.Load += new System.EventHandler(this.AIForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox answerTextBox;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.TextBox inputTextBox;
        private System.Windows.Forms.Button buttonLearn;
        private System.Windows.Forms.CheckBox checkBoxLearn;
    }
}

