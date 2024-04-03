using WebApplication1.Models;

namespace WebApplication1.Database;

public abstract class ItemsDatabase
{
    private static int _maxLength;
    private static readonly List<ItemModel> Items = [];

    public static DataBaseResult<ItemModel?> AddItem(ItemModel item)
    {
        if (Items.Exists(model => model.Id == item.Id))
        {
            return new DataBaseResult<ItemModel?>(success: false, message: "Item already exist", data: null);
        }

        _maxLength += 1;
        item.Id = _maxLength;
        Items.Add(item);
        return new DataBaseResult<ItemModel?>(success: true, message: "Item added successfully", data: item);
    }

    public static DataBaseResult<ItemModel?> GetItem(int id)
    {
        var item = Items.Find((model => model.Id == id));

        return item == null
            ? new DataBaseResult<ItemModel?>(data: null, success: false, message: "Item not found")
            : new DataBaseResult<ItemModel?>(data: item, success: true, message: "Item fetched successfully");
    }

    public static DataBaseResult<List<ItemModel>> GetAllItems()
    {
        return new DataBaseResult<List<ItemModel>>(data: Items, success: true, message: "Items fetched successfully");
    }

    public static DataBaseResult<ItemModel?> DeleteItem(int id)
    {
        var item = Items.Find(model => model.Id == id);
        if (item == null) return new DataBaseResult<ItemModel?>(data: null, success: false, message: "Item not found");
        Items.Remove(item);
        return new DataBaseResult<ItemModel?>(data: item, success: true, message: "Item deleted successfully");
    }

    public static DataBaseResult<ItemModel?> UpdateItem(int id, ItemModel itemModel)
    {
        var item = Items.Find(model => model.Id == id);
        if (item == null) return new DataBaseResult<ItemModel?>(data: null, success: false, message: "Item not found");
        Items.Remove(item);
        itemModel.Id = id;
        Items.Add(itemModel);

        return new DataBaseResult<ItemModel?>(data: itemModel, success: true, message: "Item updated successfully");
    }
}