using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models;

public class ItemContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<ItemModel> Items { get; init; }
    public DbSet<PriceModel> Prices { get; init; }
    public DbSet<ShopModel> Shops { get; init; }
}