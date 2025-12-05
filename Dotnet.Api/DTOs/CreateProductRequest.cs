namespace Dotnet.Api.DTOs;

public class CreateProductRequest
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required decimal Price { get; set; }
    public required string Category { get; set; }
    public bool InStock { get; set; } = true;
}
