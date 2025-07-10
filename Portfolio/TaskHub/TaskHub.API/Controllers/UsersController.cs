using Microsoft.AspNetCore.Mvc;

using TaskHub.API.Dtos;
using TaskHub.API.Models;
using TaskHub.API.Repositories;

namespace TaskHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UsersRepository repository;

        public UsersController(UsersRepository repository)
        {
            this.repository = repository;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<IEnumerable<User>> GetUsers()
        {
            return await repository.GetAllUsersAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User?>> GetUser(int id)
        {
            var user = await repository.GetUserByIdAsync(id);

            if (user is null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(UserDto dto)
        {
            if (dto is null)
            {
                return BadRequest("User cannot be null.");
            }

            var user = await repository.CreateUserAsync(dto);

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, UserDto dto)
        {
            if (UserExists(id) is false)
            {
                return NotFound("User does not exists.");
            }

            await repository.UpdateUserAsync(id, dto);

            return NoContent();
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await repository.GetUserByIdAsync(id);
            if (user is null)
            {
                return NotFound();
            }

            await repository.DeleteUserAsync(user);

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return repository.GetUserByIdAsync(id) is not null;
        }
    }
}
