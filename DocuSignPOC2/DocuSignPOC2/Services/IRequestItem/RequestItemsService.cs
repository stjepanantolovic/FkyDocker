using System;
using System.Linq;
using System.Security.Claims;
using DocuSign.eSign.Client;
using DocuSign.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using static DocuSign.eSign.Client.Auth.OAuth;
using static DocuSign.eSign.Client.Auth.OAuth.UserInfo;

namespace DocuSignPOC2.Services.IRequestItem
{
    public class RequestItemsService : IRequestItemsService
    {
        private static Account account;

        private static Guid? organizationId;

#nullable enable
        private static string? authenticatedUserEmail;
#nullable disable

        private readonly IHttpContextAccessor httpContextAccessor;

        private readonly IConfiguration configuration;

        private readonly IMemoryCache cache;

#nullable enable
        private readonly string? id;
#nullable disable

        private OAuthToken authToken;

        public RequestItemsService(IHttpContextAccessor httpContextAccessor, IMemoryCache cache, IConfiguration configuration)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.configuration = configuration;
            this.cache = cache;
            Status = "sent";
            DocuSignClient ??= new DocuSignClient();
            var identity = httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;

            if (identity != null && identity.IsAuthenticated)
            {
                id = httpContextAccessor?.HttpContext.User.Identity.Name;
            }
        }

#nullable enable
        public string? EgName
        {
            get => cache.Get<string>(GetKey("EgName"));
            set => cache.Set(GetKey("EgName"), value);
        }
#nullable disable

        public Session Session
        {
            get => cache.Get<Session>(GetKey("Session"));
            set => cache.Set(GetKey("Session"), value);
        }

#nullable enable
        public User? User
        {
            get => cache.Get<User>(GetKey("User"));
            set => cache.Set(GetKey("User"), value);
        }
#nullable disable

        public Guid? OrganizationId
        {
            get
            {
                if (organizationId == null)
                {
                    var apiClient = new DocuSign.Admin.Client.ApiClient(Session.AdminApiBasePath);
                    apiClient.Configuration.DefaultHeader.Add("Authorization", "Bearer " + User.AccessToken);
                    var accountApi = new DocuSign.Admin.Api.AccountsApi(apiClient);
                    var org = accountApi.GetOrganizations().Organizations.FirstOrDefault();
                    if (org == null)
                    {
                        throw new DocuSign.Admin.Client.ApiException(0, "You must create an organization for this account to be able to use the DocuSign Admin API.");
                    }
                    else
                    {
                        organizationId = org.Id;
                    }
                }

                return organizationId;
            }

            set
            {
                organizationId = value;
            }
        }

        public string AuthenticatedUserEmail
        {
            get
            {
                if (authenticatedUserEmail == null)
                {
                    DocuSignClient.SetOAuthBasePath(configuration["DocuSignJWT:AuthServer"]);
                    UserInfo userInfo = DocuSignClient.GetUserInfo(User?.AccessToken);

                    authenticatedUserEmail = userInfo.Email;
                }

                return authenticatedUserEmail;
            }

            set
            {
                authenticatedUserEmail = value;
            }
        }

        public string EnvelopeId
        {
            get => cache.Get<string>(GetKey("EnvelopeId"));
            set => cache.Set(GetKey("EnvelopeId"), value);
        }

        public string DocumentId
        {
            get => cache.Get<string>(GetKey("DocumentId"));
            set => cache.Set(GetKey("DocumentId"), value);
        }

        public EnvelopeDocuments EnvelopeDocuments
        {
            get => cache.Get<EnvelopeDocuments>(GetKey("EnvelopeDocuments"));
            set => cache.Set(GetKey("EnvelopeDocuments"), value);
        }

        public string TemplateId
        {
            get => cache.Get<string>(GetKey("TemplateId"));
            set => cache.Set(GetKey("TemplateId"), value);
        }

        public string ClickwrapId
        {
            get => cache.Get<string>(GetKey("ClickwrapId"));
            set => cache.Set(GetKey("ClickwrapId"), value);
        }

        public string ClickwrapName
        {
            get => cache.Get<string>(GetKey("ClickwrapName"));
            set => cache.Set(GetKey("ClickwrapName"), value);
        }

        public string PausedEnvelopeId
        {
            get => cache.Get<string>(GetKey("PausedEnvelopeId"));
            set => cache.Set(GetKey("PausedEnvelopeId"), value);
        }

        public string Status { get; set; }

        public string EmailAddress
        {
            get => cache.Get<string>(GetKey("EmailAddress"));
            set => cache.Set(GetKey("EmailAddress"), value);
        }

        protected static DocuSignClient DocuSignClient { get; private set; }

        public void UpdateUserFromJWT()
        {
            authToken = DocuSign.JWTAuth.AuthenticateWithJWT(
                configuration["ExamplesAPI"],
                configuration["DocuSignJWT:ClientId"],
                configuration["DocuSignJWT:ImpersonatedUserId"],
                configuration["DocuSignJWT:AuthServer"],
                configuration["DocuSignJWT:PrivateKeyFile"]);
            account = GetAccountInfo(authToken);

            User = new User
            {
                Name = account.AccountName,
                AccessToken = authToken.access_token,
                ExpireIn = DateTime.Now,
                AccountId = account.AccountId,
            };

            if (authToken.expires_in.HasValue)
            {
                User.ExpireIn.Value.AddSeconds(authToken.expires_in.Value);
            }

            Session = new Session
            {
                AccountId = account.AccountId,
                AccountName = account.AccountName,
                BasePath = account.BaseUri,
                RoomsApiBasePath = configuration["DocuSign:RoomsApiEndpoint"],
                AdminApiBasePath = configuration["DocuSign:AdminApiEndpoint"],
            };
        }

        public void Logout()
        {
            authToken = null;
            EgName = null;
            User = null;
        }

        public bool CheckToken(int bufferMin = 60)
        {
            bool isAuthCodeGrantAuthenticated = httpContextAccessor.HttpContext.User.Identity.IsAuthenticated
                && DateTime.Now.Subtract(TimeSpan.FromMinutes(bufferMin)) < User.ExpireIn.Value;

            bool isJWTGrantAuthenticated = User?.AccessToken != null
                    && DateTime.Now.Subtract(TimeSpan.FromMinutes(bufferMin)) < User.ExpireIn.Value;

            return isAuthCodeGrantAuthenticated || isJWTGrantAuthenticated;
        }

        private string GetKey(string key)
        {
            return string.Format("{0}_{1}", id, key);
        }

        private Account GetAccountInfo(OAuthToken authToken)
        {
            DocuSignClient.SetOAuthBasePath(configuration["DocuSignJWT:AuthServer"]);
            UserInfo userInfo = DocuSignClient.GetUserInfo(authToken.access_token);
            Account acct = userInfo.Accounts.FirstOrDefault();
            if (acct == null)
            {
                throw new Exception("The user does not have access to account");
            }

            return acct;
        }
    }
}
