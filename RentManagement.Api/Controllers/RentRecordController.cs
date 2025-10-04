using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentManagement.Api.DTOs;
using RentManagement.Api.Interfaces;

namespace RentManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RentRecordController : ControllerBase
    {
        private readonly IRentRecordService _service;

        public RentRecordController(IRentRecordService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RentRecordCreateDto model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var (record, error) = await _service.CreateAsync(model);
            if (error != null) return BadRequest(new { Message = error });

            return CreatedAtAction(nameof(GetById), new { id = record!.Id }, record);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int? shopId, [FromQuery] int? tenantId,
                                                [FromQuery] int? year, [FromQuery] int? month)
        {
            var items = await _service.GetAllAsync(shopId, tenantId, year, month);
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var record = await _service.GetByIdAsync(id);
            if (record == null) return NotFound();
            return Ok(record);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ok = await _service.DeleteAsync(id);
            if (!ok) return NotFound();
            return NoContent();
        }
    }
}
