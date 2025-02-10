using Consul;
using Microsoft.Extensions.Configuration;

namespace Common.Models.Configs.ConsulConfig
{
    public static class ConsulExtensions
    {
        public static void UseConsul(this IConfiguration configuration, ConsulConfigInfo consulCfgInfo)
        {
            if (consulCfgInfo == null) consulCfgInfo = new ConsulConfigInfo();

            ConsulClient client = new ConsulClient(m =>
            {
                m.Address = new Uri(consulCfgInfo.Address);//new Uri("http://localhost:8500/");
                m.Datacenter = consulCfgInfo.Datacenter;//"dc1";
                m.Token = consulCfgInfo.Token;
            });
            //启动的时候在consul中注册实例服务
            //在consul中注册的ip,port
            string ip = consulCfgInfo.RegIP;
            int port = consulCfgInfo.RegPort;
            string[] tags = consulCfgInfo.Tags;
            var agentServiceRegInfo = new AgentServiceRegistration()
            {
                //ID = consulCfgInfo.AgentServiceRegInfo.Name + Guid.NewGuid(),//唯一的
                ID = consulCfgInfo.AgentServiceRegInfo.Name + "-" + ip + "-" + port.ToString(),
                Name = consulCfgInfo.AgentServiceRegInfo.Name,//组(服务)名称
                Address = ip,
                Port = port,//不同的端口=>不同的实例
                Tags = tags,//标签
                Check = new AgentServiceCheck()//服务健康检查
                {
                    Interval = TimeSpan.FromSeconds(consulCfgInfo.AgentServiceRegInfo.IntervalSec),//间隔多少秒一次 检查
                    HTTP = consulCfgInfo.AgentServiceRegInfo.CheckAddress,//$"http://{ip}:{port}/Api/Health/Index",
                    Timeout = TimeSpan.FromSeconds(consulCfgInfo.AgentServiceRegInfo.CheckTimeout),//检测等待时间
                    DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(consulCfgInfo.AgentServiceRegInfo.DeregisterSecond)//失败后多久移除
                }
            };
            client.Agent.ServiceRegister(agentServiceRegInfo);
        }
    }
}
