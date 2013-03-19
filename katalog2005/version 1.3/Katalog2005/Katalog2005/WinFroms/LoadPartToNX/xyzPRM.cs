using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Katalog2005.WinFroms.LoadPartToNX
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class xyzPRM : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox xCoord;
		private System.Windows.Forms.TextBox yCoord;
		private System.Windows.Forms.TextBox zCoord;
		private System.Windows.Forms.Button setCoord;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public xyzPRM()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(xyzPRM));
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.xCoord = new System.Windows.Forms.TextBox();
			this.yCoord = new System.Windows.Forms.TextBox();
			this.zCoord = new System.Windows.Forms.TextBox();
			this.setCoord = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(0, 0);
			this.label1.Name = "label1";
			this.label1.TabIndex = 0;
			this.label1.Text = "X";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(0, 24);
			this.label2.Name = "label2";
			this.label2.TabIndex = 1;
			this.label2.Text = "Y";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(0, 48);
			this.label3.Name = "label3";
			this.label3.TabIndex = 2;
			this.label3.Text = "Z";
			// 
			// xCoord
			// 
			this.xCoord.Location = new System.Drawing.Point(16, 0);
			this.xCoord.MaxLength = 10;
			this.xCoord.Name = "xCoord";
			this.xCoord.Size = new System.Drawing.Size(112, 20);
			this.xCoord.TabIndex = 3;
			this.xCoord.Text = "";
			this.xCoord.TextChanged += new System.EventHandler(this.xCoord_TextChanged);
			// 
			// yCoord
			// 
			this.yCoord.Location = new System.Drawing.Point(16, 24);
			this.yCoord.MaxLength = 10;
			this.yCoord.Name = "yCoord";
			this.yCoord.Size = new System.Drawing.Size(112, 20);
			this.yCoord.TabIndex = 4;
			this.yCoord.Text = "";
			// 
			// zCoord
			// 
			this.zCoord.Location = new System.Drawing.Point(16, 48);
			this.zCoord.MaxLength = 10;
			this.zCoord.Name = "zCoord";
			this.zCoord.Size = new System.Drawing.Size(112, 20);
			this.zCoord.TabIndex = 5;
			this.zCoord.Text = "";
			// 
			// setCoord
			// 
			this.setCoord.Location = new System.Drawing.Point(0, 72);
			this.setCoord.Name = "setCoord";
			this.setCoord.Size = new System.Drawing.Size(128, 23);
			this.setCoord.TabIndex = 6;
			this.setCoord.Text = "ОК";
			this.setCoord.Click += new System.EventHandler(this.button1_Click);
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(128, 94);
			this.Controls.Add(this.setCoord);
			this.Controls.Add(this.zCoord);
			this.Controls.Add(this.yCoord);
			this.Controls.Add(this.xCoord);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "Form1";
			this.ResumeLayout(false);

		}
		#endregion

		public int xCoordPrm=0,yCoordPrm=0,zCoordPrm=0;

		bool allParametrsIn=false;

		private void button1_Click(object sender, System.EventArgs e)
		{
			if(chekNotNumber(this.xCoord.Text.ToString())&&chekNotNumber(this.yCoord.Text.ToString())&&chekNotNumber(this.zCoord.Text.ToString()))
			{
				if((this.xCoord.Text.ToString()!="")&&(this.yCoord.Text.ToString()!="")&&(this.zCoord.Text.ToString()!=""))
				{
					xCoordPrm=Convert.ToInt32(this.xCoord.Text.ToString());
					yCoordPrm=Convert.ToInt32(this.yCoord.Text.ToString());
					zCoordPrm=Convert.ToInt32(this.zCoord.Text.ToString());
					allParametrsIn=true;
					this.Close();

				}
				else
				{
					MessageBox.Show("Вы заполнили не все поля!");
					allParametrsIn=false;
				}
			}
			else
			{		
				MessageBox.Show("Вы ввели не числовые данные!");
				allParametrsIn=false;
			}
		
		}

		private void xCoord_TextChanged(object sender, System.EventArgs e)
		{
		
		}

		private bool chekNotNumber(String text)
		{
			
			foreach(char sym in text)
			{
				
				if(!Char.IsNumber(sym))
				{
					
					return false;
				}
				
			}
			return true;
		}

		

		

		
	}
}
