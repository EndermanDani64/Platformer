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
            debugCheckBox = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)playerObj).BeginInit();
            SuspendLayout();
            // 
            // playerObj
            // 
            playerObj.BackColor = Color.Transparent;
            playerObj.Location = new Point(287, 229);
            playerObj.Name = "playerObj";
            playerObj.Size = new Size(30, 60);
            playerObj.TabIndex = 0;
            playerObj.TabStop = false;
            // 
            // debugCheckBox
            // 
            debugCheckBox.AutoSize = true;
            debugCheckBox.Location = new Point(703, 12);
            debugCheckBox.Name = "debugCheckBox";
            debugCheckBox.Size = new Size(85, 19);
            debugCheckBox.TabIndex = 1;
            debugCheckBox.Text = "debug logs";
            debugCheckBox.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(debugCheckBox);
            Controls.Add(playerObj);
            MaximizeBox = false;
            Name = "Form1";
            Text = "Form1";
            KeyUp += Form1_KeyUp;
            ((System.ComponentModel.ISupportInitialize)playerObj).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox playerObj;
        internal System.Windows.Forms.Timer mainTimer;
        private CheckBox checkBox1;
        private CheckBox debugCheckBox;
    }
}