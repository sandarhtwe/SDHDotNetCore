namespace HWDotNetCore.RestApi.Models
{
    public class StudentResponseModel
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public StudentDataModel Data { get; set; }
    }
}
