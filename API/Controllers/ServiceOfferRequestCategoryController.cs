// using Microsoft.AspNetCore.Mvc;
// using ServiceOfferRequest.BLL.Dtos;
// using ServiceOfferRequest.BLL.Services.Interfaces;
//
// namespace API.Controllers;
//
// [ApiController]
// [Route("api/[controller]")]
// public class ServiceOfferRequestCategoryController(IServiceOfferRequestCategoryService serviceOfferRequestCategoryService):ControllerBase
// {
//     [HttpGet("[action]")]
//     public async Task<ActionResult<ServiceOfferRequestCategoryDto>> Get([FromQuery] int id, CancellationToken cancellationToken)
//     {
//         try
//         {
//             var serviceOfferRequestCategory = await serviceOfferRequestCategoryService.GetByIdAsync(id, cancellationToken);
//
//             if (serviceOfferRequestCategory is null)
//                 return NotFound();
//
//             return Ok(serviceOfferRequestCategory);
//         }
//         catch (Exception)
//         {
//             return StatusCode(500, "An unexpected error occurred.");
//         }
//     }
// }