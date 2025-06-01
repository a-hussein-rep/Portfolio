using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonalBlog.Database;
using PersonalBlog.Models;
using System.Diagnostics;
using System.Text;

namespace PersonalBlog.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> logger;
    private readonly BlogDbContext dbContext; 

    public HomeController(ILogger<HomeController> logger, BlogDbContext dbContext)
    {
        this.logger = logger;
        this.dbContext = dbContext;
    }

    public async Task<IActionResult> Index()
    {
        var config = HttpContext.RequestServices.GetService(typeof(IConfiguration)) as IConfiguration;
        ViewBag.IsAdmin = IsAuthorized(HttpContext, config);

        return View(await dbContext.Articles.ToListAsync());
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

    private bool IsAuthorized(HttpContext context, IConfiguration config)
    {
        var authHeader = context.Request.Headers["Authorization"].ToString();
        var username = config["AdminAuth:Username"];
        var password = config["AdminAuth:Password"];

        if (authHeader?.StartsWith("Basic ") == true)
        {
            var encoded = authHeader["Basic ".Length..].Trim();
            var decoded = Encoding.UTF8.GetString(Convert.FromBase64String(encoded));
            var parts = decoded.Split(':');
            return parts.Length == 2 && parts[0] == username && parts[1] == password;
        }
        return false;
    }

}
