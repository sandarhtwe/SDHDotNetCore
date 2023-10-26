using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HWDotNetCore.ConsoleApp.Models
{
    [Table("Tbl_Student")]
    public class StudentDataModel
    {
        [Key]
        [Column("StudentId")]
        public int StudentId { get; set; }

        [Column("Name")]
        public string? Student_Name { get; set; }

        [Column("Ph_no")]
        public string? Student_PhNo { get; set; }

        [Column("Age")]
        public int? Age { get; set; }

        [Column("Subject")]
        public string? Subject { get; set; }

        [Column("Address")]
        public string? Address { get; set; }
    }
}
