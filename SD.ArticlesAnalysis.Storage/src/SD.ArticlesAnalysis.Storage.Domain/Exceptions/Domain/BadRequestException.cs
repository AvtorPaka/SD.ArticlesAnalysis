using System.Text;
using FluentValidation;

namespace SD.ArticlesAnalysis.Storage.Domain.Exceptions.Domain;

public class BadRequestException: DomainException
{
    private ValidationException ValidationException { get;}
    
    public BadRequestException(string? message, ValidationException innerException) : base(message, innerException)
    {
        ValidationException = innerException;
    }
    
    public override string ToString()
    {
        StringBuilder stringBuilder = new StringBuilder();

        foreach (var failure in ValidationException.Errors)
        {
            stringBuilder.Append($"Field: {failure.PropertyName}, Violation: {failure.ErrorMessage}\n");
        }

        return stringBuilder.ToString();
    }
}