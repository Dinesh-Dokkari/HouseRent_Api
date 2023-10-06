using AutoMapper;
using HappyHouse.Models;
using HappyHouse.Services.IServices;
using HappyHouse_Utility;
using Microsoft.AspNetCore.Authorization;
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

            var responce = await _houseNumberService.GetAllAsync<APIresponse>(HttpContext.Session.GetString(Static_Details.SessionToken));

            if (responce != null && responce.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<HouseNumberDto>>(Convert.ToString(responce.Result));
            }
            return View(list);
        }

        [Authorize(Roles="admin")]
        public Task<IActionResult> CreateHouseNumber()
        {
            return Task.FromResult<IActionResult>(View());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]

        public async Task<IActionResult> CreateHouseNumber(HouseNumberCreateDto model)
        {
            if (ModelState.IsValid)
            {
                var responce = await _houseNumberService.CreateAsync<APIresponse>(model, HttpContext.Session.GetString(Static_Details.SessionToken));

                if (responce != null && responce.IsSuccess)
                {
                    return RedirectToAction(nameof(IndexHouseNumber));
                }
            }
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]

        public async Task<IActionResult> UpdateHouseNumber(int HouseNo)
        {
            if (ModelState.IsValid)
            {
                var responce = await _houseNumberService.GetAsync<APIresponse>(HouseNo, HttpContext.Session.GetString(Static_Details.SessionToken));

                if (responce != null && responce.IsSuccess)
                {
                    HouseNumberDto model = JsonConvert.DeserializeObject<HouseNumberDto>(Convert.ToString(responce.Result));
                    return View(_mapper.Map<HouseNumberUpdateDto>(model));
                }
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]

        public async Task<IActionResult> UpdateHouseNumber(HouseNumberUpdateDto model)
        {
            if (ModelState.IsValid)
            {
                var responce = await _houseNumberService.UpdateAsync<APIresponse>(model, HttpContext.Session.GetString(Static_Details.SessionToken));

                if (responce != null && responce.IsSuccess)
                {

                    return RedirectToAction(nameof(IndexHouseNumber));
                }
            }
            return RedirectToAction(nameof(IndexHouseNumber));
        }


        [HttpGet]
        [Authorize(Roles = "admin")]

        public async Task<IActionResult> DeleteHouseNumber(int houseid)
        {
            if (houseid != null)
            {
                var responce = await _houseNumberService.GetAsync<APIresponse>(houseid, HttpContext.Session.GetString(Static_Details.SessionToken));

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
        [Authorize(Roles = "admin")]

        public async Task<IActionResult> DeletehouseNumber(int HouseNo)
        {
            if (ModelState.IsValid)
            {
                var responce = await _houseNumberService.DeleteAsync<APIresponse>(HouseNo, HttpContext.Session.GetString(Static_Details.SessionToken));

                if (responce != null && responce.IsSuccess)
                {

                    return RedirectToAction(nameof(IndexHouseNumber));
                }
            }
            return View();
        }
    }
}
