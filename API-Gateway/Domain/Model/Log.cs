using Domain.Validation;

namespace Domain.Entities;

public sealed class Log : Entity
{
    public string? ClientIP { get; private set; }
    public string? Method { get; private set; }
    public string? Request { get; private set; }
    public string? Response { get; private set; }
    public DateTime Date { get; private set; }
    public string? StatusCode { get; private set; }
    public string? Message { get; private set; }

    public Log(Guid? id, string clientIP, string method, string request, string response,
        DateTime date, string statusCode, string message)
    {
        base.Id = id;

        ValidateDomain(clientIP, method, request, response, date, statusCode, message);
    }

    private void ValidateDomain(string clientIP, string method, string request, string response,
        DateTime date, string statusCode, string message)
    {
        // ClientIP
        DomainExecptionValidation.When(string.IsNullOrEmpty(clientIP),
               "IP do Cliente não informado");

        // Date
        DomainExecptionValidation.When(date == DateTime.MinValue,
              "Horário da Requesição não informado");

        // Método
        DomainExecptionValidation.When(string.IsNullOrEmpty(method),
              "Método HTTP não informado");

        // StatusCode
        DomainExecptionValidation.When(string.IsNullOrEmpty(statusCode),
              "Código de Status HTTP não informado");

        // Finished Validation - Insert Data
        ClientIP = clientIP;
        Method = method;
        Request = request;
        Response = response;
        Date = date;
        StatusCode = statusCode;
        Message = message;
    }
}