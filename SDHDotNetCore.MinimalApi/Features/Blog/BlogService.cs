using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SDHDotNetCore.MinimalApi.EFDbContext;
using SDHDotNetCore.MinimalApi.Models;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text.Json;

namespace SDHDotNetCore.MinimalApi.Features.Blog;

public static class BlogService
{
    public static void AddBlogService(this IEndpointRouteBuilder app)
    {
        app.MapGet("/blog/{pageNo}/{pageSize}", async ([FromServices] AppDbContext db, [FromServices] ILogger<Program> _logger, int pageNo, int pageSize) =>
        {
			var lst = await db.Blogs
				.AsNoTracking()
				.Skip((pageNo - 1) * pageSize)
				.Take(pageSize)
				.ToListAsync();
			_logger.LogInformation("Blog List => " + JsonSerializer.Serialize(lst));
			return lst;
		})
        .WithName("GetBlogs")
        .WithOpenApi();

        //Create Blog
        app.MapPost("/blog", async ([FromServices] AppDbContext db, [FromServices] ILogger < Program > _logger, BlogDataModel blog) =>
        {
            await db.Blogs.AddAsync(blog);
            int result = await db.SaveChangesAsync();

            string message = result > 0 ? "Saving Successful." : "Saving Failed.";

			_logger.LogInformation("Created Blog => " + JsonSerializer.Serialize(blog),"Creating Status"+JsonSerializer.Serialize(message));
			return Results.Ok(new BlogResponseModel
            {
                Data = blog,
                IsSuccess = result > 0,
                Message = message
            });
		})
       .WithName("CreateBlog")
       .WithOpenApi();
        
        //Edit Blog
        app.MapGet("/blog/{id}", async ([FromServices] AppDbContext db, int id) =>
        {
            var item = await db.Blogs.FirstOrDefaultAsync(x => x.BlogId == id);

            if (item is null)
            {
                return Results.NotFound(new BlogResponseModel
                {
                    IsSuccess = false,
                    Message = "No data found."
                });
            }
            return Results.Ok(new BlogResponseModel
            {
                Data = item,
                IsSuccess = true,
                Message = "Success."
            });
        })
        .WithName("GetBlog")
        .WithOpenApi();

        //Update Blog
        app.MapPut("/blog/{id}", async ([FromServices] AppDbContext db, int id, BlogDataModel blog) =>
        {
            var item = await db.Blogs.FirstOrDefaultAsync(x => x.BlogId == id);

            if (item is null)
            {
                return Results.NotFound(new BlogResponseModel
                {
                    IsSuccess = false,
                    Message = "No data found."
                });
            }
            item.BlogTitle = blog.BlogTitle;
            item.BlogAuthor = blog.BlogAuthor;
            item.BlogContent=blog.BlogContent; 

            var result = await db.SaveChangesAsync();
            string message = result > 0 ? "Updating Successful." : "Updating Failed.";
            return Results.Ok(new BlogResponseModel
            {
                Data = item,
                IsSuccess = result > 0,
                Message = message
            });
        })
        .WithName("UpdateBlog")
        .WithOpenApi();

        //Patch Blog
        app.MapPatch("/blog/{id}", async ([FromServices] AppDbContext db, int id, BlogDataModel blog) =>
        {
            var item = await db.Blogs.FirstOrDefaultAsync(x => x.BlogId == id);

            if (item is null)
            {
                return Results.NotFound(new BlogResponseModel
                {
                    IsSuccess = false,
                    Message = "No data found."
                });
            }
            if (!string.IsNullOrEmpty(blog.BlogTitle))
            {
                item.BlogTitle = blog.BlogTitle;
            }
            if (!string.IsNullOrEmpty(blog.BlogAuthor))
            {
                item.BlogAuthor = blog.BlogAuthor;
            }
            if (!string.IsNullOrEmpty(blog.BlogContent))
            {
                item.BlogContent = blog.BlogContent;
            }

            var result = await db.SaveChangesAsync();
            string message = result > 0 ? "Updating Successful." : "Updating Failed.";
            return Results.Ok(new BlogResponseModel
            {
                Data = item,
                IsSuccess = result > 0,
                Message = message
            });
        })
        .WithName("PatchBlog")
        .WithOpenApi();

        //Delete Blog
        app.MapDelete("/blog/{id}", async ([FromServices] AppDbContext db, int id) =>
        {
            var item = await db.Blogs.FirstOrDefaultAsync(x => x.BlogId == id);

            if (item is null)
            {
                return Results.NotFound(new BlogResponseModel
                {
                    IsSuccess = false,
                    Message = "No data found."
                });
            }

            db.Blogs.Remove(item);
            int result = await db.SaveChangesAsync();

            string message = result > 0 ? "Deleting Successful." : "Deleting Failed.";
            return Results.Ok(new BlogResponseModel
            {
                IsSuccess = result > 0,
                Message = message
            });
        })
        .WithName("DeleteBlog")
        .WithOpenApi();
    }
}