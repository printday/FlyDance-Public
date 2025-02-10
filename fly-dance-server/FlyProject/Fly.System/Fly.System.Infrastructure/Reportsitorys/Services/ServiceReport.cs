using Common.Base.Model;
using Common.Factorys;
using Common.IOC;
using Fly.System.Domain.IReportsitorys.Services;
using Fly.System.Domain.Models.Entities.Services;
using Fly.System.Domain.Models.Interfaces;

namespace Fly.System.Infrastructure.Reportsitorys.Services
{
    public class ServiceReport : IServiceReport, ISystemReport
    {
        [Autowired]
        public SqlSugarFactory SqlSugarFactory {  get; set; }

        public List<Service> GetServiceByType(int type, bool? isEnabled = null)
        {
            using (var db = SqlSugarFactory.GetMySqlConnection(DBNameModel.System))
            {
                return db.Queryable<Service>()
                    .Where(s => s.Type == type)
                    .WhereIF(isEnabled.HasValue && isEnabled.Value, s => s.State != 0)
                    .ToList();
            }
        }
    }
}
