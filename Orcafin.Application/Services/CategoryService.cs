using Orcafin.Application.Dto;
using Orcafin.Application.Interfaces;
using Orcafin.Domain.Entities;
using Orcafin.Domain.Interfaces;

namespace Orcafin.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;

        public CategoryService(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<CategoryResponse> CreateCategoryAsync(CategoryRequest request)
        {
            var category = new Category
            {
                Name = request.Name
            };
            var createdCategory = await _repository.AddAsync(category);
            return new CategoryResponse
            {
                Id = createdCategory.Id,
                Name = createdCategory.Name
            };
        }

        public async Task<CategoryResponse> GetCategoryByIdAsync(int id)
        {
            var category = await _repository.GetByIdAsync(id);
            if (category == null) return null;
            return new CategoryResponse
            {
                Id = category.Id,
                Name = category.Name
            };
        }

        public async Task<IEnumerable<CategoryResponse>> GetAllCategoriesAsync()
        {
            var categories = await _repository.GetAllAsync();
            return categories.Select(category => new CategoryResponse
            {
                Id = category.Id,
                Name = category.Name
            });
        }

        public async Task<CategoryResponse> UpdateCategoryAsync(int id, CategoryRequest request)
        {
            var categoryToUpdate = await _repository.GetByIdAsync(id);
            if (categoryToUpdate == null) return null;

            categoryToUpdate.Name = request.Name;

            await _repository.UpdateAsync(categoryToUpdate);
            return new CategoryResponse
            {
                Id = categoryToUpdate.Id,
                Name = categoryToUpdate.Name
            };
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var category = await _repository.GetByIdAsync(id);
            if (category == null)
            {
                return false;
            }
            await _repository.DeleteAsync(id);
            return true;
        }
    }
}