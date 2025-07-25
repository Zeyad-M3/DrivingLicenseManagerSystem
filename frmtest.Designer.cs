namespace _3tr
{
    partial class frmtest
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
            this.controlForPeopleAndFindby1 = new _3tr.Peoples.ControlForPeopleAndFindby();
            this.SuspendLayout();
            // 
            // controlForPeopleAndFindby1
            // 
            this.controlForPeopleAndFindby1.Location = new System.Drawing.Point(25, 63);
            this.controlForPeopleAndFindby1.Name = "controlForPeopleAndFindby1";
            this.controlForPeopleAndFindby1.Size = new System.Drawing.Size(751, 375);
            this.controlForPeopleAndFindby1.TabIndex = 0;
            // 
            // frmtest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.controlForPeopleAndFindby1);
            this.Name = "frmtest";
            this.Text = "frmtest";
            this.Load += new System.EventHandler(this.frmtest_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Peoples.ControlForPeopleAndFindby controlForPeopleAndFindby1;
    }
}