﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using WebAPI.Services;
namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet]

        public async Task<IActionResult> Get()
        {
            var categories = await _categoryService.GetCategories();
            return Ok(categories);
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetByID(int id)
        {
            var category = await _categoryService.GetCategoryByID(id);
            if (category == null)
                return NotFound();
            else return Ok(category);
        }
        [HttpPost]
        public async Task<IActionResult> Post(Category newcategory)
        {
            if (_categoryService.isThere(newcategory.CategoryId))
                return new JsonResult("The document already exists!");

            else
            {
                int lastID = _categoryService.GetCollectionCount();
                newcategory.CategoryId = lastID + 1;
                await _categoryService.CreateCategory(newcategory);
                return Ok(newcategory);
            }
        }
        [HttpDelete]
        [Route("{id}")]

        public async Task<IActionResult> Delete(int id)
        {
            var category = await _categoryService.GetCategoryByID(id);
            if (category == null)
                return NotFound();
            else
            {
                await _categoryService.DeleteCategory(id);
                return NoContent();
            }
        }
        [HttpPut]

        public async Task<IActionResult> Update(Category updatedcategory)
        {
            var category = await _categoryService.GetCategoryByID(updatedcategory.CategoryId);
            if (category == null)
                return NotFound();
            else
            {
                if ((category.Id != updatedcategory.Id) || (category.CategoryId != updatedcategory.CategoryId))
                {
                    return new JsonResult("You can not change the '_id' and 'PostID' fields !");
                }
                else
                {
                    await _categoryService.UpdateCategory(updatedcategory);
                    return Ok(updatedcategory);
                }
            }
        }
    }
}
