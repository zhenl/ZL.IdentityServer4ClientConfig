using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZL.IdentityServer4ClientConfig
{
    public class ApiOption
    {
        public string Authority { get; set; }

        public List<string> CorsOrgins { get; set; }

        public List<Policy> Policies { get; set; }
        public bool RequireHttpsMetadata { get; set; }
    }

    public class Policy
    {
        public string Name { get; set; }

        public bool RequireAuthenticatedUser { get; set; }

        public List<Claim> Claims { get; set; }

    }

    public class Claim
    {
        public string ClaimType { get; set; }

        public List<string> AllowValues { get; set; }
    }
}
