using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MrHuo.LanShare
{
    public class ShareServerApi : Nancy.NancyModule
    {
        public ShareServerApi()
        {
            Before.InsertAfter("t", ctx =>
            {
                Utils.LogRequest(ctx.Request);
                return null;
            });

            //访问主页，输出版权信息
            Get("/", _ => ShareServer.SHARE_SERVER_COPYRIGHT);
            Get("/stat", _ => Response.AsJson(ShareServer.GetStat()));
            Get("/share/{key}", (parameters) =>
            {
                string key = parameters.key;
                if (string.IsNullOrEmpty(key) || !Program.ShareServerInstance.IsExists(key))
                {
                    return new NotFoundResponse();
                }
                var share = Program.ShareServerInstance.GetShare(key);
                if (share == null)
                {
                    return new NotFoundResponse();
                }
                switch (share.ShareType)
                {
                    case ShareType.TEXT:
                        return Response.AsText((share as TextShare).Text);
                    case ShareType.FILE:
                        return Response.AsFile((share as FileShare).FilePath);
                }
                return Response.AsJson(share);
            });
        }
    }
}
