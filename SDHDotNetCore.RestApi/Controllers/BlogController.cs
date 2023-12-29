using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
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
        public IActionResult GetBlogs()
        {
            var lst = _appDbContext.Blogs.ToList();
            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult GetBlogs(int id)
        {
            BlogResponseModel model=new BlogResponseModel();
            var item = _appDbContext.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (item is null)
            {
                model.IsSuccess = false;
                model.Message = "No Data Found";
                return NotFound(model);
            }

            model.IsSuccess=true;
            model.Message = "Success.";
            model.Data = item;

            //model.Data = item;
            return Ok(model);
        }

        [HttpPost]
        public IActionResult CreateBlog([FromBody] BlogDataModel blog)
        {
            BlogResponseModel model= new BlogResponseModel();

            _appDbContext.Blogs.Add(blog);
            var result = _appDbContext.SaveChanges();
            string message = result > 0 ? "Saving Successful." : "Saving Failed.";

            model.IsSuccess = result > 0;
            model.Message=message;

            return Ok(model);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id,[FromBody] BlogDataModel blog)
        {
            var item = _appDbContext.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (item == null)
            {
                return NotFound("No Data Found.");
            }
            item.BlogTitle = blog.BlogTitle;
            item.BlogAuthor = blog.BlogAuthor;
            item.BlogContent= blog.BlogContent;

            BlogResponseModel model = new BlogResponseModel();

            var result = _appDbContext.SaveChanges();
            string message = result > 0 ? "Update Successful." : "Update Failed.";

            model.IsSuccess = result > 0;
            model.Message = message;

            return Ok(model);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, [FromBody] BlogDataModel blog)
        {
            BlogResponseModel model=new BlogResponseModel();
            var item=_appDbContext.Blogs.FirstOrDefault(x=>x.BlogId == id);

            if(item == null)
            {
                return NotFound("No Data Found.");
            }

            if(!string.IsNullOrWhiteSpace(blog.BlogAuthor))
            {
                item.BlogAuthor=blog.BlogAuthor;
            }

            if (!string.IsNullOrWhiteSpace(blog.BlogTitle))
            {
                item.BlogTitle = blog.BlogTitle;
            }

            if (!string.IsNullOrWhiteSpace(blog.BlogContent))
            {
                item.BlogContent = blog.BlogContent;
            }

            var result = _appDbContext.SaveChanges();
            string message = result > 0 ? "Update Successful." : "Update Failed.";

            model.IsSuccess = result > 0;
            model.Message = message;

            return Ok(model);
           // return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            BlogResponseModel model = new BlogResponseModel();
            var item = _appDbContext.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (item == null)
            {
                model.IsSuccess = false;
                model.Message = "No Data Found.";
                //model.Data = item;
                return NotFound(model);
            }
            _appDbContext.Blogs.Remove(item);
            var result = _appDbContext.SaveChanges();
            string message = result > 0 ? "Delete Successful" : "Delete Failed";

            model.IsSuccess= result > 0;
            model.Message= message;
        
            return Ok(model);
        }
    }
}
