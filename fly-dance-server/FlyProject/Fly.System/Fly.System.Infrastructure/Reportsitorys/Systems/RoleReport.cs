using Common.Base.Model;
using Common.Factorys;
using Common.IOC;
using Fly.System.Domain.IReportsitorys.Systems;
using Fly.System.Domain.Models.Entities.Systems;
using Fly.System.Domain.Models.Interfaces;

namespace Fly.System.Infrastructure.Reportsitorys.Systems
{
    public class RoleReport : IRoleReport, ISystemReport
    {
        [Autowired]
        public SqlSugarFactory SqlSugarFactory { get; set; }

        public List<Role> GetByUserIds(List<string> userId)
        {
            using (var db = SqlSugarFactory.GetMySqlConnection(DBNameModel.System))
            {
                return db.Queryable<Role>()
                    .InnerJoin<UserRole>((a, b) => a.Id == b.Rid)
                    .InnerJoin<User>((a, b, c) => b.Uid == c.Id)
                    .Where((a, b, c) => userId.Contains(c.UserId) && c.State != 0)
                    .ToList();
            }
        }
    }
}
