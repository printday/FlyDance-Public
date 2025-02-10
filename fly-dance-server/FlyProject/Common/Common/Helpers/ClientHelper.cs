using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;

namespace Common.Helpers
{
    /// <summary>
    /// Http请求操作类
    /// </summary>
    public class ClientHelper
    {
        public RestClientOptions ClientOptions { get; set; }

        public RestClient Client { get; set; }

        public ClientHelper(string baseUrl)
        {
            this.ClientOptions = new RestClientOptions(baseUrl);
            this.Client = new RestClient(this.ClientOptions, configureSerialization: s => s.UseNewtonsoftJson());
            this.SetSettings();
        }

        public ClientHelper(Uri baseUrl)
        {
            this.ClientOptions = new RestClientOptions(baseUrl);
            this.Client = new RestClient(this.ClientOptions, configureSerialization: s => s.UseNewtonsoftJson());
            this.SetSettings();
        }

        protected void SetSettings()
        {
        }

        #region 发送请求类型（基本）
        /// <summary>
        /// 发送一个GET请求
        /// </summary>
        /// <returns></returns>
        public virtual RestResponse ExecuteGet(string resource, object paramObj = null, IDictionary<string, string> headerObj = null)
        {
            return this.Execute(resource, Method.Get, paramObj, headerObj);
        }

        /// <summary>
        /// 发送一个Post请求
        /// </summary>
        /// <returns></returns>
        public virtual RestResponse ExecutePost(string resource, object paramObj = null, IDictionary<string, string> headerObj = null)
        {
            return this.Execute(resource, Method.Post, paramObj, headerObj);
        }

        /// <summary>
        /// 发送一个Put请求
        /// </summary>
        /// <returns></returns>
        public virtual RestResponse ExecutePut(string resource, object paramObj = null, IDictionary<string, string> headerObj = null)
        {
            return this.Execute(resource, Method.Put, paramObj, headerObj);
        }

        /// <summary>
        /// 发送一个Delete请求
        /// </summary>
        /// <returns></returns>
        public virtual RestResponse ExecuteDelete(string resource, object paramObj = null, IDictionary<string, string> headerObj = null)
        {
            return this.Execute(resource, Method.Delete, paramObj, headerObj);
        }
        #endregion

        /// <summary>
        /// 发送请求
        /// </summary>
        /// <returns></returns>
        public virtual RestResponse Execute(string resource, Method method, object paramObj = null, IDictionary<string, string> headerObj = null)
        {
            RestRequest request = new RestRequest(resource, method);
            RestRequestWithParam(request, method, paramObj);
            return this.Client.Execute(request);
        }

        /// <summary>
        /// 处理参数
        /// </summary>
        protected void RestRequestWithParam(RestRequest request, Method method, object paramObj)
        {
            if (paramObj != null)
            {
                switch (method)
                {
                    case Method.Get:
                        var properties = paramObj.GetType().GetProperties();
                        foreach (var property in properties)
                        {
                            var value = property.GetValue(paramObj);
                            if (value != null)
                            {
                                request.AddParameter(property.Name, value.ToString());
                            }
                        }
                        break;
                    case Method.Post:
                    case Method.Put:
                    case Method.Delete:
                        request.AddJsonBody(paramObj);
                        break;
                    default:
                        request.AddObject(paramObj);
                        break;
                }
            }
        }

        /// <summary>
        /// 用于设置请求头
        /// </summary>
        protected void RestRequestWithHeader(RestRequest request, IDictionary<string, string> headerObj)
        {
            var newHeaderObj = RestRequestWithCustomHeader(request);
            if (newHeaderObj != null && newHeaderObj.Count > 0)
            {
                if (headerObj == null) headerObj = new Dictionary<string, string>();
                foreach (KeyValuePair<string, string> item in newHeaderObj)
                {
                    if (!headerObj.ContainsKey(item.Key))
                    {
                        headerObj.Add(item.Key, item.Value);
                    }
                }
            }
            if (headerObj != null)
            {
                foreach (KeyValuePair<string, string> item in headerObj)
                {
                    request.AddHeader(item.Key, item.Value);
                }
            }
        }

        #region Custom
        protected virtual IDictionary<string, string> RestRequestWithCustomHeader(RestRequest request)
        {
            return null;
        }
        #endregion
    }
}
