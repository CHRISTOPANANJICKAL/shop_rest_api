using Microsoft.AspNetCore.Mvc;
using WebApplication1.interfaces;
using WebApplication1.Models;
using WebApplication1.Models.common;
using WebApplication1.utils;

namespace WebApplication1.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ItemController(IItemRepository repository) : ControllerBase
{
    [HttpPost]
    public ApiResponse<ItemModel?> AddItem([FromBody] ItemModel item)
    {
        var dbResult = repository.AddItem(item);
        return new ApiResponse<ItemModel?>(result: dbResult);
    }


    [HttpGet("all")]
    public ApiResponse<List<ItemModel>> GetAllItems()
    {
        var dbResult = repository.GetAllItems();
        return new ApiResponse<List<ItemModel>>(dbResult);
    }

    [HttpGet("{id}")]
    public ApiResponse<ItemModel?> GetItem(string id)
    {
        var parsedId = Utils.ValidId(id);

        if (parsedId == null)
            return new ApiResponse<ItemModel?>(result: new DataBaseResult<ItemModel?>(data: null, success: false,
                message: "Invalid Id",
                statusCode: 400
            ));


        var dbResult = repository.GetItem(parsedId.Value);
        return new ApiResponse<ItemModel?>(result: dbResult);
    }

    [HttpDelete("{id}")]
    public ApiResponse<ItemModel?> DeleteItem(string id)
    {
        var parsedId = Utils.ValidId(id);

        if (parsedId == null)
            return new ApiResponse<ItemModel?>(result: new DataBaseResult<ItemModel?>(data: null, success: false,
                message: "Invalid Id",
                statusCode: 400
            ));


        var dbResult = repository.DeleteItem(parsedId.Value);
        return new ApiResponse<ItemModel?>(result: dbResult);
    }

    [HttpPatch("{id}")]
    public ApiResponse<ItemModel?> UpdateItem(String id, ItemModel item)
    {
        var parsedId = Utils.ValidId(id);

        if (parsedId == null)
            return new ApiResponse<ItemModel?>(result: new DataBaseResult<ItemModel?>(data: null, success: false,
                message: "Invalid Id",
                statusCode: 400
            ));
        var dbResult = repository.UpdateItem(parsedId.Value, item);
        return new ApiResponse<ItemModel?>(result: dbResult);
    }
}