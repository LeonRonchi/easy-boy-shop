using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Incoming.Http;

[ApiController]
[Tags("Log")]
[Route("/api-gateway/v1/log")]
public class LogController : ControllerBase
{
    private readonly ILogServices _logService;

    public LogController(ILogServices logService)
    {
        _logService = logService;
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetLogs()
    {
        try
        {
            var request = await _logService.GetLogs();

            return Ok(request);
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }
}
