using Microsoft.AspNetCore.Mvc;
using Orcafin.Application.Dto;
using Orcafin.Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Orcafin.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost]
        [EndpointSummary("Cria uma nova categoria")]
        [ProducesResponseType(typeof(CategoryResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CategoryResponse>> CreateCategory([FromBody] CategoryRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var response = await _categoryService.CreateCategoryAsync(request);
            return CreatedAtAction(nameof(GetCategoryById), new { id = response.Id }, response);
        }

        [HttpGet("{id}")]
        [EndpointSummary("Obtém uma categoria pelo ID")]
        [ProducesResponseType(typeof(CategoryResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CategoryResponse>> GetCategoryById(int id)
        {
            var response = await _categoryService.GetCategoryByIdAsync(id);
            if (response == null)
            {
                return NotFound($"Categoria com ID {id} não encontrada.");
            }
            return Ok(response);
        }

        [HttpGet]
        [EndpointSummary("Obtém todas as categorias")]
        [ProducesResponseType(typeof(IEnumerable<CategoryResponse>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<CategoryResponse>>> GetAllCategories()
        {
            var response = await _categoryService.GetAllCategoriesAsync();
            return Ok(response);
        }

        [HttpPut("{id}")]
        [EndpointSummary("Atualiza uma categoria existente")]
        [ProducesResponseType(typeof(CategoryResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CategoryResponse>> UpdateCategory(int id, [FromBody] CategoryRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var response = await _categoryService.UpdateCategoryAsync(id, request);
            if (response == null)
            {
                return NotFound($"Categoria com ID {id} não encontrada para atualização.");
            }
            return Ok(response);
        }

        [HttpDelete("{id}")]
        [EndpointSummary("Exclui uma categoria pelo ID")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            var success = await _categoryService.DeleteCategoryAsync(id);
            if (!success)
            {
                return NotFound($"Categoria com ID {id} não encontrada para exclusão.");
            }
            return NoContent();
        }
    }
}