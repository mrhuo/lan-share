using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MrHuo.LanShare
{
    public partial class FrmTextWindow : Form
    {
        public FrmTextWindow()
        {
            InitializeComponent();
        }

        public string SharedText { get; set; }

        private void btnConfirmText_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(SharedText = shareTextContent.Text.Trim()))
            {
                MessageBox.Show("请输入文本分享！");
                return;
            }
            DialogResult = DialogResult.OK;
        }
    }
}
