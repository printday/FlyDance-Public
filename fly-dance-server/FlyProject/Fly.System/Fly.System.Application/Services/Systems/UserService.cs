using Common.Base.Model;
using Common.Const;
using Common.Enums;
using Common.Helpers;
using Common.IOC;
using Common.Models.Configs;
using Common.Models.Logins;
using Fly.System.Application.IServices.Systems;
using Fly.System.Domain.IReportsitorys.Systems;
using Fly.System.Domain.Models.Dtos.RegisterDtos;
using Fly.System.Domain.Models.Entities.Systems;
using Fly.System.Domain.Models.Interfaces;
using Fly.System.Domain.Models.Requests.Users;
using Fly.System.Domain.Models.Responses.Systems;
using Newtonsoft.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Fly.System.Application.Services.Systems
{
    public class UserService : ISystemService, IUserService
    {
        [Autowired]
        public IUserReport UserReport { get; set; }

        public User GetUserById(int id)
        {
            return UserReport.GetUserById(id);
        }

        public BaseResponse<LoginUserResp> Login(LoginInfoReq req)
        {
            User? user = null;
            switch (req.LoginType)
            {
                case 1:
                    user = UserReport.GetByUPwd(req);
                    break;
                case 2:
                    user = UserReport.GetByEmailPwd(req);
                    break;
                default:
                    return new BaseResponse<LoginUserResp>()
                    {
                        Isok = false,
                        Code = ErrorCode.Fail.ToString(),
                        Message = "参数错误, 请填写登录信息！",
                        Data = null
                    };
            }
            return new()
            {
                Isok = user != null,
                Code = user != null ? ErrorCode.Success.ToString() : ErrorCode.Fail.ToString(),
                Message = user != null ? "登录成功" : "登录失败",
                Data = new LoginUserResp()
                {
                    userId = user == null ? "" : user.UserId,
                }
            };
        }

        public BaseResponse<bool> Register(RegisterReq req)
        {
            if (req.Password != req.PasswordConfirm)
            {
                return new BaseResponse<bool>() { Isok=false, Code=ErrorCode.Fail.ToString(), Data=false, Message="两次密码输入不一样！" };
            }
            if (UserReport.GetByUName(req.UserName) !=null)
            {
                return new BaseResponse<bool>() { Isok=false, Code=ErrorCode.Fail.ToString(), Data=false, Message="该用户名已被使用！" };
            }
            if (UserReport.GetByEmail(req.Email) != null)
            {
                return new BaseResponse<bool>() { Isok=false, Code=ErrorCode.Fail.ToString(), Data=false, Message="该邮箱已被注册！" };
            }
            RedisConfigModel redisConfig = ServiceConfig.Redis;
            using (RedisHelper redisHelper = new RedisHelper(redisConfig.Host, redisConfig.Port, redisConfig.Password))
            {
                CodeInfo codeInfo = JsonConvert.DeserializeObject<CodeInfo>(redisHelper.Get<string>(req.Key)) ?? new();
                if (string.IsNullOrEmpty(codeInfo.Code))
                {
                    return new BaseResponse<bool>()
                    {
                        Isok = false,
                        Code = ErrorCode.Fail.ToString(),
                        Data = false,
                        Message = "注册失败, 验证码已过期！"
                    };
                }
                if (req.Email.Trim() != codeInfo.Email)
                {
                    return new BaseResponse<bool>()
                    {
                        Isok = false,
                        Code = ErrorCode.Fail.ToString(),
                        Data = false,
                        Message = "注册失败, 注册的邮箱与发送验证码的邮箱不一样！"
                    };
                }
                if (req.Code.Trim() != codeInfo.Code.Trim())
                {
                    return new BaseResponse<bool>()
                    {
                        Isok = false,
                        Code = ErrorCode.Fail.ToString(),
                        Data = false,
                        Message = "注册失败, 验证码错误"
                    };
                }
            }
            bool isok = UserReport.Register(req);
            return new BaseResponse<bool>()
            {
                Isok = isok,
                Code = isok ? ErrorCode.Success.ToString() : ErrorCode.Fail.ToString(),
                Data = isok,
                Message = isok ? "注册成功" : "注册失败"
            };
        }
    }
}
