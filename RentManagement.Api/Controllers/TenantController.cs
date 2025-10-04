using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentManagement.Api.DTOs;
using RentManagement.Api.Interfaces;

namespace RentManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TenantController : ControllerBase
    {
        private readonly ITenantService _tenantService;

        public TenantController(ITenantService tenantService)
        {
            _tenantService = tenantService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTenant([FromBody] TenantCreateDto model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var newTenantDto = await _tenantService.CreateTenantAsync(model);

            return CreatedAtAction(nameof(GetTenantById), new { id = newTenantDto.Id }, newTenantDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTenantById(int id)
        {
            var tenant = await _tenantService.GetTenantByIdAsync(id);

            if (tenant == null) return NotFound();

            return Ok(tenant);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTenants()
        {
            var tenants = await _tenantService.GetAllTenantsAsync();

            return Ok(tenants);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTenant(int id, [FromBody] TenantCreateDto model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var success = await _tenantService.UpdateTenantAsync(id, model);

            if (!success) return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTenant(int id)
        {
            var success = await _tenantService.DeleteTenantAsync(id);

            if (!success) return NotFound();

            return NoContent();
        }
    }
}