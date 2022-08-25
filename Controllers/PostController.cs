using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using WebAPI.Models;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        public PostController(IPostService postService)
        {
            _postService = postService;

        }
        [HttpGet]

        public async Task<IActionResult> Get()
        {
            var posts = await _postService.GetAllPosts();
            return Ok(posts);
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetByID(int id)
        {
            var post = await _postService.GetPostByID(id);
            if (post == null)
                return NotFound();
            else return Ok(post);
        }
        [HttpPost]
        public async Task<IActionResult> Post(Post newPost)
        {
            
            if(_postService.isThere(newPost.PostID))
            {
                 return new JsonResult("The document already exists");
            }
            else
            {
                int lastID = _postService.GetCollectionCount();
                newPost.PostID = lastID + 1;
                await _postService.CreatePost(newPost);
                return Ok(newPost);
            }
        }
        [HttpDelete]
        [Route("{id}")]

        public async Task<IActionResult> Delete(int id)
        {
            var post = await _postService.GetPostByID(id);
            if (post == null)
                return NotFound();
            else
            {
                await _postService.DeletePost(id);
                return NoContent();
            }
        }
        [HttpPut]
        public async Task<IActionResult> Update(Post updatedPost)
        {
            var post = await _postService.GetPostByID(updatedPost.PostID);
            if (post == null)
                return NotFound();
            else
            {
                if (post.Id != updatedPost.Id || (post.PostID != updatedPost.PostID))
                {
                    return new JsonResult("You can not change the '_id' and 'PostID' fields !");
                }
                else
                {
                    await _postService.UpdatePost(updatedPost);
                    return Ok(updatedPost);
                }
            }
        }
    }
}
