using Fly.System.Domain.Models.Entities.Systems;

namespace Fly.System.Domain.IReportsitorys.Systems
{
    /// <summary>
    /// 路由基础设施
    /// </summary>
    public interface IRouteReport
    {
        /// <summary>
        /// 根据权限id获取路由信息
        /// </summary>
        /// <param name="permIds"></param>
        /// <returns></returns>
        List<Route> GetByPermIds(List<int> permIds, bool? isEnabled = null);
    }
}
