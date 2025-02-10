using Common.Base.Model;
using Common.Const;
using Common.Enums;
using Common.Helpers;
using Common.Models;
using Common.Models.Configs;
using Common.Models.Logins;
using Common.Models.Requests;
using Common.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net;
using System.Security.Principal;

namespace Common.Filters
{
    /// <summary>
    /// 
    /// </summary>
    public class AppTokenAuthorization : IAuthorizationFilter
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IConfiguration _configuration;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public AppTokenAuthorization([FromServices] IConfiguration configuration) { _configuration = configuration; }

        /// <summary>
		/// 认证过滤器
		/// </summary>
		/// <param name="context"></param>
		public void OnAuthorization(AuthorizationFilterContext context)
        {
            LoginData reqLogin = new LoginData();
            var actionContext = context.HttpContext;

            var descriptor = context.ActionDescriptor as ControllerActionDescriptor;

            if (descriptor.ControllerTypeInfo.GetCustomAttributes(typeof(AllowAnonymousAttribute), true).Any()) { return; }
            if (descriptor.MethodInfo.GetCustomAttributes(typeof(AllowAnonymousAttribute), true).Any()) { return; }
            //Token验证方式
            string token = CommHelper.GetHeaderPara(actionContext.Request.Headers, FrameConst.HttpParaKey_Token);

            //websocket（暂时用不到，后续有需要可以继续）
            //string websockettoken = CommHelper.GetHeaderPara(actionContext.Request.Headers, "Sec-WebSocket-Protocol");
            //websockettoken = string.IsNullOrEmpty(websockettoken) ? string.Empty : websockettoken.Split(",")[0];
            //token = string.IsNullOrEmpty(token) ? websockettoken : token;
            //if (!string.IsNullOrEmpty(websockettoken) && !actionContext.Response.Headers.ContainsKey("Sec-WebSocket-Protocol"))
            //{ 
            //    actionContext.Response.Headers.Add("Sec-WebSocket-Protocol", websockettoken);
            //}

            token = WebUtility.UrlDecode(token);    // 解URL编码
            if (string.IsNullOrWhiteSpace(token) == false)//token不为空，验证token
            {
                reqLogin = CheckAndGetTokenInfo(context, FrameConst.HttpParaKey_Token, token);
                if (reqLogin == null) return;
            }
            else//token为空验证用户名密码
            {
                //验证用户登录名
                string loginName = CommHelper.GetHeaderPara(actionContext.Request.Headers, FrameConst.HttpParaKey_UserLoginName);
                if (!string.IsNullOrWhiteSpace(loginName)) { reqLogin.UserName = loginName; }
                var clientIP = IPHelper.GetUserRealIP();
                var allowIPList = ServiceConfig.AllowClientIP;
                if (!allowIPList.Exists(p => p == "*") && !allowIPList.Exists(p => p == clientIP))
                {
                    string errMsg = $"未授权的IP：{clientIP}";
                    CreateErrorResponse(context, ErrorCode.AuthenticationFailed, errMsg, loginName);
                    return;
                }
            }
            //获取登录者信息
            LoginInfoRes loginInfo = null;
            if (!string.IsNullOrWhiteSpace(reqLogin.UserId))
            {
                string redisKey = FrameConst.CacheKey_LoginInfo + reqLogin.UserId;  // 以UserId为key将信息存到redis中
                RedisConfigModel redisConfig = ServiceConfig.Redis;
                using (RedisHelper redisHelper = new RedisHelper(redisConfig.Host, redisConfig.Port, redisConfig.Password))
                {
                    loginInfo = redisHelper.Get<LoginInfoRes>(redisKey);
                    if (loginInfo == null)
                    {
                        //获取登录者信息
                        loginInfo = this.GetLoginInfo(reqLogin);
                        if (loginInfo == null) { CreateErrorResponse(context, ErrorCode.AuthenticationFailed, "登录失败", token); return; }
                        redisHelper.Set(redisKey, loginInfo);   // 修改缓存中的登录信息
                    }
                    // 有修改密码，则重新登录
                    if (!string.IsNullOrEmpty(reqLogin.strExpireTime) && loginInfo.ChangePwdTime.HasValue)
                    {
                        bool result = DateTime.Compare(loginInfo.ChangePwdTime.Value, Convert.ToDateTime(reqLogin.strExpireTime)) > 0;
                        if (result) { CreateErrorResponse(context, ErrorCode.AuthenticationFailed, "登录失败", token); return; }
                    }
                    actionContext.User = new FlyClaimsPrincipal(new GenericIdentity(loginInfo.Id.ToString()));
                    ((FlyClaimsPrincipal)actionContext.User).LoginInfo = loginInfo;
                    ((GenericIdentity)actionContext.User.Identity).Label = JsonConvert.SerializeObject(new
                    {
                        ID = loginInfo.Id,
                        UserId = loginInfo.UserId,
                        UserName = loginInfo.UserName,
                        Token = loginInfo.Token
                    });
                }
            }
            else { CreateErrorResponse(context, ErrorCode.AuthenticationFailed, "登录失败", token); return; }
            if (loginInfo == null) { CreateErrorResponse(context, ErrorCode.AuthenticationFailed, "登录失败", token); return; }
            this.OnCustomerAuthorization(context);
        }

        /// <summary>
        /// 验证token并解析token中的信息
        /// </summary>
        /// <param name="context"></param>
        /// <param name="headerKey"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        private LoginData CheckAndGetTokenInfo(AuthorizationFilterContext context, string headerKey, string token)
        {
            LoginData reqLogin = null;
            try
            {
                //验证token得到登陆者信息
                var chkToken = CheckToken(token);
                if (chkToken == null || chkToken.code != "200") { CreateErrorResponse(context, ErrorCode.AuthenticationFailed, "登录失败", token); return new(); }
                reqLogin = new LoginData();
                reqLogin.Id = chkToken.Data.Id;
                reqLogin.UserName = chkToken.Data.UserName;
            }
            catch { CreateErrorResponse(context, ErrorCode.AuthenticationFailed, "登录失败", token); return new(); }
            return reqLogin;
        }

        /// <summary>
		/// 验证token
		/// </summary>
		/// <param name="token">token</param>
		/// <returns></returns>
		private CheckTokenRes CheckToken(string token)
        {
            BaseClientHelper client = new BaseClientHelper(ServiceConfig.GatewayUrl);
            var reqUrl = "/FlySSO/api/SSO/CheckToken";
            var req = new CheckTokenReq() { Token = token };
            BaseResponse<SSOTokenData> ret = client.ExecutePost<SSOTokenData>(reqUrl, req);
            if (ret != null && ret.Isok == true)
            {
                var result = new CheckTokenRes()
                {
                    code = ret.Code,
                    message = ret.Message,
                    Data = ret.Data
                };
                return result;
            }
            var retStr = ret == null ? "" : JsonConvert.SerializeObject(ret);
            var msg = $"请求登录接口出错，url：{ServiceConfig.GatewayUrl + reqUrl}，req：{JsonConvert.SerializeObject(req)}，ret：{retStr}";
            LogHelper.ErrorAsync(msg);
            return new CheckTokenRes() { code="80001", message="账号或密码错误" };
        }

        /// <summary>
        /// 创建一个错误返回
        /// </summary>
        public void CreateErrorResponse(AuthorizationFilterContext context, ErrorCode errorCode, string errorMsg, string token)
        {
            var controllerActionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
            var controllerName = controllerActionDescriptor?.ControllerName;
            var actionName = controllerActionDescriptor?.ActionName;
            string logMsg = $"未授权。[Controller：{controllerName}，Action：{actionName}，Token：{token}，Message：{errorMsg}。]";
            LogHelper.WarnAsync(logMsg);
            context.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Result = new JsonResult(new BaseResponse<object>() { Isok = false, Code = errorCode.ToString(), Message = errorMsg, Data = null });
        }

        /// <summary>
		/// 获取登录者信息
		/// </summary>
		/// <param name="data">登录信息</param>
		/// <returns>机场信息</returns>
		private LoginInfoRes GetLoginInfo(LoginData data)
        {
            LoginInfoReq req = new LoginInfoReq();
            var reqUrl = "/FlySystem/api/Login/GetLoginInfo";
            req.UserId = data.UserId;
            //从基础微服务获取登录者信息
            BaseClientHelper client = new BaseClientHelper(ServiceConfig.GatewayUrl);
            var response = client.ExecuteGet<LoginInfoRes>(reqUrl, req);
            if (response != null && response.Isok && response.Data != null)
            { response.Data.Token = data.Token; return response.Data; }
            else { return null; }
        }

        /// <summary>
		/// 自定义认证行为
		/// </summary>
		/// <param name="context"></param>
		public void OnCustomerAuthorization(AuthorizationFilterContext context)
        {

        }
    }
}
