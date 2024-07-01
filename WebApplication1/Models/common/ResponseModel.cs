using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace WebApplication1.Models.common;

public class ApiResponse<T> : ObjectResult
{
    public ApiResponse(DataBaseResult<T> result) : base(result)
    {
        StatusCode = result.StatusCode;
    }

    public static IActionResult GenerateErrorResponseModel(ActionContext context)
    {
        List<string> apiError = [];
        var errorList = context.ModelState.AsEnumerable();

        apiError.AddRange(from e in errorList from ie in e.Value!.Errors select ie.ErrorMessage);

        return new ApiResponse<T?>(new DataBaseResult<T?>(
            statusCode: 400,
            data: default,
            errors: apiError,
            message: apiError.ToList().IsNullOrEmpty() ? "Bad request" : apiError.First(),
            success: false
        ));
    }
}