﻿namespace IntepreterProgram.Semantics
{
    partial class Identitables
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
            this.IdentTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // IdentTextBox
            // 
            this.IdentTextBox.Location = new System.Drawing.Point(46, 39);
            this.IdentTextBox.Multiline = true;
            this.IdentTextBox.Name = "IdentTextBox";
            this.IdentTextBox.Size = new System.Drawing.Size(717, 382);
            this.IdentTextBox.TabIndex = 0;
            // 
            // Identitables
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.IdentTextBox);
            this.Name = "Identitables";
            this.Text = "Identitables";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox IdentTextBox;
    }
}