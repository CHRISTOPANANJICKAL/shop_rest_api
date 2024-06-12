using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models;

public class PriceModel
{
    [Required]
    public int Id { get; init; }
    [Required, Range(0,double.MaxValue)]
    public double Price { get; init; }
    [Required, Range(0,double.MaxValue)]
    public double Quantity { get; init; }
    [Required]
    public Unit Unit { get; init; }
    [Required,Range(0,int.MaxValue)]
    public int ItemId { get; init; }
    [Required,Range(0,int.MaxValue)]
    public int ShopId { get; init; }
    [Required]
    public DateTime CreatedAt { get; init; }
    [Required]
    public DateTime UpdatedAt { get; init; }
}