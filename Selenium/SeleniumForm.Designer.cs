﻿namespace Selenium
{
   partial class SeleniumForm
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
         this.buttonStart = new System.Windows.Forms.Button();
         this.richTextBoxResult = new System.Windows.Forms.RichTextBox();
         this.SuspendLayout();
         // 
         // buttonStart
         // 
         this.buttonStart.Location = new System.Drawing.Point(294, 148);
         this.buttonStart.Name = "buttonStart";
         this.buttonStart.Size = new System.Drawing.Size(172, 98);
         this.buttonStart.TabIndex = 0;
         this.buttonStart.Text = "Start";
         this.buttonStart.UseVisualStyleBackColor = true;
         this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
         // 
         // richTextBoxResult
         // 
         this.richTextBoxResult.Location = new System.Drawing.Point(506, 87);
         this.richTextBoxResult.Name = "richTextBoxResult";
         this.richTextBoxResult.Size = new System.Drawing.Size(237, 250);
         this.richTextBoxResult.TabIndex = 1;
         this.richTextBoxResult.Text = "";
         // 
         // SeleniumForm
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(800, 450);
         this.Controls.Add(this.richTextBoxResult);
         this.Controls.Add(this.buttonStart);
         this.Name = "SeleniumForm";
         this.Text = "Selenium - Nhom01";
         this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.Button buttonStart;
      private System.Windows.Forms.RichTextBox richTextBoxResult;
   }
}

