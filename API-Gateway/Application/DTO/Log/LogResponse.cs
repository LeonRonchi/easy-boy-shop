using System.ComponentModel.DataAnnotations;

namespace Application.DTO.Log;

public class LogResponse
{
    private string _request;
    private string _response;

    public Guid? Id { get; set; }

    [MaxLength(50)]
    public string ClientIP { get; set; }

    [MaxLength(50)]
    public string Method { get; set; }

    [MaxLength(20)]
    public string StatusCode { get; set; }


    [MaxLength(2000)]
    public string Request
    {
        get => _request;
        set
        {
            _request = value.Length > 2000 ? value.Substring(0, 2000) : value;
        }
    }

    [MaxLength(2000)]
    public string Response
    {
        get => _response;
        set
        {
            _response = value.Length > 2000 ? value.Substring(0, 2000) : value;
        }
    }

    public DateTime Date { get; set; }

    [MaxLength(200)]
    public string Message { get; set; }

    public LogResponse() { }

    public LogResponse(string? clientIP, string method)
    {
        Id = Guid.Empty;
        ClientIP = clientIP ?? "";
        Method = method;
        _request = "{}";
        _response = "{}";
        Date = DateTime.Now;
        StatusCode = string.Empty;
        Message = string.Empty;
    }
}
