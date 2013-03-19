namespace Katalog2005.WinFroms.AddInformationAboutElements
{
    partial class FindLoadedModels
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
            this.ÔÓÏÓ˘¸ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.searchBox = new System.Windows.Forms.TextBox();
            this.treeOfModel = new System.Windows.Forms.TreeView();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ÔÓÏÓ˘¸ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(525, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // ÔÓÏÓ˘¸ToolStripMenuItem
            // 
            this.ÔÓÏÓ˘¸ToolStripMenuItem.Name = "ÔÓÏÓ˘¸ToolStripMenuItem";
            this.ÔÓÏÓ˘¸ToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.ÔÓÏÓ˘¸ToolStripMenuItem.Text = "œÓÏÓ˘¸";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 283);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(525, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(109, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.searchBox);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(525, 36);
            this.panel1.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(154, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "¬‚Â‰ËÚÂ ÌÓÏÂ ÏÓ‰ÂÎË ”—œ:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(414, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Õ‡ÈÚË";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // searchBox
            // 
            this.searchBox.Location = new System.Drawing.Point(163, 7);
            this.searchBox.MaxLength = 10;
            this.searchBox.Name = "searchBox";
            this.searchBox.Size = new System.Drawing.Size(245, 20);
            this.searchBox.TabIndex = 0;
            this.searchBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchBox_KeyDown);
            // 
            // treeOfModel
            // 
            this.treeOfModel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.treeOfModel.Location = new System.Drawing.Point(0, 62);
            this.treeOfModel.Name = "treeOfModel";
            this.treeOfModel.Size = new System.Drawing.Size(525, 221);
            this.treeOfModel.TabIndex = 5;
            this.treeOfModel.DoubleClick += new System.EventHandler(this.treeOfModel_DoubleClick);
      
            // 
            // FindLoadedModels
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(525, 305);
            this.Controls.Add(this.treeOfModel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximumSize = new System.Drawing.Size(533, 339);
            this.MinimumSize = new System.Drawing.Size(533, 339);
            this.Name = "FindLoadedModels";
            this.Text = "œÓËÒÍ ÒÓÁ‰‡ÌÌÌ˚ı ÏÓ‰ÂÎÂÈ";
            this.Load += new System.EventHandler(this.FindLoadedModels_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ÔÓÏÓ˘¸ToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox searchBox;
        private System.Windows.Forms.TreeView treeOfModel;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;

    }
}