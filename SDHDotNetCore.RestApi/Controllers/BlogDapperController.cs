using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using SDHDotNetCore.RestApi.Models;
using System.Data;

namespace SDHDotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogDapperController : ControllerBase
    {
        private readonly SqlConnectionStringBuilder _connectionStringBuilder;

        public BlogDapperController(IConfiguration configuration)
        {
            _connectionStringBuilder = new SqlConnectionStringBuilder(configuration.GetConnectionString("DbConnection"));
        }

        [HttpGet]
        public IActionResult GetBlogs()
        {
            string query = "select * from tbl_blog"; // Select Query
            using IDbConnection db = new SqlConnection(_connectionStringBuilder.ConnectionString);
            List<BlogDataModel> lst = db.Query<BlogDataModel>(query).ToList();
            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult GetBlogs(int id)
        {
            BlogResponseModel model = new BlogResponseModel();
            string query = "select * from tbl_blog where BlogId = @BlogId;"; // Select Query

            using IDbConnection db = new SqlConnection(_connectionStringBuilder.ConnectionString);
            BlogDataModel blog = new BlogDataModel
            {
                BlogId = id
            };
            var item = db.Query<BlogDataModel>(query, blog).FirstOrDefault();

            if (item is null)
            {
                model.IsSuccess = false;
                model.Message = "No data found.";
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
            string query = @"INSERT INTO [dbo].[Tbl_Blog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent])
     VALUES
           (@BlogTitle
           ,@BlogAuthor
           ,@BlogContent)"; // Insert Query
            using IDbConnection db = new SqlConnection(_connectionStringBuilder.ConnectionString);
            var result = db.Execute(query, blog);

            string message = result > 0 ? "Saving Successful." : "Saving Failed.";

            model.IsSuccess = result > 0;
            model.Message = message;

            return Ok(model);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, [FromBody] BlogDataModel blog)
        {
            BlogResponseModel model = new BlogResponseModel();

            string query = "select * from tbl_blog where BlogId = @BlogId;"; // Select Query

            using IDbConnection db = new SqlConnection(_connectionStringBuilder.ConnectionString);
            var item = db.Query<BlogDataModel>(query, new { BlogId = id }).FirstOrDefault();

            if (item is null)
            {
                model.IsSuccess = false;
                model.Message = "No data found.";
                return NotFound(model);
            }

            query = @"
                    UPDATE [dbo].[Tbl_Blog]
                       SET [BlogTitle] = @BlogTitle
                          ,[BlogAuthor] = @BlogAuthor
                          ,[BlogContent] = @BlogContent
                     WHERE BlogId = @BlogId"; // Update Query

            using IDbConnection db2 = new SqlConnection(_connectionStringBuilder.ConnectionString);
            blog.BlogId = id;
            int result = db2.Execute(query, blog);
            string message = result > 0 ? "Updating Successful." : "Updating Failed.";

            model.IsSuccess = result > 0;
            model.Message = message;

            return Ok(model);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, [FromBody] BlogDataModel blog)
        {
            BlogResponseModel model = new BlogResponseModel();

            string query = "select * from tbl_blog where BlogId = @BlogId;"; // Select Query

            using IDbConnection db = new SqlConnection(_connectionStringBuilder.ConnectionString);
            var item = db.Query<BlogDataModel>(query, new { BlogId = id }).FirstOrDefault();

            if (item is null)
            {
                model.IsSuccess = false;
                model.Message = "No data found.";
                return NotFound(model);
            }

            query = @"
                    UPDATE [dbo].[Tbl_Blog]
                       SET [BlogTitle] = @BlogTitle
                          ,[BlogAuthor] = @BlogAuthor
                          ,[BlogContent] = @BlogContent
                     WHERE BlogId = @BlogId"; // Update Query

            if (!string.IsNullOrWhiteSpace(blog.BlogTitle))
            {
                item.BlogTitle = blog.BlogTitle;
            }
            if (!string.IsNullOrWhiteSpace(blog.BlogAuthor))
            {
                item.BlogAuthor = blog.BlogAuthor;
            }
            if (!string.IsNullOrWhiteSpace(blog.BlogContent))
            {
                item.BlogContent = blog.BlogContent;
            }

            using IDbConnection db2 = new SqlConnection(_connectionStringBuilder.ConnectionString);
            blog.BlogId = id;
            int result = db2.Execute(query, item);
            string message = result > 0 ? "Updating Successful." : "Updating Failed.";

            model.IsSuccess = result > 0;
            model.Message = message;

            return Ok(model);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            BlogResponseModel model = new BlogResponseModel();

            string query = "select * from tbl_blog where BlogId = @BlogId;"; // Select Query

            using IDbConnection db = new SqlConnection(_connectionStringBuilder.ConnectionString);
            var item = db.Query<BlogDataModel>(query, new { BlogId = id }).FirstOrDefault();

            if (item is null)
            {
                model.IsSuccess = false;
                model.Message = "No data found.";
                return NotFound(model);
            }

            query = @"
                    DELETE FROM [dbo].[Tbl_Blog]
                          WHERE BlogId = @BlogId"; // Delete Query
            BlogDataModel blog = new BlogDataModel
            {
                BlogId = id,
            };

            using IDbConnection db2 = new SqlConnection(_connectionStringBuilder.ConnectionString);
            int result = db2.Execute(query, blog);
            string message = result > 0 ? "Deleting Successful." : "Deleting Failed.";

            model.IsSuccess = result > 0;
            model.Message = message;

            return Ok(model);
        }
    }
}
