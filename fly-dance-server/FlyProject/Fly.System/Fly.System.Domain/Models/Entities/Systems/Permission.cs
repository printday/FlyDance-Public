using SqlSugar;

namespace Fly.System.Domain.Models.Entities.Systems
{
    /// <summary>
    /// 权限表
    /// </summary>
    [SugarTable("t_sys_permission")]
    public class Permission
    {
        /// <summary>
        /// Id
        /// </summary>
        [SugarColumn(ColumnName = "pid")]
        public int Id { get; set; }

        /// <summary>
        /// 权限Id
        /// </summary>
        [SugarColumn(ColumnName = "perm_id")]
        public string PermId {  get; set; }

        /// <summary>
        /// 权限名称
        /// </summary>
        [SugarColumn(ColumnName = "perm_name")]
        public string PermName {  get; set; }

        /// <summary>
        /// 权限状态
        /// </summary>
        [SugarColumn(ColumnName = "state")]
        public char State { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        [SugarColumn(ColumnName = "icon")]
        public string Icon { get; set; }

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
