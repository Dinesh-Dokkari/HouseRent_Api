using AutoMapper;
using HappyHouse.Models;
using HappyHouse.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Reflection;

namespace HappyHouse.Controllers
{
    public class HouseController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IHouseService _houseService;
        public HouseController(IHouseService houseService,IMapper Mapper)
        {
            _houseService = houseService;
            _mapper = Mapper;
        }
        public async Task<IActionResult> IndexView()
        {
            List<HouseDto> list = new();

            var responce = await _houseService.GetAllAsync<APIresponse>();

            if (responce != null && responce.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<HouseDto>>(Convert.ToString(responce.Result));
            }
            return View(list);
        }

        public Task<IActionResult> CreateHouse()
        {
            return Task.FromResult<IActionResult>(View());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
		public async Task<IActionResult> CreateHouse(HouseCreateDto model)
		{
            if (ModelState.IsValid)
            {
                var responce = await _houseService.CreateAsync<APIresponse>(model);

                if (responce != null && responce.IsSuccess)
                {
                    return RedirectToAction(nameof(IndexView));
                }
            }
			return View(model);
		}

        [HttpGet]
        public async Task<IActionResult> UpdateHouse(int houseid)
        {
            if (ModelState.IsValid)
            {
                var responce = await _houseService.GetAsync<APIresponse>(houseid);

                if (responce != null && responce.IsSuccess)
                {
                    HouseDto model = JsonConvert.DeserializeObject<HouseDto>(Convert.ToString(responce.Result));
                    return View(_mapper.Map<HouseUpdateDto>(model));
                }
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(HouseUpdateDto model)
        {
            if (ModelState.IsValid)
            {
                var responce = await _houseService.UpdateAsync<APIresponse>(model);

                if (responce != null && responce.IsSuccess)
                {

                    return RedirectToAction(nameof(IndexView));
                }
            }
            return RedirectToAction(nameof(IndexView));
        }


        [HttpGet]
        public async Task<IActionResult> DeleteHouse(int houseid)
        {
            if (houseid != null)
            {
                var responce = await _houseService.GetAsync<APIresponse>(houseid);

                if (responce != null && responce.IsSuccess)
                {
                    HouseDto model = JsonConvert.DeserializeObject<HouseDto>(Convert.ToString(responce.Result));
                    return View(model);
                }
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Deletehouse(int id)
        {
            if (ModelState.IsValid)
            {
                var responce = await _houseService.DeleteAsync<APIresponse>(id);

                if (responce != null && responce.IsSuccess)
                {

                    return RedirectToAction(nameof(IndexView));
                }
            }
            return View();
        }
    }
}
