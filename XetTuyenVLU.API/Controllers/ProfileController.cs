using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using XetTuyenVLU.Interfaces;
using XetTuyenVLU.ViewModels.Profile;


namespace XetTuyenVLU.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {

        private readonly IProfileService _iProfileService;

        public ProfileController(IProfileService iProfileService)
        {
            _iProfileService = iProfileService;
        }

        [HttpGet("GetProfileByCMND/{cmnd}/{dot}")]
        public IActionResult GetProfileByCMND(string cmnd, int dot)
        {
            var result = _iProfileService.GetProfileByCMND(cmnd,dot);
            if(result == null)
            {
                return NotFound("Not Found!");
            }
            return Ok(result);
        }


        [HttpGet("GetBangDiem/{maHoSo}")]
        public IActionResult GetBangDiem(int maHoSo)
        {
            var result = _iProfileService.GetBangDiem(maHoSo);
            if (result.Count == 0)
                return NotFound("Not Found!");
            return Ok(result);
        }


        [HttpGet("ValidateCMNDEdit/{cmnd}/{currentCmnd}")]
        public IActionResult ValidateCMND(string cmnd, string currentCmnd)
        {
            var result = _iProfileService.ValidateCMNDToEdit(cmnd, currentCmnd);
            return Ok(result);
        }

        [HttpPut("EditProfile")]
        public async Task<IActionResult> EditProfile([FromForm] EditProfileRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _iProfileService.EditProfile(request);
            if (!result)
                return BadRequest("Edit was unsuccessfully!");
            return Ok(result);
        }
        [HttpPost("AddImgPathHocBa")]
        public async Task<IActionResult> AddImgPathHocBa([FromForm] AddImgPath hocBa)
        {
            var result = await _iProfileService.AddImgPathHocBa(hocBa);

            return Ok(result);
        }


    }
}
