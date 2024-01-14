using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDHDotNetCore.MvcApp.Models
{
    [Table("Tbl_Blog")]
    public class BlogDataModel
    {
        [Key]
        [Column("BlogId")]
        public int BlogId { get; set; }
        public string BlogTitle { get; set; }
        public string BlogAuthor { get; set; }
        public string BlogContent { get; set; }
    }

    public class BlogDataResponseModel
    {
        public PageSettingModel PageSetting { get; set; }
        public List<BlogDataModel> Blogs { get; set; }
    }

    public class BlogResponseModel
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public BlogDataModel Data { get; set; }
    }

    public class BlogListResponseModel
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<BlogDataModel> Data { get; set; }
    }

    public class PageSettingModel
    {
        public PageSettingModel()
        {
        }

        public PageSettingModel(int pageNo, int pageSize, int pageCount, string pageUrl)
        {
            PageNo = pageNo;
            PageSize = pageSize;
            PageCount = pageCount;
            PageUrl = pageUrl;
        }

        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public int PageCount { get; set; }
        public string PageUrl { get; set; }
    }

    public class MessageModel
    {
        public MessageModel() { }
        public MessageModel(bool isSuccess, string message)
        {
            IsSuccess = isSuccess;
            Message = message;
        }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
    public class BlogListResponseModel1
    {
        public List<BlogDataModel> BlogList { get; set; }
        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public int PageCount { get; set; }
        public int PageRowCount { get; set; }
    }
}

//public class ScatterResponseModel
//{
//    public List<ScatterModel> Data { get; set; }
//}

//public class ScatterModel
//{
//    public string name { get; set; }
//    public float[][] data { get; set; }
//}
