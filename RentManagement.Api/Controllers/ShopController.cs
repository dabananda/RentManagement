using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentManagement.Api.DTOs;
using RentManagement.Api.Interfaces;

namespace RentManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ShopController : ControllerBase
    {
        private readonly IShopService _shopService;

        public ShopController(IShopService shopService)
        {
            _shopService = shopService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateShop([FromBody] ShopCreateDto model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var newShopDto = await _shopService.CreateShopAsync(model);

            return CreatedAtAction(nameof(GetShopById), new { id = newShopDto.Id }, newShopDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetShopById(int id)
        {
            var shop = await _shopService.GetShopByIdAsync(id);

            if (shop == null) return NotFound();

            return Ok(shop);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllShops()
        {
            var shops = await _shopService.GetAllShopsAsync();

            return Ok(shops);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateShop(int id, [FromBody] ShopCreateDto model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var success = await _shopService.UpdateShopAsync(id, model);

            if (!success) return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShop(int id)
        {
            var success = await _shopService.DeleteShopAsync(id);

            if (!success) return NotFound();

            return NoContent();
        }
    }
}