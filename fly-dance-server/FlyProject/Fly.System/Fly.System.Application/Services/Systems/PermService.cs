using Common.Base.Model;
using Common.Enums;
using Common.IOC;
using Fly.System.Application.IServices.Systems;
using Fly.System.Domain.IReportsitorys.Systems;
using Fly.System.Domain.Models.Entities.Systems;
using Fly.System.Domain.Models.Responses.Systems;

namespace Fly.System.Application.Services.Systems
{
    public class PermService : IPermService
    {
        [Autowired]
        public IPermReport PermReport { get; set; }

        [Autowired]
        public IRoleReport RoleReport { get; set; }

        [Autowired]
        public IRouteReport RouteReport { get; set; }

        public BaseResponse<List<ContentMenuResp>> GetContentMenu(string userId, bool isEnabled = true)
        {
            List<Role> roles = RoleReport.GetByUserIds([userId]);
            List<Permission> perms = PermReport.GetPermByRoleIds(roles.Select(r => r.RoleId).ToList(), isEnabled);
            List<Route> routes = RouteReport.GetByPermIds(perms.Select(per=>per.Id).ToList(), true);
            return new BaseResponse<List<ContentMenuResp>>()
            {
                Isok = true,
                Code = ErrorCode.Success.ToString(),
                Message = "获取成功",
                Data = routes
                    .Select(x => new ContentMenuResp()
                    {
                        Roid = x.Id,
                        RouteId = x.RouteId,
                        RouteName = x.RouteName,
                        PathUrl = x.PathUrl,
                    }).ToList()
            };
        }
    }
}
