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
    public class VillaAPIController : BaseController
    {
        private readonly IDemoServices _service;
        private ILogger<VillaAPIController> _logger;
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;
        public VillaAPIController(ILogger<VillaAPIController> logger, IDemoServices service )
        {
            _logger = logger;
            //_context = context;
            _service = service;
            //_mapper = mapper;
        }
        [HttpGet("villa")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<APIResponse> GetVillas(int pageIndex = 1, int pageSize = 1)
        {
            try
            {
                if (pageIndex < 1)
                    pageIndex = 1;
                if (pageSize < 1)
                    pageSize = 10;
                var villas = _service.GetVillas(pageIndex, pageSize);
                _logger.LogInformation("Getting All Villas");
                return SuccessResponse(villas, "Getting All Villas");
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex.Message);
            }
        }
        [HttpGet("villa/{id:int}", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<APIResponse> GetVilla(int id)
        {
            try
            {

                if (id == 0)
                {
                    _logger.LogError("Get Villa Error With Id :" + id);
                    return ErrorResponse("Get Villa By Id Which is Not exist");
                }
                var villa = _service.getvillabyid(id);
                if(villa== null)
                {
                    return ErrorResponse("Get Villa By Id Which is Not exist");
                }
                return SuccessResponse(villa, "Getting All Villas");
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex.Message);
            }
        }
        [HttpPost("villa")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<APIResponse> CreateVilla([FromBody] VillaDTO villaDTo)
        {
            var villaname = _service.GetVillaByName(villaDTo.Name);
            try
            {
                if (villaname)
                {
                    ModelState.AddModelError("CustomError", "Villa Already Exist");
                    return BadRequest(ModelState);
                }
                if (villaDTo == null)
                {
                    return ErrorResponse("Data not Exist Perfectly");
                }
                if (villaDTo.Id > 0)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
                _service.CreateVilla(villaDTo);
                return SuccessResponse(villaDTo, "Villa Created Successfully");
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex.Message);
            }
        }
        [HttpDelete("villa/{id:int}", Name = "DeleteVilla")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> DeleteVilla(int id)
        {
            try
            {
                if (id == 0)
                {
                    return ErrorResponse("Villa Doesn't Exist");
                }
                var villa = _service.getvillabyid(id);
                if (villa == null)
                {
                    return SuccessResponse(villa,"Villa Doesn't Exist");
                }
                _service.DeleteVilla(villa);
                return SuccessResponse(villa, "Getting All Villas");
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex.Message);
            }
        }
        [HttpPut("villa/{id:int}", Name = "UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdateVilla(int id, [FromBody] VillaDTO villaDTo)
        {            
            try
            {
                if (villaDTo == null || id != villaDTo.Id)
                {
                    return ErrorResponse("Vila not found");
                }
                var villaInfo = _service.UpdateVilla(id, villaDTo);
                return SuccessResponse(villaInfo, "Getting All Villas");               
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex.Message);
            }
        }
        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<APIResponse> UpdatePartialVilla(int id, JsonPatchDocument<VillaDTO> PatchDTO)
        {
            if (PatchDTO == null || id == 0)
            {
                return ErrorResponse("Villa Not Exist");
            }
            var villa = _service.getvillabyid(id);                
            if (villa == null)
            {
                return ErrorResponse("Villa Not Found");
            }
            _service.UpdatePartialVilla(id,PatchDTO);
            if (!ModelState.IsValid)
            {
                return ErrorResponse("Villa Not Found");
            }
            return NoContent();
        }
    }
}
