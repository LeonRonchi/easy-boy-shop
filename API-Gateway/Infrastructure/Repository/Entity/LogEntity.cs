namespace Gateway.Infrastructure.Repository.Entity;

public class LogEntity
{
    public Guid? Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string? ClientIP { get; set; }
    public string? Method { get; set; }
    public string? Request { get; set; }
    public string? Response { get; set; }
    public DateTime Date { get; set; }
    public string? StatusCode { get; set; }
    public string? Message { get; set; }      
}
