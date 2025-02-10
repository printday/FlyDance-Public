namespace Common.Models.Configs.ConsulConfig
{
    /// <summary>
    /// Consul 配置类
    /// </summary>
    public class ConsulConfigInfo
    {
        /// <summary>
        /// Consul地址
        /// </summary>
        public string Address { get; set; } = "http://localhost:8500/";

        /// <summary>
        /// Consul Datacenter名
        /// </summary>
        public string Datacenter { get; set; } = "fly-datacenter-system";

        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; set; } = "32FC24DE-3376-4AEA-AF13-5397FB6B7830";

        /// <summary>
        /// 注册实例服务的IP
        /// </summary>
        public string RegIP { get; set; } = "localhost";

        /// <summary>
        /// 注册实例服务的Port
        /// </summary>
        public int RegPort { get; set; } = 80;

        /// <summary>
        /// 标签
        /// </summary>
        public string[] Tags { get; set; }

        /// <summary>
        /// 检查Model
        /// </summary>
        public ConsulCfgAgentSvcReg AgentServiceRegInfo { get; set; } = new ConsulCfgAgentSvcReg();
    }

    public class ConsulCfgAgentSvcReg
    {
        /// <summary>
        /// 服务名称
        /// </summary>
        public string Name { get; set; } = "Service-Fly";

        /// <summary>
        /// 间隔检查时间，单位秒
        /// </summary>
        public double IntervalSec { get; set; } = 10;

        /// <summary>
        /// 检查地址
        /// </summary>
        public string CheckAddress { get; set; } = "http://localhost:80/Base/ConsulHealth";

        /// <summary>
        /// 检查等待超时时间
        /// </summary>
        public double CheckTimeout { get; set; } = 5;

        /// <summary>
        /// 失败后多久移除，单位秒
        /// </summary>
        public double DeregisterSecond { get; set; } = 20;
    }
}
