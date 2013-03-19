namespace Katalog2005.WinFroms.search
{
    partial class InformAboutElement
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ïîìîùüToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ñïğàâêàToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DataInformTable = new System.Windows.Forms.DataGridView();
            this.ParamCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ValueCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ıêïîğòToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.îòîáğàçèòüÂExcelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataInformTable)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ıêïîğòToolStripMenuItem,
            this.ïîìîùüToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(602, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.SizeChanged += new System.EventHandler(this.menuStrip1_SizeChanged);
            // 
            // ïîìîùüToolStripMenuItem
            // 
            this.ïîìîùüToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ñïğàâêàToolStripMenuItem});
            this.ïîìîùüToolStripMenuItem.Name = "ïîìîùüToolStripMenuItem";
            this.ïîìîùüToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.ïîìîùüToolStripMenuItem.Text = "Ïîìîùü";
            // 
            // ñïğàâêàToolStripMenuItem
            // 
            this.ñïğàâêàToolStripMenuItem.Name = "ñïğàâêàToolStripMenuItem";
            this.ñïğàâêàToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.ñïğàâêàToolStripMenuItem.Text = "Ñïğàâêà";
            // 
            // DataInformTable
            // 
            this.DataInformTable.AllowUserToAddRows = false;
            this.DataInformTable.AllowUserToDeleteRows = false;
            this.DataInformTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.DataInformTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ParamCol,
            this.ValueCol});
            this.DataInformTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DataInformTable.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.DataInformTable.Location = new System.Drawing.Point(0, 24);
            this.DataInformTable.Name = "DataInformTable";
            this.DataInformTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.DataInformTable.Size = new System.Drawing.Size(602, 424);
            this.DataInformTable.TabIndex = 1;
            this.DataInformTable.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.DataInformTable_RowsAdded);
            this.DataInformTable.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.DataInformTable_EditingControlShowing);
            // 
            // ParamCol
            // 
            this.ParamCol.HeaderText = "Ïàğàìåòğ";
            this.ParamCol.Name = "ParamCol";
            // 
            // ValueCol
            // 
            this.ValueCol.HeaderText = "Çíà÷åíèå";
            this.ValueCol.Name = "ValueCol";
            // 
            // ıêïîğòToolStripMenuItem
            // 
            this.ıêïîğòToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.îòîáğàçèòüÂExcelToolStripMenuItem});
            this.ıêïîğòToolStripMenuItem.Name = "ıêïîğòToolStripMenuItem";
            this.ıêïîğòToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.ıêïîğòToolStripMenuItem.Text = "İêïîğò";
            // 
            // îòîáğàçèòüÂExcelToolStripMenuItem
            // 
            this.îòîáğàçèòüÂExcelToolStripMenuItem.Name = "îòîáğàçèòüÂExcelToolStripMenuItem";
            this.îòîáğàçèòüÂExcelToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.îòîáğàçèòüÂExcelToolStripMenuItem.Text = "Îòîáğàçèòü â Excel";
            this.îòîáğàçèòüÂExcelToolStripMenuItem.Click += new System.EventHandler(this.îòîáğàçèòüÂExcelToolStripMenuItem_Click);
            // 
            // InformAboutElement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(602, 448);
            this.Controls.Add(this.DataInformTable);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "InformAboutElement";
            this.Text = "Èíôîğìàöèÿ î òåêóùåì ıëåìåíòå";
            this.Load += new System.EventHandler(this.InformAboutElement_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataInformTable)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ïîìîùüToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ñïğàâêàToolStripMenuItem;
        private System.Windows.Forms.DataGridView DataInformTable;
        private System.Windows.Forms.DataGridViewTextBoxColumn ParamCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn ValueCol;
        private System.Windows.Forms.ToolStripMenuItem ıêïîğòToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem îòîáğàçèòüÂExcelToolStripMenuItem;
    }
}