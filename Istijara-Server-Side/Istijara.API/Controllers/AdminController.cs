using Istijara.API.Dtos;
using Istijara.Core.Entities;
using Istijara.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Istijara.API.Controllers
{
    //[Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/admin")]
    public class AdminController : BaseApiController
    {

        private readonly IAdminServices _adminService;

        public AdminController(IAdminServices adminService)
        {
            _adminService = adminService;
        }


        [HttpPost("categories")]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryDto dto)
        {
            var category = new CategoryDto
            { 
                Name = dto.Name,
                Description = dto.Description,
                IconUrl = dto.IconUrl,
                IsActive = dto.IsActive,
            };
            await _adminService.CreateCategoryAsync(category);
            return Ok("Category created");
        }














    }
}
