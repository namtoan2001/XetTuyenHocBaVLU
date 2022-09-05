using Microsoft.AspNetCore.Mvc;
using XetTuyenVLU.Interfaces;
using XetTuyenVLU.ViewModels.Admission;

namespace XetTuyenVLU.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdmissionController : ControllerBase
    {
        private readonly IAdmissionService _admissionService;

        public AdmissionController(IAdmissionService admissionService)
        {
            _admissionService = admissionService;
        }

        [HttpGet("GetCityProvincesForHoKhau")]
        public IActionResult GetCityProvincesForHoKhau()
        {
            var result = _admissionService.GetCityProvincesForHoKhau();
            if(result.Count == 0)
                return NotFound("Not Found!");
            return Ok(result);
        }

        [HttpGet("GetDistrictsForHoKhau/{MaTinhTP}")]
        public IActionResult GetDistrictsForHoKhau(string MaTinhTP)
        {
            var result = _admissionService.GetDistrictsForHoKhau(MaTinhTP);
            if (result.Count == 0)
                return NotFound("Not Found!");
            return Ok(result);
        }

        [HttpGet("GetWardsForHoKhau/{MaQH}")]
        public IActionResult GetWardsForHoKhau(string MaQH)
        {
            var result = _admissionService.GetWardsForHoKhau(MaQH);
            if (result.Count == 0)
                return NotFound("Not Found!");
            return Ok(result);
        }

        [HttpGet("GetCityProvincesForSchool")]
        public IActionResult GetCityProvincesForSchool()
        {
            var result = _admissionService.GetCityProvincesForSchool();
            if (result.Count == 0)
                return NotFound("Not Found!");
            return Ok(result);
        }

        [HttpGet("GetDistrictsForSchool/{MaTinhTP}")]
        public IActionResult GetDistrictsForSchool(string MaTinhTP)
        {
            var result = _admissionService.GetDistrictsForSchool(MaTinhTP);
            if (result.Count == 0)
                return NotFound("Not Found!");
            return Ok(result);
        }

        [HttpGet("GetSchools/{MaTinhTP}/{MaQH}")]
        public IActionResult GetSchools(string MaTinhTP, string MaQH)
        {
            var result = _admissionService.GetSchools(MaTinhTP, MaQH);
            if (result.Count == 0)
                return NotFound("Not Found!");
            return Ok(result);
        }

        [HttpGet("GetEthnics")]
        public IActionResult GetEthnics()
        {
            var result = _admissionService.GetEthnics();
            if (result.Count == 0)
                return NotFound("Not Found!");
            return Ok(result);
        }

        [HttpGet("GetReligions")]
        public IActionResult GetReligions()
        {
            var result = _admissionService.GetReligions();
            if (result.Count == 0)
                return NotFound("Not Found!");
            return Ok(result);
        }

        [HttpGet("GetNationalities")]
        public IActionResult GetNationalities()
        {
            var result = _admissionService.GetNationalities();
            if (result.Count == 0)
                return NotFound("Not Found!");
            return Ok(result);
        }

        [HttpGet("GetCertificateLanguages")]
        public IActionResult GetCertificateLanguages()
        {
            var result = _admissionService.GetCertificateLanguages();
            if (result.Count == 0)
                return NotFound("Not Found!");
            return Ok(result);
        }

        [HttpGet("GetNganhXetTuyen")]
        public IActionResult GetNganhXetTuyen()
        {
            var result = _admissionService.GetNganhXetTuyen();
            if (result.Count == 0)
                return NotFound("Not Found!");
            return Ok(result);
        }

        [HttpGet("GetToHopXetTuyen/{MaNganh}")]
        public IActionResult GetToHopXetTuyen(string MaNganh)
        {
            var result = _admissionService.GetToHopXetTuyen(MaNganh);
            if (result.Count == 0)
                return NotFound("Not Found!");
            return Ok(result);
        }

        [HttpGet("ValidateCMND/{cmnd}")]
        public IActionResult ValidateCMND(string cmnd)
        {
            var result = _admissionService.ValidateCMND(cmnd);
            return Ok(result);
        }

        [HttpPost("CreateAdmission")]
        public async Task<IActionResult> CreateAdmission([FromForm] AdmissionCreateRequest request)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            var admissionId = await _admissionService.CreateAdmission(request);

            if(admissionId == "")
                return BadRequest("Creating Admission was unsuccessfully!");
            return Ok(admissionId);
        }

        [HttpGet("GetPhase")]
        public IActionResult GetPhase()
        {
            var result = _admissionService.GetPhase();
            if (result == null)
                return NotFound("Not Found!");
            return Ok(result);
        }

        [HttpGet("GetScheduleForEditProfile")]
        public IActionResult GetScheduleForEditProfile()
        {
            var result = _admissionService.GetScheduleForEditProfile();
            if (result == null)
                return NotFound("Not Found!");
            return Ok(result);
        }
            
        [HttpGet("ValidatePhaseIsExpired")]
        public IActionResult ValidatePhaseIsExpired()
        {
            var result = _admissionService.ValidatePhaseIsExpired();
            return Ok(result);
        }

        [HttpGet("GetNotificationForPhaseIsExpired")]
        public IActionResult GetNotificationForPhaseIsExpired()
        {
            var result = _admissionService.GetNotificationForPhaseIsExpired();
            if (result == null)
                return NotFound("Not Found!");
            return Ok(result);
        }
    }
}
