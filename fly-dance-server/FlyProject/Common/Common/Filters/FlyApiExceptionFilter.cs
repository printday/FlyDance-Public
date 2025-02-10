using Common.Base.Model;
using Common.Filters.Models;
using Common.Helpers;
using Common.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System.Text;

namespace Common.Filters
{
    public class FlyApiExceptionFilter : IExceptionFilter, IActionFilter
    {
        private IDictionary<string, object> ActionArguments;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="environment"></param>
        public FlyApiExceptionFilter(IWebHostEnvironment environment) { }

        #region ExceptionFilter
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is FlyException flyException)
            {
                context.ExceptionHandled = true;
                context.Result = new JsonResult(new BaseResponse<object>() { Isok = false, Code = flyException.GetErrorCode(), Message = flyException.Message, Data = null });
                return;
            }
            StringBuilder sb = new StringBuilder();

            var host = context.HttpContext.Request.Host;
            var path = context.HttpContext.Request.Path;

            sb.Append("url:" + host + path + Environment.NewLine);
            sb.Append("arguments:" + GetArguments(context) + Environment.NewLine);
            sb.Append("userInfo:" + GetUserInfo(context) + Environment.NewLine);
            if (context.Exception != null)
            {
                sb.Append("exception info:");
                sb.Append(context.Exception.Message + Environment.NewLine);
                sb.Append(context.Exception.StackTrace + Environment.NewLine);
                sb.Append(context.Exception.Source + Environment.NewLine);
            }
            if (context.Exception.InnerException != null)
            {
                sb.Append("inner exception info:");
                sb.Append(context.Exception.InnerException.Message + Environment.NewLine);
                sb.Append(context.Exception.InnerException.StackTrace + Environment.NewLine);
                sb.Append(context.Exception.InnerException.Source + Environment.NewLine);
            }
            LogHelper.ErrorAsync(sb.ToString());

            context.ExceptionHandled = true;
            context.Result = new JsonResult(new BaseResponse<object>() { Isok = false, Code = "-1", Message = "程序错误", Data = null });

        }

        /// <summary>
        /// 获取参数信息
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private string GetArguments(ExceptionContext context)
        {
            StringBuilder argsb = new StringBuilder();
            if (ActionArguments != null && ActionArguments.Count > 0)
            {
                foreach (var item in ActionArguments)
                {
                    if (item.Value != null)
                    {
                        argsb.Append(item.Key + ":" + JsonConvert.SerializeObject(item.Value) + "。");
                    }
                }
            }
            return argsb.ToString();
        }

        /// <summary>
        /// 获取当前用户信息
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private string GetUserInfo(ExceptionContext context)
        {
            if (context != null && context.HttpContext != null && context.HttpContext.User != null
                && context.HttpContext.User is FlyClaimsPrincipal)
            {
                if (((FlyClaimsPrincipal)context.HttpContext.User).LoginInfo != null)
                {
                    return JsonConvert.SerializeObject(((FlyClaimsPrincipal)context.HttpContext.User).LoginInfo);
                }
            }
            return string.Empty;
        }
        #endregion

        #region ActionFilter
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            ActionArguments = context.ActionArguments;
        }

        /// <summary>
        /// 请求完成后执行
        /// </summary>
        /// <param name="context"></param>

        public void OnActionExecuted(ActionExecutedContext context)
        {

        }
        #endregion
    }
}
