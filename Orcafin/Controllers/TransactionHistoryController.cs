using Microsoft.AspNetCore.Mvc;
using Orcafin.Application.Dto;
using Orcafin.Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Orcafin.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionHistoryController : ControllerBase
    {
        private readonly ITransactionHistoryService _transactionHistoryService;

        public TransactionHistoryController(ITransactionHistoryService transactionHistoryService)
        {
            _transactionHistoryService = transactionHistoryService;
        }

        [HttpPost]
        [EndpointSummary("Cria um novo registro de histórico de transação")]
        [ProducesResponseType(typeof(TransactionHistoryResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TransactionHistoryResponse>> CreateTransactionHistory([FromBody] TransactionHistoryRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var response = await _transactionHistoryService.CreateTransactionHistoryAsync(request);
                return CreatedAtAction(nameof(GetTransactionHistoryById), new { id = response.Id }, response);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [EndpointSummary("Obtém um registro de histórico de transação pelo ID")]
        [ProducesResponseType(typeof(TransactionHistoryResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TransactionHistoryResponse>> GetTransactionHistoryById(int id)
        {
            var response = await _transactionHistoryService.GetTransactionHistoryByIdAsync(id);
            if (response == null)
            {
                return NotFound($"Registro de histórico de transação com ID {id} não encontrado.");
            }
            return Ok(response);
        }

        [HttpGet]
        [EndpointSummary("Obtém todos os registros de histórico de transação")]
        [ProducesResponseType(typeof(IEnumerable<TransactionHistoryResponse>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<TransactionHistoryResponse>>> GetAllTransactionHistory()
        {
            var response = await _transactionHistoryService.GetAllTransactionHistoryAsync();
            return Ok(response);
        }

        [HttpPut("{id}")]
        [EndpointSummary("Atualiza um registro de histórico de transação existente")]
        [ProducesResponseType(typeof(TransactionHistoryResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TransactionHistoryResponse>> UpdateTransactionHistory(int id, [FromBody] TransactionHistoryRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var response = await _transactionHistoryService.UpdateTransactionHistoryAsync(id, request);
                if (response == null)
                {
                    return NotFound($"Registro de histórico de transação com ID {id} não encontrado para atualização.");
                }
                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [EndpointSummary("Exclui um registro de histórico de transação pelo ID")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteTransactionHistory(int id)
        {
            var success = await _transactionHistoryService.DeleteTransactionHistoryAsync(id);
            if (!success)
            {
                return NotFound($"Registro de histórico de transação com ID {id} não encontrado para exclusão.");
            }
            return NoContent();
        }
    }
}