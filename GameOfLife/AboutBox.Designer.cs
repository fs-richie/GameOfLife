namespace GameOfLife
{
    partial class AboutBox
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
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
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.labelCopyright = new System.Windows.Forms.Label();
            this.labelProductName = new System.Windows.Forms.Label();
            this.labelVersion = new System.Windows.Forms.Label();
            this.labelSchool = new System.Windows.Forms.Label();
            this.labelClass = new System.Windows.Forms.Label();
            this.okButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 1;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 67F));
            this.tableLayoutPanel.Controls.Add(this.labelCopyright, 0, 4);
            this.tableLayoutPanel.Controls.Add(this.labelProductName, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.labelVersion, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.labelSchool, 0, 2);
            this.tableLayoutPanel.Controls.Add(this.labelClass, 0, 3);
            this.tableLayoutPanel.Controls.Add(this.okButton, 0, 5);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(9, 9);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 6;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18.77551F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.42857F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13.46939F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 45.71429F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(417, 265);
            this.tableLayoutPanel.TabIndex = 0;
            // 
            // labelCopyright
            // 
            this.labelCopyright.AutoSize = true;
            this.labelCopyright.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelCopyright.Location = new System.Drawing.Point(6, 219);
            this.labelCopyright.Margin = new System.Windows.Forms.Padding(6, 0, 3, 10);
            this.labelCopyright.MaximumSize = new System.Drawing.Size(0, 25);
            this.labelCopyright.Name = "labelCopyright";
            this.labelCopyright.Size = new System.Drawing.Size(408, 10);
            this.labelCopyright.TabIndex = 25;
            this.labelCopyright.Text = "Game of Life © 2022 by Richie Chao is licensed under CC BY 4.0";
            this.labelCopyright.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelCopyright.UseCompatibleTextRendering = true;
            // 
            // labelProductName
            // 
            this.labelProductName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelProductName.Location = new System.Drawing.Point(6, 30);
            this.labelProductName.Margin = new System.Windows.Forms.Padding(6, 30, 3, 0);
            this.labelProductName.MaximumSize = new System.Drawing.Size(0, 17);
            this.labelProductName.Name = "labelProductName";
            this.labelProductName.Size = new System.Drawing.Size(408, 16);
            this.labelProductName.TabIndex = 19;
            this.labelProductName.Text = "Game of Life";
            this.labelProductName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelVersion
            // 
            this.labelVersion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelVersion.Location = new System.Drawing.Point(6, 46);
            this.labelVersion.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
            this.labelVersion.MaximumSize = new System.Drawing.Size(0, 17);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(408, 17);
            this.labelVersion.TabIndex = 0;
            this.labelVersion.Text = "Version 1.0.0";
            this.labelVersion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelSchool
            // 
            this.labelSchool.AutoSize = true;
            this.labelSchool.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelSchool.Location = new System.Drawing.Point(6, 74);
            this.labelSchool.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
            this.labelSchool.MaximumSize = new System.Drawing.Size(0, 34);
            this.labelSchool.Name = "labelSchool";
            this.labelSchool.Size = new System.Drawing.Size(408, 33);
            this.labelSchool.TabIndex = 21;
            this.labelSchool.Text = "Full Sail University\r\nProject and Portfolio I: Computer Science - Online\r\n";
            this.labelSchool.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelSchool.UseCompatibleTextRendering = true;
            // 
            // labelClass
            // 
            this.labelClass.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelClass.Location = new System.Drawing.Point(6, 107);
            this.labelClass.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
            this.labelClass.MaximumSize = new System.Drawing.Size(0, 17);
            this.labelClass.Name = "labelClass";
            this.labelClass.Size = new System.Drawing.Size(408, 17);
            this.labelClass.TabIndex = 22;
            this.labelClass.Text = "COS119-O 02";
            this.labelClass.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // okButton
            // 
            this.okButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.okButton.Location = new System.Drawing.Point(171, 242);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 20);
            this.okButton.TabIndex = 24;
            this.okButton.Text = "&OK";
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // AboutBox
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(435, 283);
            this.Controls.Add(this.tableLayoutPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutBox";
            this.Padding = new System.Windows.Forms.Padding(9);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Game of Life";
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Label labelProductName;
        private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Label labelSchool;
        private System.Windows.Forms.Label labelClass;
        private System.Windows.Forms.Label labelCopyright;
    }
}
