using System.Diagnostics;
using System.Net.NetworkInformation;
using Microsoft.AspNetCore.Mvc;
using NetflixServer.Models;

namespace NetflixServer.Controllers;

public class HomeController : Controller
{
  private readonly ILogger<HomeController> _logger;
  public HomeController(ILogger<HomeController> logger)
  {
    _logger = logger ?? throw new ArgumentNullException(nameof(logger));
  }

  public IActionResult Index()
  {
    _logger.LogInformation("Home view initiated");

    return View();
  }

  public IActionResult Privacy()
  {
    _logger.LogInformation("Privacy view initiated");

    return View();
  }

  public string TestConnection()
  {
    try
    {
      Ping myPing = new Ping();
      PingReply reply = myPing.Send("www.google.com", 1000);
      if (reply != null)
      {
        // Console.WriteLine("Status :  " + reply.Status + " \n Time : " + reply.RoundtripTime.ToString() + " \n Address : " + reply.Address);
      }
      Thread.Sleep(60);
      _logger.LogInformation("Testing Connection:" + " Status :  " + reply.Status + " \n Time: " + reply.RoundtripTime.ToString() + " \n Address: " + reply.Address);

      return "Status :  " + reply.Status + " \n Time : " + reply.RoundtripTime.ToString() + " \n Address : " + reply.Address;

    }
    catch (Exception e)
    {
      _logger.LogError("Testing Connection Failed:" + " Status :  " + e);

      return "ERROR: You have Some TIMEOUT issue";
    }
  }
  public string TestConnections()
  {

    try
    {
      Ping myPing = new Ping();
      PingReply reply = myPing.Send("www.g10oogle.com", 1000);
      if (reply != null)
      {
        // Console.WriteLine("Status :  " + reply.Status + " \n Time : " + reply.RoundtripTime.ToString() + " \n Address : " + reply.Address);
      }
      Thread.Sleep(60);
      _logger.LogError("Testing Connection Failed:" + " Status :  " + reply.Status + " \n Time: " + reply.RoundtripTime.ToString() + " \n Address: " + reply.Address);

      return "Status :  " + reply.Status + " \n Time : " + reply.RoundtripTime.ToString() + " \n Address : " + reply.Address;
    }
    catch (Exception e)
    {
      _logger.LogError(e.StackTrace);
      throw new Exception(e.StackTrace);
    }

  }
  [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
  public IActionResult Error()
  {
    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
  }
}
