namespace Application.DTO.Customer.Request;

public class CustomerRequest
{
    public Guid? Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Cpf { get; set; }
    public DateTime BithDate { get; set; }
}
