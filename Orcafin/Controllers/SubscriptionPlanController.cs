using Microsoft.AspNetCore.Mvc;
using Orcafin.Application.Dto;
using Orcafin.Application.Interfaces;

namespace Orcafin.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubscriptionPlanController : ControllerBase
    {
        private readonly ISubscriptionPlanService _subscriptionPlanService;

        public SubscriptionPlanController(ISubscriptionPlanService subscriptionPlanService)
        {
            _subscriptionPlanService = subscriptionPlanService;
        }

        [HttpPost]
        [EndpointSummary("Cria um novo plano de assinatura")]
        public async Task<ActionResult<SubscriptionPlanResponse>> CreateSubscriptionPlan([FromBody] SubscriptionPlanRequest request)
        {
            var response = await _subscriptionPlanService.CreateSubscriptionPlanAsync(request);
            return CreatedAtAction(nameof(GetSubscriptionPlanById), new { id = response.Id }, response);
        }

        [HttpGet("{id}")]
        [EndpointSummary("Obtém um plano de assinatura pelo ID")]
        public async Task<ActionResult<SubscriptionPlanResponse>> GetSubscriptionPlanById(int id)
        {
            var response = await _subscriptionPlanService.GetSubscriptionPlanByIdAsync(id);
            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        [HttpGet]
        [EndpointSummary("Obtém todos os planos de assinatura")]
        public async Task<ActionResult<IEnumerable<SubscriptionPlanResponse>>> GetAllSubscriptionPlans()
        {
            var response = await _subscriptionPlanService.GetAllSubscriptionPlansAsync();
            return Ok(response);
        }

        [HttpPut("{id}")]
        [EndpointSummary("Atualiza um plano de assinatura existente")]
        public async Task<ActionResult<SubscriptionPlanResponse>> UpdateSubscriptionPlan(int id, [FromBody] SubscriptionPlanRequest request)
        {
            var response = await _subscriptionPlanService.UpdateSubscriptionPlanAsync(id, request);
            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        [HttpDelete("{id}")]
        [EndpointSummary("Exclui um plano de assinatura pelo ID")]
        public async Task<ActionResult> DeleteSubscriptionPlan(int id)
        {
            await _subscriptionPlanService.DeleteSubscriptionPlanAsync(id);
            return NoContent();
        }
    }
}