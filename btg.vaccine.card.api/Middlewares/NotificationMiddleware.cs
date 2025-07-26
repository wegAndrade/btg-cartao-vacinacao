using btg.vaccine.card.domain.Notifications;
using System.Text.Json;

namespace btg.vaccine.card.api.Middlewares
{
    public class NotificationMiddleware
    {
        private readonly RequestDelegate _next;

        public NotificationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, NotificationContext notificationContext)
        {
            await _next(context);

            if (notificationContext.HasNotifications)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Response.ContentType = "application/json";

                var json = JsonSerializer.Serialize(notificationContext.Notifications);
                await context.Response.WriteAsync(json);
            }
        }
    }

}
