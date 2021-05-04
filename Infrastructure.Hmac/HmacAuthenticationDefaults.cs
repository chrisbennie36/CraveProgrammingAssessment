using System;

namespace Infrastructure.Hmac
{
    public class HmacAuthenticationDefaults
    {
        public const string AuthenticationScheme = "Hmac";
        public const string DisplayName = "Hmac";
        public const bool EnableNonce = true;
        public const int MaxRequestAgeInSeconds = 300;
        public const HmacCipherStrength CipherStrength = HmacCipherStrength.Hmac256;
    }
}
