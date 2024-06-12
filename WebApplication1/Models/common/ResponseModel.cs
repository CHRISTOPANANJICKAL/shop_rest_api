using Microsoft.AspNetCore.Mvc;

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

         return new  ApiResponse<T?>(new DataBaseResult<T?>(
             statusCode:400,
             data:default,
             errors: apiError,
             message:"Bad request",
             success:false
             ));
     }
    
}

// public abstract class ApiResponseModel(int statusCode, string message, dynamic? data, List<String>? errors)
// {
//     public int StatusCode { get; set; } = statusCode;
//     public bool Success { get; set; } = statusCode.ToString().StartsWith("2");
//     public string Message { get; set; } = message;
//
//     [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
//     public dynamic? Data { get; set; } = data;
//
//     [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
//     public List<String>? Errors { get; set; } = errors;
// }

// public class SuccessResponse(int statusCode, string message = "Ok", object? data = null) : ApiResponseModel(
//     statusCode: statusCode,
//     message: message,
//     data: data,
//     errors: null
// );
//
// public class ErrorResponse(int statusCode, string message = "Error", object? data = null, List<string>? errors = null)
//     : ApiResponseModel(
//         statusCode: statusCode,
//         message: message,
//         data: data,
//         errors: errors
//     )
// {
//     public static IActionResult GenerateErrorResponseModel(ActionContext context)
//     {
//         List<string> apiError = [];
//         var errorList = context.ModelState.AsEnumerable();
//
//         apiError.AddRange(from e in errorList from ie in e.Value!.Errors select ie.ErrorMessage);
//
//         return new BadRequestObjectResult(new ErrorResponse(statusCode: 400, errors: apiError, message: "Bad request"));
//     }
// }