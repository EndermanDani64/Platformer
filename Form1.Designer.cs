namespace Platformer
{
    partial class Form1
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
            components = new System.ComponentModel.Container();
            playerObj = new PictureBox();
            mainTimer = new System.Windows.Forms.Timer(components);
            ((System.ComponentModel.ISupportInitialize)playerObj).BeginInit();
            SuspendLayout();
            // 
            // playerObj
            // 
            playerObj.BackColor = SystemColors.ActiveCaptionText;
            playerObj.Location = new Point(331, 233);
            playerObj.Name = "playerObj";
            playerObj.Size = new Size(31, 54);
            playerObj.TabIndex = 0;
            playerObj.TabStop = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(playerObj);
            MaximizeBox = false;
            Name = "Form1";
            Text = "Form1";
            KeyDown += Form1_KeyDown;
            KeyUp += Form1_KeyUp;
            ((System.ComponentModel.ISupportInitialize)playerObj).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox playerObj;
        internal System.Windows.Forms.Timer mainTimer;
    }
}