using HappyHouse.Models;
using HappyHouse.Services.IServices;
using HappyHouse_Utility;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace HappyHouse.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IHouseService _houseService;   

        public HomeController(ILogger<HomeController> logger, IHouseService houseService)
        {
            _logger = logger;
            _houseService = houseService;
        }

        public async Task<IActionResult> Index()
        {
            List<HouseDto> list = new();


            var responce = await _houseService.GetAllAsync<APIresponse>(HttpContext.Session.GetString(Static_Details.SessionToken));

            if (responce != null && responce.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<HouseDto>>(Convert.ToString(responce.Result));
            }
            return View(list);
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
}