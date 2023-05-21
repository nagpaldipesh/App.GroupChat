using App.GroupChat.Api.Exceptions;
using App.GroupChat.Api.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using NUnit.Framework;
using System.Security.Claims;

namespace App.GroupChat.Api.UnitTests.Filter {
    [TestFixture]
    public class AdminAuthFilterAttributeTests {
        [SetUp]
        public void Setup() {

        }
        [Test]
        public void OnActionExecuted_ShouldThrowForbiddenError_WhenUserHasUserRole() {
            var claims = new[] { new Claim(ClaimTypes.Role, "3") };
            var actionArguments = new Dictionary<string, object>();

            var actionExecutingContext = GetMockedActionExecutingContext(claims, actionArguments);

            var attribute = new AdminAuthFilterAttribute();

            Assert.Throws<ForbiddenException>(() => attribute.OnActionExecuting(actionExecutingContext));
        }

        [Test]
        public void OnActionExecuted_ShouldNotThrowForbiddenError_WhenUserHasAdminRole() {
            var claims = new[] { new Claim(ClaimTypes.Role, "1") };
            var actionArguments = new Dictionary<string, object>();

            var actionExecutingContext = GetMockedActionExecutingContext(claims, actionArguments);

            var attribute = new AdminAuthFilterAttribute();
            attribute.OnActionExecuting(actionExecutingContext);

            Assert.AreEqual(null, actionExecutingContext.Result);
        }
        [Test]
        public void OnActionExecuted_ShouldNotThrowForbiddenError_WhenUserHasSuperAdminRole() {
            var claims = new[] { new Claim(ClaimTypes.Role, "2") };
            var actionArguments = new Dictionary<string, object>();

            var actionExecutingContext = GetMockedActionExecutingContext(claims, actionArguments);

            var attribute = new AdminAuthFilterAttribute();
            attribute.OnActionExecuting(actionExecutingContext);

            Assert.AreEqual(null, actionExecutingContext.Result);
        }

        private static ActionExecutingContext GetMockedActionExecutingContext(IEnumerable<Claim> claims, Dictionary<string, object> actionArguments) {

            var autenticationType = "Bearrer";
            var actionContext = new ActionContext() {
                HttpContext = new DefaultHttpContext() {
                    User = new ClaimsPrincipal(new ClaimsIdentity(claims, autenticationType)),
                    Request = { Method = "GET", Path = "" }
                },
                RouteData = new RouteData() { },
                ActionDescriptor = new Microsoft.AspNetCore.Mvc.Abstractions.ActionDescriptor()
            };
            return new ActionExecutingContext(actionContext, new List<IFilterMetadata>(), actionArguments, null);
        }
    }
}
