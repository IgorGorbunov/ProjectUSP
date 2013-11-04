
    partial class ExactHeightForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExactHeightForm));
            this.lessBtn = new System.Windows.Forms.Button();
            this.moreBtn = new System.Windows.Forms.Button();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.label = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lessBtn
            // 
            this.lessBtn.Location = new System.Drawing.Point(32, 71);
            this.lessBtn.Name = "lessBtn";
            this.lessBtn.Size = new System.Drawing.Size(75, 23);
            this.lessBtn.TabIndex = 0;
            this.lessBtn.Text = "lessBtn";
            this.lessBtn.UseVisualStyleBackColor = true;
            this.lessBtn.Click += new System.EventHandler(this.lessBtn_Click);
            // 
            // moreBtn
            // 
            this.moreBtn.Location = new System.Drawing.Point(150, 71);
            this.moreBtn.Name = "moreBtn";
            this.moreBtn.Size = new System.Drawing.Size(75, 23);
            this.moreBtn.TabIndex = 1;
            this.moreBtn.Text = "moreBtn";
            this.moreBtn.UseVisualStyleBackColor = true;
            this.moreBtn.Click += new System.EventHandler(this.moreBtn_Click);
            // 
            // cancelBtn
            // 
            this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelBtn.Location = new System.Drawing.Point(150, 112);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(75, 23);
            this.cancelBtn.TabIndex = 2;
            this.cancelBtn.Text = "Отмена";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // label
            // 
            this.label.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label.Location = new System.Drawing.Point(32, 18);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(183, 50);
            this.label.TabIndex = 3;
            this.label.Text = "         Для набора высоты 192,3 мм элементов нет. Выберите другое расстояние.";
            // 
            // ExactHeightForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.CancelButton = this.cancelBtn;
            this.ClientSize = new System.Drawing.Size(257, 147);
            this.Controls.Add(this.label);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.moreBtn);
            this.Controls.Add(this.lessBtn);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ExactHeightForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Ошибка - некорректная высота!";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button lessBtn;
        private System.Windows.Forms.Button moreBtn;
        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.Label label;
    }