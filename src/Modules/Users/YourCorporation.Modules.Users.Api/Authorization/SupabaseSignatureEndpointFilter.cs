using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using System.Text;
using YourCorporation.Shared.Abstractions.Auth;

namespace YourCorporation.Modules.Users.Api.Authorization
{
    internal class SupabaseSignatureEndpointFilter : IEndpointFilter
    {
        private const string SupabaseSignatureHeaderName = "x-supabase-signature";

        private readonly SupabaseAuthenticationOptions _authOptions;

        public SupabaseSignatureEndpointFilter(IOptions<SupabaseAuthenticationOptions> options)
        {
            _authOptions = options.Value;
        }

        public async ValueTask<object> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            string supabaseSignature = context.HttpContext.Request.Headers[SupabaseSignatureHeaderName];

            context.HttpContext.Request.Body.Position = 0;
            var bodyContent = await new StreamReader(context.HttpContext.Request.Body).ReadToEndAsync();           
            var bodyContentBytes = Encoding.UTF8.GetBytes(bodyContent);
            context.HttpContext.Request.Body.Position = 0;

            if (!await IsSignatureValid(bodyContentBytes, supabaseSignature))
            {
                return Results.Unauthorized();
            }

            return await next(context);
        }

        private async Task<bool> IsSignatureValid(byte[] bodyBytes, string signature)
        {
            if (string.IsNullOrWhiteSpace(signature))
            {
                return false;
            }
            var decodedSignature = Convert.FromBase64String(signature);

            using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(_authOptions.Signature));
            var calculatedSignature = hmac.ComputeHash(bodyBytes);

            var hmacMatch = CryptographicOperations.FixedTimeEquals(decodedSignature, calculatedSignature);

            return hmacMatch;
        }
    }
}
