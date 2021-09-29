
namespace WinFormsApp
{
    partial class FormTestControl
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
            this.userControlListBox = new WinFormsControlLibrary.Main.UserControlListBox();
            this.buttonAddToUserList = new System.Windows.Forms.Button();
            this.userControlTextBox = new WinFormsControlLibrary.Main.UserControlTextBox();
            this.SuspendLayout();
            // 
            // userControlListBox
            // 
            this.userControlListBox.Location = new System.Drawing.Point(12, 12);
            this.userControlListBox.Name = "userControlListBox";
            this.userControlListBox.SelectedElement = "";
            this.userControlListBox.Size = new System.Drawing.Size(348, 254);
            this.userControlListBox.TabIndex = 0;
            // 
            // buttonAddToUserList
            // 
            this.buttonAddToUserList.Location = new System.Drawing.Point(234, 273);
            this.buttonAddToUserList.Name = "buttonAddToUserList";
            this.buttonAddToUserList.Size = new System.Drawing.Size(114, 53);
            this.buttonAddToUserList.TabIndex = 1;
            this.buttonAddToUserList.Text = "Add String To UserList";
            this.buttonAddToUserList.UseVisualStyleBackColor = true;
            this.buttonAddToUserList.Click += new System.EventHandler(this.buttonAddToUserList_Click);
            // 
            // userControlTextBox
            // 
            this.userControlTextBox.Location = new System.Drawing.Point(12, 360);
            this.userControlTextBox.Name = "userControlTextBox";
            this.userControlTextBox.Size = new System.Drawing.Size(338, 52);
            this.userControlTextBox.TabIndex = 3;
            // 
            // FormTestControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(806, 606);
            this.Controls.Add(this.userControlTextBox);
            this.Controls.Add(this.buttonAddToUserList);
            this.Controls.Add(this.userControlListBox);
            this.Name = "FormTestControl";
            this.Text = "FormTestControl";
            this.ResumeLayout(false);

        }

        #endregion

        private WinFormsControlLibrary.Main.UserControlListBox userControlListBox;
        private System.Windows.Forms.Button buttonAddToUserList;
        private WinFormsControlLibrary.Main.UserControlTextBox userControlTextBox;
    }
}