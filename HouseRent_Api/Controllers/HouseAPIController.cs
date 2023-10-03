using AutoMapper;
using Azure;
using HouseRent_Api.Data;
using HouseRent_Api.IRepository;
using HouseRent_Api.Models;
using HouseRent_Api.Models.DTO;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace HouseRent_Api.Controllers
{

    [Route("api/HouseAPI")]
    [ApiController]
    public class HouseAPIController : ControllerBase
    {
        private readonly ILogger<HouseAPIController> _logger;
        private readonly IHouseRepository _dbHouse;
        private readonly IMapper _mapper;
        private APIresponse _response;
        public HouseAPIController(ILogger<HouseAPIController> logger, IHouseRepository dbHouse, IMapper Mapper)
        {
            _logger = logger;
            _mapper = Mapper;
            _dbHouse = dbHouse;
            this._response = new();
        }

        [HttpGet]
        public async Task<ActionResult<APIresponse>> GetHouses()
        {
            try
            {
                IEnumerable<House> HouseList = await _dbHouse.GetAllAsync();

                _response.Result = _mapper.Map<List<HouseDto>>(HouseList);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                    = new List<string>() { ex.ToString() };
            }
            return _response;

        }

        [HttpGet("{id:int}", Name = "GetHouse")]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(404)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(HouseDto))]
        public async Task<ActionResult<APIresponse>> GetHouse(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var house = await _dbHouse.GetAsync(u => u.Id == id);
                if (house == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;

                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<HouseDto>(house);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                    = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPost]
        public async Task<ActionResult<APIresponse>> CreateHouse([FromBody] HouseCreateDto CreateDto)
        {
            try
            {
                //if(!ModelState.IsValid)
                //{
                //    return BadRequest(ModelState);
                //}
                if (await _dbHouse.GetAsync(u => u.Name.ToLower() == CreateDto.Name.ToLower()) != null)
                {
                    ModelState.AddModelError("Custom Error", "House already exists");

                    return BadRequest(ModelState);
                }




                if (CreateDto == null)
                {
                    return BadRequest(CreateDto);
                }
                //if (HouseDto.Id > 0)
                //{
                //    return StatusCode(StatusCodes.Status500InternalServerError);
                //}

                House house = _mapper.Map<House>(CreateDto);





                await _dbHouse.CreateAsync(house);
                //HouseDto.Id = HouseStore.HouseList.OrderByDescending(u => u.Id).FirstOrDefault().Id + 1;
                //HouseStore.HouseList.Add(HouseDto);


                _response.Result = _mapper.Map<HouseDto>(house);
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetHouse", new { id = house.Id }, _response);

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                    = new List<string>() { ex.ToString() };
            }
            return _response;
        }


        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id:int}", Name = "DeleteHouse")]
        public async Task<ActionResult<APIresponse>> DeleteHouse(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var house = await _dbHouse.GetAsync(u => u.Id == id);
                if (house == null)
                {
                    return NotFound();
                }
                await _dbHouse.RemoveAsync(house);

                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                    = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("{id:int}", Name = "UpdateHouse")]
        public async Task<ActionResult<APIresponse>> UpdateHouse(int id, [FromBody] HouseUpdateDto UpdateDto)
        {
            try
            {
                if (UpdateDto == null || id != UpdateDto.Id)
                {
                    return BadRequest();
                }
                var House = await _dbHouse.GetAsync(u => u.Id == id);
                //House.Name= houseDto.Name;
                //House.Sqft = houseDto.Sqft;
                //House.Occupancy = houseDto.Occupancy;
                House model = _mapper.Map<House>(UpdateDto);

                await _dbHouse.UpdateAsync(model);

                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                    = new List<string>() { ex.ToString() };
            }
            return _response;


        }


        [HttpPatch("id:int", Name = "UpdatePartialHouse")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIresponse>> UpdatePartialHouse(int id, JsonPatchDocument<HouseUpdateDto> patchDto)
        {
            try
            {
                if (patchDto == null || id == 0)
                {
                    return BadRequest();
                }
                var House = await _dbHouse.GetAsync(u => u.Id == id);


                HouseUpdateDto modelDto = _mapper.Map<HouseUpdateDto>(House);


                if (House == null)
                {
                    return BadRequest();
                }
                patchDto.ApplyTo(modelDto, ModelState);


                House model = _mapper.Map<House>(modelDto);


                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);

                }
                _dbHouse.UpdateAsync(model);

                return NoContent();
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;


        }
    }
}
