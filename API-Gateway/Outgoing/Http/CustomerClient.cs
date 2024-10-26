
using Application.Client;
using Application.DTO.Customer.Request;
using Application.DTO.Customer.Response;
using Application.Interface;
using Domain.Exception;
using Outgoing.Http.Refit;
using Refit;
using System.Net;

namespace Outgoing.Http;

public class CustomerClient : ICustomerClient
{
    private readonly ICustomerRefitClient _customertRefitClient;
    private readonly ICustomerMapper _customerMapper;

    public CustomerClient(ICustomerRefitClient customertRefitClient, ICustomerMapper customerMapper)
    {
        _customertRefitClient = customertRefitClient;
        _customerMapper = customerMapper;
    }

    public async Task<CustomerResponse> CreateCustomerAsync(CustomerRequest request)
    {
        try
        {
            var customerDto = await _customertRefitClient.CreateAsync(request);

            return _customerMapper.ToCustomerResponse(customerDto);
        }
        catch (ApiException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
        {
            throw new NotFoundException($"Customer ID {request.Id} not found.");
        }
    }

    public async Task<IEnumerable<CustomerResponse>> GetCustomerAllAsync()
    {
        try
        {
            var customersDto = await _customertRefitClient.GetCustomerAllAsync();

            if (!customersDto.Any())
            {
                throw new NotFoundException("No clients found.");
            }

            return customersDto.Select(customer => _customerMapper.ToCustomerResponse(customer));
        }
        catch (ApiException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
        {
            throw new NotFoundException("No clients found.");
        }
    }

    public async Task<CustomerResponse> GetCustomerByIdAsync(CustomerRequest request)
    {
        try
        {
            var customerDto = await _customertRefitClient.GetCustomerByIdAsync(request.Id);

            return _customerMapper.ToCustomerResponse(customerDto);
        }
        catch (ApiException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
        {
            throw new NotFoundException($"Customer ID {request.Id} not found.");
        }
    }

   









    //private readonly IWalletRefitClient _walletRefitClient;
    //private readonly IWalletMapper _walletMapper;


}
