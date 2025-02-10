using Fly.System.Domain.Models.Entities.Systems;

namespace Fly.System.Domain.IReportsitorys.Systems
{
    /// <summary>
    /// 角色基础接口
    /// </summary>
    public interface IRoleReport
    {
        /// <summary>
        /// 根据用户Id获取角色列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        List<Role> GetByUserIds(List<string> userId);
    }
}
