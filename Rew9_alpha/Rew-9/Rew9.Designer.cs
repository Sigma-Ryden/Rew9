
namespace rew
{
    partial class Rew9
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Rew9));
            this.plot = new OxyPlot.WindowsForms.PlotView();
            this.functionCreateButton = new System.Windows.Forms.Button();
            this.functionField = new System.Windows.Forms.TextBox();
            this.functionLabel = new System.Windows.Forms.Label();
            this.OXmin = new System.Windows.Forms.TextBox();
            this.OXmax = new System.Windows.Forms.TextBox();
            this.ColorsCollection = new System.Windows.Forms.DomainUpDown();
            this.log = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // plot
            // 
            this.plot.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.plot.BackColor = System.Drawing.SystemColors.ControlLight;
            this.plot.CausesValidation = false;
            this.plot.Enabled = false;
            this.plot.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.plot.Location = new System.Drawing.Point(12, 11);
            this.plot.Margin = new System.Windows.Forms.Padding(0);
            this.plot.Name = "plot";
            this.plot.PanCursor = System.Windows.Forms.Cursors.No;
            this.plot.Size = new System.Drawing.Size(775, 375);
            this.plot.TabIndex = 0;
            this.plot.TabStop = false;
            this.plot.ZoomHorizontalCursor = System.Windows.Forms.Cursors.No;
            this.plot.ZoomRectangleCursor = System.Windows.Forms.Cursors.No;
            this.plot.ZoomVerticalCursor = System.Windows.Forms.Cursors.No;
            // 
            // functionCreateButton
            // 
            this.functionCreateButton.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.functionCreateButton.Location = new System.Drawing.Point(383, 389);
            this.functionCreateButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.functionCreateButton.Name = "functionCreateButton";
            this.functionCreateButton.Size = new System.Drawing.Size(59, 24);
            this.functionCreateButton.TabIndex = 0;
            this.functionCreateButton.Text = "Create";
            this.functionCreateButton.UseVisualStyleBackColor = true;
            this.functionCreateButton.Click += new System.EventHandler(this.functionCreateButton_Click);
            // 
            // functionField
            // 
            this.functionField.Location = new System.Drawing.Point(55, 390);
            this.functionField.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.functionField.Name = "functionField";
            this.functionField.PlaceholderText = "sin(x)/x";
            this.functionField.Size = new System.Drawing.Size(244, 22);
            this.functionField.TabIndex = 0;
            // 
            // functionLabel
            // 
            this.functionLabel.AutoSize = true;
            this.functionLabel.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.functionLabel.Location = new System.Drawing.Point(12, 395);
            this.functionLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.functionLabel.Name = "functionLabel";
            this.functionLabel.Size = new System.Drawing.Size(42, 14);
            this.functionLabel.TabIndex = 3;
            this.functionLabel.Text = "f(x)=";
            // 
            // OXmin
            // 
            this.OXmin.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.OXmin.Location = new System.Drawing.Point(305, 390);
            this.OXmin.Name = "OXmin";
            this.OXmin.PlaceholderText = "-5";
            this.OXmin.Size = new System.Drawing.Size(35, 22);
            this.OXmin.TabIndex = 0;
            this.OXmin.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // OXmax
            // 
            this.OXmax.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.OXmax.Location = new System.Drawing.Point(341, 390);
            this.OXmax.Name = "OXmax";
            this.OXmax.PlaceholderText = "15";
            this.OXmax.Size = new System.Drawing.Size(35, 22);
            this.OXmax.TabIndex = 3;
            this.OXmax.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ColorsCollection
            // 
            this.ColorsCollection.BackColor = System.Drawing.SystemColors.Control;
            this.ColorsCollection.Cursor = System.Windows.Forms.Cursors.Default;
            this.ColorsCollection.Items.Add("Red");
            this.ColorsCollection.Items.Add("Orange");
            this.ColorsCollection.Items.Add("Yellow");
            this.ColorsCollection.Items.Add("Green");
            this.ColorsCollection.Items.Add("Blue");
            this.ColorsCollection.Items.Add("Violet");
            this.ColorsCollection.Items.Add("White");
            this.ColorsCollection.Items.Add("Black");
            this.ColorsCollection.Location = new System.Drawing.Point(695, 390);
            this.ColorsCollection.Name = "ColorsCollection";
            this.ColorsCollection.ReadOnly = true;
            this.ColorsCollection.Size = new System.Drawing.Size(92, 22);
            this.ColorsCollection.TabIndex = 0;
            this.ColorsCollection.Text = "Red";
            // 
            // log
            // 
            this.log.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.log.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.log.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.log.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.log.Location = new System.Drawing.Point(12, 415);
            this.log.Name = "log";
            this.log.ReadOnly = true;
            this.log.Size = new System.Drawing.Size(775, 29);
            this.log.TabIndex = 0;
            this.log.Text = "Console: Rew9";
            // 
            // Rew9
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(799, 446);
            this.Controls.Add(this.log);
            this.Controls.Add(this.ColorsCollection);
            this.Controls.Add(this.OXmax);
            this.Controls.Add(this.OXmin);
            this.Controls.Add(this.functionLabel);
            this.Controls.Add(this.functionField);
            this.Controls.Add(this.functionCreateButton);
            this.Controls.Add(this.plot);
            this.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Rew9";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Rew9";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private OxyPlot.WindowsForms.PlotView plot;
        private System.Windows.Forms.Button functionCreateButton;
        private System.Windows.Forms.TextBox functionField;
        private System.Windows.Forms.Label functionLabel;
        private System.Windows.Forms.TextBox OXmin;
        private System.Windows.Forms.TextBox OXmax;
        private System.Windows.Forms.DomainUpDown ColorsCollection;
        private System.Windows.Forms.RichTextBox log;
    }
}

