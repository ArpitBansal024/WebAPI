using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace WebAPI
{
    public partial class OAuthAppProvider : OAuthAuthorizationServerProvider
    {
        private string clientSecret;

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            string clientId;
            context.TryGetFormCredentials(out clientId, out clientSecret);
            if (!string.IsNullOrEmpty(clientId))
            {
                context.Validated(clientId);
            }
            else
            {
                context.Validated();
            }
            return base.ValidateClientAuthentication(context);
        }
    }
}