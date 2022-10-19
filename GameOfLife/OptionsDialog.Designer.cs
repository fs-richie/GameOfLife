namespace GameOfLife
{
    partial class OptionsDialog
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
            this.optionsAcceptButton = new System.Windows.Forms.Button();
            this.optionsCancelButton = new System.Windows.Forms.Button();
            this.timerOptionsNumeric = new System.Windows.Forms.NumericUpDown();
            this.cellWidthOptionsNumeric = new System.Windows.Forms.NumericUpDown();
            this.cellHeightOptionsNumeric = new System.Windows.Forms.NumericUpDown();
            this.timerOptionsLabel = new System.Windows.Forms.Label();
            this.cellWidthOptionsLabel = new System.Windows.Forms.Label();
            this.cellHeightOptionsLabel = new System.Windows.Forms.Label();
            this.colorOptionsCheckBox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.timerOptionsNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cellWidthOptionsNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cellHeightOptionsNumeric)).BeginInit();
            this.SuspendLayout();
            // 
            // optionsAcceptButton
            // 
            this.optionsAcceptButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.optionsAcceptButton.Location = new System.Drawing.Point(85, 175);
            this.optionsAcceptButton.Name = "optionsAcceptButton";
            this.optionsAcceptButton.Size = new System.Drawing.Size(100, 25);
            this.optionsAcceptButton.TabIndex = 0;
            this.optionsAcceptButton.Text = "Accept";
            this.optionsAcceptButton.UseVisualStyleBackColor = true;
            // 
            // optionsCancelButton
            // 
            this.optionsCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.optionsCancelButton.Location = new System.Drawing.Point(200, 175);
            this.optionsCancelButton.Name = "optionsCancelButton";
            this.optionsCancelButton.Size = new System.Drawing.Size(100, 25);
            this.optionsCancelButton.TabIndex = 1;
            this.optionsCancelButton.Text = "Cancel";
            this.optionsCancelButton.UseVisualStyleBackColor = true;
            // 
            // timerOptionsNumeric
            // 
            this.timerOptionsNumeric.Location = new System.Drawing.Point(200, 45);
            this.timerOptionsNumeric.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.timerOptionsNumeric.Name = "timerOptionsNumeric";
            this.timerOptionsNumeric.Size = new System.Drawing.Size(120, 20);
            this.timerOptionsNumeric.TabIndex = 2;
            // 
            // cellWidthOptionsNumeric
            // 
            this.cellWidthOptionsNumeric.Location = new System.Drawing.Point(200, 75);
            this.cellWidthOptionsNumeric.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.cellWidthOptionsNumeric.Name = "cellWidthOptionsNumeric";
            this.cellWidthOptionsNumeric.Size = new System.Drawing.Size(120, 20);
            this.cellWidthOptionsNumeric.TabIndex = 3;
            // 
            // cellHeightOptionsNumeric
            // 
            this.cellHeightOptionsNumeric.Location = new System.Drawing.Point(200, 105);
            this.cellHeightOptionsNumeric.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.cellHeightOptionsNumeric.Name = "cellHeightOptionsNumeric";
            this.cellHeightOptionsNumeric.Size = new System.Drawing.Size(120, 20);
            this.cellHeightOptionsNumeric.TabIndex = 4;
            // 
            // timerOptionsLabel
            // 
            this.timerOptionsLabel.AutoSize = true;
            this.timerOptionsLabel.Location = new System.Drawing.Point(52, 48);
            this.timerOptionsLabel.Name = "timerOptionsLabel";
            this.timerOptionsLabel.Size = new System.Drawing.Size(142, 13);
            this.timerOptionsLabel.TabIndex = 5;
            this.timerOptionsLabel.Text = "Timer Interval in Milliseconds";
            // 
            // cellWidthOptionsLabel
            // 
            this.cellWidthOptionsLabel.AutoSize = true;
            this.cellWidthOptionsLabel.Location = new System.Drawing.Point(66, 78);
            this.cellWidthOptionsLabel.Name = "cellWidthOptionsLabel";
            this.cellWidthOptionsLabel.Size = new System.Drawing.Size(128, 13);
            this.cellWidthOptionsLabel.TabIndex = 6;
            this.cellWidthOptionsLabel.Text = "Width of Universe in Cells";
            // 
            // cellHeightOptionsLabel
            // 
            this.cellHeightOptionsLabel.AutoSize = true;
            this.cellHeightOptionsLabel.Location = new System.Drawing.Point(63, 108);
            this.cellHeightOptionsLabel.Name = "cellHeightOptionsLabel";
            this.cellHeightOptionsLabel.Size = new System.Drawing.Size(131, 13);
            this.cellHeightOptionsLabel.TabIndex = 7;
            this.cellHeightOptionsLabel.Text = "Height of Universe in Cells";
            // 
            // colorOptionsCheckBox
            // 
            this.colorOptionsCheckBox.AutoSize = true;
            this.colorOptionsCheckBox.Location = new System.Drawing.Point(60, 140);
            this.colorOptionsCheckBox.Name = "colorOptionsCheckBox";
            this.colorOptionsCheckBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.colorOptionsCheckBox.Size = new System.Drawing.Size(260, 17);
            this.colorOptionsCheckBox.TabIndex = 8;
            this.colorOptionsCheckBox.Text = "Background and Dead Cells share the same color";
            this.colorOptionsCheckBox.UseVisualStyleBackColor = true;
            // 
            // OptionsDialog
            // 
            this.AcceptButton = this.optionsAcceptButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.optionsCancelButton;
            this.ClientSize = new System.Drawing.Size(384, 211);
            this.Controls.Add(this.colorOptionsCheckBox);
            this.Controls.Add(this.cellHeightOptionsLabel);
            this.Controls.Add(this.cellWidthOptionsLabel);
            this.Controls.Add(this.timerOptionsLabel);
            this.Controls.Add(this.cellHeightOptionsNumeric);
            this.Controls.Add(this.cellWidthOptionsNumeric);
            this.Controls.Add(this.timerOptionsNumeric);
            this.Controls.Add(this.optionsCancelButton);
            this.Controls.Add(this.optionsAcceptButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OptionsDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Options";
            ((System.ComponentModel.ISupportInitialize)(this.timerOptionsNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cellWidthOptionsNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cellHeightOptionsNumeric)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button optionsAcceptButton;
        private System.Windows.Forms.Button optionsCancelButton;
        private System.Windows.Forms.NumericUpDown timerOptionsNumeric;
        private System.Windows.Forms.NumericUpDown cellWidthOptionsNumeric;
        private System.Windows.Forms.NumericUpDown cellHeightOptionsNumeric;
        private System.Windows.Forms.Label timerOptionsLabel;
        private System.Windows.Forms.Label cellWidthOptionsLabel;
        private System.Windows.Forms.Label cellHeightOptionsLabel;
        private System.Windows.Forms.CheckBox colorOptionsCheckBox;
    }
}