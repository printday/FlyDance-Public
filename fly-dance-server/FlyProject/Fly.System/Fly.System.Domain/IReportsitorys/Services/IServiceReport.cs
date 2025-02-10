using Fly.System.Domain.Models.Entities.Services;

namespace Fly.System.Domain.IReportsitorys.Services
{
    /// <summary>
    /// 基础服务
    /// </summary>
    public interface IServiceReport
    {
        /// <summary>
        /// 根据传入的类型 和是否启用 获取对应服务
        /// </summary>
        /// <param name="type"></param>
        /// <param name="isEnabled"></param>
        /// <returns></returns>
        List<Service> GetServiceByType(int type, bool? isEnabled = null);
    }
}
