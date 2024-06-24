using Estudo.TRIMANIA.Domain.Exceptions;
using FluentValidation;
using MediatR;

namespace Estudo.TRIMANIA.Api.Middlewares
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomExceptionMiddleware> _logger;

        public CustomExceptionMiddleware(RequestDelegate next, ILogger<CustomExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }


        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);     
            }
            catch (ValidationException ex)
            {
                throw;
            }
            catch (ServiceException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
            }
        }
    }
}
