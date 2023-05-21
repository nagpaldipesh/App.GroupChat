using App.GroupChat.Api.Middlewares;

namespace App.GroupChat.Api.Extensions {
    public static class ExceptionHandlingExtension {
        public static IApplicationBuilder UseCustomExceptionHandling(this IApplicationBuilder builder) {
            return builder.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}
