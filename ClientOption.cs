using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZL.IdentityServer4ClientConfig
{
    public class ClientOption
    {

        public string Authority { get; set; }
        public string ClientId { get; set; }

        public string ClientSecret { get; set; }

        public string ResponseType { get; set; }

        public bool SaveTokens { get; set; }

        public List<string> Scopes { get; set; }

        public List<JsonKey> JsonKeys { get; set; }
        public bool RequireHttpsMetadata { get; set; }
    }

    public class JsonKey
    {
        public string ClaimType { get; set; }

        public string Key { get; set; }
    }
}
