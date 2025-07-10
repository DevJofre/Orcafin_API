using Microsoft.AspNetCore.Mvc;
using Orcafin.Application.Dto;
using Orcafin.Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Orcafin.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentHistoryController : ControllerBase
    {
        private readonly IPaymentHistoryService _paymentHistoryService;

        public PaymentHistoryController(IPaymentHistoryService paymentHistoryService)
        {
            _paymentHistoryService = paymentHistoryService;
        }

        [HttpPost]
        [EndpointSummary("Cria um novo registro de histórico de pagamento")]
        [ProducesResponseType(typeof(PaymentHistoryResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PaymentHistoryResponse>> CreatePaymentHistory([FromBody] PaymentHistoryRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var response = await _paymentHistoryService.CreatePaymentHistoryAsync(request);
                return CreatedAtAction(nameof(GetPaymentHistoryById), new { id = response.Id }, response);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [EndpointSummary("Obtém um registro de histórico de pagamento pelo ID")]
        [ProducesResponseType(typeof(PaymentHistoryResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PaymentHistoryResponse>> GetPaymentHistoryById(int id)
        {
            var response = await _paymentHistoryService.GetPaymentHistoryByIdAsync(id);
            if (response == null)
            {
                return NotFound($"Registro de histórico de pagamento com ID {id} não encontrado.");
            }
            return Ok(response);
        }

        [HttpGet]
        [EndpointSummary("Obtém todos os registros de histórico de pagamento")]
        [ProducesResponseType(typeof(IEnumerable<PaymentHistoryResponse>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PaymentHistoryResponse>>> GetAllPaymentHistory()
        {
            var response = await _paymentHistoryService.GetAllPaymentHistoryAsync();
            return Ok(response);
        }

        [HttpPut("{id}")]
        [EndpointSummary("Atualiza um registro de histórico de pagamento existente")]
        [ProducesResponseType(typeof(PaymentHistoryResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PaymentHistoryResponse>> UpdatePaymentHistory(int id, [FromBody] PaymentHistoryRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var response = await _paymentHistoryService.UpdatePaymentHistoryAsync(id, request);
                if (response == null)
                {
                    return NotFound($"Registro de histórico de pagamento com ID {id} não encontrado para atualização.");
                }
                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [EndpointSummary("Exclui um registro de histórico de pagamento pelo ID")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeletePaymentHistory(int id)
        {
            var success = await _paymentHistoryService.DeletePaymentHistoryAsync(id);
            if (!success)
            {
                return NotFound($"Registro de histórico de pagamento com ID {id} não encontrado para exclusão.");
            }
            return NoContent();
        }
    }
}