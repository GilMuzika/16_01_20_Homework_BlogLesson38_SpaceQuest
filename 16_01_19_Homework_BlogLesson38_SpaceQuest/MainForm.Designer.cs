namespace _16_01_19_Homework_BlogLesson38_SpaceQuest
{
    partial class MainForm
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
            this.pcbMessages = new System.Windows.Forms.PictureBox();
            this.pcbLevel = new System.Windows.Forms.PictureBox();
            this.pcbHitPoints = new System.Windows.Forms.PictureBox();
            this.lblNewGame = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pcbMessages)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbLevel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbHitPoints)).BeginInit();
            this.SuspendLayout();
            // 
            // pcbMessages
            // 
            this.pcbMessages.Location = new System.Drawing.Point(91, 84);
            this.pcbMessages.Name = "pcbMessages";
            this.pcbMessages.Size = new System.Drawing.Size(743, 179);
            this.pcbMessages.TabIndex = 1;
            this.pcbMessages.TabStop = false;
            // 
            // pcbLevel
            // 
            this.pcbLevel.Location = new System.Drawing.Point(747, 3);
            this.pcbLevel.Name = "pcbLevel";
            this.pcbLevel.Size = new System.Drawing.Size(153, 75);
            this.pcbLevel.TabIndex = 2;
            this.pcbLevel.TabStop = false;
            // 
            // pcbHitPoints
            // 
            this.pcbHitPoints.Location = new System.Drawing.Point(3, 3);
            this.pcbHitPoints.Name = "pcbHitPoints";
            this.pcbHitPoints.Size = new System.Drawing.Size(131, 75);
            this.pcbHitPoints.TabIndex = 0;
            this.pcbHitPoints.TabStop = false;
            // 
            // lblNewGame
            // 
            this.lblNewGame.AutoEllipsis = true;
            this.lblNewGame.AutoSize = true;
            this.lblNewGame.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lblNewGame.Location = new System.Drawing.Point(140, 9);
            this.lblNewGame.Margin = new System.Windows.Forms.Padding(5);
            this.lblNewGame.Name = "lblNewGame";
            this.lblNewGame.Padding = new System.Windows.Forms.Padding(5);
            this.lblNewGame.Size = new System.Drawing.Size(115, 34);
            this.lblNewGame.TabIndex = 3;
            this.lblNewGame.Text = "New Game";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(902, 533);
            this.Controls.Add(this.lblNewGame);
            this.Controls.Add(this.pcbMessages);
            this.Controls.Add(this.pcbLevel);
            this.Controls.Add(this.pcbHitPoints);
            this.Name = "MainForm";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pcbMessages)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbLevel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbHitPoints)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pcbMessages;
        private System.Windows.Forms.PictureBox pcbLevel;
        private System.Windows.Forms.PictureBox pcbHitPoints;
        private System.Windows.Forms.Label lblNewGame;
    }
}

