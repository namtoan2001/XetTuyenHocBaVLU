using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using XetTuyenVLU.Interfaces;
using XetTuyenVLU.Models;

namespace XetTuyenVLU.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class PhaseController : ControllerBase
    {
        private readonly IPhaseService _phaseService;
        public PhaseController(IPhaseService phaseService)
        {
            _phaseService = phaseService;
        }

        [HttpGet("GetAllPhases")]
        public IActionResult GetAllPhases()
        {
            var result = _phaseService.GetAllPhases();
            if (result.Count == 0)
                return NotFound("Not Found!");
            return Ok(result);
        }

        [HttpPost("CreatePhase")]
        public async Task<IActionResult> CreatePhase([FromForm] Dot request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _phaseService.CreatePhase(request);
            if (result == 0)
                return BadRequest("Create phase was unsuccessfully!");
            return Ok(result);
        }

        [HttpPut("ChangeStatusPhase/{id}")]
        public async Task<IActionResult> ChangeStatusPhase(int id)
        {
            var result = await _phaseService.ChangeStatusPhase(id);
            if (!result)
                return BadRequest("Change status phase was unsuccessfully!");
            return Ok(result);
        }

        [HttpGet("ValidateAllPhasesWereExpired")]
        public IActionResult ValidateAllPhasesWereExpired()
        {
            var result = _phaseService.ValidateAllPhasesWereExpired();
            return Ok(result);
        }
    }
}
