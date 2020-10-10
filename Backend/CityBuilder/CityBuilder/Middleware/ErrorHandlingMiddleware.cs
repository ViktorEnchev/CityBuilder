using System;
using System.Net;
using System.Threading.Tasks;
using CityBuilder.Models.CustomeExceptions;
using CityBuilder.Models.OutputModels.ErrorOutputModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CityBuilder.Middleware
{
    public class ErrorHandlingMiddleware
    {
        public ErrorHandlingMiddleware(RequestDelegate next, IHostEnvironment env)
        {
            this.next = next;
            this.env = env;
        }

        private readonly RequestDelegate next;
        private readonly IHostEnvironment env;

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await this.next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            HttpStatusCode status = HttpStatusCode.InternalServerError;
            var response = new ErrorOutputModel();

            if (exception is BadRequestException)
            {
                status = HttpStatusCode.BadRequest;
            }
            else if (exception is NotFoundException)
            {
                status = HttpStatusCode.NotFound;
            }
            else if (exception is UnauthorizedException)
            {
                status = HttpStatusCode.Unauthorized;
            }

            response.Message = exception.Message;
            if (env.IsDevelopment())
            {
                response.StackTrace = exception.StackTrace;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)status;

            var responseString = JsonConvert.SerializeObject(response, new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });

            return context.Response.WriteAsync(responseString);
        }
    }
}
