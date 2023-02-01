using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using GrpcClientDemo.Models;
using Grpc.Net.Client;

namespace GrpcClientDemo.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly GrpcGreeterClient.Greeter.GreeterClient _client;

    public HomeController(ILogger<HomeController> logger, GrpcGreeterClient.Greeter.GreeterClient client)
    {
        _logger = logger;
        _client = client;
    }

    public IActionResult Index()
    {
        // var channel = GrpcChannel.ForAddress("http://localhost:5171");
        // var client = new GrpcGreeterClient.Greeter.GreeterClient(channel);
        try {
            var request = new GrpcGreeterClient.HelloRequest();
            request.Name = "paynow";

            var result = _client.SayHello(request);

            return Json(new {
                result=result,
            });
        } catch (Exception ex) {
            _logger.LogError(ex, "Error calling gRPC service");

            return Json(new {error=ex.Message});
        }
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
