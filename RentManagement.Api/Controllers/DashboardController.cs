using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentManagement.Api.Interfaces;

namespace RentManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCardData()
        {
            var dashboardData = await _dashboardService.GetCardDataAsync();

            return Ok(dashboardData);
        }

        [HttpGet("table")]
        public async Task<IActionResult> GetAgreementTableData([FromQuery] string? search = null)
        {
            var agreements = await _dashboardService.GetAgreementTableDataAsync(search);

            return Ok(agreements);
        }
    }
}
