using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using trainee_projectmanagement.Models;

namespace trainee_projectmanagement.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        var userName = HttpContext.Session.GetString("UserName");
        var userType = HttpContext.Session.GetString("UserType");
        
        ViewBag.UserName = userName;
        ViewBag.UserType = userType;
        ViewBag.IsLoggedIn = !string.IsNullOrEmpty(userType);

        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
