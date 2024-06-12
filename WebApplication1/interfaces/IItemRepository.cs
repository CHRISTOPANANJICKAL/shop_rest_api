using WebApplication1.Models;
using WebApplication1.Models.common;

namespace WebApplication1.interfaces;

public interface IItemRepository
{
    DataBaseResult<List<ItemModel>> GetAllItems();
    DataBaseResult<ItemModel?> GetItem(int id);
    DataBaseResult<ItemModel?> AddItem(ItemModel item);
    DataBaseResult<ItemModel?> UpdateItem(int id, ItemModel item);
    DataBaseResult<ItemModel?> DeleteItem(int id);
}