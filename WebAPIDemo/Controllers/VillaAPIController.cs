using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
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
        public VillaAPIController(ILogger<VillaAPIController> logger, IDemoServices service)
        {
            _logger = logger;
            _service = service;
        }
        [HttpGet("villa")]
        //[Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetVillas(int pageIndex = 1, int pageSize = 1, string search = null)
        {
            try
            {
                if (pageIndex < 1)
                    pageIndex = 1;
                if (pageSize < 1)
                    pageSize = 10;
                
                if (!string.IsNullOrEmpty(search))
                {
                    var villaDto = await _service.GetVillas(pageIndex, pageSize, search);
                    _logger.LogInformation("Getting Villas");
                    return SuccessResponse(villaDto, "Getting Villas");
                    //var result = await _service.Search(pageIndex, pageSize,search);
                    //if (result.Any())
                    //{
                    //    return SuccessResponse(result, "Getting Search Filter Wise Villa");
                    //}
                    //else
                    //{
                    //    return ErrorResponse("No villas matched the search criteria");
                    //}
                }
                else
                {
                    var villas = await _service.GetVillas(pageIndex, pageSize);
                    _logger.LogInformation("Getting All Villas");
                    return SuccessResponse(villas, "Getting All Villas");
                }
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex.Message);
            }
        }
        //[Authorize]
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
        //[Authorize]
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
                _service.CreateVilla(villaDTo);
                return SuccessResponse(villaDTo, "Villa Created Successfully");
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex.Message);
            }
        }
        [HttpDelete("villa/{id:int}", Name = "DeleteVilla")]
        //[Authorize]
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
                if (villaDTo == null)
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

        //[HttpGet("villa/{search}")]
        //public async Task<ActionResult<APIResponse>> Search(string name)
        //{
        //    try
        //    {
        //        var result = await _service.Search(name);

        //        if (result.Any())
        //        {
        //            return SuccessResponse(result, "Getting Search Filter Wise Villa");
        //        }
        //        else
        //        {
        //            return ErrorResponse("No villas matched the search criteria");
        //        }                
        //    }
        //    catch (Exception ex)
        //    {
        //        return ErrorResponse(ex.Message);
        //    }
        //}
    }
}