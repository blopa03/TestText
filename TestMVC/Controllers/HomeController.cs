using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TestMVC.Models;
using TestMVC.Services;

namespace TestMVC.Controllers
{
    public class HomeController(ILogger<HomeController> logger, ITextService textService, IConfiguration conf) : Controller
    {
        private readonly ILogger<HomeController> _logger = logger;

        private readonly ITextService _textService = textService;
        private readonly IConfiguration _conf = conf;

        public IActionResult Index()
        {
            ViewBag.Api = _conf.GetValue<string>("urlApi");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetWordByMaxLong([FromBody] WordModelRequest input)
        {
            try
            {
                var res = await _textService.GetWordOrderByMaxLong(input.Text);
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, null);
            }
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
