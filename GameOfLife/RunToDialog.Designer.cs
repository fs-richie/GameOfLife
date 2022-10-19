namespace GameOfLife
{
    partial class RunToDialog
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
            this.runToAcceptButton = new System.Windows.Forms.Button();
            this.runToNumeric = new System.Windows.Forms.NumericUpDown();
            this.runToNumericLabel = new System.Windows.Forms.Label();
            this.runToCancelButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.runToNumeric)).BeginInit();
            this.SuspendLayout();
            // 
            // runToAcceptButton
            // 
            this.runToAcceptButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.runToAcceptButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.runToAcceptButton.Location = new System.Drawing.Point(40, 75);
            this.runToAcceptButton.Name = "runToAcceptButton";
            this.runToAcceptButton.Size = new System.Drawing.Size(100, 23);
            this.runToAcceptButton.TabIndex = 0;
            this.runToAcceptButton.Text = "Accept";
            this.runToAcceptButton.UseVisualStyleBackColor = true;
            // 
            // runToNumeric
            // 
            this.runToNumeric.Location = new System.Drawing.Point(80, 39);
            this.runToNumeric.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.runToNumeric.Name = "runToNumeric";
            this.runToNumeric.Size = new System.Drawing.Size(120, 20);
            this.runToNumeric.TabIndex = 1;
            // 
            // runToNumericLabel
            // 
            this.runToNumericLabel.AutoSize = true;
            this.runToNumericLabel.Location = new System.Drawing.Point(94, 21);
            this.runToNumericLabel.Name = "runToNumericLabel";
            this.runToNumericLabel.Size = new System.Drawing.Size(92, 13);
            this.runToNumericLabel.TabIndex = 2;
            this.runToNumericLabel.Text = "Run to generation";
            // 
            // runToCancelButton
            // 
            this.runToCancelButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.runToCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.runToCancelButton.Location = new System.Drawing.Point(145, 75);
            this.runToCancelButton.Name = "runToCancelButton";
            this.runToCancelButton.Size = new System.Drawing.Size(100, 23);
            this.runToCancelButton.TabIndex = 3;
            this.runToCancelButton.Text = "Cancel";
            this.runToCancelButton.UseVisualStyleBackColor = true;
            // 
            // RunToDialog
            // 
            this.AcceptButton = this.runToAcceptButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 111);
            this.Controls.Add(this.runToCancelButton);
            this.Controls.Add(this.runToNumericLabel);
            this.Controls.Add(this.runToNumeric);
            this.Controls.Add(this.runToAcceptButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RunToDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RunToDialog";
            ((System.ComponentModel.ISupportInitialize)(this.runToNumeric)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button runToAcceptButton;
        private System.Windows.Forms.NumericUpDown runToNumeric;
        private System.Windows.Forms.Label runToNumericLabel;
        private System.Windows.Forms.Button runToCancelButton;
    }
}