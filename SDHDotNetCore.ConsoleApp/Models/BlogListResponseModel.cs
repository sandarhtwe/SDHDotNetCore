using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDHDotNetCore.ConsoleApp.Models
{
    internal class BlogListResponseModel
    {
            public bool IsSuccess { get; set; }
            public string Message { get; set; }
            public List<BlogDataModel> Data { get; set; }
        
    }

}

