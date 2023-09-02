using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using AbusinessLayer.exeptions;
using businessLayer.exeptions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace StorePro.Utilities.CustomMiddleWares

{
    public class ExceptionMiddleware  
    {
        private RequestDelegate Next { get; }
        public ILogger<ExceptionMiddleware> _logger { get; }

        public ExceptionMiddleware(RequestDelegate next , ILogger<ExceptionMiddleware> Logger)
        {
            Next = next;
            _logger = Logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await Next(context);
            }
            catch (NotFoundException ex)
            {
                context.Response.ContentType = "application/problem+json";
                context.Response.StatusCode = StatusCodes.Status400BadRequest;

                var problemDetails = new ProblemDetails()
                {
                    Status = StatusCodes.Status404NotFound,
                    Detail = string.Empty,
                    Instance = "",
                    Title = $"{ex.Type} for id {ex.Id} not found",
                    Type = "Error"
                };

                string problemDetailsJson = JsonSerializer.Serialize(problemDetails);
              //  _logger.LogError(problemDetailsJson);

                await context.Response.WriteAsync(problemDetailsJson);
            }
            catch (CanNotEvaluateExeption ex)
            {
                context.Response.ContentType = "application/problem+json";
                context.Response.StatusCode = StatusCodes.Status400BadRequest;

                var problemDetails = new ProblemDetails()
                {
                    Status = StatusCodes.Status400BadRequest,
                    Detail = string.Empty,
                    Instance = "",
                    Title = $"Can Not Evaluate Inventory  ",
                    Type = "Error"
                };

                var problemDetailsJson = JsonSerializer.Serialize(problemDetails);
               // _logger.LogError(problemDetailsJson);

                await context.Response.WriteAsync(problemDetailsJson);
            }
            catch (CanNotCreateExeption ex)
            {
                context.Response.ContentType = "application/problem+json";
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                var problemDetails = new ProblemDetails()
                {
                    Status = StatusCodes.Status400BadRequest,
                    Detail = string.Empty,
                    Instance = "",
                    Title = $"Can Not Create   " +ex.DependentEntityName,
                    Type = "Error"
                };

                var problemDetailsJson = JsonSerializer.Serialize(problemDetails);
             //   _logger.LogError(problemDetailsJson);

                await context.Response.WriteAsync(problemDetailsJson);
            }
            catch (ValidationException ex)
            {
                context.Response.ContentType = "application/problem+json";
                context.Response.StatusCode = StatusCodes.Status400BadRequest;

                var problemDetails = new ProblemDetails()
                {
                    Status = StatusCodes.Status400BadRequest,
                    Detail = JsonSerializer.Serialize(ex.Message),
                    Instance = "",
                    Title = "Validation Error",
                    Type = "Error"
                };

                var problemDetailsJson = JsonSerializer.Serialize(problemDetails);
              //  _logger.LogError(problemDetailsJson);

                await context.Response.WriteAsync(problemDetailsJson);
            }
            catch (OrderExceedItemExeption ex)
            {
                context.Response.ContentType = "application/problem+json";
                context.Response.StatusCode = StatusCodes.Status400BadRequest;

                var problemDetails = new ProblemDetails()
                {
                    Status = StatusCodes.Status400BadRequest,
                    Detail = JsonSerializer.Serialize(ex.Message),
                    Instance = "",
                    Title = "Validation Error",
                    Type = "Error"
                };

                var problemDetailsJson = JsonSerializer.Serialize(problemDetails);
                await context.Response.WriteAsync(problemDetailsJson);
            }
            catch (Exception ex)
            {
                context.Response.ContentType = "application/problem+json";
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                var problemDetails = new ProblemDetails()
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Detail = ex.Message,
                    Instance = "",
                    Title = "Internal Server Error - something went wrong",
                    Type = "Error"
                };

                var problemDetailsJson = JsonSerializer.Serialize(problemDetails);
             //   _logger.LogError(problemDetailsJson);

                await context.Response.WriteAsync(problemDetailsJson);
            }
        }

    }
}

