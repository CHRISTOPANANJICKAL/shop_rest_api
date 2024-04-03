using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models;

public class ItemModel
{
    public int? Id { get; set; }
    [Required] public string ItemName { get; set; } = "";
    [Required, Range(0, double.MaxValue)] public double? Quantity { get; set; }
    [Required, Range(0, double.MaxValue)] public double? Price { get; set; }
}