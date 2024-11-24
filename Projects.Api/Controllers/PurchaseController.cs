using InventoryApi.DTOs;
using Microsoft.AspNetCore.Mvc;
using Project.Application.ApiResponse;
using Project.Application.DTOs;
using Project.Application.Interfaces;
using System.Net;

namespace Projects.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {
        private readonly IBaseServices<PurchaseDTOs> _service;
        private readonly IPurchaseServices _purchaseServices;

        public PurchaseController(IBaseServices<PurchaseDTOs> service, IPurchaseServices purchaseServices)
        {
            _service = service;
            _purchaseServices = purchaseServices;
        }
        [HttpPost("Create")]
        public async Task<IActionResult> Create(PurchaseDTOs model)
        {
            var result = await _service.CreateAsync(model);
            if (result)
            {
                return StatusCode((int)HttpStatusCode.Created, new ApiResponse<string>
                {
                    Success = true,
                    Status = HttpStatusCode.Created,
                    ErrorMessage = " Purchase Created  successfully !!."
                });
            }
            return StatusCode((int)HttpStatusCode.BadRequest, result);
        }
        [HttpPost("Purchase")]
        public async Task<IActionResult> Purchase(PurchaseItemDTOs model)
        {
            var result = await _purchaseServices.PurchaseProduct(model);
            if (result)
            {
                return StatusCode((int)HttpStatusCode.Created, new ApiResponse<string>
                {
                    Success = true,
                    Status = HttpStatusCode.Created,
                    Data = " Purchase successfully !!."
                });
            }
            return StatusCode((int)HttpStatusCode.BadRequest, result);
        }
        [HttpGet("All")]
        public async Task<IActionResult> getAll()
        {
            var result = await _service.GetAllAsync();
            if (result != null)
            {
                return StatusCode((int)HttpStatusCode.OK, result);
            }
            return StatusCode((int)HttpStatusCode.BadRequest, result);
        }
        [HttpGet("get/{id}")]
        public async Task<IActionResult> getById(Guid id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result != null)
            {
                return StatusCode((int)HttpStatusCode.OK, new ApiResponse<PurchaseDTOs>
                {
                    Success = true,
                    Data = result,
                    Status = HttpStatusCode.OK,
                    ErrorMessage = "Purchase  get   successfully !!."
                });
            }
            return StatusCode((int)HttpStatusCode.BadRequest, result);
        }
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _service.DeleteAsync(id);
            if (result)
            {
                return StatusCode((int)HttpStatusCode.OK, new ApiResponse<string>
                {
                    Success = true,
                    Status = HttpStatusCode.OK,
                    Data = "Purchase deleted successfully"
                });
            }
            return StatusCode((int)HttpStatusCode.BadRequest, result);
        }
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update(Guid id, PurchaseDTOs model)
        {
            var result = await _service.UpdateAsync(id, model);
            if (result)
            {
                return StatusCode((int)HttpStatusCode.OK, new ApiResponse<string>
                {
                    Success = true,
                    Status = HttpStatusCode.OK,
                    Data = "Purchase updated successfully"
                });
            }
            return StatusCode((int)HttpStatusCode.BadRequest, result);
        }
    }
}
