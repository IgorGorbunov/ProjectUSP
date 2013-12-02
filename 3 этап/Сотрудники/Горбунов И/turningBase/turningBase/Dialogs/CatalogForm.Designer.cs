namespace ProjectUsp.Dialogs
{
    partial class CatalogForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CatalogForm));
            this.label1 = new System.Windows.Forms.Label();
            this.cat8Btn = new System.Windows.Forms.Button();
            this.cat12Btn = new System.Windows.Forms.Button();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(-1, 9);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(5);
            this.label1.Size = new System.Drawing.Size(228, 56);
            this.label1.TabIndex = 0;
            this.label1.Text = "Выберите серию деталей УСП, с которой Вы хотите работать.";
            // 
            // cat8Btn
            // 
            this.cat8Btn.Location = new System.Drawing.Point(12, 54);
            this.cat8Btn.Name = "cat8Btn";
            this.cat8Btn.Size = new System.Drawing.Size(75, 39);
            this.cat8Btn.TabIndex = 1;
            this.cat8Btn.Text = "8 паз\r\n2 серия";
            this.cat8Btn.UseVisualStyleBackColor = true;
            // 
            // cat12Btn
            // 
            this.cat12Btn.Location = new System.Drawing.Point(93, 54);
            this.cat12Btn.Name = "cat12Btn";
            this.cat12Btn.Size = new System.Drawing.Size(75, 39);
            this.cat12Btn.TabIndex = 2;
            this.cat12Btn.Text = "12 паз\r\n3 серия\r\n";
            this.cat12Btn.UseVisualStyleBackColor = true;
            // 
            // cancelBtn
            // 
            this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelBtn.Location = new System.Drawing.Point(93, 108);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(75, 23);
            this.cancelBtn.TabIndex = 3;
            this.cancelBtn.Text = "Отмена";
            this.cancelBtn.UseVisualStyleBackColor = true;
            // 
            // CatalogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.CancelButton = this.cancelBtn;
            this.ClientSize = new System.Drawing.Size(179, 142);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.cat12Btn);
            this.Controls.Add(this.cat8Btn);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CatalogForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Выбор каталога";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cat8Btn;
        private System.Windows.Forms.Button cat12Btn;
        private System.Windows.Forms.Button cancelBtn;
    }
}