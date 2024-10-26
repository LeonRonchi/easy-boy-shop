using System.ComponentModel.DataAnnotations;

namespace Application.DTO.Log;

public class LogRequest
{
    private string _request;
    private string _response;

    public Guid? Id { get; set; }
    public string ClientIP { get; set; }
   
    [MaxLength(100)]
    public string Method { get; set; }

    public string StatusCode { get; set; }

    public DateTime Date { get; set; }

    [MaxLength(100)]
    public string Message { get; set; }

    public string Request
    {
        get => _request;
        set
        {
            _request = value.Length > 2000 ? value.Substring(0, 2000) : value;
        }
    }

    public string Response
    {
        get => _response;
        set
        {
            _response = value.Length > 2000 ? value.Substring(0, 2000) : value;
        }
    }

    public LogRequest(string? clientIP, string method)
    {
        Id = null;
        ClientIP = clientIP ?? "";
        Method = method;
        _request = "{}";
        _response = "{}";
        Date = DateTime.Now;
        StatusCode = string.Empty;
        Message = string.Empty;
    }
}
