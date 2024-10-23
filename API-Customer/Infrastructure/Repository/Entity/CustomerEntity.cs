namespace Infrastructure.Repository.Entity;

public class CustomerEntity
{
    public Guid? Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Cpf { get; set; }
    public DateTime BithDate { get; set; }
    public DateTime RegisterDate { get; set; }
}
