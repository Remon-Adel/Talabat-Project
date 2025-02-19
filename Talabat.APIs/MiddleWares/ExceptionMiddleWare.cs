using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.APIs.Errors;

namespace Talabat.APIs.MiddleWares
{
    public class ExceptionMiddleWare
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionMiddleWare> logger;
        private readonly IHostEnvironment environment;
        


        public ExceptionMiddleWare(RequestDelegate next ,ILogger<ExceptionMiddleWare> logger ,IHostEnvironment environment)
        {
            this.next = next;
            this.logger = logger;
            this.environment = environment;
            
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                // Log Exception In DataBase

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                
                var ExceptionErrorResponse =environment.IsDevelopment()?
                    new ApiExceptionResponse(500,ex.Message,ex.StackTrace.ToString())
                    : 
                    new ApiExceptionResponse(500);

                var options =new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

                var json = JsonSerializer.Serialize(ExceptionErrorResponse, options);
                       await context.Response.WriteAsync(json);
            }
        }
    }
}
