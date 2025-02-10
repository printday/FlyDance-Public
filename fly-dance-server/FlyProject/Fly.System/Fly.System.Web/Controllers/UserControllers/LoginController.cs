using Common.Base.Model;
using Common.Const;
using Common.Enums;
using Common.Helpers;
using Common.IOC;
using Common.Models.Configs;
using Common.Models.Logins;
using Common.Utils;
using Fly.System.Application.IServices.Systems;
using Fly.System.Domain.Models.Dtos.RegisterDtos;
using Fly.System.Domain.Models.Requests.Emails;
using Fly.System.Domain.Models.Requests.Users;
using Fly.System.Domain.Models.Responses.Systems;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Cryptography;

namespace Fly.System.Web.Controllers.UserControllers
{
    /// <summary>
    /// 登录控制器
    /// </summary>
    public class LoginController : FlyControllerBase
    {
        [Autowired]
        public IUserService UserService { get; set; }

        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public BaseResponse<LoginUserResp> Login([FromBody] LoginInfoReq req)
        {
            if (req == null || req.LoginType == 0)
            {
                return new BaseResponse<LoginUserResp>()
                {
                    Isok = false,
                    Code = ErrorCode.Fail.ToString(),
                    Message = "参数错误, 请填写登录信息！",
                    Data = null
                };
            }
            return UserService.Login(req);
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public BaseResponse<bool> Register([FromBody] RegisterReq req)
        {
            if (req == null || string.IsNullOrWhiteSpace(req.UserName) || string.IsNullOrWhiteSpace(req.Email) ||
                string.IsNullOrWhiteSpace(req.PasswordConfirm) ||string.IsNullOrWhiteSpace(req.Password) || 
                string.IsNullOrWhiteSpace(req.Key) || string.IsNullOrWhiteSpace(req.Code))
            {
                return new BaseResponse<bool>()
                {
                    Isok = false,
                    Code = ErrorCode.Fail.ToString(),
                    Message = "参数错误, 请填写注册信息！",
                    Data = false
                };
            }
             return UserService.Register(req);
        }

        /// <summary>
        /// 发送验证码
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public BaseResponse<string> SendCode([FromBody] SendCodeReq req)
        {
            RedisConfigModel redisConfig = ServiceConfig.Redis;
            // 存储到Redis中，
            using (RedisHelper redisHelper = new RedisHelper(redisConfig.Host, redisConfig.Port, redisConfig.Password))
            {
                if (redisHelper.RedisConnection.IsConnected)
                {
                    string code = CodeUtil.GetCodeSix();
                    string message = $"亲爱的飞舞您好，您的验证码是-> {code}。好生保管，切莫被他人所闻。 -- 来自爱你的 FlyDance🍕";
                    bool isok = EmailHelper.SendEmail(req.Email, message).Result;
                    if (isok)
                    {
                        string key = DateTime.Now.ToString("yyyyMMddHHmmss") + req.Email;
                        string email = req.Email;
                        string codeJson = JsonConvert.SerializeObject(new CodeInfo{ Code = code, Email = email });
                        bool redisSetOk = redisHelper.Set<string>(key, codeJson, new TimeSpan(0,0,70));
                        return new BaseResponse<string>()
                        {
                            Isok = redisSetOk,
                            Code=redisSetOk ? ErrorCode.Success.ToString() : ErrorCode.Fail.ToString(),
                            Data = key,
                            Message= redisSetOk ? "验证码发送成功" : "验证码发送失败"
                        };
                    }
                }
            }
            return new BaseResponse<string>() { Isok = false, Code = ErrorCode.Fail.ToString(), Data = null, Message="验证码发送失败" };
        }
    }
}
