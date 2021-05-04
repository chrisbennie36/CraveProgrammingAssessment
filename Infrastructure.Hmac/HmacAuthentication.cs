using Microsoft.AspNetCore.Authentication;
using System.Collections.Generic;

namespace Infrastructure.Hmac
{
    public class HmacAuthentication : AuthenticationSchemeOptions
    {
        public Dictionary<string, string> AllowedApps { get; set; }
        public string DefaultScheme { get; set; }
        public string Scheme { get; }
        public bool EnableNonce { get; set; }
        public ulong MaxRequestAgeInSeconds { get; set; }
        public HmacCipherStrength CipherStrength { get; set; }
    }
}
