using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SDHDotNetCore.ATMWebApp.Models
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
}
