using System;

namespace TodoApplicationApi.Controllers.Models.AuthModels;

public class UserDTO
{
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
