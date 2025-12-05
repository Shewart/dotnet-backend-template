using Dotnet.Core.Entities;
using FluentValidation;

namespace Dotnet.Api.Validators;

public class ProductValidator : AbstractValidator<Product>
{
    private readonly string[] _validCategories = ["Electronics", "Books", "Clothing", "Home", "Sports", "Automotive"];

    public ProductValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("Product name is required")
            .Length(2, 100).WithMessage("Product name must be between 2 and 100 characters");

        RuleFor(p => p.Description)
            .MaximumLength(500).WithMessage("Description cannot exceed 500 characters")
            .When(p => !string.IsNullOrEmpty(p.Description));

        RuleFor(p => p.Price)
            .GreaterThan(0).WithMessage("Price must be greater than 0")
            .LessThanOrEqualTo(1000000).WithMessage("Price cannot exceed $1,000,000");

        RuleFor(p => p.Category)
            .NotEmpty().WithMessage("Category is required")
            .Must(BeValidCategory).WithMessage($"Category must be one of: {string.Join(", ", _validCategories)}");

        RuleFor(p => p.InStock)
            .NotNull().WithMessage("InStock status is required");

        // Custom validation rule for business logic
        RuleFor(p => p)
            .Must(HaveValidNameForCategory)
            .WithMessage("Electronics products cannot contain certain keywords")
            .When(p => p.Category == "Electronics");
    }

    private bool BeValidCategory(string category)
    {
        return _validCategories.Contains(category);
    }

    private bool HaveValidNameForCategory(Product product)
    {
        if (product.Category == "Electronics" && !string.IsNullOrEmpty(product.Name))
        {
            var restrictedWords = new[] { "illegal", "banned", "forbidden" };
            return !restrictedWords.Any(word =>
                product.Name.Contains(word, StringComparison.OrdinalIgnoreCase));
        }
        return true;
    }
}
