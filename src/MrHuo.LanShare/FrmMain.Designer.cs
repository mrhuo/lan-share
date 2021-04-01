
namespace MrHuo.LanShare
{
    partial class FrmMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.scanTip = new System.Windows.Forms.Label();
            this.serverUrl = new System.Windows.Forms.Label();
            this.shareFile = new System.Windows.Forms.PictureBox();
            this.qrcode = new System.Windows.Forms.PictureBox();
            this.shareText = new System.Windows.Forms.PictureBox();
            this.shareClipboard = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.shareFile)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.qrcode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.shareText)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.shareClipboard)).BeginInit();
            this.SuspendLayout();
            // 
            // scanTip
            // 
            this.scanTip.AutoSize = true;
            this.scanTip.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.scanTip.ForeColor = System.Drawing.Color.Red;
            this.scanTip.Location = new System.Drawing.Point(562, 283);
            this.scanTip.Name = "scanTip";
            this.scanTip.Size = new System.Drawing.Size(169, 19);
            this.scanTip.TabIndex = 3;
            this.scanTip.Text = "扫描生成的二维码";
            // 
            // serverUrl
            // 
            this.serverUrl.AutoSize = true;
            this.serverUrl.ForeColor = System.Drawing.Color.DimGray;
            this.serverUrl.Location = new System.Drawing.Point(25, 287);
            this.serverUrl.Name = "serverUrl";
            this.serverUrl.Size = new System.Drawing.Size(91, 15);
            this.serverUrl.TabIndex = 4;
            this.serverUrl.Text = "正在检测...";
            this.serverUrl.Click += new System.EventHandler(this.serverUrl_Click);
            // 
            // shareFile
            // 
            this.shareFile.Image = global::MrHuo.LanShare.Properties.Resources.icon_file;
            this.shareFile.Location = new System.Drawing.Point(280, 21);
            this.shareFile.Name = "shareFile";
            this.shareFile.Size = new System.Drawing.Size(100, 100);
            this.shareFile.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.shareFile.TabIndex = 5;
            this.shareFile.TabStop = false;
            this.shareFile.Click += new System.EventHandler(this.shareFile_Click);
            // 
            // qrcode
            // 
            this.qrcode.BackColor = System.Drawing.SystemColors.ControlLight;
            this.qrcode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.qrcode.Location = new System.Drawing.Point(520, 21);
            this.qrcode.Name = "qrcode";
            this.qrcode.Size = new System.Drawing.Size(250, 250);
            this.qrcode.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.qrcode.TabIndex = 2;
            this.qrcode.TabStop = false;
            // 
            // shareText
            // 
            this.shareText.Image = global::MrHuo.LanShare.Properties.Resources.icon_text;
            this.shareText.Location = new System.Drawing.Point(154, 21);
            this.shareText.Name = "shareText";
            this.shareText.Size = new System.Drawing.Size(100, 100);
            this.shareText.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.shareText.TabIndex = 1;
            this.shareText.TabStop = false;
            this.shareText.Click += new System.EventHandler(this.shareText_Click);
            // 
            // shareClipboard
            // 
            this.shareClipboard.Image = global::MrHuo.LanShare.Properties.Resources.icon_clipboard;
            this.shareClipboard.Location = new System.Drawing.Point(28, 21);
            this.shareClipboard.Name = "shareClipboard";
            this.shareClipboard.Size = new System.Drawing.Size(100, 100);
            this.shareClipboard.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.shareClipboard.TabIndex = 0;
            this.shareClipboard.TabStop = false;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 321);
            this.Controls.Add(this.shareFile);
            this.Controls.Add(this.serverUrl);
            this.Controls.Add(this.scanTip);
            this.Controls.Add(this.qrcode);
            this.Controls.Add(this.shareText);
            this.Controls.Add(this.shareClipboard);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "局域网分享";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmMain_FormClosed);
            this.Load += new System.EventHandler(this.FrmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.shareFile)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.qrcode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.shareText)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.shareClipboard)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox shareClipboard;
        private System.Windows.Forms.PictureBox shareText;
        private System.Windows.Forms.PictureBox qrcode;
        private System.Windows.Forms.Label scanTip;
        private System.Windows.Forms.Label serverUrl;
        private System.Windows.Forms.PictureBox shareFile;
    }
}

