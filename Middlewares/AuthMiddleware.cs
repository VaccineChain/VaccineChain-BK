using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Net;

namespace vaccine_chain_bk.Middlewares
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Check if the route has AllowAnonymous attribute
            var endpoint = context.GetEndpoint();
            if (endpoint != null && endpoint.Metadata.GetMetadata<AllowAnonymousAttribute>() != null)
            {
                await _next(context);  // Skip authentication for allowed endpoints
                return;
            }

            // Get the Authorization header
            var authorizationHeader = context.Request.Headers["Authorization"].ToString();

            if (string.IsNullOrEmpty(authorizationHeader))
            {
                // Set status code and return error response
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                await context.Response.WriteAsJsonAsync(new { message = "Authorization header is missing." });
                return; // Stop further processing
            }

            // Extract Bearer token
            var token = authorizationHeader.StartsWith("Bearer ")
                        ? authorizationHeader.Substring("Bearer ".Length).Trim()
                        : null;

            if (string.IsNullOrEmpty(token))
            {
                // Set status code and return error response
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                await context.Response.WriteAsJsonAsync(new { message = "Bearer token is missing." });
                return; // Stop further processing
            }

            var handler = new JwtSecurityTokenHandler();
            var payloadData = new Dictionary<string, string>();

            if (handler.CanReadToken(token))
            {
                var jwtToken = handler.ReadJwtToken(token);

                // Claims (Payload)
                var claims = jwtToken.Claims;
                foreach (var claim in claims)
                {
                    payloadData[claim.Type] = claim.Value;
                }
            }
            else
            {
                // Set status code and return error response
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                await context.Response.WriteAsJsonAsync(new { message = "Invalid JWT token." });
                return; // Stop further processing
            }

            // Add some data to HttpContext.Items
            context.Items["Token"] = token;
            context.Items["Email"] = payloadData.FirstOrDefault(kvp => kvp.Key == "username").Value;

            // Call the next middleware in the pipeline
            await _next(context);
        }
    }
}
