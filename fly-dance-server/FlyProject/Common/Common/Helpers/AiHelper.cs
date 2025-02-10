using Common.Base.Model;
using Common.Const;
using Common.Models.Responses;
using Newtonsoft.Json;

namespace Common.Helpers
{
    /// <summary>
    /// AI 帮助类
    /// </summary>
    public class AiHelper : IDisposable
    {
        public string Characters {  get; set; }

        private bool disposed = false; // 标识是否已经调用过Dispose

        private AiConst _aiConst = new();

        private IDictionary<string, string> _headers = new Dictionary<string, string>();

        public AiHelper(AiConst aiConst)
        {
            if (aiConst == null) throw new ArgumentNullException(nameof(aiConst));
            this._aiConst = aiConst;
            this.Characters = aiConst.Characters ?? throw new Exception("未能读取到ai的默认配置");
            this._headers.Add("Authorization", $"Bearer {_aiConst.Aliyun_API_Key}");
        }

        #region 对话引擎
        /// <summary>
        /// 聊天对话引擎
        /// </summary>
        /// <param name="characters">特定人设(不填按实例化时的默认人设)</param>
        public BaseResponse<Root> SendChat(string text)
        {
            using (HttpHelper http = new HttpHelper(_aiConst.Aliyun_Api_Base_Url ?? throw new Exception("未能读取到TongYi的接口服务地址配置")))
            {
                var requestBody = new
                {
                    model = "qwen-plus",
                    messages = new[]
                    {
                        new { role = "system", content = this._aiConst.Characters },
                        new { role = "user", content = text }
                    }
                };
                return http.SendPostResponse<Root>("", requestBody, this._headers);
            }
        }
        #endregion

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing) { }
                disposed = true;    // 标识对象已释放
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
