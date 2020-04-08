using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhiskyArchive.Services.Identity.API.Configuration
{
    public class Config
    {
        // ApiResources define the apis in your system
        public static IEnumerable<ApiResource> GetApis()
        {
            return new List<ApiResource>
            {
                new ApiResource("whiskyrecords", "Whisky Records Service"),
                /*new ApiResource("basket", "Basket Service"),
                new ApiResource("marketing", "Marketing Service"),
                new ApiResource("locations", "Locations Service"),
                new ApiResource("mobileshoppingagg", "Mobile Shopping Aggregator"),*/
                new ApiResource("webwhiskyarchiveagg", "Web Whisky Archive Aggregator")
                //new ApiResource("orders.signalrhub", "Ordering Signalr Hub")
            };
        }

        // Identity resources are data like user ID, name, or email address of a user
        // see: http://docs.identityserver.io/en/release/configuration/resources.html
        public static IEnumerable<IdentityResource> GetResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
        }

        // client want to access resources (aka scopes)
        public static IEnumerable<Client> GetClients(Dictionary<string, string> clientsUrl)
        {
            var RedirectUris =  $"{clientsUrl["WhiskyRecordingApi"]}/swagger/oauth2-redirect.html" ;
            return new List<Client>
            {
                // JavaScript Client
               
                new Client
                {
                    ClientId = "mvc",
                    ClientName = "MVC Client",
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("secret".Sha256())
                    },
                    ClientUri = $"{clientsUrl["Mvc"]}",                             // public uri of the client
                    AllowedGrantTypes = GrantTypes.Hybrid,
                    AllowAccessTokensViaBrowser = false,
                    RequireConsent = false,
                    AllowOfflineAccess = true,
                    AlwaysIncludeUserClaimsInIdToken = true,
                    RedirectUris = new List<string>
                    {
                        $"{clientsUrl["Mvc"]}/signin-oidc"
                    },
                    PostLogoutRedirectUris = new List<string>
                    {
                        $"{clientsUrl["Mvc"]}/signout-callback-oidc"
                    },
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                        "whiskyrecords",
                        /*"basket",
                        "locations",
                        "marketing",*/
                        "webwhiskyarchiveagg"
                        //"orders.signalrhub"
                    },
                    AccessTokenLifetime = 60*60*2, // 2 hours
                    IdentityTokenLifetime= 60*60*2 // 2 hours
                },
                new Client
                {
                    ClientId = "whiskyrecordingswaggerui",
                    ClientName = "Whisky Recording Swagger UI",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,

                    RedirectUris = { $"{clientsUrl["WhiskyRecordingApi"]}/swagger/oauth2-redirect.html" },
                    PostLogoutRedirectUris = { $"{clientsUrl["WhiskyRecordingApi"]}/swagger/" },

                    AllowedScopes =
                    {
                        "whiskyrecords"
                    }
                },
                new Client
                {
                    ClientId = "webwhiskyarchiveaggswaggerui",
                    ClientName = "Web Whisky Archive Aggregattor Swagger UI",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,

                    RedirectUris = { $"{clientsUrl["WebWhiskyArchiveAgg"]}/swagger/oauth2-redirect.html" },
                    PostLogoutRedirectUris = { $"{clientsUrl["WebWhiskyArchiveAgg"]}/swagger/" },

                    AllowedScopes =
                    {
                        "webwhiskyarchiveagg"
                    }
                }
            };
        }
    }
}
