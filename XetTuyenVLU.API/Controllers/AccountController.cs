using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using XetTuyenVLU.Interfaces;
using XetTuyenVLU.Models;
using XetTuyenVLU.ViewModels.Account;

namespace XetTuyenVLU.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetAllAccounts")]
        public IActionResult GetAllAccounts()
        {
            var result = _accountService.GetAllAccounts();
            if (result.Count == 0)
                return NotFound("Not Found!");
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetAllRoles")]
        public IActionResult GetAllRoles()
        {
            var result = _accountService.GetAllRoles();
            if (result.Count == 0)
                return NotFound("Not Found!");
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetAccountById/{id}")]
        public IActionResult GetAccountById(int id)
        {
            var result = _accountService.GetAccountById(id);
            if (result == null)
                return NotFound("Not Found!");
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("CreateAccount")]
        public async Task<IActionResult> CreateAccount([FromForm] AccountRegisterRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _accountService.CreateAccount(request);
            if (result == 0)
                return BadRequest("Register was unsuccessfully!");
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromForm] AccountLoginRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _accountService.Login(request);
            if (result == null)
                return BadRequest("Login was unsuccessfully!");
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("EditAccount")]
        public async Task<IActionResult> EditAccount([FromForm] AccountEditingRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _accountService.EditAccount(request);
            if (!result)
                return BadRequest("Edit was unsuccessfully!");
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("DeleteAccount/{id}")]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            var result = await _accountService.DeleteAccount(id);
            if (!result)
                return BadRequest("Delete was unsuccessfully!");
            return Ok(result);
        }

        [HttpGet("GetAccountProfile")]
        public IActionResult GetAccountProfile()
        {
            var result = _accountService.GetAccountProfile();
            if (result == null)
                return NotFound("Not Found!");
            return Ok(result);
        }

        [HttpPut("EditAccountProfile")]
        public async Task<IActionResult> EditAccountProfile([FromForm] ProfileEditingRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _accountService.EditAccountProfile(request);
            if (!result)
                return BadRequest("Edit profile was unsuccessfully!");
            return Ok(result);
        }

        [HttpPut("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromForm] ChangePasswordRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _accountService.ChangePassword(request);
            if (!result)
                return BadRequest("Change password was unsuccessfully!");
            return Ok(result);
        }
    }
}
