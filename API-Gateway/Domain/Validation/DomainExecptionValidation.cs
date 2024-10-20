namespace Gateway.Domain.Validation;

public class DomainExecptionValidation : Exception
{
    public DomainExecptionValidation(string error) : base(error)
    { }

    public static void When(bool hasError, string error)
    {
        if (hasError)
            throw new DomainExecptionValidation(error);
    }
}