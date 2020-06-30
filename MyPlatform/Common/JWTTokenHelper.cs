using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public static class JWTTokenHelper
    {
        public static string GenerateToken(string account, string name)
        {
            var payload = new Dictionary<string, object>
            {
                 { "Account", account },
                 { "Name", name}
            };
            const string secret = "GQDstcKsx0NHjPOuXOYg5MbeJ1XT0uFiwDVvVBrk";

            IJwtAlgorithm algorithm = new HMACSHA256Algorithm(); // symmetric
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);
            JWT.Builder.JwtBuilder s = new JWT.Builder.JwtBuilder();
            //s.AddClaim("exp",30*1000);
            //s.AddClaim

            var token = encoder.Encode(payload, secret);
            return token.ToString();
        }

        public static bool ValidateToken(string token)
        {
            return true;
        }
    }
}
