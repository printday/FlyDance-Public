using Microsoft.AspNetCore.Mvc;

namespace Common.Base.Model
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FlyControllerBase : ControllerBase
    {
    }
}
