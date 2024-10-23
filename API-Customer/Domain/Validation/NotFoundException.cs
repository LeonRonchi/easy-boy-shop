using System.Net;

namespace Domain.Validation;

public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message) { }
}
