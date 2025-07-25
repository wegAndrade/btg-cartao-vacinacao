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
            if (!notificationContext.HasNotifications)
                await _next(context);
               
           
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Response.ContentType = "application/json";

            var json = JsonSerializer.Serialize(notificationContext.Notifications);
            await context.Response.WriteAsync(json);
            await _next(context);    
        }
    }

}
