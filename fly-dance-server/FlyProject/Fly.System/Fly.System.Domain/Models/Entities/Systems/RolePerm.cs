using SqlSugar;

namespace Fly.System.Domain.Models.Entities.Systems
{
    /// <summary>
    /// 角色权限关联表
    /// </summary>
    [SugarTable("t_sys_role_permission")]
    public class RolePerm
    {
        /// <summary>
        /// id
        /// </summary>
        [SugarColumn(ColumnName = "id")]
        public int Id { get; set; }

        /// <summary>
        /// 权限id
        /// </summary>
        [SugarColumn(ColumnName = "pid")]
        public int Pid { get; set; }

        /// <summary>
        /// 角色id
        /// </summary>
        [SugarColumn(ColumnName = "rid")]
        public int Rid {  get; set; }

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
