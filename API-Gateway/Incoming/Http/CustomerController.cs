using Gateway.Application.DTO.Log;
using Gateway.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Gateway.Incoming.Http;

[ApiController]
[Tags("Customer")]
public class CustomerController : ControllerBase
{

    private readonly ILogServices _logService;

    public CustomerController(ILogServices logService)
    {
        _logService = logService;
    }

    [HttpGet]
    [Route("/api-gateway/v1/customer/{id}")]
    public async Task<IActionResult> GetCustomer([FromRoute] string id)
    {
        LogRequest logRequest = new(HttpContext.Connection.RemoteIpAddress?.ToString(),
           $"{{\"Method\": \"{HttpContext.Request.Method}\", \"Path\": \"{HttpContext.Request.Path}\"}}");

        try
        {
            var account = new
            {
                Id = "1899992",
                Name = "Joao Santos",
                Email = "joao@teste.com"
            };

            logRequest.StatusCode = HttpStatusCode.OK.ToString();

            return Ok(account);
        }
        catch (Exception ex)
        {
            logRequest.StatusCode = HttpStatusCode.BadRequest.ToString();

            return BadRequest(ex);
        }
        finally
        {
            await _logService.Create(logRequest);
        }
    }

    [HttpGet]
    [Route("/api/v1/logs")]
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
