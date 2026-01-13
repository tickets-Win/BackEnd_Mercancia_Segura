using System.Threading.Tasks;
using MercanciaSegura.RestAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace MercanciaSegura.RestAPI.Controllers.Implementation;

/// <summary>
/// 
/// </summary>
public class UserApi : UsersApiController
{
    public override Task<IActionResult> DeleteUser(string version)
    {
        throw new System.NotImplementedException();
    }

    public override Task<IActionResult> GetUser(string version)
    {
        throw new System.NotImplementedException();
    }

    public override Task<IActionResult> PostUser(string version, UserRequest body)
    {
        throw new System.NotImplementedException();
    }

    public override Task<IActionResult> PutUser(string version, UserUpdateRequest body)
    {
        throw new System.NotImplementedException();
    }
}