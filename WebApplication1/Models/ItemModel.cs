using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models;

public class ItemModel
{
    public int Id { get; init; }
    [Required] [MaxLength(length: 200)] public string ItemName { get; set; } = "";
    [MaxLength(length: 500)] public string Description { get; set; } = "";
    [Required] public DateTime CreatedAt { get; set; }
    [Required] public DateTime UpdatedAt { get; set; }


    public ItemModel UpdateFrom(ItemModel item)
    {
        ItemName = item.ItemName;
        Description = item.Description;

        return this;
    }
}