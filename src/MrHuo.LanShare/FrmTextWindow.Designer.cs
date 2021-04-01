
namespace MrHuo.LanShare
{
    partial class FrmTextWindow
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
            this.shareTextContent = new System.Windows.Forms.TextBox();
            this.btnConfirmText = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // shareTextContent
            // 
            this.shareTextContent.Dock = System.Windows.Forms.DockStyle.Top;
            this.shareTextContent.Location = new System.Drawing.Point(0, 0);
            this.shareTextContent.Multiline = true;
            this.shareTextContent.Name = "shareTextContent";
            this.shareTextContent.Size = new System.Drawing.Size(482, 389);
            this.shareTextContent.TabIndex = 0;
            // 
            // btnConfirmText
            // 
            this.btnConfirmText.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnConfirmText.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnConfirmText.Location = new System.Drawing.Point(0, 395);
            this.btnConfirmText.Name = "btnConfirmText";
            this.btnConfirmText.Size = new System.Drawing.Size(482, 58);
            this.btnConfirmText.TabIndex = 1;
            this.btnConfirmText.Text = "确认";
            this.btnConfirmText.UseVisualStyleBackColor = true;
            this.btnConfirmText.Click += new System.EventHandler(this.btnConfirmText_Click);
            // 
            // FrmTextWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(482, 453);
            this.Controls.Add(this.btnConfirmText);
            this.Controls.Add(this.shareTextContent);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FrmTextWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmTextWindow";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox shareTextContent;
        private System.Windows.Forms.Button btnConfirmText;
    }
}