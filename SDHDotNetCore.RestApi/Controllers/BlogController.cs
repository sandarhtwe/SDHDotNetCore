using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SDHDotNetCore.RestApi.EFDbContext;
using SDHDotNetCore.RestApi.Models;

namespace SDHDotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public BlogController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet]
        public IActionResult GetBlog()
        {
            var lst = _appDbContext.Blogs.ToList();
            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult EditBlog(int id)
        {
            BlogResponseModel model = new BlogResponseModel();
            var item = _appDbContext.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (item is null)
            {
                model.IsSuccess = false;
                model.Message = "No Data Found.";
                return NotFound(model);
            }
            model.IsSuccess = true;
            model.Message = "Success.";
            model.Data = item;
            return Ok(model);
        }

        [HttpPost]
        public IActionResult CreateBlog([FromBody] BlogDataModel blog)
        {
            BlogResponseModel model = new BlogResponseModel();
            _appDbContext.Blogs.Add(blog);
            var result = _appDbContext.SaveChanges();
            string message = result > 0 ? "Saving Successful" : "Saving Failed";

            model.IsSuccess = result > 0;
            model.Message = message;
            return Ok(model);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, BlogDataModel blog)
        {
            BlogResponseModel model = new BlogResponseModel();
            var item = _appDbContext.Blogs.FirstOrDefault(x => x.BlogId == id);
            if(item is null)
            {
                return NotFound("No Data Found.");
            }
            item.BlogTitle = blog.BlogTitle;
            item.BlogAuthor=blog.BlogAuthor;    
            item.BlogContent = blog.BlogContent;

            var result = _appDbContext.SaveChanges();
            string message = result > 0 ? "Updating Successful" : "Updating Failed";

            model.IsSuccess = result > 0;
            model.Message = message;
            return Ok(model);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, BlogDataModel blog)
        {
            BlogResponseModel model = new BlogResponseModel();
            var item = _appDbContext.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (item is null)
            {
                return NotFound("No Data Found.");
            }
            if(!string.IsNullOrEmpty(blog.BlogTitle))
            {
                item.BlogTitle=blog.BlogTitle;
            }
            if (!string.IsNullOrEmpty(blog.BlogAuthor))
            {
                item.BlogAuthor = blog.BlogAuthor;
            }
            if (!string.IsNullOrEmpty(blog.BlogContent))
            {
                item.BlogContent = blog.BlogContent;
            }
            var result = _appDbContext.SaveChanges();
            string message = result > 0 ? "Updating Successful" : "Updating Failed";

            model.IsSuccess = result > 0;
            model.Message = message;
            return Ok(model);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            BlogResponseModel model= new BlogResponseModel();
            var item = _appDbContext.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (item is null)
            {
                return NotFound("No Data Found.");
            }
            _appDbContext.Blogs.Remove(item);
            var result = _appDbContext.SaveChanges();
            string message = result > 0 ? "Deleting Successful" : "Deleting Failed";

            model.IsSuccess = result > 0;
            model.Message = message;
            return Ok(model);
        }
    }
}
