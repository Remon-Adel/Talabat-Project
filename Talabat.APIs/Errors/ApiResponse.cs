using System;

namespace Talabat.APIs.Errors
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public ApiResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message?? GetDefaultMessageForStatusCode(StatusCode);
        }

        private string GetDefaultMessageForStatusCode(int StatusCode)
        {
            return StatusCode switch
            {
                400 => "A bad Request ,You Have Made",
                401 => "Authorized You Are Not",
                404 => "Resource Found ,It Was Not",
                500 => "Errors Are The Path To Dark Side.Errors Lead To Anger.Anger Lead To Hate.Hate Lead To Carrer Change",
                _ => null
            };
        }
    }
}
