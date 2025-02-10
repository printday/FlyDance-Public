using Common.Base.Model;
using Consul;
using Microsoft.AspNetCore.Http.Headers;
using Newtonsoft.Json;
using RestSharp;
using System.Text;

namespace Common.Helpers
{
    /// <summary>
    /// Fly Http帮助类
    /// </summary>
    public class HttpHelper : ClientHelper, IDisposable
    {
        public HttpHelper(string baseUrl) : base(baseUrl)
        {
        }

        /// <summary>
        /// 发送一个get请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="paramObj"></param>
        /// <param name="headerObj"></param>
        /// <returns></returns>
        public T SendGet<T>(string url, object paramObj = null, IDictionary<string, string> headerObj = null) where T : new()
        {
            return this.ExecuteSingle<T>(url, Method.Get, paramObj, headerObj);
        }

        /// <summary>
        /// 发送一个post请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="paramObj"></param>
        /// <param name="headerObj"></param>
        /// <returns></returns>
        public T SendPost<T>(string url, object paramObj = null, IDictionary<string, string> headerObj = null) where T : new()
        {
            return this.ExecuteSingle<T>(url, Method.Post, paramObj, headerObj);
        }

        /// <summary>
        /// 匹配项目返回类的请求方式
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url">请求路径</param>
        /// <param name="method">请求类型</param>
        /// <param name="paramObj">请求参数</param>
        /// <param name="headerObj">请求头</param>
        /// <returns></returns>
        public T ExecuteSingle<T>(string url, Method method, object paramObj = null, IDictionary<string, string> headerObj = null) where T : new()
        {
            headerObj.Add("Content-Type", "application/json");  // 加上Json
            var request = new RestRequest(url, method);
            RestRequestWithParam(request, method, paramObj);
            RestRequestWithHeader(request, headerObj);
            RestResponse<T> response = this.Client.Execute<T>(request);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                if (response.Data == null) { response.Data = new T(); }
            }
            return response.Data ?? new T();
        }

        /// <summary>
        /// 发送一个get请求（带BaseResponse）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="paramObj"></param>
        /// <param name="headerObj"></param>
        /// <returns></returns>
        public BaseResponse<T> SendGetResponse<T>(string url, object paramObj = null, IDictionary<string, string> headerObj = null) where T : class
        {
            return this.ExecuteSingleResponse<T>(url, Method.Get, paramObj, headerObj);
        }

        /// <summary>
        /// 发送一个post请求（带BaseResponse）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="paramObj"></param>
        /// <param name="headerObj"></param>
        /// <returns></returns>
        public BaseResponse<T> SendPostResponse<T>(string url, object paramObj = null, IDictionary<string, string> headerObj = null) where T :class
        {
            return this.ExecuteSingleResponse<T>(url, Method.Post, paramObj, headerObj);
        }

        /// <summary>
        /// 匹配项目返回类的请求方式（带BaseResponse）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url">请求路径</param>
        /// <param name="method">请求类型</param>
        /// <param name="paramObj">请求参数</param>
        /// <param name="headerObj">请求头</param>
        /// <returns></returns>
        public BaseResponse<T> ExecuteSingleResponse<T>(string url, Method method, object paramObj = null, IDictionary<string, string> headerObj = null) where T : class
        {
            headerObj.Add("Content-Type", "application/json");  // 加上Json
            var request = new RestRequest(url, method);
            RestRequestWithParam(request, method, paramObj);
            RestRequestWithHeader(request, headerObj);
            RestResponse<T> response = this.Client.Execute<T>(request);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                if (response.Data == null) { response.Data = null; }
            }
            return new BaseResponse<T>()
            {
                Isok = response.StatusCode == System.Net.HttpStatusCode.OK,
                Code = response.StatusCode.ToString(),
                Message = response.ErrorMessage,
                Data = JsonConvert.DeserializeObject<T>(response.Content)
            };
        }

        public void Dispose()
        { // GC 回收时，调用Dispose方法
        }
    }
}
