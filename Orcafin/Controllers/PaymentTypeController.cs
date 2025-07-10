using Microsoft.AspNetCore.Mvc;
using Orcafin.Application.Dto;
using Orcafin.Application.Interfaces;

namespace Orcafin.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentTypeController : ControllerBase
    {
        private readonly IPaymentTypeService _paymentTypeService;

        public PaymentTypeController(IPaymentTypeService paymentTypeService)
        {
            _paymentTypeService = paymentTypeService;
        }

        [HttpPost]
        [EndpointSummary("Cria um novo tipo de pagamento")]
        public async Task<ActionResult<PaymentTypeResponse>> CreatePaymentType([FromBody] PaymentTypeRequest request)
        {
            var response = await _paymentTypeService.CreatePaymentTypeAsync(request);
            return CreatedAtAction(nameof(GetPaymentTypeById), new { id = response.Id }, response);
        }

        [HttpGet("{id}")]
        [EndpointSummary("Obtém um tipo de pagamento pelo ID")]
        public async Task<ActionResult<PaymentTypeResponse>> GetPaymentTypeById(int id)
        {
            var response = await _paymentTypeService.GetPaymentTypeByIdAsync(id);
            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        [HttpGet]
        [EndpointSummary("Obtém todos os tipos de pagamento")]
        public async Task<ActionResult<IEnumerable<PaymentTypeResponse>>> GetAllPaymentTypes()
        {
            var response = await _paymentTypeService.GetAllPaymentTypesAsync();
            return Ok(response);
        }

        [HttpPut("{id}")]
        [EndpointSummary("Atualiza um tipo de pagamento existente")]
        public async Task<ActionResult<PaymentTypeResponse>> UpdatePaymentType(int id, [FromBody] PaymentTypeRequest request)
        {
            var response = await _paymentTypeService.UpdatePaymentTypeAsync(id, request);
            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        [HttpDelete("{id}")]
        [EndpointSummary("Exclui um tipo de pagamento pelo ID")]
        public async Task<ActionResult> DeletePaymentType(int id)
        {
            await _paymentTypeService.DeletePaymentTypeAsync(id);
            return NoContent();
        }
    }
}