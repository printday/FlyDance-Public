using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Base.Model
{
    [Produces("application/json")]
    [Route("huaju-ai/[controller]/[action]")]
    [ApiController]
    public class HuajuAiControllerBase : ControllerBase
    {
    }
}
