using SqlSugar;

namespace Fly.System.Domain.Models.Entities.Systems
{
    /// <summary>
    /// 路由规则表
    /// </summary>
    [SugarTable("t_sys_route")]
    public class Route
    {
        /// <summary>
        /// Id
        /// </summary>
        [SugarColumn(ColumnName = "roid")]
        public int Id {  get; set; }

        /// <summary>
        /// 权限ID
        /// </summary>
        [SugarColumn(ColumnName = "pid")]
        public int PId {  get; set; }

        /// <summary>
        /// 路由Id
        /// </summary>
        [SugarColumn(ColumnName = "route_id")]
        public string RouteId { get; set; }

        /// <summary>
        /// 父级路由Id
        /// </summary>
        [SugarColumn(ColumnName = "parent_id")]
        public int ParentId {  get; set; }

        /// <summary>
        /// 路由名称
        /// </summary>
        [SugarColumn(ColumnName = "route_name")]
        public string RouteName {  get; set; }

        /// <summary>
        /// 路由类型(1：首页Tab, 2：底部菜单)
        /// </summary>
        [SugarColumn(ColumnName = "route_type")]
        public int Type {  get; set; }

        /// <summary>
        /// 路由状态
        /// </summary>
        [SugarColumn(ColumnName = "state")]
        public char State { get; set; }

        /// <summary>
        /// 路由地址
        /// </summary>
        [SugarColumn(ColumnName = "path_url")]
        public string PathUrl { get; set; }

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
