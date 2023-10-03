using AutoMapper;
using HappyHouse.Models;
using HappyHouse.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Reflection;

namespace HappyHouse.Controllers
{
    public class HouseNumberController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IHouseNumberService _houseNumberService;
        public HouseNumberController(IHouseNumberService houseNumberService, IMapper Mapper)
        {
            _houseNumberService = houseNumberService;
            _mapper = Mapper;
        }
        public async Task<IActionResult> IndexHouseNumber()
        {
            List<HouseNumberDto> list = new();

            var responce = await _houseNumberService.GetAllAsync<APIresponse>();

            if (responce != null && responce.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<HouseNumberDto>>(Convert.ToString(responce.Result));
            }
            return View(list);
        }

        public Task<IActionResult> CreateHouseNumber()
        {
            return Task.FromResult<IActionResult>(View());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateHouseNumber(HouseNumberCreateDto model)
        {
            if (ModelState.IsValid)
            {
                var responce = await _houseNumberService.CreateAsync<APIresponse>(model);

                if (responce != null && responce.IsSuccess)
                {
                    return RedirectToAction(nameof(IndexHouseNumber));
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateHouseNumber(int houseid)
        {
            if (ModelState.IsValid)
            {
                var responce = await _houseNumberService.GetAsync<APIresponse>(houseid);

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
        public async Task<IActionResult> UpdateNumber(HouseNumberUpdateDto model)
        {
            if (ModelState.IsValid)
            {
                var responce = await _houseNumberService.UpdateAsync<APIresponse>(model);

                if (responce != null && responce.IsSuccess)
                {

                    return RedirectToAction(nameof(IndexHouseNumber));
                }
            }
            return RedirectToAction(nameof(IndexHouseNumber));
        }


        [HttpGet]
        public async Task<IActionResult> DeleteHouseNumber(int houseid)
        {
            if (houseid != null)
            {
                var responce = await _houseNumberService.GetAsync<APIresponse>(houseid);

                if (responce != null && responce.IsSuccess)
                {
                    HouseNumberDto model = JsonConvert.DeserializeObject<HouseNumberDto>(Convert.ToString(responce.Result));
                    return View(model);
                }
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletehouseNumber(int id)
        {
            if (ModelState.IsValid)
            {
                var responce = await _houseNumberService.DeleteAsync<APIresponse>(id);

                if (responce != null && responce.IsSuccess)
                {

                    return RedirectToAction(nameof(IndexHouseNumber));
                }
            }
            return View();
        }
    }
}
