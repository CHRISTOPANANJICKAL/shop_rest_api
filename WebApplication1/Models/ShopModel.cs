using System.ComponentModel.DataAnnotations;
namespace WebApplication1.Models;

public class ShopModel
{
    [Required]
    public int Id { get; init; }
    [Required,MaxLength(length:250)]
    public string Name { get; init; } = "";
    [Required,MaxLength(length:500)]
    public string Location { get; init; } = "";
    [Required]
    public DateTime CreatedAt { get; init; }
    [Required]
    public DateTime UpdatedAt { get; init; }
}