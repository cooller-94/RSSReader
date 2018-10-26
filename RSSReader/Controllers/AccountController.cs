using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Infrastructure;
using Infrastructure.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Web.Models;
using Web.Models.Account;

namespace RSSReader.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private RSSReaderContext _context;
        private readonly UserManager<User> _userManager;
        private readonly ClaimsPrincipal _caller;

        public AccountController(RSSReaderContext context, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _userManager = userManager;
            _caller = httpContextAccessor.HttpContext.User;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterUser([FromBody]ResigstrationUserModel registerUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _userManager.CreateAsync(new User() {UserName = registerUser.Email, Email = registerUser.Email, FullName = registerUser.FullName}, registerUser.Password);

            if (result.Succeeded)
            {
                return new OkResult();
            }
            else
            {
                return BadRequest(result.Errors.FirstOrDefault().Description);
            }
        }

        [HttpGet("loggedIn")]
        public async Task<IActionResult> GetCurrentUser()
        {
            Claim userId = _caller.Claims.FirstOrDefault(c => c.Type == "id");

            if (userId == null)
            {
                return Unauthorized();
            }

            var user = await _userManager.FindByIdAsync(userId.Value);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(new UserModel() { Email = user.Email, FullName = user.FullName, PictureUrl = user.PictureUrl });
        }
    }
}