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
            this.buttonCaptureWindowFrame = new System.Windows.Forms.Button();
            this.txtMessage = new System.Windows.Forms.TextBox();
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
            this.dealerChipPositionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.captureAll = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.CC5pictureBox = new System.Windows.Forms.PictureBox();
            this.CC4pictureBox = new System.Windows.Forms.PictureBox();
            this.CC3pictureBox = new System.Windows.Forms.PictureBox();
            this.CC2pictureBox = new System.Windows.Forms.PictureBox();
            this.CC1pictureBox = new System.Windows.Forms.PictureBox();
            this.HC2pictureBox = new System.Windows.Forms.PictureBox();
            this.HC1pictureBox = new System.Windows.Forms.PictureBox();
            this.ComunitaryCard5 = new System.Windows.Forms.Label();
            this.ComunitaryCard4label = new System.Windows.Forms.Label();
            this.ComunitaryCard3Label = new System.Windows.Forms.Label();
            this.ComunitaryCard2Label = new System.Windows.Forms.Label();
            this.CominitaryCard1Label = new System.Windows.Forms.Label();
            this.ComunitaryCardsLabel = new System.Windows.Forms.Label();
            this.HandCard2Label = new System.Windows.Forms.Label();
            this.HandCard1Label = new System.Windows.Forms.Label();
            this.HandCardsLabel = new System.Windows.Forms.Label();
            this.StackLabel = new System.Windows.Forms.Label();
            this.Player8Label = new System.Windows.Forms.Label();
            this.Player7Label = new System.Windows.Forms.Label();
            this.Player6Label = new System.Windows.Forms.Label();
            this.Player5Label = new System.Windows.Forms.Label();
            this.Player4Label = new System.Windows.Forms.Label();
            this.Player3Label = new System.Windows.Forms.Label();
            this.Player2Label = new System.Windows.Forms.Label();
            this.Player1Label = new System.Windows.Forms.Label();
            this.Player0Label = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CC5pictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CC4pictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CC3pictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CC2pictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CC1pictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HC2pictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HC1pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonCaptureWindowFrame
            // 
            this.buttonCaptureWindowFrame.Location = new System.Drawing.Point(17, 224);
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
            this.txtMessage.Location = new System.Drawing.Point(419, 27);
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.ReadOnly = true;
            this.txtMessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMessage.Size = new System.Drawing.Size(333, 191);
            this.txtMessage.TabIndex = 5;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.configureToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(766, 24);
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
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
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
            // dealerChipPositionsToolStripMenuItem
            // 
            this.dealerChipPositionsToolStripMenuItem.Name = "dealerChipPositionsToolStripMenuItem";
            this.dealerChipPositionsToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.dealerChipPositionsToolStripMenuItem.Text = "Dealer Chip Positions";
            this.dealerChipPositionsToolStripMenuItem.Click += new System.EventHandler(this.dealerChipPositionsToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // captureAll
            // 
            this.captureAll.Location = new System.Drawing.Point(104, 225);
            this.captureAll.Name = "captureAll";
            this.captureAll.Size = new System.Drawing.Size(75, 37);
            this.captureAll.TabIndex = 8;
            this.captureAll.Text = "Cap 2";
            this.captureAll.UseVisualStyleBackColor = true;
            this.captureAll.Click += new System.EventHandler(this.captureAll_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.CC5pictureBox);
            this.panel1.Controls.Add(this.CC4pictureBox);
            this.panel1.Controls.Add(this.CC3pictureBox);
            this.panel1.Controls.Add(this.CC2pictureBox);
            this.panel1.Controls.Add(this.CC1pictureBox);
            this.panel1.Controls.Add(this.HC2pictureBox);
            this.panel1.Controls.Add(this.HC1pictureBox);
            this.panel1.Controls.Add(this.ComunitaryCard5);
            this.panel1.Controls.Add(this.ComunitaryCard4label);
            this.panel1.Controls.Add(this.ComunitaryCard3Label);
            this.panel1.Controls.Add(this.ComunitaryCard2Label);
            this.panel1.Controls.Add(this.CominitaryCard1Label);
            this.panel1.Controls.Add(this.ComunitaryCardsLabel);
            this.panel1.Controls.Add(this.HandCard2Label);
            this.panel1.Controls.Add(this.HandCard1Label);
            this.panel1.Controls.Add(this.HandCardsLabel);
            this.panel1.Controls.Add(this.StackLabel);
            this.panel1.Controls.Add(this.Player8Label);
            this.panel1.Controls.Add(this.Player7Label);
            this.panel1.Controls.Add(this.Player6Label);
            this.panel1.Controls.Add(this.Player5Label);
            this.panel1.Controls.Add(this.Player4Label);
            this.panel1.Controls.Add(this.Player3Label);
            this.panel1.Controls.Add(this.Player2Label);
            this.panel1.Controls.Add(this.Player1Label);
            this.panel1.Controls.Add(this.Player0Label);
            this.panel1.Location = new System.Drawing.Point(17, 27);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(396, 191);
            this.panel1.TabIndex = 9;
            // 
            // CC5pictureBox
            // 
            this.CC5pictureBox.Location = new System.Drawing.Point(359, 135);
            this.CC5pictureBox.Name = "CC5pictureBox";
            this.CC5pictureBox.Size = new System.Drawing.Size(32, 44);
            this.CC5pictureBox.TabIndex = 25;
            this.CC5pictureBox.TabStop = false;
            // 
            // CC4pictureBox
            // 
            this.CC4pictureBox.Location = new System.Drawing.Point(318, 135);
            this.CC4pictureBox.Name = "CC4pictureBox";
            this.CC4pictureBox.Size = new System.Drawing.Size(32, 44);
            this.CC4pictureBox.TabIndex = 24;
            this.CC4pictureBox.TabStop = false;
            // 
            // CC3pictureBox
            // 
            this.CC3pictureBox.Location = new System.Drawing.Point(276, 135);
            this.CC3pictureBox.Name = "CC3pictureBox";
            this.CC3pictureBox.Size = new System.Drawing.Size(32, 44);
            this.CC3pictureBox.TabIndex = 23;
            this.CC3pictureBox.TabStop = false;
            // 
            // CC2pictureBox
            // 
            this.CC2pictureBox.Location = new System.Drawing.Point(236, 135);
            this.CC2pictureBox.Name = "CC2pictureBox";
            this.CC2pictureBox.Size = new System.Drawing.Size(32, 44);
            this.CC2pictureBox.TabIndex = 22;
            this.CC2pictureBox.TabStop = false;
            // 
            // CC1pictureBox
            // 
            this.CC1pictureBox.Location = new System.Drawing.Point(193, 135);
            this.CC1pictureBox.Name = "CC1pictureBox";
            this.CC1pictureBox.Size = new System.Drawing.Size(32, 44);
            this.CC1pictureBox.TabIndex = 21;
            this.CC1pictureBox.TabStop = false;
            // 
            // HC2pictureBox
            // 
            this.HC2pictureBox.Location = new System.Drawing.Point(318, 55);
            this.HC2pictureBox.Name = "HC2pictureBox";
            this.HC2pictureBox.Size = new System.Drawing.Size(32, 44);
            this.HC2pictureBox.TabIndex = 20;
            this.HC2pictureBox.TabStop = false;
            // 
            // HC1pictureBox
            // 
            this.HC1pictureBox.Location = new System.Drawing.Point(275, 55);
            this.HC1pictureBox.Name = "HC1pictureBox";
            this.HC1pictureBox.Size = new System.Drawing.Size(32, 44);
            this.HC1pictureBox.TabIndex = 19;
            this.HC1pictureBox.TabStop = false;
            // 
            // ComunitaryCard5
            // 
            this.ComunitaryCard5.AutoSize = true;
            this.ComunitaryCard5.Location = new System.Drawing.Point(356, 119);
            this.ComunitaryCard5.Name = "ComunitaryCard5";
            this.ComunitaryCard5.Size = new System.Drawing.Size(37, 16);
            this.ComunitaryCard5.TabIndex = 18;
            this.ComunitaryCard5.Text = "CC5";
            // 
            // ComunitaryCard4label
            // 
            this.ComunitaryCard4label.AutoSize = true;
            this.ComunitaryCard4label.Location = new System.Drawing.Point(313, 119);
            this.ComunitaryCard4label.Name = "ComunitaryCard4label";
            this.ComunitaryCard4label.Size = new System.Drawing.Size(37, 16);
            this.ComunitaryCard4label.TabIndex = 17;
            this.ComunitaryCard4label.Text = "CC4";
            // 
            // ComunitaryCard3Label
            // 
            this.ComunitaryCard3Label.AutoSize = true;
            this.ComunitaryCard3Label.Location = new System.Drawing.Point(275, 119);
            this.ComunitaryCard3Label.Name = "ComunitaryCard3Label";
            this.ComunitaryCard3Label.Size = new System.Drawing.Size(37, 16);
            this.ComunitaryCard3Label.TabIndex = 16;
            this.ComunitaryCard3Label.Text = "CC3";
            // 
            // ComunitaryCard2Label
            // 
            this.ComunitaryCard2Label.AutoSize = true;
            this.ComunitaryCard2Label.Location = new System.Drawing.Point(233, 119);
            this.ComunitaryCard2Label.Name = "ComunitaryCard2Label";
            this.ComunitaryCard2Label.Size = new System.Drawing.Size(37, 16);
            this.ComunitaryCard2Label.TabIndex = 15;
            this.ComunitaryCard2Label.Text = "CC2";
            this.ComunitaryCard2Label.Click += new System.EventHandler(this.label2_Click);
            // 
            // CominitaryCard1Label
            // 
            this.CominitaryCard1Label.AutoSize = true;
            this.CominitaryCard1Label.Location = new System.Drawing.Point(190, 119);
            this.CominitaryCard1Label.Name = "CominitaryCard1Label";
            this.CominitaryCard1Label.Size = new System.Drawing.Size(37, 16);
            this.CominitaryCard1Label.TabIndex = 14;
            this.CominitaryCard1Label.Text = "CC1";
            this.CominitaryCard1Label.Click += new System.EventHandler(this.label3_Click);
            // 
            // ComunitaryCardsLabel
            // 
            this.ComunitaryCardsLabel.AutoSize = true;
            this.ComunitaryCardsLabel.Location = new System.Drawing.Point(227, 103);
            this.ComunitaryCardsLabel.Name = "ComunitaryCardsLabel";
            this.ComunitaryCardsLabel.Size = new System.Drawing.Size(125, 16);
            this.ComunitaryCardsLabel.TabIndex = 13;
            this.ComunitaryCardsLabel.Text = "Comunitary cards";
            // 
            // HandCard2Label
            // 
            this.HandCard2Label.AutoSize = true;
            this.HandCard2Label.Location = new System.Drawing.Point(315, 39);
            this.HandCard2Label.Name = "HandCard2Label";
            this.HandCard2Label.Size = new System.Drawing.Size(37, 16);
            this.HandCard2Label.TabIndex = 12;
            this.HandCard2Label.Text = "HC2";
            // 
            // HandCard1Label
            // 
            this.HandCard1Label.AutoSize = true;
            this.HandCard1Label.Location = new System.Drawing.Point(272, 39);
            this.HandCard1Label.Name = "HandCard1Label";
            this.HandCard1Label.Size = new System.Drawing.Size(37, 16);
            this.HandCard1Label.TabIndex = 11;
            this.HandCard1Label.Text = "HC1";
            // 
            // HandCardsLabel
            // 
            this.HandCardsLabel.AutoSize = true;
            this.HandCardsLabel.Location = new System.Drawing.Point(268, 21);
            this.HandCardsLabel.Name = "HandCardsLabel";
            this.HandCardsLabel.Size = new System.Drawing.Size(84, 16);
            this.HandCardsLabel.TabIndex = 10;
            this.HandCardsLabel.Text = "Hand cards";
            // 
            // StackLabel
            // 
            this.StackLabel.AutoSize = true;
            this.StackLabel.Location = new System.Drawing.Point(77, 21);
            this.StackLabel.Name = "StackLabel";
            this.StackLabel.Size = new System.Drawing.Size(85, 16);
            this.StackLabel.TabIndex = 9;
            this.StackLabel.Text = "Stack Value";
            // 
            // Player8Label
            // 
            this.Player8Label.AutoSize = true;
            this.Player8Label.Location = new System.Drawing.Point(3, 167);
            this.Player8Label.Name = "Player8Label";
            this.Player8Label.Size = new System.Drawing.Size(65, 16);
            this.Player8Label.TabIndex = 8;
            this.Player8Label.Text = "Player 8:";
            // 
            // Player7Label
            // 
            this.Player7Label.AutoSize = true;
            this.Player7Label.Location = new System.Drawing.Point(3, 151);
            this.Player7Label.Name = "Player7Label";
            this.Player7Label.Size = new System.Drawing.Size(65, 16);
            this.Player7Label.TabIndex = 7;
            this.Player7Label.Text = "Player 7:";
            // 
            // Player6Label
            // 
            this.Player6Label.AutoSize = true;
            this.Player6Label.Location = new System.Drawing.Point(3, 135);
            this.Player6Label.Name = "Player6Label";
            this.Player6Label.Size = new System.Drawing.Size(65, 16);
            this.Player6Label.TabIndex = 6;
            this.Player6Label.Text = "Player 6:";
            // 
            // Player5Label
            // 
            this.Player5Label.AutoSize = true;
            this.Player5Label.Location = new System.Drawing.Point(3, 119);
            this.Player5Label.Name = "Player5Label";
            this.Player5Label.Size = new System.Drawing.Size(65, 16);
            this.Player5Label.TabIndex = 5;
            this.Player5Label.Text = "Player 5:";
            // 
            // Player4Label
            // 
            this.Player4Label.AutoSize = true;
            this.Player4Label.Location = new System.Drawing.Point(3, 103);
            this.Player4Label.Name = "Player4Label";
            this.Player4Label.Size = new System.Drawing.Size(65, 16);
            this.Player4Label.TabIndex = 4;
            this.Player4Label.Text = "Player 4:";
            // 
            // Player3Label
            // 
            this.Player3Label.AutoSize = true;
            this.Player3Label.Location = new System.Drawing.Point(3, 87);
            this.Player3Label.Name = "Player3Label";
            this.Player3Label.Size = new System.Drawing.Size(65, 16);
            this.Player3Label.TabIndex = 3;
            this.Player3Label.Text = "Player 3:";
            // 
            // Player2Label
            // 
            this.Player2Label.AutoSize = true;
            this.Player2Label.Location = new System.Drawing.Point(3, 71);
            this.Player2Label.Name = "Player2Label";
            this.Player2Label.Size = new System.Drawing.Size(65, 16);
            this.Player2Label.TabIndex = 2;
            this.Player2Label.Text = "Player 2:";
            // 
            // Player1Label
            // 
            this.Player1Label.AutoSize = true;
            this.Player1Label.Location = new System.Drawing.Point(3, 55);
            this.Player1Label.Name = "Player1Label";
            this.Player1Label.Size = new System.Drawing.Size(65, 16);
            this.Player1Label.TabIndex = 1;
            this.Player1Label.Text = "Player 1:";
            this.Player1Label.Click += new System.EventHandler(this.Player1Label_Click);
            // 
            // Player0Label
            // 
            this.Player0Label.AutoSize = true;
            this.Player0Label.Location = new System.Drawing.Point(37, 39);
            this.Player0Label.Name = "Player0Label";
            this.Player0Label.Size = new System.Drawing.Size(31, 16);
            this.Player0Label.TabIndex = 0;
            this.Player0Label.Text = "Me:";
            // 
            // Testing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(766, 270);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.captureAll);
            this.Controls.Add(this.txtMessage);
            this.Controls.Add(this.buttonCaptureWindowFrame);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Lucida Sans", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Testing";
            this.Text = "Calculadora";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CC5pictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CC4pictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CC3pictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CC2pictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CC1pictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HC2pictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HC1pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonCaptureWindowFrame;
        private System.Windows.Forms.TextBox txtMessage;
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
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label Player1Label;
        private System.Windows.Forms.Label Player0Label;
        private System.Windows.Forms.Label HandCardsLabel;
        private System.Windows.Forms.Label StackLabel;
        private System.Windows.Forms.Label Player8Label;
        private System.Windows.Forms.Label Player7Label;
        private System.Windows.Forms.Label Player6Label;
        private System.Windows.Forms.Label Player5Label;
        private System.Windows.Forms.Label Player4Label;
        private System.Windows.Forms.Label Player3Label;
        private System.Windows.Forms.Label Player2Label;
        private System.Windows.Forms.Label HandCard2Label;
        private System.Windows.Forms.Label HandCard1Label;
        private System.Windows.Forms.Label ComunitaryCard2Label;
        private System.Windows.Forms.Label CominitaryCard1Label;
        private System.Windows.Forms.Label ComunitaryCardsLabel;
        private System.Windows.Forms.Label ComunitaryCard5;
        private System.Windows.Forms.Label ComunitaryCard4label;
        private System.Windows.Forms.Label ComunitaryCard3Label;
        private System.Windows.Forms.PictureBox CC5pictureBox;
        private System.Windows.Forms.PictureBox CC4pictureBox;
        private System.Windows.Forms.PictureBox CC3pictureBox;
        private System.Windows.Forms.PictureBox CC2pictureBox;
        private System.Windows.Forms.PictureBox CC1pictureBox;
        private System.Windows.Forms.PictureBox HC2pictureBox;
        private System.Windows.Forms.PictureBox HC1pictureBox;
    }
}

