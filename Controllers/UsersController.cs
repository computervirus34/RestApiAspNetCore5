using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestAPI.Core.IConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public UsersController(
            ILogger<UsersController> logger,
            IUnitOfWork unitOfWork
            )
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(User user)
        {
            if (ModelState.IsValid)
            {
                user.Id = Guid.NewGuid();

                await _unitOfWork.Users.Add(user);
                await _unitOfWork.CompleteAsync();

                return CreatedAtAction("GetItem", new { user.Id }, user);
            }

            return new JsonResult("Something went wrong") { StatusCode = 500 };
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetItem(Guid id)
        {
            var user = await _unitOfWork.Users.GetByID(id);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users =await _unitOfWork.Users.All();

            return Ok(users);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, User user)
        {
            if (id != user.Id)
                return BadRequest();

            await _unitOfWork.Users.Update(user);
            await _unitOfWork.CompleteAsync();
            
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var user = await _unitOfWork.Users.GetByID(id);

            if (user == null)
                return NotFound();

            await _unitOfWork.Users.Delete(id);
            await _unitOfWork.CompleteAsync();

            return Ok(id);
        }
    }
}
