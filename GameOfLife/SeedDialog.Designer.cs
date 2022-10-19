namespace GameOfLife
{
    partial class SeedDialog
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
            this.seedAcceptButton = new System.Windows.Forms.Button();
            this.seedCancelButton = new System.Windows.Forms.Button();
            this.seedNumeric = new System.Windows.Forms.NumericUpDown();
            this.seedLabel = new System.Windows.Forms.Label();
            this.seedRandomizeButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.seedNumeric)).BeginInit();
            this.SuspendLayout();
            // 
            // seedAcceptButton
            // 
            this.seedAcceptButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.seedAcceptButton.Location = new System.Drawing.Point(60, 100);
            this.seedAcceptButton.Name = "seedAcceptButton";
            this.seedAcceptButton.Size = new System.Drawing.Size(100, 25);
            this.seedAcceptButton.TabIndex = 0;
            this.seedAcceptButton.Text = "Accept";
            this.seedAcceptButton.UseVisualStyleBackColor = true;
            // 
            // seedCancelButton
            // 
            this.seedCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.seedCancelButton.Location = new System.Drawing.Point(170, 100);
            this.seedCancelButton.Name = "seedCancelButton";
            this.seedCancelButton.Size = new System.Drawing.Size(100, 25);
            this.seedCancelButton.TabIndex = 1;
            this.seedCancelButton.Text = "Cancel";
            this.seedCancelButton.UseVisualStyleBackColor = true;
            // 
            // seedNumeric
            // 
            this.seedNumeric.Location = new System.Drawing.Point(79, 40);
            this.seedNumeric.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.seedNumeric.Name = "seedNumeric";
            this.seedNumeric.Size = new System.Drawing.Size(120, 20);
            this.seedNumeric.TabIndex = 2;
            // 
            // seedLabel
            // 
            this.seedLabel.AutoSize = true;
            this.seedLabel.Location = new System.Drawing.Point(41, 43);
            this.seedLabel.Name = "seedLabel";
            this.seedLabel.Size = new System.Drawing.Size(32, 13);
            this.seedLabel.TabIndex = 3;
            this.seedLabel.Text = "Seed";
            // 
            // seedRandomizeButton
            // 
            this.seedRandomizeButton.Location = new System.Drawing.Point(205, 37);
            this.seedRandomizeButton.Name = "seedRandomizeButton";
            this.seedRandomizeButton.Size = new System.Drawing.Size(75, 25);
            this.seedRandomizeButton.TabIndex = 4;
            this.seedRandomizeButton.Text = "Randomize";
            this.seedRandomizeButton.UseVisualStyleBackColor = true;
            this.seedRandomizeButton.Click += new System.EventHandler(this.seedRandomizeButton_Click);
            // 
            // SeedDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 136);
            this.Controls.Add(this.seedRandomizeButton);
            this.Controls.Add(this.seedLabel);
            this.Controls.Add(this.seedNumeric);
            this.Controls.Add(this.seedCancelButton);
            this.Controls.Add(this.seedAcceptButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SeedDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Seed Dialog";
            ((System.ComponentModel.ISupportInitialize)(this.seedNumeric)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button seedAcceptButton;
        private System.Windows.Forms.Button seedCancelButton;
        private System.Windows.Forms.NumericUpDown seedNumeric;
        private System.Windows.Forms.Label seedLabel;
        private System.Windows.Forms.Button seedRandomizeButton;
    }
}