using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using XetTuyenVLU.Interfaces;
using XetTuyenVLU.ViewModels.AdmissionAdmin;

namespace XetTuyenVLU.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AdmissionAdminController : Controller
    {
        private readonly IAdmisionAdminService _admissionAdminService;
        public AdmissionAdminController(IAdmisionAdminService admisionAdminService)
        {
            _admissionAdminService = admisionAdminService;
        }

        [HttpGet("GetDataForHoso")]
        public IActionResult GetDataForHoso()
        {
            var result = _admissionAdminService.GetDatainHoso();
            if (result.Count == 0)
            {
                return NotFound("Not Found!");
            }
            return Ok(result);
        }
        [HttpGet("GetDataForBangDiem")]
        public IActionResult GetDataForBangDiem()
        {
            var result = _admissionAdminService.GetBangDiem();
            if (result.Count == 0)
            {
                return NotFound("Not Found!");
            }
            return Ok(result);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("GetMailBeforeSend")]
        public IActionResult GetMailBeforeSend()
        {
            var result = _admissionAdminService.GetMailBeforeSend();
            if (result == null)
            {
                return NotFound("Not Found!");
            }
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetAdmissionById/{id}")]
        public IActionResult GetAdmissionById(int id)
        {
            var result = _admissionAdminService.GetAdmissionById(id);
            if (result == null)
            {
                return NotFound("Not Found!");
            }
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("DeleteDataForHosoThpt/{Id}")]
        public async Task<IActionResult> DeleteDataForHosoThpt(long Id)
        {
            try
            {
                var result = await _admissionAdminService.DeleteHoSoThpts(Id);
                if (!result)
                {
                    return BadRequest("Delete was unsuccessfully!");
                }
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("DeleteBangDiemForBangDiemThpt/{MaHoSoThpt}")]
        public async Task<IActionResult> DeleteBangDiemForBangdiemThpt(long MaHoSoThpt)
        {
            try
            {
                var result = await _admissionAdminService.DeleteBangDiemThpts(MaHoSoThpt);
                if (!result)
                {
                    return BadRequest("Delete was unsuccessfully!");
                }
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("EditAdmission")]
        public async Task<IActionResult> EditAdmission([FromForm] AdmissionEditingRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _admissionAdminService.EditAdmission(request);
            if (!result)
                return BadRequest("Edit admission was unsuccessfully!");
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("SendEmailForAdmission")]
        public async Task<IActionResult> SendEmailForAdmission([FromForm] AdmissionSendMailRequest request)
        {
            var result = await _admissionAdminService.SendEmailForAdmission(request);
            if (!result)
                return BadRequest("Send mail for admission was unsuccessfully!");
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("ReceiveAdmissionProfileById/{id}")]
        public IActionResult ReceiveAdmissionProfileById(int id)
        {
            var result = _admissionAdminService.ReceiveAdmissionProfileById(id);
            if (result == null)
            {
                return NotFound("Not Found!");
            }
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("RejectAdmissionProfileById/{id}")]
        public IActionResult RejectAdmissionProfileById(int id)
        {
            var result = _admissionAdminService.RejectAdmissionProfileById(id);
            if (result == null)
            {
                return NotFound("Not Found!");
            }
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetPhase")]
        public IActionResult GetPhase()
        {
            var result = _admissionAdminService.GetPhase();
            if (result.Count == 0)
            {
                return NotFound("Not Found!");
            }
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("DownloadAdmissionFiles/{id}")]
        public IActionResult DownloadAdmissionFiles(int id)
        {
            var result = _admissionAdminService.DownloadAdmissionFiles(id);
            if (result == null)
                return NotFound("Not Found!");
            return File(result.fileContents, result.fileType, result.fileName);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("UpdateSendMailStatusById/{id}")]
        public IActionResult UpdateSendMailStatusById(int id)
        {
            var result = _admissionAdminService.UpdateSendMailStatusById(id);
            if (result == null)
            {
                return NotFound("Not Found!");
            }
            return Ok(result);
        }
    }
}
