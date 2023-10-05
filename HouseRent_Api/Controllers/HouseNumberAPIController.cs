using AutoMapper;
using HouseRent_Api.IRepository;
using HouseRent_Api.Models;
using HouseRent_Api.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HouseRent_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HouseNumberAPIController : ControllerBase
    {
        private readonly ILogger<HouseNumberAPIController> _logger;
        private readonly IHouseNumberRepository _dbHouseNumber;
        private readonly IHouseRepository _dbHouse;
        private readonly IMapper _mapper;
        private APIresponse _response;
        public HouseNumberAPIController(ILogger<HouseNumberAPIController> logger, IHouseNumberRepository dbHouseNumber, IMapper Mapper,IHouseRepository dbHouse)
        {
            _logger = logger;
            _mapper = Mapper;
            _dbHouse = dbHouse;
            _dbHouseNumber = dbHouseNumber;
            this._response = new();
        }

        [HttpGet]
        public async Task<ActionResult<APIresponse>> GetHouseNumbers()
        {
            try 
            {
                IEnumerable<HouseNumber> HouseNumberList = await _dbHouseNumber.GetAllAsync(includeProperties:"House");

                _response.Result = _mapper.Map<List<HouseNumberDto>>(HouseNumberList);
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

        [HttpGet("{HouseNo:int}", Name = "GetHouseNumber")]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(404)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(HouseNumberDto))]
        public async Task<ActionResult<APIresponse>> GetHouseNumber(int HouseNo)
        {
            try
            {
                if (HouseNo == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var houseNumber = await _dbHouseNumber.GetAsync(u => u.HouseNo == HouseNo);
                if (houseNumber == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;

                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<HouseNumberDto>(houseNumber);
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
        public async Task<ActionResult<APIresponse>> CreateHouseNumber([FromBody] HouseNumberCreateDto CreateDto)
        {
            try
            {

                if (await _dbHouseNumber.GetAsync(u => u.HouseNo == CreateDto.HouseNo) != null)
                {
                    ModelState.AddModelError("Custom Error", "House Number already exists");

                    return BadRequest(ModelState);
                }

                if(await _dbHouse.GetAsync(u=>u.Id == CreateDto.HouseId) == null)
                {


                }

                if (CreateDto == null)
                {
                    return BadRequest(CreateDto);
                }
                
                HouseNumber houseNumber = _mapper.Map<HouseNumber>(CreateDto);

                await _dbHouseNumber.CreateAsync(houseNumber);

                _response.Result = _mapper.Map<HouseNumberDto>(houseNumber);
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetHouse", new { id = houseNumber.HouseNo }, _response);

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                    = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{HouseNo:int}", Name = "DeleteHouseNumber")]
        public async Task<ActionResult<APIresponse>> DeleteHouseNumber(int HouseNo)
        {
            try
            {
                if (HouseNo == 0)
                {
                    return BadRequest();
                }
                var houseNumber = await _dbHouseNumber.GetAsync(u => u.HouseNo == HouseNo);
                if (houseNumber == null)
                {
                    return NotFound();
                }
                await _dbHouseNumber.RemoveAsync(houseNumber);

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

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("{HouseNo:int}", Name = "UpdateHouseNumber")]
        public async Task<ActionResult<APIresponse>> UpdateHouseNumber(int HouseNo, [FromBody] HouseNumberUpdateDto UpdateDto)
        {
            try
            {
                if (UpdateDto == null || HouseNo != UpdateDto.HouseNo)
                {
                    return BadRequest();
                }
                var House = await _dbHouseNumber.GetAsync(u => u.HouseNo == HouseNo);

                HouseNumber model = _mapper.Map<HouseNumber>(UpdateDto);

                await _dbHouseNumber.UpdateAsync(model);

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

        //[HttpPatch("id:HouseNo", Name = "UpdatePartialHouseNumber")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public async Task<ActionResult<APIresponse>> UpdatePartialHouse(int HouseNo, JsonPatchDocument<HouseNumberUpdateDto> patchDto)
        //{
        //    try
        //    {
        //        if (patchDto == null || HouseNo == 0)
        //        {
        //            return BadRequest();
        //        }
        //        var HouseNumber = await _dbHouseNumber.GetAsync(u => u.HouseNo == HouseNo);


        //        HouseNumberUpdateDto modelDto = _mapper.Map<HouseNumberUpdateDto>(HouseNumber);


        //        if (HouseNumber == null)
        //        {
        //            return BadRequest();
        //        }
        //        patchDto.ApplyTo(modelDto, ModelState);


        //        HouseNumber model = _mapper.Map<HouseNumber>(modelDto);


        //        if (!ModelState.IsValid)
        //        {
        //            return BadRequest(ModelState);

        //        }
        //        _dbHouseNumber.UpdateAsync(model);

        //        return NoContent();
        //    }
        //    catch (Exception ex)
        //    {
        //        _response.IsSuccess = false;
        //        _response.ErrorMessages
        //            = new List<string>() { ex.ToString() };
        //    }
        //    return _response;


        //}
    }

}
