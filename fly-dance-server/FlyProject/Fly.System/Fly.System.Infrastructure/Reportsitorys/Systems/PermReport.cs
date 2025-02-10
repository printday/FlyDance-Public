using Common.Base.Model;
using Common.Factorys;
using Common.IOC;
using Fly.System.Domain.IReportsitorys.Systems;
using Fly.System.Domain.Models.Entities.Systems;
using Fly.System.Domain.Models.Interfaces;

namespace Fly.System.Infrastructure.Reportsitorys.Systems
{
    public class PermReport : IPermReport, ISystemReport
    {
        [Autowired]
        public SqlSugarFactory SqlSugarFactory { get; set; }

        public List<Permission> GetPermByRoleIds(List<string> roleId, bool? isEnabled = null)
        {
            using (var db = SqlSugarFactory.GetMySqlConnection(DBNameModel.System))
            {
                return db.Queryable<Permission>()
                    .InnerJoin<RolePerm>((a, b) => a.Id == b.Pid)
                    .InnerJoin<Role>((a, b, c) => b.Rid == c.Id)
                    .Where((a ,b ,c) => roleId.Contains(c.RoleId))
                    .ToList();
            }
        }
    }
}
