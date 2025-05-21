using System.Text.RegularExpressions;
using FluentValidation;
using SD.ArticlesAnalysis.Storage.Domain.Models;

namespace SD.ArticlesAnalysis.Storage.Domain.Validators;

public class ArticleMetaModelValidator: AbstractValidator<ArticleMetaModel>
{
    public ArticleMetaModelValidator()
    {
        RuleFor(m => m.Name)
            .NotNull()
            .NotEmpty()
            .Matches(@"\.txt$", RegexOptions.IgnoreCase)
            .WithMessage("Article must have .txt extension and be compatible with .txt format");
        RuleFor(m => m.Hash).NotNull().NotEmpty();
        RuleFor(m => m.Location).NotNull().NotEmpty();
    }
}