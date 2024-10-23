using Application.DTO;
using Application.Interface;
using Domain.Validation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Incoming.Http;

[ApiController]
[Tags("Customer")]
public class CustomerController : ControllerBase
{
    private readonly ICustomerServices _customerService;

    public CustomerController(ICustomerServices customerService)
    {
        _customerService = customerService;
    }

    [HttpPost]
    [Route("/api/v1/customer")]
    public async Task<ActionResult<CustomerResponse>> Create([FromBody] CustomerRequest request)
    {
        try
        {
            var response = await _customerService.Create(request);
            var url = Url.Action(nameof(GetById), new { id = response.Id });

            return Created(url, response);
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }

    [HttpGet]
    [Route("/api/v1/customer/{id}")]
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

    [HttpGet]
    [Route("/api/v1/customers")]
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
