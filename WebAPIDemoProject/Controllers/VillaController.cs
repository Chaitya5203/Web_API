using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebAPIDemoRepositorys.ViewModel;
using WebAPIDemoService.Interface;
namespace WebAPIDemoProject.Controllers
{
    public class VillaController : Controller
    {
        private readonly IDemoServices _Service;
        public VillaController(IDemoServices services)
        {
            _Service = services;
        }
        public async Task<IActionResult> IndexVilla()
        {
            List<VillaDTO> list = new();
            var response = await _Service.GetAllAsync<APIResponse>();
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<VillaDTO>>(Convert.ToString(response.Result));
            }
            return View(list);
        }
        public async Task<IActionResult> CreateVilla()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateVilla(VillaDTO villaDTO)
        {
            if (ModelState.IsValid)
            {
                var response = await _Service.CreateAsync<APIResponse>(villaDTO);
                if (response != null && response.IsSuccess == true)
                {
                    return RedirectToAction(nameof(IndexVilla));
                }
                return View(villaDTO);
            }
            return View();
        }
        public async Task<IActionResult> UpdateVilla(int id)
        {
            var response = await _Service.GetAsync<APIResponse>(id);
            if (response != null && response.IsSuccess == true)
            {
                VillaDTO model = JsonConvert.DeserializeObject<VillaDTO>(Convert.ToString(response.Result));
                return View(model);
            }
            return View();

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateVilla(VillaDTO villaDTO)
        {
            if (ModelState.IsValid)
            {
                var response = await _Service.UpdateAsync<APIResponse>(villaDTO);
                if (response != null && response.IsSuccess == true)
                {
                    return RedirectToAction(nameof(IndexVilla));
                }
                return View(villaDTO);
            }
            return View(villaDTO);
        }
        public async Task<IActionResult> DeleteVilla(int id)
        {
            var response = await _Service.GetAsync<APIResponse>(id);
            if (response != null && response.IsSuccess == true)
            {
                VillaDTO model = JsonConvert.DeserializeObject<VillaDTO>(Convert.ToString(response.Result));
                return View(model);
            }
            return View();

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteVilla(VillaDTO villaDTO)
        {
            var response = await _Service.DeleteAsync<APIResponse>(villaDTO.Id);
            if (response != null && response.IsSuccess == true)
            {
                return RedirectToAction(nameof(IndexVilla));
            }
            return View(villaDTO);
        }
    }
}