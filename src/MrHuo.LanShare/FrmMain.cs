using QRCoder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MrHuo.LanShare
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void serverUrl_Click(object sender, EventArgs e)
        {
            Process.Start(serverUrl.Text);
        }

        private void shareText_Click(object sender, EventArgs e)
        {
            if (Program.ShareServerInstance == null || !Program.ShareServerInstance.IsServerRunning)
            {
                Utils.Error("服务器没有启动，请启动后再试一次！");
                return;
            }
            var frmText = new LanShare.FrmTextWindow();
            if (frmText.ShowDialog() == DialogResult.OK)
            {
                var share = Program.ShareServerInstance.AddTextShare(frmText.SharedText);
                if (share != null)
                {
                    qrcode.Image = share.GetShareQRcode();
                }
                else
                {
                    qrcode.Image = null;
                }
            }
        }

        private void shareFile_Click(object sender, EventArgs e)
        {
            if (Program.ShareServerInstance == null || !Program.ShareServerInstance.IsServerRunning)
            {
                Utils.Error("服务器没有启动，请启动后再试一次！");
                return;
            }
            var opd = new OpenFileDialog()
            {
                InitialDirectory = Directory.GetCurrentDirectory()
            };
            if (opd.ShowDialog() == DialogResult.OK)
            {
                var share = Program.ShareServerInstance.AddFileShare(opd.FileName);
                if (share != null)
                {
                    qrcode.Image = share.GetShareQRcode();
                }
                else
                {
                    qrcode.Image = null;
                }
            }
        }

        private Timer serverStatusFetchTimer = null;
        private void FrmMain_Load(object sender, EventArgs e)
        {
            //加载服务器状态
            serverStatusFetchTimer = new Timer
            {
                Interval = 3000
            };
            serverStatusFetchTimer.Tick += AutoRefreshServerStatus;
            serverStatusFetchTimer.Enabled = true;
            serverStatusFetchTimer.Start();
        }

        private void AutoRefreshServerStatus(object sender, EventArgs e)
        {
            if (Program.ShareServerInstance == null || !Program.ShareServerInstance.IsServerRunning)
            {
                this.serverUrl.Text = "服务器未启动";
                this.serverUrl.ForeColor = Color.Red;
                return;
            }
            this.serverUrl.Text = Program.ShareServerInstance.ServerUrl;
            this.serverUrl.ForeColor = Color.Green;
        }

        private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (serverStatusFetchTimer != null)
            {
                serverStatusFetchTimer.Enabled = false;
                serverStatusFetchTimer.Stop();
                serverStatusFetchTimer = null;
            }
        }
    }
}
