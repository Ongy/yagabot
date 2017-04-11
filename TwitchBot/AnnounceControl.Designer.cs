namespace TwitchBot
{
    partial class AnnounceControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.AnnounceText = new System.Windows.Forms.TextBox();
            this.AnnounceGame = new System.Windows.Forms.TextBox();
            this.Active = new System.Windows.Forms.CheckBox();
            this.Weight = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // AnnounceText
            // 
            this.AnnounceText.Location = new System.Drawing.Point(4, 4);
            this.AnnounceText.Name = "AnnounceText";
            this.AnnounceText.Size = new System.Drawing.Size(328, 20);
            this.AnnounceText.TabIndex = 0;
            // 
            // AnnounceGame
            // 
            this.AnnounceGame.Location = new System.Drawing.Point(4, 31);
            this.AnnounceGame.Name = "AnnounceGame";
            this.AnnounceGame.Size = new System.Drawing.Size(139, 20);
            this.AnnounceGame.TabIndex = 1;
            // 
            // Active
            // 
            this.Active.AutoSize = true;
            this.Active.Location = new System.Drawing.Point(276, 30);
            this.Active.Name = "Active";
            this.Active.Size = new System.Drawing.Size(56, 17);
            this.Active.TabIndex = 2;
            this.Active.Text = "Active";
            this.Active.UseVisualStyleBackColor = true;
            // 
            // Weight
            // 
            this.Weight.Location = new System.Drawing.Point(150, 31);
            this.Weight.Name = "Weight";
            this.Weight.Size = new System.Drawing.Size(100, 20);
            this.Weight.TabIndex = 3;
            // 
            // AnnounceControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Weight);
            this.Controls.Add(this.Active);
            this.Controls.Add(this.AnnounceGame);
            this.Controls.Add(this.AnnounceText);
            this.Name = "AnnounceControl";
            this.Size = new System.Drawing.Size(335, 58);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox AnnounceText;
        private System.Windows.Forms.TextBox AnnounceGame;
        private System.Windows.Forms.CheckBox Active;
        private System.Windows.Forms.TextBox Weight;
    }
}
