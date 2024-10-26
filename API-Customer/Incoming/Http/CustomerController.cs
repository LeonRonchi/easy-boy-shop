using Application.DTO;
using Application.Interface;
using Domain.Exception;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Incoming.Http;

[ApiController]
[Route("/api/v1/customer")]
[Tags("Customer")]
public class CustomerController : ControllerBase
{
    private readonly ICustomerServices _customerService;

    public CustomerController(ICustomerServices customerService)
    {
        _customerService = customerService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(CustomerResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Create([FromBody] CustomerRequest request)
    {
        var response = await _customerService.Create(request);
        var url = Url.Action(nameof(GetById), new { id = response.Id });

        return Created(url, response);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(CustomerResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CustomerResponse>> GetById([FromRoute] Guid id)
    {
        try
        {
            var response = await _customerService.GetCustomerById(id);

            return Ok(response); 
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex);
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }

    [HttpGet("all")]
    [ProducesResponseType(typeof(CustomerResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CustomerResponse>> GetCustomers()
    {
        try
        {
            var response = await _customerService.GetCustomers();
            
            if (!response.Any())
            {
                return NotFound(response);
            }

            return Ok(response);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex);
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }
}
