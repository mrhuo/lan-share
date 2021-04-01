using Nancy;
using Nancy.Conventions;
using Nancy.Hosting.Self;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MrHuo.LanShare
{
    /// <summary>
    /// 分享服务器
    /// </summary>
    class ShareServer
    {
        internal static string SHARE_SERVER_COPYRIGHT = "LanShare Server. <a href='https://github.com/mrhuo'>github.com/mrhuo</a>";
        internal static string SHARE_TEMP_DIR = Path.Combine(Application.StartupPath, "temp");
        internal static string SHARE_LOG_FILE = Path.Combine(Application.StartupPath, "share-log.log");
        internal static string SHARE_REQUEST_LOG_FILE = Path.Combine(Application.StartupPath, "share-request-log.log");

        internal static ConcurrentDictionary<string, ShareBase> SHARE_DICT
            = new ConcurrentDictionary<string, ShareBase>();
        internal static ConcurrentDictionary<string, DateTime> SHARE_TIMEOUT
            = new ConcurrentDictionary<string, DateTime>();

        private Thread ServerThread = null;
        private Thread CacheTimeoutCheckThread = null;

        /// <summary>
        /// 分享信息过期时间，默认1小时，1小时候不可用
        /// </summary>
        private int ShareTimeout { get; } = 60 * 60 * 1000;
        /// <summary>
        /// 服务器API启动URL
        /// </summary>
        public string ServerUrl { get; private set; }
        /// <summary>
        /// 服务器是否在运行
        /// </summary>
        public bool IsServerRunning { get; private set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="serverUrl"></param>
        public ShareServer(string serverUrl)
        {
            this.ServerUrl = serverUrl;
        }

        public ShareServer()
        {
            var port = Utils.GetAvailableServerPort();
            var serverUrl = $"http://{Utils.GetServerLanIP()}:{port}";

            this.ServerUrl = serverUrl;
        }

        /// <summary>
        /// 初始化服务器线程
        /// </summary>
        public void InitServer()
        {
            if (!Directory.Exists(SHARE_TEMP_DIR))
            {
                Directory.CreateDirectory(SHARE_TEMP_DIR);
            }

            StopServer();
            ServerThread = new Thread(new ThreadStart(() =>
            {
                IsServerRunning = true;
                using (var host = new NancyHost(new Uri(this.ServerUrl)))
                {
                    host.Start();
                    Utils.Log($"Share Server Running on {this.ServerUrl}.");
                    while (IsServerRunning)
                    {
                        Thread.Sleep(1000);
                    }
                    host.Stop();
                    Utils.Log("Share Server Stoped.");
                }
            }));

            CacheTimeoutCheckThread = new Thread(new ThreadStart(CacheTimeoutChecker));

            //初始化局域网服务发现
            InitLanServerDiscovery();
        }

        /// <summary>
        /// 以指定的服务名，在局域网使用MDNS协议广播地址，手机端等设备可以自动发现，并分享到服务器
        /// </summary>
        private void InitLanServerDiscovery()
        {
            //TODO
        }

        public string GenerateShareUrl(string serverUrl, string shareKey)
        {
            return $"{serverUrl}/share/{shareKey}";
        }

        public string GenerateShareUrl(string shareKey)
        {
            return GenerateShareUrl(this.ServerUrl, shareKey);
        }

        private T AddShare<T>(string key, T share)
            where T : ShareBase
        {
            if (!IsExists(key))
            {
                SHARE_DICT[key] = share;
            }
            SHARE_TIMEOUT[key] = DateTime.Now;
            Utils.Log($"[新增] 分享内容[{share.ShareType}], URL:{GenerateShareUrl(key)}");
            return share;
        }

        public TextShare AddTextShare(string text)
        {
            try
            {
                var textShare = new TextShare(text);
                return AddShare(textShare.ShareKey, textShare);
            }
            catch (Exception ex)
            {
                Utils.Error(ex.Message, "分享文本失败");
            }
            return null;
        }

        public FileShare AddFileShare(string filePath)
        {
            try
            {
                var ext = Path.GetExtension(filePath);
                var newPath = Path.Combine(SHARE_TEMP_DIR, Guid.NewGuid().ToString("N") + ext);
                File.Copy(filePath, newPath, true);
                var fileShare = new FileShare(newPath);
                return AddShare(fileShare.ShareKey, fileShare);
            }
            catch (Exception ex)
            {
                Utils.Error(ex.Message, "分享文件失败");
            }
            return null;
        }

        public void RemoveShare(string key)
        {
            SHARE_DICT.TryRemove(key, out var item);
            SHARE_TIMEOUT.TryRemove(key, out _);

            Utils.Log($"[移除] 分享内容[{key}], URL:{GenerateShareUrl(key)}");
            if (item is FileShare fs)
            {
                if (fs.FilePath.StartsWith(SHARE_TEMP_DIR))
                {
                    Utils.TryCatchDo(() => File.Delete(fs.FilePath));
                    Utils.Log($"[移除] 分享文件[{fs.FilePath}]");
                }
            }
        }

        public ShareBase GetShare(string key)
        {
            SHARE_DICT.TryGetValue(key, out var ret);
            return ret;
        }

        public bool IsExists(string key)
        {
            if (IsTimeout(key))
            {
                RemoveShare(key);
                return false;
            }
            return true;
        }

        public bool IsTimeout(string key)
        {
            if (!SHARE_DICT.ContainsKey(key))
            {
                return true;
            }
            if (!SHARE_TIMEOUT.ContainsKey(key))
            {
                return true;
            }
            return (DateTime.Now - SHARE_TIMEOUT[key]).TotalMilliseconds >= ShareTimeout;
        }

        private void CacheTimeoutChecker()
        {
            while (IsServerRunning)
            {
                var timeoutKeys = SHARE_TIMEOUT
                    .Where(p => IsTimeout(p.Key))
                    .Select(p => p.Key)
                    .ToArray();
                foreach (var key in timeoutKeys)
                {
                    Utils.Log($"[过期] 分享链接[{key}]已过期");
                    RemoveShare(key);
                }
                Thread.Sleep(1000);
            }
        }

        /// <summary>
        /// 启动服务器
        /// </summary>
        public void StartServer()
        {
            if (ServerThread != null && ServerThread.ThreadState == ThreadState.Running)
            {
                throw new Exception("服务器线程已经在运行，无需重复启动！");
            }
            InitServer();
            ServerThread.Start();
            CacheTimeoutCheckThread.Start();
        }

        /// <summary>
        /// 停止服务器
        /// </summary>
        public void StopServer()
        {
            if (IsServerRunning)
            {
                IsServerRunning = false;
                if (ServerThread != null)
                {
                    Utils.TryCatchDo(() => ServerThread.Abort());
                    ServerThread = null;
                }
                if (CacheTimeoutCheckThread != null)
                {
                    Utils.TryCatchDo(() => CacheTimeoutCheckThread.Abort());
                    CacheTimeoutCheckThread = null;
                }
                Utils.TryCatchDo(() => Directory.Delete(SHARE_TEMP_DIR, true));
                this.ServerUrl = "";
            }
        }

        public static object GetStat()
        {
            var items = SHARE_DICT.Select(p => new
            {
                Type = p.Value.ShareType.ToString(),
                Url = p.Value.GetShareUrl()
            });
            return new { total = SHARE_DICT.Count, items };
        }
    }

    public class CustomBootstrapper : DefaultNancyBootstrapper
    {
        protected override void ConfigureConventions(NancyConventions conventions)
        {
            base.ConfigureConventions(conventions);
            conventions.StaticContentsConventions.Add(
                StaticContentConventionBuilder.AddDirectory("assets", @"temp")
            );
        }
    }
}
