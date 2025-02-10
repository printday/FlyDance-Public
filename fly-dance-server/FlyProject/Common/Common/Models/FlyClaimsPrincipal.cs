using Common.Models.Logins;
using System.Runtime.Serialization;
using System.Security.Claims;
using System.Security.Principal;

namespace Common.Models
{
    public class FlyClaimsPrincipal : ClaimsPrincipal
    {
        /// <summary>
        /// 登录信息
        /// </summary>
        public LoginInfoRes LoginInfo { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public FlyClaimsPrincipal() : base()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="identities"></param>
        public FlyClaimsPrincipal(IEnumerable<ClaimsIdentity> identities) : base(identities)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        public FlyClaimsPrincipal(BinaryReader reader) : base(reader)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="identity"></param>
        public FlyClaimsPrincipal(IIdentity identity) : base(identity)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="principal"></param>
        public FlyClaimsPrincipal(IPrincipal principal) : base(principal)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected FlyClaimsPrincipal(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
