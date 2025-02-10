using Common.Base.Model;
using DotNetCore.CAP;
using Fly.System.Domain.Models.Requests;
using Fly.System.Domain.Models.Responses;
using Fly.System.Web.Common.Cap.Models;

namespace Fly.System.Web.Common.Cap
{
    /// <summary>
    /// 
    /// </summary>
    public class CapSubscriberService : ICapSubscriberService, ICapSubscribe
    {

        /// <summary>
        /// receive message demo by self class
        /// </summary>
        /// <param name="req"></param>
        /// <param name="header"></param>
        [CapSubscribe(MsgSubConst.SelfClass)]
        public BaseResponse<SystemRes> ReceiveMessageSelfClass(SystemModelReq req, [FromCap] CapHeader header)
        {
            if (req != null) { }
            return new BaseResponse<SystemRes>() { Isok = true, Data = new SystemRes() { Name = "test.show.selfclass return value" } };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="req"></param>
        /// <param name="header"></param>
        [CapSubscribe(MsgSubConst.SelfClassCallback)]
        public void ReceiveMessageSelfClassCallback(BaseResponse<SystemRes> req, [FromCap] CapHeader header)
        {
            if (req != null) { }
        }
    }
}
