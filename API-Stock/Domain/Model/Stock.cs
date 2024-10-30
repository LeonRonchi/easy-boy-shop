using Domain.Exception;

namespace Domain.Model;

public class Stock
{
    public Guid? Id { get; set; }
    public Guid IdProduct { get; set; }
    public Guid IdSeller { get; set; }
    public int Quantidade { get; set; }

    public Stock (Guid? id, Guid idProduct, Guid idSeller, int quantidade)
    {
        ValidateDomain(quantidade);
        Id = id;
        IdProduct = idProduct;
        IdSeller = idSeller;
        Quantidade = quantidade;
    }

    private void ValidateDomain(int quantidade)
    {
        DomainExecptionValidation.When(quantidade < 0, "Quantidade menor que 0");
    }
}
