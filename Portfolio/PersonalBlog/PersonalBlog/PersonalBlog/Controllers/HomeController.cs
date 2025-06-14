using System.Diagnostics;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using PersonalBlog.Database;
using PersonalBlog.Models;

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
        //ViewBag.IsAdmin = IsAuthorized(HttpContext, config);

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
}
