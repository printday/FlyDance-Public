using Common.Base.Model;
using DotNetCore.CAP;
using Fly.System.Domain.Models.Requests;
using Fly.System.Domain.Models.Responses;

namespace Fly.System.Web.Common.Cap
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICapSubscriberService
    {
        /// <summary>
        /// test self class
        /// </summary>
        /// <param name="req"></param>
        /// <param name="header"></param>
        public BaseResponse<SystemRes> ReceiveMessageSelfClass(SystemModelReq req, [FromCap] CapHeader header);

        /// <summary>
        /// test self class callback
        /// </summary>
        /// <param name="req"></param>
        /// <param name="header"></param>
        public void ReceiveMessageSelfClassCallback(BaseResponse<SystemRes> req, [FromCap] CapHeader header);
    }
}
