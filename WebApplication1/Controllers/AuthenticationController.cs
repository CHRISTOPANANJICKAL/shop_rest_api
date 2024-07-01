using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTOs.Authentication;
using WebApplication1.Helpers;
using WebApplication1.Models.common;

namespace WebApplication1.Controllers;

[Route("api/user/")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private JwtHelper _jwtHelper;

    public AuthenticationController(JwtHelper jwtHelper)
    {
        _jwtHelper = jwtHelper;
    }


    [HttpPost("register")]
    public ApiResponse<dynamic> Register([FromBody] RegisterUserDto userDto)
    {
        var result = new DataBaseResult<dynamic>(data: null,
            success: true,
            message: "User created successfully",
            statusCode: 201, errors: null
        );

        return new ApiResponse<dynamic>(result);
    }

    [HttpPost("login")]
    public ApiResponse<dynamic> Login([FromBody] LoginUserDto userDto)
    {
        var token = _jwtHelper.GenerateToken(1.ToString(), "Christo@mail.com");
        var result = new DataBaseResult<dynamic>(data: token,
            success: true,
            message: "User created successfully",
            statusCode: 201, errors: null
        );

        return new ApiResponse<dynamic>(result);
    }
    // [HttpPost]
    // public ApiResponse<dynamic> Register([FromBody] RegisterUserDto userDto)
    // {
    //     var result = new DataBaseResult<dynamic>(data: null,
    //         success: true,
    //         message: "User created successfully",
    //         statusCode: 201, errors: null
    //     );
    //
    //     return new ApiResponse<dynamic>(result);
    // }
}