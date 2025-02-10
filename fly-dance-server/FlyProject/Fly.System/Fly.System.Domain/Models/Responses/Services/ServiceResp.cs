using SqlSugar;

namespace Fly.System.Domain.Models.Responses.Services
{
    public class ServiceResp
    {
        /// <summary>
        /// 服务id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 父服务id
        /// </summary>
        public int ParentId { get; set; }

        /// <summary>
        /// 服务名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 服务类型
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 服务状态
        /// </summary>
        public char State { get; set; }
    }
}
