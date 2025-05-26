using Microsoft.AspNetCore.Mvc;
using Orcafin.Application.Dto;
using Orcafin.Application.Interfaces;


namespace Orcafin.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/users
        [HttpGet]
        [EndpointSummary("Lista todos os usuários")]
        public async Task<ActionResult<IEnumerable<UserResponse>>> GetAll()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }

        // GET: api/users/{id}
        [HttpGet("{id}")]
        [EndpointSummary("Busca usuário por ID")]
        public async Task<ActionResult<UserResponse>> GetById(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        // POST: api/users
        [HttpPost]
        [EndpointSummary("Cria um novo usuário")]
        public async Task<ActionResult> Create([FromBody] UserCreateRequest request)
        {
            var user = await _userService.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
        }

        // PUT: api/users/{id}
        [HttpPut("{id}")]
        [EndpointSummary("Atualiza um usuário")]
        public async Task<ActionResult> Update(int id, [FromBody] UserUpdateRequest request)
        {
            var success = await _userService.UpdateAsync(id, request);
            if (!success)
                return NotFound();

            return NoContent();
        }

        // DELETE: api/users/{id}
        [HttpDelete("{id}")]
        [EndpointSummary("Remove um usuário")]
        public async Task<ActionResult> Delete(int id)
        {
            var success = await _userService.DeleteAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
