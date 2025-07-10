using Microsoft.AspNetCore.Mvc;
using Orcafin.Application.Dto;
using Orcafin.Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Orcafin.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserAssignmentController : ControllerBase
    {
        private readonly IUserAssignmentService _userAssignmentService;

        public UserAssignmentController(IUserAssignmentService userAssignmentService)
        {
            _userAssignmentService = userAssignmentService;
        }

        [HttpPost]
        [EndpointSummary("Cria uma nova atribuição de usuário a um plano")]
        [ProducesResponseType(typeof(UserAssignmentResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserAssignmentResponse>> CreateUserAssignment([FromBody] UserAssignmentRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var response = await _userAssignmentService.CreateUserAssignmentAsync(request);
                return CreatedAtAction(nameof(GetUserAssignmentById), new { id = response.Id }, response);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [EndpointSummary("Obtém uma atribuição de usuário a um plano pelo ID")]
        [ProducesResponseType(typeof(UserAssignmentResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserAssignmentResponse>> GetUserAssignmentById(int id)
        {
            var response = await _userAssignmentService.GetUserAssignmentByIdAsync(id);
            if (response == null)
            {
                return NotFound($"Atribuição de usuário com ID {id} não encontrada.");
            }
            return Ok(response);
        }

        [HttpGet]
        [EndpointSummary("Obtém todas as atribuições de usuário a planos")]
        [ProducesResponseType(typeof(IEnumerable<UserAssignmentResponse>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<UserAssignmentResponse>>> GetAllUserAssignments()
        {
            var response = await _userAssignmentService.GetAllUserAssignmentsAsync();
            return Ok(response);
        }

        [HttpPut("{id}")]
        [EndpointSummary("Atualiza uma atribuição de usuário a um plano existente")]
        [ProducesResponseType(typeof(UserAssignmentResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserAssignmentResponse>> UpdateUserAssignment(int id, [FromBody] UserAssignmentRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var response = await _userAssignmentService.UpdateUserAssignmentAsync(id, request);
                if (response == null)
                {
                    return NotFound($"Atribuição de usuário com ID {id} não encontrada para atualização.");
                }
                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [EndpointSummary("Exclui uma atribuição de usuário a um plano pelo ID")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteUserAssignment(int id)
        {
            var success = await _userAssignmentService.DeleteUserAssignmentAsync(id);
            if (!success)
            {
                return NotFound($"Atribuição de usuário com ID {id} não encontrada para exclusão.");
            }
            return NoContent();
        }
    }
}