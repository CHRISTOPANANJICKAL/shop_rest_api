using Microsoft.EntityFrameworkCore;
using WebApplication1.Models.Item;
using WebApplication1.Models.User;

namespace WebApplication1.Models;

public class AppDbContext(DbContextOptions options) : Microsoft.EntityFrameworkCore.DbContext(options)
{
    public DbSet<ItemModel> Items { get; init; }
    public DbSet<PriceModel> Prices { get; init; }
    public DbSet<ShopModel> Shops { get; init; }
    public DbSet<UserModel> Users { get; init; }
}