
    partial class AngleAlgo
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
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.txtSolutionNumber = new System.Windows.Forms.TextBox();
            this.chkInStock = new System.Windows.Forms.CheckBox();
            this.rtbAnswer = new System.Windows.Forms.RichTextBox();
            this.txtHeight = new System.Windows.Forms.TextBox();
            this.btFind = new System.Windows.Forms.Button();
            this.lstBigAngle = new System.Windows.Forms.ListBox();
            this.btSelectAngle = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(354, 268);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 16;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(218, 268);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 15;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(67, 268);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 14;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtSolutionNumber
            // 
            this.txtSolutionNumber.Location = new System.Drawing.Point(67, 210);
            this.txtSolutionNumber.Name = "txtSolutionNumber";
            this.txtSolutionNumber.Size = new System.Drawing.Size(100, 20);
            this.txtSolutionNumber.TabIndex = 13;
            // 
            // chkInStock
            // 
            this.chkInStock.AutoSize = true;
            this.chkInStock.Location = new System.Drawing.Point(336, 12);
            this.chkInStock.Name = "chkInStock";
            this.chkInStock.Size = new System.Drawing.Size(193, 17);
            this.chkInStock.TabIndex = 12;
            this.chkInStock.Text = "Не учитывать наличие на складе";
            this.chkInStock.UseVisualStyleBackColor = true;
            // 
            // rtbAnswer
            // 
            this.rtbAnswer.Location = new System.Drawing.Point(58, 342);
            this.rtbAnswer.Name = "rtbAnswer";
            this.rtbAnswer.Size = new System.Drawing.Size(413, 176);
            this.rtbAnswer.TabIndex = 11;
            this.rtbAnswer.Text = "";
            // 
            // txtHeight
            // 
            this.txtHeight.Location = new System.Drawing.Point(380, 45);
            this.txtHeight.Name = "txtHeight";
            this.txtHeight.Size = new System.Drawing.Size(100, 20);
            this.txtHeight.TabIndex = 10;
            // 
            // btFind
            // 
            this.btFind.Location = new System.Drawing.Point(315, 85);
            this.btFind.Name = "btFind";
            this.btFind.Size = new System.Drawing.Size(214, 23);
            this.btFind.TabIndex = 9;
            this.btFind.Text = "Найти возможные элементы";
            this.btFind.UseVisualStyleBackColor = true;
            this.btFind.Click += new System.EventHandler(this.btFind_Click);
            // 
            // lstBigAngle
            // 
            this.lstBigAngle.FormattingEnabled = true;
            this.lstBigAngle.Location = new System.Drawing.Point(12, 12);
            this.lstBigAngle.Name = "lstBigAngle";
            this.lstBigAngle.Size = new System.Drawing.Size(297, 186);
            this.lstBigAngle.TabIndex = 17;
            // 
            // btSelectAngle
            // 
            this.btSelectAngle.Location = new System.Drawing.Point(316, 134);
            this.btSelectAngle.Name = "btSelectAngle";
            this.btSelectAngle.Size = new System.Drawing.Size(213, 23);
            this.btSelectAngle.TabIndex = 18;
            this.btSelectAngle.Text = "Подобрать угол";
            this.btSelectAngle.UseVisualStyleBackColor = true;
            this.btSelectAngle.Click += new System.EventHandler(this.button4_Click);
            // 
            // AngleAlgo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(541, 530);
            this.Controls.Add(this.btSelectAngle);
            this.Controls.Add(this.lstBigAngle);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtSolutionNumber);
            this.Controls.Add(this.chkInStock);
            this.Controls.Add(this.rtbAnswer);
            this.Controls.Add(this.txtHeight);
            this.Controls.Add(this.btFind);
            this.Name = "AngleAlgo";
            this.Text = "AngleAlgo";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtSolutionNumber;
        private System.Windows.Forms.CheckBox chkInStock;
        private System.Windows.Forms.RichTextBox rtbAnswer;
        private System.Windows.Forms.TextBox txtHeight;
        private System.Windows.Forms.Button btFind;
        private System.Windows.Forms.ListBox lstBigAngle;
        private System.Windows.Forms.Button btSelectAngle;
    }
