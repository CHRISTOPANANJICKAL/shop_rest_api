using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models;

public class AppDbContext(DbContextOptions options) : Microsoft.EntityFrameworkCore.DbContext(options)
{
    public DbSet<ItemModel> Items { get; init; }
    public DbSet<PriceModel> Prices { get; init; }
    public DbSet<ShopModel> Shops { get; init; }
    public DbSet<UserModel.UserModel> Users { get; init; }
}