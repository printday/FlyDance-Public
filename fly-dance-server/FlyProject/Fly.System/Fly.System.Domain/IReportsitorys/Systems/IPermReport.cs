using Fly.System.Domain.Models.Entities.Systems;

namespace Fly.System.Domain.IReportsitorys.Systems
{
    /// <summary>
    /// 权限基础服务
    /// </summary>
    public interface IPermReport
    {
        /// <summary>
        /// 获取角色对应的权限
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="isEnabled">是否启用</param>
        /// <returns></returns>
        List<Permission> GetPermByRoleIds(List<string> roleId, bool? isEnabled = null);
    }
}
