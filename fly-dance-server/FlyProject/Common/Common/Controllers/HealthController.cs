using Common.Base.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Common.Controllers
{
    /// <summary>
    /// Health 控制器
    /// </summary>
    [AllowAnonymous]
    public class HealthController : FlyControllerBase
    {
        /// <summary>
        /// HealthController
        /// </summary>
        public HealthController()
        {

        }

        /// <summary>
        /// Check
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Produces("text/plain")]
        [AllowAnonymous]
        public string Check()
        {
            return "OK";
        }

        /// <summary>
        /// Check
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Produces("text/plain")]
        [AllowAnonymous]
        public string CheckHost()
        {
            StringBuilder sb = new StringBuilder();
            string hostname = Dns.GetHostName(); //得到主机名
            sb.Append("HostName:" + hostname + "        ");
            sb.Append("IPv4 Info:");
            IPHostEntry IpEntry = Dns.GetHostEntry(hostname); //通过主机名去获取全部ip信息
            IpEntry.AddressList.Where(x => x.AddressFamily == AddressFamily.InterNetwork).ToList().ForEach(y => sb.Append(y + "        ")); //获取Ipv4
            sb.Append("IPv6 Info:");
            IpEntry.AddressList.Where(x => x.AddressFamily == AddressFamily.InterNetworkV6).ToList().ForEach(y => sb.Append(y + "        ")); //获取Ipv6
            sb.Append("RunPath:" + AppDomain.CurrentDomain.BaseDirectory + "        ");
            return sb.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public ContentResult GetString()
        {
            return Content("OK", "application/text", System.Text.Encoding.UTF8);
        }
    }
}
