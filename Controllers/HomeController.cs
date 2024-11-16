using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ASPNETMVC8_NET9SDK.Models;

namespace ASPNETMVC8_NET9SDK.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IWebHostEnvironment webHostEnvironment;

    public HomeController(ILogger<HomeController> logger, IWebHostEnvironment webHostEnvironment)
    {
        _logger = logger;
        this.webHostEnvironment = webHostEnvironment;
    }

    public IActionResult Index()
    {
        /** Infinite loop because dotnet watch causes the home page to reload on every new file creation */
        Task.Run(async () =>
        {
            await Task.Delay(5_000);

            string appDataPath = Path.Combine(webHostEnvironment.ContentRootPath, "App_Data");
            string testDirectoryPath = Path.Combine(appDataPath, "Test");

            if (!Directory.Exists(testDirectoryPath))
            {
                Directory.CreateDirectory(testDirectoryPath);
            }

            long unixTimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            string filePath = Path.Combine(testDirectoryPath, $"{unixTimestamp}.txt");

            System.IO.File.WriteAllText(filePath, unixTimestamp.ToString());
            Console.WriteLine($"File created: {filePath}");
        });

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
