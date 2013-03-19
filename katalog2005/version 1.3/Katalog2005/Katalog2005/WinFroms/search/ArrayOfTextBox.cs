using System;
using System.Collections;


namespace Katalog2005.WinFroms.search
{
    /// <summary>
    /// Summary description for ArrayOfControllers.
    /// </summary>
    public class ArrayOfTextBox : System.Collections.CollectionBase
    {
        private readonly System.Windows.Forms.Form HostForm;

        public ArrayOfTextBox(System.Windows.Forms.Form host)
        {
            HostForm = host;

            this.AddNewTextBox();
            
        }

        public System.Windows.Forms.TextBox AddNewTextBox()
        {

            System.Windows.Forms.TextBox aTextBox = new System.Windows.Forms.TextBox();

            this.List.Add(aTextBox);

            HostForm.Controls.Add(aTextBox);


            aTextBox.Top = (Count - 1) * 21;
            aTextBox.Left = 200;
            aTextBox.Tag = this.Count;
            aTextBox.Width = 224;
            aTextBox.Height = 21;

            //aTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBox_KeyDown);
            return aTextBox;
        }




        public System.Windows.Forms.TextBox this[int Index]
        {
            get
            {
                return (System.Windows.Forms.TextBox)this.List[Index];
            }
        }




        public void Remove()
        {

            if (this.Count > 0)
            {
                HostForm.Controls.Remove(this[this.Count - 1]);
                this.List.RemoveAt(this.Count - 1);
            }
        }

        //				public void ClickHandler(Object sender, System.EventArgs e)
        //		{
        //			System.Windows.Forms.MessageBox.Show("You have clicked button " + 
        //				((System.Windows.Forms.Button) sender).Tag.ToString());
        //		}


        //	
        //		private void TextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        //		{
        //			if(e.KeyCode==System.Windows.Forms.Keys.Enter)
        //			{	
        //					//((System.Windows.Forms.TextBox) sender)
        //				
        //			}
        //		}








    }
}
