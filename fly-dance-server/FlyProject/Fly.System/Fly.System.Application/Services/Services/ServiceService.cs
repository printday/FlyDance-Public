using Common.Base.Model;
using Common.Enums;
using Common.Enums.Services;
using Common.IOC;
using Fly.System.Application.IServices.Services;
using Fly.System.Domain.IReportsitorys.Services;
using Fly.System.Domain.Models.Entities.Services;
using Fly.System.Domain.Models.Responses.Services;

namespace Fly.System.Application.Services.Services
{
    public class ServiceService : IServiceService
    {
        [Autowired]
        public IServiceReport ServiceReport {  get; set; }

        public BaseResponse<List<ServiceResp>> GetHomeService()
        {
            List<Service> homeServices = ServiceReport.GetServiceByType((int)ServiceType.Home, true);
            return new BaseResponse<List<ServiceResp>>()
            {
                Isok = true,
                Code = ErrorCode.Success.ToString(),
                Data = homeServices.Select(s => new ServiceResp()
                {
                    Id = s.Id,
                    ParentId = s.ParentId,
                    Name = s.Name,
                    Type = s.Type,
                    State = s.State,
                }).ToList()
            };
        }
    }
}
