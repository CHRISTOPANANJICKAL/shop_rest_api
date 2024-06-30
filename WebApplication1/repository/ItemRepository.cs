using WebApplication1.interfaces;
using WebApplication1.Models;
using WebApplication1.Models.common;

namespace WebApplication1.repository;

public class ItemRepository(AppDbContext context) : IItemRepository
{
    public DataBaseResult<List<ItemModel>> GetAllItems()
    {
        var items = context.Items.ToList();
        return new DataBaseResult<List<ItemModel>>(data: items, success: true, message: "Ok", statusCode: 200);
    }

    public DataBaseResult<ItemModel?> AddItem(ItemModel item)
    {
        try
        {
            item.CreatedAt = DateTime.Now;
            item.UpdatedAt = item.CreatedAt;
            var savedItem = context.Items.Add(item);
            var saved = context.SaveChanges();
            return saved > 0
                    ? new DataBaseResult<ItemModel?>(data: savedItem.Entity, true, "Item added successfully",
                        statusCode: 201)
                    : new DataBaseResult<ItemModel?>(data: null, false, "Error in adding item", statusCode: 400)
                ;
        }
        catch (Exception)
        {
            return new DataBaseResult<ItemModel?>(data: null, false, "Error in adding item", statusCode: 400);
        }
    }

    public DataBaseResult<ItemModel?> GetItem(int id)
    {
        var dbResult = GetAllItems();

        if (dbResult.Data == null || !dbResult.Success)
        {
            return new DataBaseResult<ItemModel?>(data: null, success: false, message: "No item found",
                statusCode: 404);
        }

        var item = dbResult.Data.Find((s) => s.Id == id);

        return item != null
            ? new DataBaseResult<ItemModel?>(data: item, success: true, message: "Ok", statusCode: 200)
            : new DataBaseResult<ItemModel?>(data: null, success: false, message: "No item found", statusCode: 404);
    }


    public DataBaseResult<ItemModel?> DeleteItem(int id)
    {
        try
        {
            var getResult = GetItem(id);
            if (!getResult.Success || getResult.Data == null)
            {
                return getResult;
            }


            context.Items.Remove(getResult.Data);
            var saved = context.SaveChanges();
            return saved > 0
                    ? new DataBaseResult<ItemModel?>(data: null, true, "Item deleted successfully",
                        statusCode: 200)
                    : new DataBaseResult<ItemModel?>(data: null, false, "Error in deleting item", statusCode: 400)
                ;
        }
        catch (Exception)
        {
            return new DataBaseResult<ItemModel?>(data: null, false, "Error in deleting item", statusCode: 400);
        }
    }

    public DataBaseResult<ItemModel?> UpdateItem(int id, ItemModel item)
    {
        try
        {
            var getResult = GetItem(id);
            if (!getResult.Success || getResult.Data == null)
            {
                return getResult;
            }


            getResult.Data.UpdateFrom(item: item);
            getResult.Data.UpdatedAt = DateTime.Now;

            var updatedItem = context.Items.Update(getResult.Data);
            var saved = context.SaveChanges();
            return saved > 0
                    ? new DataBaseResult<ItemModel?>(data: updatedItem.Entity, true, "Item updated successfully",
                        statusCode: 200)
                    : new DataBaseResult<ItemModel?>(data: null, false, "Error in updating item", statusCode: 400)
                ;
        }
        catch (Exception)
        {
            return new DataBaseResult<ItemModel?>(data: null, false, "Error in updating item", statusCode: 400);
        }
    }
}