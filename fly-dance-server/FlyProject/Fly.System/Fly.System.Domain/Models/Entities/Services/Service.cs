using SqlSugar;

namespace Fly.System.Domain.Models.Entities.Services
{
    [SugarTable("t_sys_service")]
    public class Service
    {
        /// <summary>
        /// 服务id
        /// </summary>
        [SugarColumn(ColumnName ="sid", IsPrimaryKey =true)]
        public int Id { get; set; }

        /// <summary>
        /// 父服务id
        /// </summary>
        [SugarColumn(ColumnName = "parent_id")]
        public int ParentId { get; set; }

        /// <summary>
        /// 服务名称
        /// </summary>
        [SugarColumn(ColumnName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// 服务类型
        /// </summary>
        [SugarColumn(ColumnName = "service_type")]
        public int Type {  get; set; }

        /// <summary>
        /// 服务状态
        /// </summary>
        [SugarColumn(ColumnName = "state")]
        public char State { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [SugarColumn(ColumnName = "create_time")]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        [SugarColumn(ColumnName = "create_name")]
        public string CreateName { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        [SugarColumn(ColumnName = "update_time")]
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>
        [SugarColumn(ColumnName = "update_name")]
        public string UpdateName { get; set; }
    }
}
