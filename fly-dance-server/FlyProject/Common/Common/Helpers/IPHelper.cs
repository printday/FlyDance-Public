using Autofac;
using Common.IOC;
using Microsoft.AspNetCore.Http;

namespace Common.Helpers
{
    public class IPHelper
    {
        /// <summary>
        /// 获取用户的真实IP，从一些真实key中过滤，只选择一个
        /// </summary>
        /// <returns></returns>
        public static string GetUserRealIP()
        {
            string realIP = "";
            try
            {
                var httpContextAccessor = AutofacContainer.Container.Resolve<IHttpContextAccessor>();

                if (httpContextAccessor != null && httpContextAccessor.HttpContext != null && httpContextAccessor.HttpContext.Request != null)
                {
                    var request = httpContextAccessor.HttpContext.Request;

                    string[] headerKeys = new string[] { "X-FORWARDED-FOR", "PROXY-CLIENT-IP", "WL-PROXY-CLIENT-IP", "HTTP_CLIENT_IP", "HTTP_X_FORWARDED_FOR", "X-REAL-IP" };

                    foreach (var item in request.Headers)
                    {
                        if (headerKeys.Contains(item.Key.ToString().ToUpper()))
                        {
                            realIP = item.Value;
                            break;
                        }
                    }
                    if (string.IsNullOrEmpty(realIP))
                    {
                        realIP = httpContextAccessor.HttpContext.Connection.RemoteIpAddress?.ToString();
                    }
                    if (string.IsNullOrEmpty(realIP))
                    {
                        realIP = request.Host.Host;
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return realIP;
        }
    }
}
