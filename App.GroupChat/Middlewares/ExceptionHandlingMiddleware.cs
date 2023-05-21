using App.GroupChat.Api.Exceptions;
using App.GroupChat.Services.Exceptions;
using System.Net;

namespace App.GroupChat.Api.Middlewares {
    public class ExceptionHandlingMiddleware {
        private readonly RequestDelegate _next;
        private readonly IWebHostEnvironment _environment;

        public ExceptionHandlingMiddleware(RequestDelegate next, IWebHostEnvironment environment) {
            _next = next;
            _environment = environment;
        }

        public async Task Invoke(HttpContext context) {
            try {
                await _next(context);
            }
            catch (Exception ex) {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception) {
            int responseHttpCode;
            string exceptionMessage = string.Empty;
            if (exception is ForbiddenException || exception is InvalidCredsException) {
                responseHttpCode = (int)HttpStatusCode.Forbidden;
            }
            else if (exception is DuplicateUsernameException) {
                responseHttpCode = (int)HttpStatusCode.BadRequest;
            }
            else {
                responseHttpCode = (int)HttpStatusCode.InternalServerError;
            }

            context.Response.StatusCode = responseHttpCode;
            context.Response.ContentType = "application/json";

            if (_environment.IsDevelopment() || _environment.IsEnvironment("QA") || _environment.IsEnvironment("local")) {
                exceptionMessage = exception.Message;
            }
            return context.Response.WriteAsJsonAsync(exceptionMessage); ;
        }

    }
}
