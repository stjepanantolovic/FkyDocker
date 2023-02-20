



using DocuSign.eSign.Api;
using DocuSign.eSign.Client;
using DocuSignPOC2.DocuSignHandling.Helpers;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static DocuSign.eSign.Client.Auth.OAuth;


namespace DocuSignPOC2.DocuSignHandling.Services
{
    public class ESignAdminService : IESignAdminService
    {
        private readonly DocuSignConfiguration _docuSignConfiguration;

        public ESignAdminService(IConfiguration config)
        {
            _docuSignConfiguration = DSHelper.GetDocuSignConfiguration(config);
        }
        public string AccountId { get { return _docuSignConfiguration.AccountId; } }

        public EnvelopesApi EnvelopesApi
        {
            get { return new EnvelopesApi(DocuSignClient); }
        }

        private DocuSignClient DocuSignClient
        {
            get
            {
                var docuSignClient = new DocuSignClient(_docuSignConfiguration.ESignBaseUrl);
                docuSignClient.Configuration.DefaultHeader.Add("Authorization", "Bearer " + ESignAdminToken);
                return docuSignClient;
            }
        }

        private string ESignAdminToken
        {
            get => GetTokenFromApi()!;
        }

        private string? GetTokenFromApi()
        {
            try
            {
                var accessToken = DSHelper.AuthenticateWithJWT(_docuSignConfiguration.ClientId, _docuSignConfiguration.ImpersonatedUserID,
                                                             _docuSignConfiguration.AuthServer, _docuSignConfiguration.PrivateKeyFile);

                return accessToken.access_token;
            }
            catch (ApiException apiExp)
            {
                // Consent for impersonation must be obtained to use JWT Grant
                if (apiExp.Message.Contains("consent_required"))
                {
                    DSHelper.OpenConsentPageInBrowser(_docuSignConfiguration.AuthorizationEndpoint, _docuSignConfiguration.ClientId, _docuSignConfiguration.AppUrl);
                }
                return null;
            }
        }


    }
}
