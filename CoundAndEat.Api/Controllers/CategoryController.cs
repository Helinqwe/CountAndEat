﻿using CoundAndEat.Api.Extension;
using CoundAndEat.Api.Interfaces;
using CoundAndEat.Dtos.Auth;
using CoundAndEat.Dtos.Category;
using CoundAndEat.Dtos.Recept;
using Microsoft.AspNetCore.Mvc;

namespace CoundAndEat.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        [Route("getCategories")]
        public async Task<ActionResult<List<CategoryDto>>> GetCategories()
        {
            try
            {
                var categories = await _categoryRepository.GetCategories();
                if (categories == null)
                {
                    return NotFound();
                }
                else
                {
                    var categoriesDto = categories.ConvertToDto();
                    return Ok(categoriesDto);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ошибка получения данных из базы данных");
            }
        }
        [HttpGet]
        [Route("getCategory/{id:long}")]
        public async Task<ActionResult<CategoryDto>> GetCategory(long id)
        {
            try
            {
                var category = await _categoryRepository.GetCategory(id);
                if (category == null)
                {
                    return NotFound();
                }
                else
                {
                    var categoryDto = category.ConverToDto();
                    return Ok(categoryDto);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ошибка получения данных из базы данных");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddNewCategory([FromBody] CategoryDto categoryDto)
        {
            try
            {
                var newCategory = await _categoryRepository.AddNewCategory(categoryDto);
                if (newCategory == null)
                {
                    return NoContent();
                }
                return Ok(newCategory);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ошибка создания");
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCategory(long id, UpdateCategoryDto categoryDto)
        {
            try
            {
                var updateCategory = await _categoryRepository.UpdateCategory(id, categoryDto);
                if (updateCategory == null)
                {
                    return NoContent();
                }
                var response = await _categoryRepository.GetCategory(updateCategory.Id);
                var result = response.ConverToDto();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpDelete]
        public async Task<ActionResult<Status>> DeleteCategory(long id)
        {
            try
            {
                var category = await _categoryRepository.DeleteCategory(id);
                return category;
            }
            catch (Exception ex)
            {
                return new Status { Message = ex.Message, StatusCode = 500 };
            }
        }

        [HttpGet]
        [Route("{catid}/getResepts")]
        public async Task<ActionResult<List<ReceptDto>>> GetReceptsByCategory(long catid)
        {
            try
            {
                var recepts = await _categoryRepository.GetReceptsByCategory(catid);
                var receptsDto = recepts.ConvertToDto();
                return Ok(receptsDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ошибка получения данных из базы данных");
            }
        }
    }
}
