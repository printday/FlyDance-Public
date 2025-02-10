using SqlSugar;

namespace Fly.System.Domain.Models.Entities.Systems
{
    /// <summary>
    /// 角色表
    /// </summary>
    [SugarTable("t_sys_role")]
    public class Role
    {
        /// <summary>
        /// id
        /// </summary>
        [SugarColumn(ColumnName = "rid")]
        public int Id { get; set; }

        /// <summary>
        /// 角色id
        /// </summary>
        [SugarColumn(ColumnName = "role_id")]
        public string RoleId { get; set; }

        /// <summary>
        /// 角色名
        /// </summary>
        [SugarColumn(ColumnName = "role_name")]
        public string Name { get; set; }

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
