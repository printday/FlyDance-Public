using Common.Base.Model;
using RestSharp;

namespace Common.Helpers
{
    public class BaseClientHelper : ClientHelper
    {
        public BaseClientHelper(string baseUrl) : base(baseUrl)
        {
        }

        #region 发送请求类型（规定返回类型T）
        /// <summary>
        /// 发送一个GET请求
        /// </summary>
        /// <returns></returns>
        public BaseResponse<T> ExecuteGet<T>(string resource, object paramObj = null, IDictionary<string, string> headerObj = null) where T : new()
        {
            return this.ExecuteSingle<T>(resource, Method.Get, paramObj, headerObj);
        }

        /// <summary>
        /// 发送一个Post请求
        /// </summary>
        /// <returns></returns>
        public BaseResponse<T> ExecutePost<T>(string resource, object paramObj = null, IDictionary<string, string> headerObj = null) where T : new()
        {
            return this.ExecuteSingle<T>(resource, Method.Post, paramObj, headerObj);
        }

        /// <summary>
        /// 发送一个Put请求
        /// </summary>
        /// <returns></returns>
        public BaseResponse<T> ExecutePut<T>(string resource, object paramObj = null, IDictionary<string, string> headerObj = null) where T : new()
        {
            return this.ExecuteSingle<T>(resource, Method.Put, paramObj, headerObj);
        }

        /// <summary>
        /// 发送一个Delete请求
        /// </summary>
        /// <returns></returns>
        public BaseResponse<T> ExecuteDelete<T>(string resource, object paramObj = null, IDictionary<string, string> headerObj = null) where T : new()
        {
            return this.ExecuteSingle<T>(resource, Method.Delete, paramObj, headerObj);
        }
        #endregion

        /// <summary>
        /// 匹配项目返回类的请求方式
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url">请求路径</param>
        /// <param name="method">请求类型</param>
        /// <param name="paramObj">请求参数</param>
        /// <param name="headerObj">请求头</param>
        /// <returns></returns>
        public BaseResponse<T> ExecuteSingle<T>(string url, Method method, object paramObj = null, IDictionary<string, string> headerObj = null) where T : new()
        {
            //var request = new RestRequest(url, method);
            var request = new RestRequest(url, method);
            RestRequestWithParam(request, method, paramObj);
            RestRequestWithHeader(request, headerObj);
            RestResponse<BaseResponse<T>> response = this.Client.Execute<BaseResponse<T>>(request);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                if (response.Data == null) { response.Data = new BaseResponse<T>(); }
                response.Data.Isok = false;
                response.Data.Message = response.ErrorMessage ?? "";
                response.Data.Code = response.StatusCode.ToString();
            }
            return response.Data ?? new BaseResponse<T>();
        }
    }
}
