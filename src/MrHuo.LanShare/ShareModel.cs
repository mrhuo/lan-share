using System.Drawing;

namespace MrHuo.LanShare
{
    /// <summary>
    /// 分享类型
    /// </summary>
    enum ShareType
    {
        /// <summary>
        /// 文本
        /// </summary>
        TEXT,
        /// <summary>
        /// 文件
        /// </summary>
        FILE
    }

    /// <summary>
    /// 分享内容基类
    /// </summary>
    abstract class ShareBase
    {
        /// <summary>
        /// 分享密钥
        /// </summary>
        public abstract string ShareKey { get; }
        /// <summary>
        /// 分享类型
        /// </summary>
        public abstract ShareType ShareType { get; }
    }

    /// <summary>
    /// 文本分享
    /// </summary>
    class TextShare : ShareBase
    {
        /// <summary>
        /// 构造函数，接受需要分享的文本
        /// </summary>
        /// <param name="text"></param>
        public TextShare(string text)
        {
            this.ShareType = ShareType.TEXT;
            this.ShareKey = Utils.GenerateShareKey();
            this.Text = text;
        }
        public override string ShareKey { get; }
        public override ShareType ShareType { get; }
        public string Text { get; }
    }

    /// <summary>
    /// 文件分享
    /// </summary>
    class FileShare : ShareBase
    {
        public FileShare(string filePath)
        {
            this.ShareType = ShareType.FILE;
            this.ShareKey = Utils.GenerateShareKey();
            this.FilePath = filePath;
        }
        public override string ShareKey { get; }
        public override ShareType ShareType { get; }
        public string FilePath { get; }
    }

    static class ShareBaseExtensions
    {
        public static string GetShareUrl<T>(this T share) where T : ShareBase
        {
            return $"{GetServerUrl()}/share/{share.ShareKey}";
        }

        private static string GetServerUrl()
        {
            var serverUrl = Program.ShareServerInstance?.ServerUrl ?? "";
            if (string.IsNullOrEmpty(serverUrl))
            {
                serverUrl = "http://<unknow-server>";
            }
            return serverUrl;
        }

        public static Bitmap GetShareQRcode<T>(this T share) where T : ShareBase
        {
            return Utils.CreateUrlQRCode(share.GetShareUrl());
        }
    }
}
