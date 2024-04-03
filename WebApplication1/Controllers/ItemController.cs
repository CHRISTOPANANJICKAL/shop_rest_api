using Microsoft.AspNetCore.Mvc;
using WebApplication1.Database;
using WebApplication1.Models;
using WebApplication1.utils;

namespace WebApplication1.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ItemController : ControllerBase
{
    [HttpPost]
    public ApiResponse AddItem([FromBody] ItemModel item)
    {
        var result = ItemsDatabase.AddItem(item);
        return result.Success
            ? new ApiResponse(new SuccessResponse(statusCode: 201, message: result.Message))
            : new ApiResponse(new ErrorResponse(statusCode: 400, message: result.Message));
    }


    [HttpGet("all")]
    public ApiResponse GetAllItems()
    {
        var result = ItemsDatabase.GetAllItems();
        return result.Success
            ? new ApiResponse(new SuccessResponse(200, message: result.Message, data: result.Data))
            : new ApiResponse(new ErrorResponse(400, message: result.Message));
    }

    [HttpGet("{id}")]
    public ApiResponse GetItem(string id)
    {
        var parsedId = Utils.ValidId(id);

        if (parsedId == null)
            return new ApiResponse(new ErrorResponse(400, message: "Invalid id"));

        var result = ItemsDatabase.GetItem(parsedId.Value);
        return result.Success
            ? new ApiResponse(new SuccessResponse(200, message: result.Message, data: result.Data))
            : new ApiResponse(new ErrorResponse(400, message: result.Message));
    }

    [HttpDelete("{id}")]
    public ApiResponse DeleteItem(string id)
    {
        var parsedId = Utils.ValidId(id);
        if (parsedId == null) return new ApiResponse(new ErrorResponse(400, message: "Invalid id"));

        var result = ItemsDatabase.DeleteItem(parsedId.Value);
        return result.Success
            ? new ApiResponse(new SuccessResponse(200, message: result.Message))
            : new ApiResponse(new ErrorResponse(400, message: result.Message));
    }

    [HttpPatch("{id}")]
    public ApiResponse UpdateItem(String id, ItemModel item)
    {
        var parsedId = Utils.ValidId(id);
        if (parsedId == null) return new ApiResponse(new ErrorResponse(400, message: "Invalid id"));

        var result = ItemsDatabase.UpdateItem(parsedId.Value, item);
        return result.Success
            ? new ApiResponse(new SuccessResponse(200, message: result.Message))
            : new ApiResponse(new ErrorResponse(400, message: result.Message));
    }
}