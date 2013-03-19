namespace Katalog2005.WinFroms.search
{
    partial class search
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
            this.addButtonCon = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.searchBtnCon = new System.Windows.Forms.Button();
            this.radioAND = new System.Windows.Forms.RadioButton();
            this.radioOR = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // addButtonCon
            // 
            this.addButtonCon.Location = new System.Drawing.Point(496, 0);
            this.addButtonCon.Name = "addButtonCon";
            this.addButtonCon.Size = new System.Drawing.Size(24, 24);
            this.addButtonCon.TabIndex = 0;
            this.addButtonCon.Text = "+";
            this.addButtonCon.UseVisualStyleBackColor = true;
            this.addButtonCon.Click += new System.EventHandler(this.addButtonCon_Click);
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(496, 24);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(24, 24);
            this.button1.TabIndex = 1;
            this.button1.Text = "-";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // searchBtnCon
            // 
            this.searchBtnCon.Location = new System.Drawing.Point(424, 0);
            this.searchBtnCon.Name = "searchBtnCon";
            this.searchBtnCon.Size = new System.Drawing.Size(70, 23);
            this.searchBtnCon.TabIndex = 2;
            this.searchBtnCon.Text = "Найти";
            this.searchBtnCon.UseVisualStyleBackColor = true;
            this.searchBtnCon.Click += new System.EventHandler(this.searchBtnCon_Click);
            // 
            // radioAND
            // 
            this.radioAND.AutoSize = true;
            this.radioAND.Checked = true;
            this.radioAND.Enabled = false;
            this.radioAND.Location = new System.Drawing.Point(424, 24);
            this.radioAND.Name = "radioAND";
            this.radioAND.Size = new System.Drawing.Size(33, 17);
            this.radioAND.TabIndex = 3;
            this.radioAND.TabStop = true;
            this.radioAND.Text = "И";
            this.radioAND.UseVisualStyleBackColor = true;
            // 
            // radioOR
            // 
            this.radioOR.AutoSize = true;
            this.radioOR.Enabled = false;
            this.radioOR.Location = new System.Drawing.Point(454, 24);
            this.radioOR.Name = "radioOR";
            this.radioOR.Size = new System.Drawing.Size(49, 17);
            this.radioOR.TabIndex = 4;
            this.radioOR.Text = "ИЛИ";
            this.radioOR.UseVisualStyleBackColor = true;
            // 
            // search
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(520, 24);
            this.Controls.Add(this.searchBtnCon);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.addButtonCon);
            this.Controls.Add(this.radioOR);
            this.Controls.Add(this.radioAND);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "search";
            this.Text = "Поиск элементов УСП";
            this.Load += new System.EventHandler(this.search_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button addButtonCon;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button searchBtnCon;
        private System.Windows.Forms.RadioButton radioAND;
        private System.Windows.Forms.RadioButton radioOR;
    }
}