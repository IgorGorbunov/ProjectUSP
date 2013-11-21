
    partial class AlgoForm
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
            this.btFind = new System.Windows.Forms.Button();
            this.txtHeight = new System.Windows.Forms.TextBox();
            this.grbElementType = new System.Windows.Forms.GroupBox();
            this.rtbAnswer = new System.Windows.Forms.RichTextBox();
            this.chkInStock = new System.Windows.Forms.CheckBox();
            this.txtSolutionNumber = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btFind
            // 
            this.btFind.Location = new System.Drawing.Point(244, 137);
            this.btFind.Name = "btFind";
            this.btFind.Size = new System.Drawing.Size(75, 23);
            this.btFind.TabIndex = 0;
            this.btFind.Text = "Подобрать";
            this.btFind.UseVisualStyleBackColor = true;
            this.btFind.Click += new System.EventHandler(this.btFind_Click);
            // 
            // txtHeight
            // 
            this.txtHeight.Location = new System.Drawing.Point(244, 12);
            this.txtHeight.Name = "txtHeight";
            this.txtHeight.Size = new System.Drawing.Size(100, 20);
            this.txtHeight.TabIndex = 1;
            // 
            // grbElementType
            // 
            this.grbElementType.Location = new System.Drawing.Point(12, 12);
            this.grbElementType.Name = "grbElementType";
            this.grbElementType.Size = new System.Drawing.Size(200, 148);
            this.grbElementType.TabIndex = 2;
            this.grbElementType.TabStop = false;
            this.grbElementType.Text = "Выбор типа подбора";
            // 
            // rtbAnswer
            // 
            this.rtbAnswer.Location = new System.Drawing.Point(12, 341);
            this.rtbAnswer.Name = "rtbAnswer";
            this.rtbAnswer.Size = new System.Drawing.Size(413, 176);
            this.rtbAnswer.TabIndex = 3;
            this.rtbAnswer.Text = "";
            // 
            // chkInStock
            // 
            this.chkInStock.AutoSize = true;
            this.chkInStock.Location = new System.Drawing.Point(244, 65);
            this.chkInStock.Name = "chkInStock";
            this.chkInStock.Size = new System.Drawing.Size(193, 17);
            this.chkInStock.TabIndex = 4;
            this.chkInStock.Text = "Не учитывать наличие на складе";
            this.chkInStock.UseVisualStyleBackColor = true;
            // 
            // txtSolutionNumber
            // 
            this.txtSolutionNumber.Location = new System.Drawing.Point(21, 209);
            this.txtSolutionNumber.Name = "txtSolutionNumber";
            this.txtSolutionNumber.Size = new System.Drawing.Size(100, 20);
            this.txtSolutionNumber.TabIndex = 5;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(21, 267);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(172, 267);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 7;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(308, 267);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 8;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // AlgoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(437, 529);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtSolutionNumber);
            this.Controls.Add(this.chkInStock);
            this.Controls.Add(this.rtbAnswer);
            this.Controls.Add(this.grbElementType);
            this.Controls.Add(this.txtHeight);
            this.Controls.Add(this.btFind);
            this.Name = "AlgoForm";
            this.Text = "AlgoForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btFind;
        private System.Windows.Forms.TextBox txtHeight;
        private System.Windows.Forms.GroupBox grbElementType;
        private System.Windows.Forms.RichTextBox rtbAnswer;
        private System.Windows.Forms.CheckBox chkInStock;
        private System.Windows.Forms.TextBox txtSolutionNumber;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
    }