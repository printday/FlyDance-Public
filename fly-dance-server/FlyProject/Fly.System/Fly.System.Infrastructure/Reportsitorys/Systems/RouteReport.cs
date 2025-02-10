using Common.Base.Model;
using Common.Factorys;
using Common.IOC;
using Fly.System.Domain.IReportsitorys.Systems;
using Fly.System.Domain.Models.Entities.Systems;
using Fly.System.Domain.Models.Interfaces;

namespace Fly.System.Infrastructure.Reportsitorys.Systems
{
    public class RouteReport : IRouteReport, ISystemReport
    {
        [Autowired]
        public SqlSugarFactory SqlSugarFactory { get; set; }

        public List<Route> GetByPermIds(List<int> permIds, bool? isEnabled = null)
        {
            using (var db = SqlSugarFactory.GetMySqlConnection(DBNameModel.System))
            {
                return db.Queryable<Route>()
                    .Where(r => permIds.Contains(r.PId))
                    .WhereIF(isEnabled.HasValue && isEnabled.Value, isEnabled.Value ? r => r.State != 0 : r => r.State == 0)
                    .ToList();
            }
        }
    }
}
