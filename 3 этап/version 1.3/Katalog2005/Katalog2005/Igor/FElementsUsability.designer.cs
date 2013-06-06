
    partial class FElementsUsability
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
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.tLPBottom = new System.Windows.Forms.TableLayoutPanel();
            this.bCancel = new System.Windows.Forms.Button();
            this.bAlternative = new System.Windows.Forms.Button();
            this.bCurrent = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lFreeElems = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lAllElems = new System.Windows.Forms.Label();
            this.lStats = new System.Windows.Forms.Label();
            this.bStats = new System.Windows.Forms.Button();
            this.lAlternative = new System.Windows.Forms.Label();
            this.lWarning = new System.Windows.Forms.Label();
            this.pnlBottom.SuspendLayout();
            this.tLPBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlBottom
            // 
            this.pnlBottom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlBottom.Controls.Add(this.tLPBottom);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 265);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(432, 47);
            this.pnlBottom.TabIndex = 0;
            // 
            // tLPBottom
            // 
            this.tLPBottom.ColumnCount = 3;
            this.tLPBottom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tLPBottom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tLPBottom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tLPBottom.Controls.Add(this.bCancel, 2, 0);
            this.tLPBottom.Controls.Add(this.bAlternative, 1, 0);
            this.tLPBottom.Controls.Add(this.bCurrent, 0, 0);
            this.tLPBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tLPBottom.Location = new System.Drawing.Point(0, 0);
            this.tLPBottom.Name = "tLPBottom";
            this.tLPBottom.RowCount = 1;
            this.tLPBottom.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tLPBottom.Size = new System.Drawing.Size(430, 45);
            this.tLPBottom.TabIndex = 0;
            // 
            // bCancel
            // 
            this.bCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bCancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bCancel.Location = new System.Drawing.Point(300, 10);
            this.bCancel.Margin = new System.Windows.Forms.Padding(14, 10, 14, 10);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(116, 25);
            this.bCancel.TabIndex = 0;
            this.bCancel.Text = "Отмена";
            this.bCancel.UseVisualStyleBackColor = true;
            this.bCancel.Click += new System.EventHandler(this.bttbCancel_Click);
            // 
            // bAlternative
            // 
            this.bAlternative.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bAlternative.Location = new System.Drawing.Point(157, 10);
            this.bAlternative.Margin = new System.Windows.Forms.Padding(14, 10, 14, 10);
            this.bAlternative.Name = "bAlternative";
            this.bAlternative.Size = new System.Drawing.Size(115, 25);
            this.bAlternative.TabIndex = 1;
            this.bAlternative.Text = "Альтернатива";
            this.bAlternative.UseVisualStyleBackColor = true;
            this.bAlternative.Click += new System.EventHandler(this.bttnAlternative_Click);
            // 
            // bCurrent
            // 
            this.bCurrent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bCurrent.Location = new System.Drawing.Point(14, 10);
            this.bCurrent.Margin = new System.Windows.Forms.Padding(14, 10, 14, 10);
            this.bCurrent.Name = "bCurrent";
            this.bCurrent.Size = new System.Drawing.Size(115, 25);
            this.bCurrent.TabIndex = 2;
            this.bCurrent.Tag = "";
            this.bCurrent.Text = "Текущий";
            this.bCurrent.UseVisualStyleBackColor = true;
            this.bCurrent.Click += new System.EventHandler(this.bttnCurrent_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(24, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(295, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Количество свободных деталей на складе - ";
            // 
            // lFreeElems
            // 
            this.lFreeElems.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lFreeElems.ForeColor = System.Drawing.Color.Red;
            this.lFreeElems.Location = new System.Drawing.Point(312, 25);
            this.lFreeElems.Name = "lFreeElems";
            this.lFreeElems.Size = new System.Drawing.Size(52, 16);
            this.lFreeElems.TabIndex = 2;
            this.lFreeElems.Text = "1";
            this.lFreeElems.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(360, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(24, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "из";
            // 
            // lAllElems
            // 
            this.lAllElems.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lAllElems.ForeColor = System.Drawing.Color.Blue;
            this.lAllElems.Location = new System.Drawing.Point(379, 25);
            this.lAllElems.Name = "lAllElems";
            this.lAllElems.Size = new System.Drawing.Size(52, 16);
            this.lAllElems.TabIndex = 4;
            this.lAllElems.Text = "10";
            this.lAllElems.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lStats
            // 
            this.lStats.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lStats.Location = new System.Drawing.Point(24, 113);
            this.lStats.Name = "lStats";
            this.lStats.Size = new System.Drawing.Size(393, 51);
            this.lStats.TabIndex = 5;
            this.lStats.Text = "Для просмотра более подробной информации Вы можете просмотреть статистику изсполь" +
                "зуемости данного элемента в сборках.\r\n";
            // 
            // bStats
            // 
            this.bStats.Location = new System.Drawing.Point(158, 167);
            this.bStats.Name = "bStats";
            this.bStats.Size = new System.Drawing.Size(115, 28);
            this.bStats.TabIndex = 6;
            this.bStats.Text = "Статистика";
            this.bStats.UseVisualStyleBackColor = true;
            this.bStats.Click += new System.EventHandler(this.bStats_Click);
            // 
            // lAlternative
            // 
            this.lAlternative.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lAlternative.Location = new System.Drawing.Point(24, 209);
            this.lAlternative.Name = "lAlternative";
            this.lAlternative.Size = new System.Drawing.Size(393, 41);
            this.lAlternative.TabIndex = 7;
            this.lAlternative.Text = "Вы можете выбрать альтернативный вариант элемента или же использовать текущий эле" +
                "мент.";
            // 
            // lWarning
            // 
            this.lWarning.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lWarning.ForeColor = System.Drawing.Color.Red;
            this.lWarning.Location = new System.Drawing.Point(24, 50);
            this.lWarning.Name = "lWarning";
            this.lWarning.Size = new System.Drawing.Size(393, 53);
            this.lWarning.TabIndex = 8;
            this.lWarning.Text = "Использование данного элемента может привести к невозможности создании какой-либо" +
                " сборки УСПО в будущем!";
            this.lWarning.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FElementsUsability
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.CancelButton = this.bCancel;
            this.ClientSize = new System.Drawing.Size(432, 312);
            this.Controls.Add(this.lWarning);
            this.Controls.Add(this.lAlternative);
            this.Controls.Add(this.bStats);
            this.Controls.Add(this.lStats);
            this.Controls.Add(this.lAllElems);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lFreeElems);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pnlBottom);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(398, 32);
            this.Name = "FElementsUsability";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Используемость элемента в сборках";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FElementsUsability_FormClosed);
            this.pnlBottom.ResumeLayout(false);
            this.tLPBottom.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.TableLayoutPanel tLPBottom;
        private System.Windows.Forms.Button bCancel;
        private System.Windows.Forms.Button bAlternative;
        private System.Windows.Forms.Button bCurrent;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lFreeElems;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lAllElems;
        private System.Windows.Forms.Label lStats;
        private System.Windows.Forms.Button bStats;
        private System.Windows.Forms.Label lAlternative;
        private System.Windows.Forms.Label lWarning;
    }