

using AgentAI.DTO.Request;
using AgentAI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgentAI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AIController(IChatService chatService) : ControllerBase
    {
        [HttpPost("chat")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Chat([FromBody] ChatRequestDto request)
        {
           var res=await chatService.AskAsync(request.Message);   
            return Ok(res);
        }
    }
}
