using Common.Base.Model;
using Common.Helpers;
using Common.Models.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Huaju.Ai.Controllers
{
    public class IndexController : HuajuAiControllerBase
    {
        public AiHelper _aiHelper { get; set; }

        public IndexController(AiHelper aiHelper)
        {
            _aiHelper = aiHelper;
        }

        /// <summary>
        /// 聊天
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        [HttpGet]
        public BaseResponse<Root> Chat(string text)
        {
            return this._aiHelper.SendChat(text);
        }
    }
}
