using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SDHDotNetCore.ATMMvcApp.Models
{
    [Table("Tbl_ATMUsers")]
    public class ATMUserModel
    {
        [Key]
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CardNumber { get; set; }
        public int Pin { get; set; }
        public double Balance { get; set; }
    }
}
