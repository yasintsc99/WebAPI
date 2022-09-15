using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using WebAPI.Services;
namespace WebAPI.Controllers

{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IJWTAuthService _jwtService;

        public CategoryController(ICategoryService categoryService, IJWTAuthService jwtService)
        {
            _categoryService = categoryService;
            _jwtService = jwtService;
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
                //return CreatedAtAction(nameof(Get), new { id = newcategory.Id }, newcategory);
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
                await _categoryService.UpdateCategory(updatedcategory);
                return Ok(updatedcategory);

            }
        }

        [HttpPost("authenticate")]
        [AllowAnonymous]
        public IActionResult Authenticate([FromBody] UserLogin userLogin)
        {
            var token = _jwtService.Authenticate(userLogin.UserName, userLogin.Password);
            if (token.Equals(null))
                return Unauthorized();
            else
            {

                return Ok(token);
            }

        }
    }
}
