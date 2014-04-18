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
            this.MoveMouseButton = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableCardsRegionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.holeCardsRegionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.playersStacksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bottonsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.captureAll = new System.Windows.Forms.Button();
            this.dealerChipPositionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.ibOriginal)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ibOriginal
            // 
            this.ibOriginal.Location = new System.Drawing.Point(17, 27);
            this.ibOriginal.Name = "ibOriginal";
            this.ibOriginal.Size = new System.Drawing.Size(957, 517);
            this.ibOriginal.TabIndex = 2;
            this.ibOriginal.TabStop = false;
            // 
            // buttonCaptureWindowFrame
            // 
            this.buttonCaptureWindowFrame.Location = new System.Drawing.Point(17, 550);
            this.buttonCaptureWindowFrame.Name = "buttonCaptureWindowFrame";
            this.buttonCaptureWindowFrame.Size = new System.Drawing.Size(77, 38);
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
            this.txtMessage.Size = new System.Drawing.Size(793, 83);
            this.txtMessage.TabIndex = 5;
            // 
            // MoveMouseButton
            // 
            this.MoveMouseButton.Location = new System.Drawing.Point(17, 594);
            this.MoveMouseButton.Name = "MoveMouseButton";
            this.MoveMouseButton.Size = new System.Drawing.Size(158, 38);
            this.MoveMouseButton.TabIndex = 6;
            this.MoveMouseButton.Text = "Read Text";
            this.MoveMouseButton.UseVisualStyleBackColor = true;
            this.MoveMouseButton.Click += new System.EventHandler(this.ReadTextButton_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.configureToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(986, 24);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.saveToolStripMenuItem.Text = "Save";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // configureToolStripMenuItem
            // 
            this.configureToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tableCardsRegionToolStripMenuItem,
            this.holeCardsRegionToolStripMenuItem,
            this.playersStacksToolStripMenuItem,
            this.bottonsToolStripMenuItem,
            this.dealerChipPositionsToolStripMenuItem});
            this.configureToolStripMenuItem.Name = "configureToolStripMenuItem";
            this.configureToolStripMenuItem.Size = new System.Drawing.Size(72, 20);
            this.configureToolStripMenuItem.Text = "Configure";
            // 
            // tableCardsRegionToolStripMenuItem
            // 
            this.tableCardsRegionToolStripMenuItem.Name = "tableCardsRegionToolStripMenuItem";
            this.tableCardsRegionToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.tableCardsRegionToolStripMenuItem.Text = "Table cards region";
            this.tableCardsRegionToolStripMenuItem.Click += new System.EventHandler(this.tableCardsRegionToolStripMenuItem_Click);
            // 
            // holeCardsRegionToolStripMenuItem
            // 
            this.holeCardsRegionToolStripMenuItem.Name = "holeCardsRegionToolStripMenuItem";
            this.holeCardsRegionToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.holeCardsRegionToolStripMenuItem.Text = "Hole cards region";
            this.holeCardsRegionToolStripMenuItem.Click += new System.EventHandler(this.holeCardsRegionToolStripMenuItem_Click);
            // 
            // playersStacksToolStripMenuItem
            // 
            this.playersStacksToolStripMenuItem.Name = "playersStacksToolStripMenuItem";
            this.playersStacksToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.playersStacksToolStripMenuItem.Text = "Players stacks";
            this.playersStacksToolStripMenuItem.Click += new System.EventHandler(this.playersStacksToolStripMenuItem_Click);
            // 
            // bottonsToolStripMenuItem
            // 
            this.bottonsToolStripMenuItem.Name = "bottonsToolStripMenuItem";
            this.bottonsToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.bottonsToolStripMenuItem.Text = "Buttons";
            this.bottonsToolStripMenuItem.Click += new System.EventHandler(this.bottonsToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // captureAll
            // 
            this.captureAll.Location = new System.Drawing.Point(100, 551);
            this.captureAll.Name = "captureAll";
            this.captureAll.Size = new System.Drawing.Size(75, 37);
            this.captureAll.TabIndex = 8;
            this.captureAll.Text = "Cap 2";
            this.captureAll.UseVisualStyleBackColor = true;
            this.captureAll.Click += new System.EventHandler(this.captureAll_Click);
            // 
            // dealerChipPositionsToolStripMenuItem
            // 
            this.dealerChipPositionsToolStripMenuItem.Name = "dealerChipPositionsToolStripMenuItem";
            this.dealerChipPositionsToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.dealerChipPositionsToolStripMenuItem.Text = "Dealer Chip Positions";
            this.dealerChipPositionsToolStripMenuItem.Click += new System.EventHandler(this.dealerChipPositionsToolStripMenuItem_Click);
            // 
            // Testing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(986, 649);
            this.Controls.Add(this.captureAll);
            this.Controls.Add(this.MoveMouseButton);
            this.Controls.Add(this.txtMessage);
            this.Controls.Add(this.buttonCaptureWindowFrame);
            this.Controls.Add(this.ibOriginal);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Lucida Sans", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Testing";
            this.Text = "Calculadora";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ibOriginal)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Emgu.CV.UI.ImageBox ibOriginal;
        private System.Windows.Forms.Button buttonCaptureWindowFrame;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.Button MoveMouseButton;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem configureToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tableCardsRegionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem holeCardsRegionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem playersStacksToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bottonsToolStripMenuItem;
        private System.Windows.Forms.Button captureAll;
        private System.Windows.Forms.ToolStripMenuItem dealerChipPositionsToolStripMenuItem;
    }
}

