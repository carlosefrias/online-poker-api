namespace PokerAPI
{
    partial class Testing
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
            this.components = new System.ComponentModel.Container();
            this.ibOriginal = new Emgu.CV.UI.ImageBox();
            this.buttonCaptureWindowFrame = new System.Windows.Forms.Button();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.ReadTextButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ibOriginal)).BeginInit();
            this.SuspendLayout();
            // 
            // ibOriginal
            // 
            this.ibOriginal.Location = new System.Drawing.Point(17, 15);
            this.ibOriginal.Name = "ibOriginal";
            this.ibOriginal.Size = new System.Drawing.Size(824, 529);
            this.ibOriginal.TabIndex = 2;
            this.ibOriginal.TabStop = false;
            // 
            // buttonCaptureWindowFrame
            // 
            this.buttonCaptureWindowFrame.Location = new System.Drawing.Point(17, 550);
            this.buttonCaptureWindowFrame.Name = "buttonCaptureWindowFrame";
            this.buttonCaptureWindowFrame.Size = new System.Drawing.Size(158, 38);
            this.buttonCaptureWindowFrame.TabIndex = 4;
            this.buttonCaptureWindowFrame.Text = "Capture";
            this.buttonCaptureWindowFrame.UseVisualStyleBackColor = true;
            this.buttonCaptureWindowFrame.Click += new System.EventHandler(this.pauseResumeButton_Click);
            // 
            // txtMessage
            // 
            this.txtMessage.Font = new System.Drawing.Font("Lucida Sans", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMessage.Location = new System.Drawing.Point(181, 550);
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.ReadOnly = true;
            this.txtMessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMessage.Size = new System.Drawing.Size(660, 83);
            this.txtMessage.TabIndex = 5;
            // 
            // ReadTextButton
            // 
            this.ReadTextButton.Location = new System.Drawing.Point(17, 594);
            this.ReadTextButton.Name = "ReadTextButton";
            this.ReadTextButton.Size = new System.Drawing.Size(158, 38);
            this.ReadTextButton.TabIndex = 6;
            this.ReadTextButton.Text = "Read Text";
            this.ReadTextButton.UseVisualStyleBackColor = true;
            this.ReadTextButton.Click += new System.EventHandler(this.ReadTextButton_Click);
            // 
            // Testing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(855, 649);
            this.Controls.Add(this.ReadTextButton);
            this.Controls.Add(this.txtMessage);
            this.Controls.Add(this.buttonCaptureWindowFrame);
            this.Controls.Add(this.ibOriginal);
            this.Font = new System.Drawing.Font("Lucida Sans", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "Testing";
            this.Text = "Calculadora";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ibOriginal)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Emgu.CV.UI.ImageBox ibOriginal;
        private System.Windows.Forms.Button buttonCaptureWindowFrame;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.Button ReadTextButton;
    }
}

