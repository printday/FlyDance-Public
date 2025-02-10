namespace Fly.System.Domain.Models
{
    /// <summary>
    /// 服务设置
    /// </summary>
    public class ServiceSettings
    {
        /// <summary>
        /// 服务路由前缀
        /// </summary>
        public string RoutePrefixName { get; set; }

        /// <summary>
        /// 是否使用IP限制
        /// </summary>
        public bool IsUseIpLimiting { get; set; }

        /// <summary>
        /// 是否使用skywalking
        /// </summary>
        public bool IsUseSkywalking { get; set; }

        /// <summary>
        /// 文件上传路径
        /// </summary>
        public string UploadPath { get; set; }

        /// <summary>
        /// 是否使用consul
        /// </summary>
        public bool IsUseConsul { get; set; }

        /// <summary>
        /// 是否使用swagger
        /// </summary>
        public bool IsUseSwagger { get; set; }
    }
}
