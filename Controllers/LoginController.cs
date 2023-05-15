using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace NetflixServer.Controllers
{
  public class LoginController : Controller
  {
    private readonly ILogger<LoginController> _logger;

    public LoginController(ILogger<LoginController> logger)
    {
      _logger = logger;

    }

    public IActionResult Login()
    {
      Thread.Sleep(60);
      _logger.LogInformation("Login Success");
      return View();

    }

    public IActionResult LoginUser()
    {
      Stopwatch stopwatch = new Stopwatch();

      stopwatch.Start();
      long i = 10000000000, j = 0;
      while (j < i)
      {
        j++;
      }
      stopwatch.Stop();

      _logger.LogError("There was some issue logging in ,Login Failed");
      throw new Exception("There was some issue logging in");

    }
  }
}
