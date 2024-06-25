using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using WebAPIDemoRepositorys.Data;
using WebAPIDemoRepositorys.ViewModel;
using WebAPIDemoService.Interface;
namespace WebAPIDemo.Controllers
{
    [Route("api/VillaAPI")]
    [ApiController]
    public class VillaAPIController : ControllerBase
    {
        private readonly IDemoServices _service;
        private ILogger<VillaAPIController> _logger;
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;
        public VillaAPIController(ILogger<VillaAPIController> logger, ApplicationContext context, IDemoServices service , IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _service = service;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<APIResponse> GetVillas()
        {
            APIResponse response = new APIResponse();
            try
            {
                _logger.LogInformation("Getting All Villas");
                response = new()
                {
                    StatusCode = HttpStatusCode.OK,
                    IsSuccess = true,
                    Result = _context.Villainfos.ToList()
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                response = new()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    ErrorMessages = new List<string>() { ex.ToString() },
                    IsSuccess = false,
                };
            }
            return response;
        }
        [HttpGet("{id:int}", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(200)]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(404)]
        public ActionResult<APIResponse> GetVilla(int id)
        {
            APIResponse response = new APIResponse();
            try
            {

                if (id == 0)
                {
                    _logger.LogError("Get Villa Error With Id :" + id);
                    return BadRequest();
                }
                response = new()
                {
                    StatusCode = HttpStatusCode.OK,
                    IsSuccess = true,
                    Result = _context.Villainfos.FirstOrDefault(e => e.Id == id)
                };
                //var villa = _context.Villainfos.FirstOrDefault(e => e.Id == id);
                if (response.Result == null)
                {
                    return NotFound();
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                response = new()
                {
                    ErrorMessages = new List<string>() { ex.ToString() },
                    IsSuccess = false,
                };
            }
            return response;
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<APIResponse> CreateVilla([FromBody] VillaDTO villaDTo)
        {
            //if(!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}
            APIResponse response = new APIResponse();
            try
            {
                if (_context.Villainfos.Select(e => e.Name).Contains(villaDTo.Name))
                {
                    ModelState.AddModelError("CustomError", "Villa Already Exist");
                    return BadRequest(ModelState);
                }
                if (villaDTo == null)
                {
                    return BadRequest(villaDTo);
                }
                if (villaDTo.Id > 0)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
                Villainfo villainfo = _mapper.Map<Villainfo>(villaDTo); 
                //Villainfo villainfo = new Villainfo();
                //villainfo.Name = villaDTo.Name;
                //villainfo.Sqft = villaDTo.sqft;
                //villainfo.Occupancy = villaDTo.occupancy;
                //villainfo.Createddate = DateTime.Now;
                //villainfo.Modifieddate = DateTime.Now;
                //villainfo.Rate = villaDTo.Rate;
                //villainfo.Details = villaDTo.Details;
                //villainfo.Amenity = villaDTo.Amenity;
                //villaDTo.Id = _context.Villainfos.OrderByDescending(e=>e.Id).FirstOrDefault().Id+1;
                _context.Villainfos.Add(villainfo);
                _context.SaveChanges();
                response = new()
                {
                    StatusCode = HttpStatusCode.Created,
                    Result = villainfo
                };
                return CreatedAtRoute("GetVilla", new { id = villaDTo.Id }, response);
            }
            catch (Exception ex)
            {
                response = new()
                {
                    ErrorMessages = new List<string>() { ex.ToString() },
                    IsSuccess = false,
                };
            }
            return response;
        }
        [HttpDelete("{id:int}", Name = "DeleteVilla")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> DeleteVilla(int id)
        {
            APIResponse response = new APIResponse();
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var villa = _context.Villainfos.FirstOrDefault(e => e.Id == id);
                if (villa == null)
                {
                    return NotFound();
                }
                _context.Villainfos.Remove(villa);
                _context.SaveChanges();
                response = new()
                {
                    StatusCode = HttpStatusCode.NoContent,
                    IsSuccess = true,
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                response = new()
                {
                    ErrorMessages = new List<string>() { ex.ToString() },
                    IsSuccess = false,
                };
            }
            return response;
        }
        [HttpPut("{id:int}", Name = "UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdateVilla(int id, [FromBody] VillaDTO villaDTo)
        {
            APIResponse response = new APIResponse();
            try
            {
                if (villaDTo == null || id != villaDTo.Id)
                {
                    return BadRequest();
                }
                var villainfo = _context.Villainfos.AsNoTracking().FirstOrDefault(e => e.Id == id);
                var villa = _mapper.Map<Villainfo>(villaDTo);
                //villainfo.Name = villaDTo.Name;
                //villainfo.Sqft = villaDTo.sqft;
                //villainfo.Occupancy = villaDTo.occupancy;
                //villainfo.Createddate = DateTime.Now;
                //villainfo.Modifieddate = DateTime.Now;
                //villainfo.Rate = villaDTo.Rate;
                //villainfo.Details = villaDTo.Details;
                //villainfo.Amenity = villaDTo.Amenity;
                _context.Villainfos.Update(villa);
                _context.SaveChanges();
                response = new()
                {
                    StatusCode = HttpStatusCode.NoContent,
                    IsSuccess = true,
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                response = new()
                {
                    ErrorMessages = new List<string>() { ex.ToString() },
                    IsSuccess = false,
                };
            }
            return response;
        }
        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdatePartialVilla(int id, JsonPatchDocument<VillaDTO> PatchDTO)
        {
            if (PatchDTO == null || id == 0)
            {
                return BadRequest();
            }
            var villa = _context.Villainfos.AsNoTracking().FirstOrDefault(e => e.Id == id);
            if (villa == null)
            {
                return BadRequest();
            }
            VillaDTO villaDTO = new VillaDTO()
            {
                Id = id,
                Name = villa.Name,
                sqft = (int)villa.Sqft,
                occupancy = (int)villa.Occupancy,
                Rate = (decimal)villa.Rate,
                Details = villa.Details,
                Amenity = villa.Amenity,
            };
            PatchDTO.ApplyTo(villaDTO, ModelState);
            var updateVilla = new Villainfo()
            {
                Id = id,
                Name = villaDTO.Name,
                Sqft = villaDTO.sqft,
                Occupancy = villaDTO.occupancy,
                Rate = villaDTO.Rate,
                Details = villaDTO.Details,
                Amenity = villaDTO.Amenity,
            };
            _context.Update(updateVilla);
            _context.SaveChanges();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return NoContent();
        }
    }
}
