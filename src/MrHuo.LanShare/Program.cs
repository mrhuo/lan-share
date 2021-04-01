using Nancy;
using Nancy.Conventions;
using Nancy.Hosting.Self;
using QRCoder;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MrHuo.LanShare
{
    static class Program
    {
        internal static ShareServer ShareServerInstance = null;

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (!Utils.CheckIsAdministrator())
            {
                Utils.Error($"您需要使用管理员权限执行此程序，否则无法运行本地服务器！");
                return;
            }

            Task.Run(() =>
            {
                try
                {
                    ShareServerInstance = new ShareServer();
                    ShareServerInstance.StartServer();
                }
                catch (Exception ex)
                {
                    Utils.Error(ex.Message);
                }
            });

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += Application_ThreadException;
            Application.Run(new FrmMain());
        }

        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            Utils.Log("发生未捕捉的异常！", e.Exception);
        }
    }

}
