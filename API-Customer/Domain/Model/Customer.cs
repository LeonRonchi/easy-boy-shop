using Domain.Validation;
using System.Text.RegularExpressions;

namespace Domain.Model;

public sealed class Customer 
{
    public Guid? Id { get; private set; }
    public string? Name { get; private set; }
    public string? Email { get; private set; }
    public string? Cpf { get; private set; }
    public DateTime BithDate { get; private set; }
    public DateTime RegisterDate { get; private set; }

    public Customer(Guid? id, string name, string email, string cpf, DateTime bithDate)
    {
        var registerDate = DateTime.Now;
        ValidateDomain(name, email, cpf, bithDate, registerDate);

        Id = id;
        Name = name;
        Email = email;
        Cpf = cpf;
        BithDate = bithDate;
        RegisterDate = registerDate;
    }

    public Customer(Guid? id, string name, string email, string cpf, DateTime bithDate, DateTime registerDate)
    {
        ValidateDomain(name, email, cpf, bithDate, registerDate);

        Id = id;
        Name = name;
        Email = email;
        Cpf = cpf;
        BithDate = bithDate;
        RegisterDate = registerDate;
    }

    private void ValidateDomain(string name, string email, string cpf, DateTime bithDate, DateTime registerDate)
    {
        Regex regex;

        // Name
        string trimName = name.Trim();
        DomainExecptionValidation.When(string.IsNullOrEmpty(trimName),
               "Nome não informado");

        DomainExecptionValidation.When(trimName.Length > 100,
               "Nome maior que 100 caracteres");

        // Email
        regex = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
        string trimEmail = email.Trim();
        DomainExecptionValidation.When(string.IsNullOrEmpty(trimEmail),
               "E-mail não informado");

        DomainExecptionValidation.When(trimEmail.Length > 100,
               "E-mail maior que 100 caracteres");

        DomainExecptionValidation.When(!regex.IsMatch(trimEmail),
               "E-mail inválido");

        // CPF
        string trimCpf = regex.Replace(cpf.Trim(), "");
        DomainExecptionValidation.When(string.IsNullOrEmpty(trimCpf),
               "CPF não informado");

        DomainExecptionValidation.When(trimCpf.Length != 11,
               "CPF inválido");

        // Bith Date
        DomainExecptionValidation.When(bithDate == DateTime.MinValue,
               "Data de Nascimento inválida");

        // Register Date
        DomainExecptionValidation.When(registerDate == DateTime.MinValue,
               "Data de Registro inválida");
    }
}
