namespace Fly.System.Domain.Models.Responses.Systems
{
    /// <summary>
    /// 主内容菜单
    /// </summary>
    public class ContentMenuResp
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Roid {  get; set; }
        
        /// <summary>
        /// 路由Id
        /// </summary>
        public string RouteId {  get; set; }

        /// <summary>
        /// 路由名称
        /// </summary>
        public string RouteName {  get; set; }
        
        /// <summary>
        /// 路由地址
        /// </summary>
        public string PathUrl {  get; set; }
    }
}
