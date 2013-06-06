    partial class StatsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StatsForm));
            this.dGVOrders = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.lblGOST = new System.Windows.Forms.Label();
            this.lblWeight = new System.Windows.Forms.Label();
            this.lblGroup = new System.Windows.Forms.Label();
            this.lblKatalog = new System.Windows.Forms.Label();
            this.lblAllN = new System.Windows.Forms.Label();
            this.lblBusy = new System.Windows.Forms.Label();
            this.lblFree = new System.Windows.Forms.Label();
            this.pBElement = new System.Windows.Forms.PictureBox();
            this.label11 = new System.Windows.Forms.Label();
            this.lInProject = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dGVOrders)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pBElement)).BeginInit();
            this.SuspendLayout();
            // 
            // dGVOrders
            // 
            this.dGVOrders.AllowUserToAddRows = false;
            this.dGVOrders.AllowUserToDeleteRows = false;
            this.dGVOrders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dGVOrders.Location = new System.Drawing.Point(12, 182);
            this.dGVOrders.Name = "dGVOrders";
            this.dGVOrders.ReadOnly = true;
            this.dGVOrders.RowHeadersVisible = false;
            this.dGVOrders.Size = new System.Drawing.Size(764, 153);
            this.dGVOrders.TabIndex = 0;
            this.dGVOrders.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dGVOrders_CellDoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Обозначение";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(35, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Наименование";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(36, 115);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "ГОСТ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(36, 140);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Масса";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(304, 14);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Группа";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(304, 37);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Каталог";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(304, 63);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(127, 13);
            this.label7.TabIndex = 7;
            this.label7.Text = "Количество элементов:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(329, 83);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(42, 13);
            this.label8.TabIndex = 8;
            this.label8.Text = "- всего";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(329, 106);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(57, 13);
            this.label9.TabIndex = 9;
            this.label9.Text = "- в работе";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(329, 129);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(63, 13);
            this.label10.TabIndex = 10;
            this.label10.Text = "- в проекте";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(134, 14);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(41, 13);
            this.lblTitle.TabIndex = 11;
            this.lblTitle.Text = "label11";
            // 
            // lblName
            // 
            this.lblName.Location = new System.Drawing.Point(134, 37);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(164, 78);
            this.lblName.TabIndex = 12;
            this.lblName.Text = "label11";
            // 
            // lblGOST
            // 
            this.lblGOST.AutoSize = true;
            this.lblGOST.Location = new System.Drawing.Point(135, 115);
            this.lblGOST.Name = "lblGOST";
            this.lblGOST.Size = new System.Drawing.Size(41, 13);
            this.lblGOST.TabIndex = 13;
            this.lblGOST.Text = "label11";
            // 
            // lblWeight
            // 
            this.lblWeight.AutoSize = true;
            this.lblWeight.Location = new System.Drawing.Point(135, 140);
            this.lblWeight.Name = "lblWeight";
            this.lblWeight.Size = new System.Drawing.Size(41, 13);
            this.lblWeight.TabIndex = 14;
            this.lblWeight.Text = "label11";
            // 
            // lblGroup
            // 
            this.lblGroup.AutoSize = true;
            this.lblGroup.Location = new System.Drawing.Point(362, 14);
            this.lblGroup.Name = "lblGroup";
            this.lblGroup.Size = new System.Drawing.Size(41, 13);
            this.lblGroup.TabIndex = 15;
            this.lblGroup.Text = "label11";
            // 
            // lblKatalog
            // 
            this.lblKatalog.AutoSize = true;
            this.lblKatalog.Location = new System.Drawing.Point(362, 37);
            this.lblKatalog.Name = "lblKatalog";
            this.lblKatalog.Size = new System.Drawing.Size(41, 13);
            this.lblKatalog.TabIndex = 16;
            this.lblKatalog.Text = "label11";
            // 
            // lblAllN
            // 
            this.lblAllN.AutoSize = true;
            this.lblAllN.Location = new System.Drawing.Point(399, 83);
            this.lblAllN.Name = "lblAllN";
            this.lblAllN.Size = new System.Drawing.Size(41, 13);
            this.lblAllN.TabIndex = 17;
            this.lblAllN.Text = "label11";
            // 
            // lblBusy
            // 
            this.lblBusy.AutoSize = true;
            this.lblBusy.Location = new System.Drawing.Point(399, 106);
            this.lblBusy.Name = "lblBusy";
            this.lblBusy.Size = new System.Drawing.Size(41, 13);
            this.lblBusy.TabIndex = 18;
            this.lblBusy.Text = "label11";
            // 
            // lblFree
            // 
            this.lblFree.AutoSize = true;
            this.lblFree.Location = new System.Drawing.Point(399, 152);
            this.lblFree.Name = "lblFree";
            this.lblFree.Size = new System.Drawing.Size(41, 13);
            this.lblFree.TabIndex = 19;
            this.lblFree.Text = "label11";
            // 
            // pBElement
            // 
            this.pBElement.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pBElement.Location = new System.Drawing.Point(472, 12);
            this.pBElement.Name = "pBElement";
            this.pBElement.Size = new System.Drawing.Size(304, 149);
            this.pBElement.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pBElement.TabIndex = 20;
            this.pBElement.TabStop = false;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(329, 152);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(68, 13);
            this.label11.TabIndex = 21;
            this.label11.Text = "- свободных";
            // 
            // lInProject
            // 
            this.lInProject.AutoSize = true;
            this.lInProject.Location = new System.Drawing.Point(399, 129);
            this.lInProject.Name = "lInProject";
            this.lInProject.Size = new System.Drawing.Size(41, 13);
            this.lInProject.TabIndex = 22;
            this.lInProject.Text = "label12";
            // 
            // StatsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(788, 347);
            this.Controls.Add(this.lInProject);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.pBElement);
            this.Controls.Add(this.lblFree);
            this.Controls.Add(this.lblBusy);
            this.Controls.Add(this.lblAllN);
            this.Controls.Add(this.lblKatalog);
            this.Controls.Add(this.lblGroup);
            this.Controls.Add(this.lblWeight);
            this.Controls.Add(this.lblGOST);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dGVOrders);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "StatsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Информация об элементе";
            ((System.ComponentModel.ISupportInitialize)(this.dGVOrders)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pBElement)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dGVOrders;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblGOST;
        private System.Windows.Forms.Label lblWeight;
        private System.Windows.Forms.Label lblGroup;
        private System.Windows.Forms.Label lblKatalog;
        private System.Windows.Forms.Label lblAllN;
        private System.Windows.Forms.Label lblBusy;
        private System.Windows.Forms.Label lblFree;
        private System.Windows.Forms.PictureBox pBElement;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lInProject;
    }