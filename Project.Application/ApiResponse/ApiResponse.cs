using System.Net;

namespace Project.Application.ApiResponse
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public T Data { get; set; }
        public string ErrorMessage { get; set; }
        public HttpStatusCode Status { get; set; }
        public ApiResponse()
        {
            Success = true;
        }
    }
}
