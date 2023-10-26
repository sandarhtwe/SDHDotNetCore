using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HWDotNetCore.RestApi.Models
{
    internal class StudentViewModel
    {
        public int Id { get; set; }
        public string? Student_Name { get; set; }

        public string? Student_PhNo { get; set; }

        public int Age { get; set; }

        public string? Subject { get; set; }

        public string? Address { get; set; }
    }
}
