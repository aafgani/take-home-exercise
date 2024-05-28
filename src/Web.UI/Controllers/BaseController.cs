using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace Web.UI;

public class BaseController : Controller
{
    protected string GetUserEmail() => HttpContext.User?.Identity?.Name;
    protected string GetUserName() => User.Claims.FirstOrDefault(i => i.Type == "name")?.Value;
    protected string GetUserId() => HttpContext.User?.FindFirst(ClaimTypes.NameIdentifier).Value;
}
