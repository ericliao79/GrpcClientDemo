using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using GrpcClientDemo.Models;
using Grpc.Net.Client;

namespace GrpcClientDemo.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        var channel = GrpcChannel.ForAddress("http://localhost:5171");
        var client = new GrpcGreeterClient.Greeter.GreeterClient(channel);
        var result = client.SayHello(new GrpcGreeterClient.HelloRequest { Name = "paynow" });

         return Json(new {result=result.Message});
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
