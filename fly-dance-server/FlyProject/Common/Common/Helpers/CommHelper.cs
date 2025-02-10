using Microsoft.AspNetCore.Http;

namespace Common.Helpers
{
    /// <summary>
    /// 公共帮助类
    /// </summary>
    public class CommHelper
    {
        /// <summary>
        /// 获取头部参数
        /// </summary>
        /// <param name="header">头部信息</param>
        /// <param name="key">key</param>
        /// <returns></returns>
        public static string GetHeaderPara(IHeaderDictionary header, string key)
        {
            header[key].FirstOrDefault();
            if (!string.IsNullOrWhiteSpace(key) && header.ContainsKey(key))
            {
                return header[key].FirstOrDefault();
            }
            return string.Empty;
        }
    }
}
