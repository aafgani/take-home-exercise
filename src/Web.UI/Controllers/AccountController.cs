using Microsoft.AspNetCore.Mvc;

namespace Web.UI;

public class AccountController : Controller
{
    [HttpGet]
    public IActionResult SignOut(string page)
    {
        return RedirectToAction("Index", "Home");
    }
}
