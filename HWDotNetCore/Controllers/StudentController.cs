using HWDotNetCore.RestAPI.EFDbContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HWDotNetCore.RestApi.Models;

namespace HWDotNetCore.RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public StudentController(AppDbContext appDbContext)
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
            StudentResponseModel model = new StudentResponseModel();
            var item = _appDbContext.Blogs.FirstOrDefault(x => x.StudentId == id);
            if (item is null)
            {
                model.IsSuccess = false;
                model.Message = "No Data Found";
                return NotFound(model);
            }

            model.IsSuccess = true;
            model.Message = "Success.";
            model.Data = item;

            //model.Data = item;
            return Ok(model);
        }

        [HttpPost]
        public IActionResult CreateBlog([FromBody] StudentDataModel student)
        {
            StudentResponseModel model = new StudentResponseModel();

            _appDbContext.Blogs.Add(student);
            var result = _appDbContext.SaveChanges();
            string message = result > 0 ? "Saving Successful." : "Saving Failed.";

            model.IsSuccess = result > 0;
            model.Message = message;

            return Ok(model);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, [FromBody] StudentDataModel student)
        {
            var item = _appDbContext.Blogs.FirstOrDefault(x => x.StudentId == id);
            if (item == null)
            {
                return NotFound("No Data Found.");
            }
            item.Student_Name = student.Student_Name;
            item.Student_PhNo = student.Student_PhNo;
            item.Age = student.Age;
            item.Subject = student.Subject;
            item.Address=student.Address;

            StudentResponseModel model = new StudentResponseModel();

            var result = _appDbContext.SaveChanges();
            string message = result > 0 ? "Update Successful." : "Update Failed.";

            model.IsSuccess = result > 0;
            model.Message = message;

            return Ok(model);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, [FromBody] StudentDataModel student)
        {
            StudentResponseModel model = new StudentResponseModel();
            var item = _appDbContext.Blogs.FirstOrDefault(x => x.StudentId == id);

            if (item == null)
            {
                return NotFound("No Data Found.");
            }

            if (!string.IsNullOrWhiteSpace(student.Student_Name))
            {
                item.Student_Name = student.Student_Name;
            }

            if (!string.IsNullOrWhiteSpace(student.Student_PhNo))
            {
                item.Student_PhNo = student.Student_PhNo;
            }

            if (student.Age != 0)
            {
                item.Age = student.Age;
            }

            if (!string.IsNullOrWhiteSpace(student.Subject))
            {
                item.Subject = student.Subject;
            }

            if (!string.IsNullOrWhiteSpace(student.Address))
            {
                item.Address = student.Address;
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
            StudentResponseModel model = new StudentResponseModel();
            var item = _appDbContext.Blogs.FirstOrDefault(x => x.StudentId == id);
            if (item == null)
            {
                model.IsSuccess = false;
                model.Message = "No Data Found.";
                return NotFound(model);
            }
            _appDbContext.Blogs.Remove(item);
            var result = _appDbContext.SaveChanges();
            string message = result > 0 ? "Delete Successful" : "Delete Failed";

            model.IsSuccess = result > 0;
            model.Message = message;

            return Ok(model);
        }
    }
}
