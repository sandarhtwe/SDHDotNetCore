using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using SDHDotNetCore.RestApi.Models;
using System.Data;

namespace SDHDotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogAdoDotNetController : ControllerBase
    {
        private readonly SqlConnectionStringBuilder _connectionStringBuilder;

        public BlogAdoDotNetController(IConfiguration configuration)
        {
            _connectionStringBuilder = new SqlConnectionStringBuilder(configuration.GetConnectionString("DbConnection"));
        }

        [HttpGet]
        public IActionResult GetBlogs()
        {
            SqlConnection connection = new SqlConnection(_connectionStringBuilder.ConnectionString);
            connection.Open();

            string query = "select * from tbl_blog"; // Select Query
            SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            connection.Close();

            List<BlogDataModel> lst = new List<BlogDataModel>();
            foreach (DataRow dr in dt.Rows)
            {
                BlogDataModel item = new BlogDataModel();
                item.BlogId = Convert.ToInt32(dr["BlogId"]);
                item.BlogTitle = Convert.ToString(dr["BlogTitle"]);
                item.BlogAuthor = Convert.ToString(dr["BlogAuthor"]);
                item.BlogContent = Convert.ToString(dr["BlogContent"]);
                lst.Add(item);
            }
            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult GetBlogs(int id)
        {
            BlogResponseModel model = new BlogResponseModel();

            SqlConnection connection = new SqlConnection(_connectionStringBuilder.ConnectionString);
            connection.Open();

            string query = "select * from tbl_blog where BlogId = @BlogId;"; // Select Query
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            connection.Close();

            if (dt.Rows.Count == 0)
            {
                model.IsSuccess = false;
                model.Message = "No data found.";
                return NotFound(model);
            }

            DataRow dr = dt.Rows[0];
            BlogDataModel item = new BlogDataModel();
            item.BlogId = Convert.ToInt32(dr["BlogId"]);
            item.BlogTitle = Convert.ToString(dr["BlogTitle"]);
            item.BlogAuthor = Convert.ToString(dr["BlogAuthor"]);
            item.BlogContent = Convert.ToString(dr["BlogContent"]);

            model.IsSuccess = true;
            model.Message = "Success.";
            model.Data = item;
            return Ok(model);
        }

        [HttpPost]
        public IActionResult CreateBlog([FromBody] BlogDataModel blog)
        {
            BlogResponseModel model = new BlogResponseModel();
            SqlConnection connection = new SqlConnection(_connectionStringBuilder.ConnectionString);
            connection.Open();

            string query = @"INSERT INTO [dbo].[Tbl_Blog]
                           ([BlogTitle]
                           ,[BlogAuthor]
                           ,[BlogContent])
                     VALUES
                           (@BlogTitle
                           ,@BlogAuthor
                           ,@BlogContent)"; // Insert Query
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogTitle", blog.BlogTitle);
            cmd.Parameters.AddWithValue("@BlogAuthor", blog.BlogAuthor);
            cmd.Parameters.AddWithValue("@BlogContent", blog.BlogContent);
            int result = cmd.ExecuteNonQuery();
            string message = result > 0 ? "Saving Successful." : "Saving Failed.";

            connection.Close();

            model.IsSuccess = result > 0;
            model.Message = message;

            return Ok(model);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, [FromBody] BlogDataModel blog)
        {
            BlogResponseModel model = new BlogResponseModel();

            SqlConnection connection = new SqlConnection(_connectionStringBuilder.ConnectionString);
            connection.Open();

            string query = "select * from tbl_blog where BlogId = @BlogId;"; // Select Query
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            connection.Close();

            if (dt.Rows.Count == 0)
            {
                model.IsSuccess = false;
                model.Message = "No data found.";
                return NotFound(model);
            }

            connection = new SqlConnection(_connectionStringBuilder.ConnectionString);
            connection.Open();

            query = @"
                    UPDATE [dbo].[Tbl_Blog]
                       SET [BlogTitle] = @BlogTitle
                          ,[BlogAuthor] = @BlogAuthor
                          ,[BlogContent] = @BlogContent
                     WHERE BlogId = @BlogId"; // Update Query
            cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            cmd.Parameters.AddWithValue("@BlogTitle", blog.BlogTitle);
            cmd.Parameters.AddWithValue("@BlogAuthor", blog.BlogAuthor);
            cmd.Parameters.AddWithValue("@BlogContent", blog.BlogContent);
            int result = cmd.ExecuteNonQuery();
            string message = result > 0 ? "Updating Successful." : "Updating Failed.";

            connection.Close();

            model.IsSuccess = result > 0;
            model.Message = message;

            return Ok(model);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, [FromBody] BlogDataModel blog)
        {
            BlogResponseModel model = new BlogResponseModel();

            SqlConnection connection = new SqlConnection(_connectionStringBuilder.ConnectionString);
            connection.Open();

            string query = "select * from tbl_blog where BlogId = @BlogId;"; // Select Query
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            connection.Close();

            if (dt.Rows.Count == 0)
            {
                model.IsSuccess = false;
                model.Message = "No data found.";
                return NotFound(model);
            }

            connection = new SqlConnection(_connectionStringBuilder.ConnectionString);
            connection.Open();

            string queryConditions = string.Empty;
            if (!string.IsNullOrWhiteSpace(blog.BlogTitle))
            {
                queryConditions += " [BlogTitle] = @BlogTitle, ";
            }
            if (!string.IsNullOrWhiteSpace(blog.BlogAuthor))
            {
                queryConditions += " [BlogAuthor] = @BlogAuthor, ";
            }
            if (!string.IsNullOrWhiteSpace(blog.BlogContent))
            {
                queryConditions += " [BlogContent] = @BlogContent, ";
            }
            queryConditions = queryConditions.Substring(0, queryConditions.Length - 2);

            query = $@"
                    UPDATE [dbo].[Tbl_Blog]
                       SET {queryConditions}
                     WHERE BlogId = @BlogId"; // Update Query
            cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            if (!string.IsNullOrWhiteSpace(blog.BlogTitle))
            {
                cmd.Parameters.AddWithValue("@BlogTitle", blog.BlogTitle);
            }
            if (!string.IsNullOrWhiteSpace(blog.BlogAuthor))
            {
                cmd.Parameters.AddWithValue("@BlogAuthor", blog.BlogAuthor);
            }
            if (!string.IsNullOrWhiteSpace(blog.BlogContent))
            {
                cmd.Parameters.AddWithValue("@BlogContent", blog.BlogContent);
            }
            int result = cmd.ExecuteNonQuery();
            string message = result > 0 ? "Updating Successful." : "Updating Failed.";

            connection.Close();

            model.IsSuccess = result > 0;
            model.Message = message;

            return Ok(model);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            BlogResponseModel model = new BlogResponseModel();

            SqlConnection connection = new SqlConnection(_connectionStringBuilder.ConnectionString);
            connection.Open();

            string query = "select * from tbl_blog where BlogId = @BlogId;"; // Select Query
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            connection.Close();

            if (dt.Rows.Count == 0)
            {
                model.IsSuccess = false;
                model.Message = "No data found.";
                return NotFound(model);
            }

            connection = new SqlConnection(_connectionStringBuilder.ConnectionString);
            connection.Open();

            query = @"
                    DELETE FROM [dbo].[Tbl_Blog]
                          WHERE BlogId = @BlogId"; // Delete Query
            cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            int result = cmd.ExecuteNonQuery();
            string message = result > 0 ? "Deleting Successful." : "Deleting Failed.";

            connection.Close();

            model.IsSuccess = result > 0;
            model.Message = message;

            return Ok(model);
        }
    }
}
