using Estudo.TRIMANIA.Domain.Commands;
using MediatR;

namespace Estudo.TRIMANIA.Api.Middlewares
{
    public class CheckAuthorizationMiddleware
    {
        private readonly RequestDelegate _next;

        public CheckAuthorizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var mediator = context.RequestServices.CreateScope().ServiceProvider.GetRequiredService<IMediator>();

            await mediator.Send(new CheckAuthorizationRequest()
            {
                Claims = context.User,
                Endpoint = context.Request.Path.ToString()
            });

            await _next.Invoke(context);
        }
    }
}
