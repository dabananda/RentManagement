using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentManagement.Api.DTOs;
using RentManagement.Api.Interfaces;

namespace RentManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RentalAgreementController : ControllerBase
    {
        private readonly IRentalAgreementService _agreementService;

        public RentalAgreementController(IRentalAgreementService agreementService)
        {
            _agreementService = agreementService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAgreement([FromBody] RentalAgreementCreateDto model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var (newAgreementDto, errorMessage) = await _agreementService.CreateAgreementAsync(model);

            if (errorMessage != null) return BadRequest(new { Message = errorMessage });

            return CreatedAtAction(nameof(GetAgreementById), new { id = newAgreementDto!.Id }, newAgreementDto);
        }

        [HttpPost("EndAgreement/{id}")]
        public async Task<IActionResult> EndAgreement(int id)
        {
            var success = await _agreementService.EndAgreementAsync(id);
            if (!success) return NotFound();

            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAgreementById(int id)
        {
            var agreement = await _agreementService.GetAgreementByIdAsync(id);
            if (agreement == null) return NotFound();

            return Ok(agreement);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAgreements()
        {
            var agreements = await _agreementService.GetAllAgreementsAsync();

            return Ok(agreements);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAgreement(int id, [FromBody] RentalAgreementCreateDto model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var agreement = await _agreementService.GetAgreementByIdAsync(id);
            if (agreement == null) return NotFound();

            var result = await _agreementService.UpdateAgreementAsync(id, model);
            if (!result) return BadRequest("Couldn't update. Please try again");

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAgreement(int id)
        {
            var agreement = await _agreementService.GetAgreementByIdAsync(id);
            if (agreement == null) return NotFound();

            var result = await _agreementService.DeleteAgreementAsync(id);
            if (!result) return BadRequest("Couldn't delete. Please try again");

            return NoContent();
        }
    }
}