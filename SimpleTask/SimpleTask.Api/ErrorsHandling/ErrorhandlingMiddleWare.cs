using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SimpleTask.Api.ErrorsHandling
{
    public class ErrorhandlingMiddleWare
    {
        private readonly RequestDelegate _next;

        public ErrorhandlingMiddleWare(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionsAsync(context, ex);
            }
        }

        private static Task HandleExceptionsAsync(HttpContext context, Exception exception)
        {
            // var StatusCode = HttpStatusCode.InternalServerError;
            var ProblemDetails = new ProblemDetails()
            {
                Title = "An error while proccesing your request",
                Status = StatusCodes.Status500InternalServerError,
            };
            var Result = JsonSerializer.Serialize(ProblemDetails);
            // context.Response.ContentType = "application/json";
            // context.Response.StatusCode = (int)StatusCode;

            return context.Response.WriteAsync(Result);
        }
    }
}