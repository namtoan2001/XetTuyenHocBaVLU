using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using XetTuyenVLU.Interfaces;
using XetTuyenVLU.ViewModels.Schedule;

namespace XetTuyenVLU.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly IScheduleService _scheduleService;
        public ScheduleController(IScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
        }

        [HttpGet("GetAllSchedules")]
        public IActionResult GetAllSchedules()
        {
            var result = _scheduleService.GetAllSchedules();
            if (result.Count == 0)
                return NotFound("Not Found!");
            return Ok(result);
        }

        [HttpPost("CreateSchedule")]
        public async Task<IActionResult> CreateSchedule([FromForm] ScheduleCreateRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _scheduleService.CreateSchedule(request);
            if (result == 0)
                return BadRequest("Create schedule was unsuccessfully!");
            return Ok(result);
        }

        [HttpPut("ChangeStatusSchedule/{id}")]
        public async Task<IActionResult> ChangeStatusSchedule(int id)
        {
            var result = await _scheduleService.ChangeStatusSchedule(id);
            if (!result)
                return BadRequest("Change status schedule was unsuccessfully!");
            return Ok(result);
        }

        [HttpGet("ValidateAllSchedulesWereExpired")]
        public IActionResult ValidateAllSchedulesWereExpired()
        {
            var result = _scheduleService.ValidateAllSchedulesWereExpired();
            return Ok(result);
        }

        [HttpGet("GetAllPhasesNotExpiry")]
        public IActionResult GetAllPhasesNotExpiry()
        {
            var result = _scheduleService.GetAllPhasesNotExpiry();
            return Ok(result);
        }

        [HttpGet("GetCategoriesForSchedule")]
        public IActionResult GetCategoriesForSchedule()
        {
            var result = _scheduleService.GetCategoriesForSchedule();
            return Ok(result);
        }
    }
}
