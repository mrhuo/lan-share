using QRCoder;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MrHuo.LanShare
{
    class Utils
    {
        private static object loggerLockObj = new object();
        public static void Alert(string message, string title = "提示")
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        public static void Info(string message, string title = "提示")
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void Error(string message, string title = "错误")
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static bool Confirm(string message, string title = "询问")
        {
            return MessageBox.Show(message, title, MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.OK;
        }

        public static void TryCatchDo(Action action)
        {
            try
            {
                action?.Invoke();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        public static void Log(string description)
        {
            lock (loggerLockObj)
            {
                var msg = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss.ffff}] [INF] {description}\r\n";
                Debug.Write(msg);
                TryCatchDo(() => File.AppendAllText(ShareServer.SHARE_LOG_FILE, msg));
            }
        }

        public static void LogRequest(Nancy.Request request)
        {
            lock (loggerLockObj)
            {
                var msg = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss.ffff}] [{request.UserHostAddress}] {request.Method} {request.Url}\r\n";
                Debug.Write(msg);
                TryCatchDo(() => File.AppendAllText(ShareServer.SHARE_REQUEST_LOG_FILE, msg));
            }
        }

        public static void Log(string description, Exception exception)
        {
            lock (loggerLockObj)
            {
                var msg = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss.ffff}] [ERR] {description}\r\n--------------------------------->\r\n{exception}\r\n<---------------------------------\r\n";
                Debug.Write(msg);
                TryCatchDo(() => File.AppendAllText(ShareServer.SHARE_LOG_FILE, msg));
            }
        }

        private static readonly string CHARS_UPPER = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private static readonly string CHARS_LOWER = CHARS_UPPER.ToLower();
        private static readonly string CHARS_NUMBER = "0123456789";
        private static readonly string CHARS = $"{CHARS_UPPER}{CHARS_LOWER}{CHARS_NUMBER}";

        public static string GenerateShareKey()
        {
            var sb = new StringBuilder();
            for (int i = 0; i < 8; i++)
            {
                var c = new Random(Guid.NewGuid().GetHashCode()).Next(0, CHARS.Length);
                sb.Append(CHARS[c]);
            }
            return sb.ToString();
        }

        public static Bitmap CreateUrlQRCode(string url)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(new PayloadGenerator.Url(url).ToString(), QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            return qrCode.GetGraphic(20);
        }

        public static string GetServerLanIP()
        {
            return Dns.GetHostEntry(Dns.GetHostName()).AddressList.FirstOrDefault().ToString();
        }

        public static bool CheckIsAdministrator()
        {
            var identity = System.Security.Principal.WindowsIdentity.GetCurrent();
            var principal = new System.Security.Principal.WindowsPrincipal(identity);
            //判断当前登录用户是否为管理员
            return principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator);
        }

        public static bool IsPortInUse(int port)
        {
            var ipProperties = IPGlobalProperties.GetIPGlobalProperties();
            return ipProperties.GetActiveTcpListeners().Any(p => p.Port == port) ||
                   ipProperties.GetActiveUdpListeners().Any(p => p.Port == port);
        }

        public static int GetAvailableServerPort()
        {
            for (int port = 1500; port < 65535; port++)
            {
                if (!IsPortInUse(port))
                {
                    return port;
                }
            }
            return 0;

        }
    }
}
