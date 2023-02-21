


using DocuSign.eSign.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static DocuSign.eSign.Client.Auth.OAuth;
using Microsoft.Extensions.Configuration;



namespace DocuSignPOC2.DocuSignHandling.Helpers
{
    public static class DSHelper
    {

        public static OAuthToken AuthenticateWithJWT(string clientId, string impersonatedUserId, string authServer, string privateKeyFile)
        {

            var docuSignClient = new DocuSignClient();

            var scopes = new List<string>
                {
                    "signature",
                    "impersonation",
                     "user_read",
                    "user_write",
                    "account_read",
                    "organization_read",
                    "group_read",
                    "permission_read",
                    "identity_provider_read",
                    "domain_read",
                };

            return docuSignClient.RequestJWTUserToken(
                clientId,
                impersonatedUserId,
                authServer,
                Encoding.Default.GetBytes("-----BEGIN RSA PRIVATE KEY-----\r\nMIIEowIBAAKCAQEApBv8T1PbliIksohyXHzvo6GIkhY320z7ci5vgfE85JS7vw4y\r\ncvqJTZrMRViW7W2aWg06MUu2FPXOsiciqcVjmuq3uZ9tNIQCue1TbrHoTvWlkIHk\r\nfI3kbgAs/a1IwkfXHg630cN7QgJGV6D6JBtxC+sWRwcoxRROKa8ej09dOczj1ZkW\r\nxNRHTDmA3SGJgK5sWE69uSN7oWx8nPZQfLnUc2NE9j6Ua8O5Vzov0kZpQPP4Bkp4\r\noD4cQ5H83QbGihWUs2QmVXgrcC4kmwyv4d6ls22Pfq2HEDsMu4CXIAn1N7goB3qN\r\nfwp9BwD7G+Ipjet89uXbv1oBXxvtPkj2SBkGaQIDAQABAoIBABbL42nimHl7vzTE\r\nsvwph8Fndjjy2KoAQNqM3EUE7YREK+tfjb7+kfWjh/YnFvoe1EbnmPqRjZbOSXri\r\naFSEfLBfpAtnO8yEfPt2XfVdxcs4INpYzNRHgqCMKjPH7zg7sgR1H3BGUxgpiDty\r\ne2TqIfM5ohPWSQHNbwkn0Bswt17Inyenl/Fi6PMyixRThZ0DnhhLwgF4vfwmSEsb\r\nxRCQrmvRzWXzsNomm2wBF6A3PgQBQDvVnl27PV47hOxDgIvMl7KoRPt00NkPlH3x\r\nSJAZhhJ1ANHABzCe1iDe2nJFEbPKAlnqyimTzx8TMyxFinM4j1SL2svugYpvbDXv\r\nrIpB3m0CgYEA+KZuNCgu3d/ePy5haam68N6i+js9mI4cfhbRqG/ze68qnRLHXbjt\r\nnYMCOGwvWeYgiPHih7KdcfbCC/sf09sugVs9rPPl6PjvmRMeTTfjBzIvk9h2HZfP\r\nvjzwW+wV3dHZQyhmWVssr7ejndhCQYAbrEw2Bccw1LnT3hMGR5KvMzUCgYEAqPXS\r\nwrIpi+yAMubsx/Sq8Wg5+TWJnI2F0WwRHz4KqIeWc7CtjfltKzb0nyfdFWEwCpUc\r\nEvGlVJVLoz+Z3JCu+camXHswA0wL64ak25Wg3sRrWrW1PrH819lfD6JW1iflCMwQ\r\nzlAy5wWx0QzauIfE5yrBZr7g21n/W3Ey6FtuWOUCgYEAwzUOEb52RNQrTBjieyy4\r\nSb/P3XnCutDex5KsmHsDgVecseH7SLYVPfKLPLaaWg6T/k8/097DQqRB5VwKua06\r\njm2ONwjnt4YvvFJJGMBGaPDab0yiNktn2edHoDLxW8sSsWm3KHGu3Gjkd9g+8+Na\r\nVmMiili+GlOlZJQ0+t3K0/0CgYB2XtJraJpG10fxYWtdowHn4tdKysFAFr47u/Q6\r\n6SJac7NqFcthfe+HqRa0Mh9njRE1OMXUV8s2eOnm0vYeWpbbktqWTA+VH7/yIAB7\r\nflaX+xAjGs6Bv/yd1EIPF/KyUnzZLu5PPEyNIaY0CUdqpGPEeGXKb8vkoSaPj7zU\r\noMmsKQKBgF1bY8mveOf9O55uvw+v78UVi82GgHi/2O3WI0TJTVEeg8nh2i7qgoqh\r\nJi/u2J94FJrejweUT4wSgZmYDjvYbomsN2HoVIc5duGZFpJdLwL686CLK5R3VIdd\r\n03NBUZnra1Uxet3AxILXJpkkLf1zLZZQbo9TY3CvZSpS+D2LGY3G\r\n-----END RSA PRIVATE KEY-----"),
                //Encoding.Default.GetBytes(privateKeyFile),
                1,
                scopes);
        }

        public static void OpenConsentPageInBrowser(string authorizationEndpoint, string clientId, string appUrl)
        {
            var consentUrl = BuildConsentURL(authorizationEndpoint, clientId, appUrl);
            Console.WriteLine($"Consent is required - launching browser (URL is {consentUrl})");

            // Start new browser window for login and consent to this app by DocuSign user
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Process.Start(new ProcessStartInfo("cmd", $"/c start {consentUrl}") { CreateNoWindow = false });
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                Process.Start("xdg-open", consentUrl);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                Process.Start("open", consentUrl);
            }
        }

        private static string BuildConsentURL(string authorizationEndpoint, string clientId, string appUrl)
        {
            var scopes = "signature impersonation user_read user_write organization_read account_read group_read permission_read identity_provider_read domain_read";

            scopes = scopes.Replace(" ", "%20");
            return authorizationEndpoint + "?response_type=code" +
                "&scope=" + scopes +
                "&client_id=" + clientId +
                "&redirect_uri=" + appUrl + "/ds/login?authType=JWT";
        }

        public static DocuSignConfiguration GetDocuSignConfiguration(IConfiguration config)
        {
            //var awsRegionName = config.GetValue<string>("AWS:AWS_REGION_NAME") ?? "us-east-1";
            //IAmazonSecretsManager amazonClient = new AmazonSecretsManagerClient(RegionEndpoint.GetBySystemName(awsRegionName));
            //var docuSignConfigSecretName = config.GetValue<string>("AWS:SecretManager:DocuSignConfigSecretName");
            //GetSecretValueResponse response = GetSecretValueResponse(amazonClient, docuSignConfigSecretName);
            //var docuSignConfiguration = response.SecretString?.FromJson<DocuSignConfiguration?>();
            return new DocuSignConfiguration();
        }

        public static DocuSignConfiguration GetDocuSignConfigurationFromAppSettings(IConfiguration config)
        {
            return config.GetValue<DocuSignConfiguration>("DocuSignConfiguration");
        }

        //public static GetSecretValueResponse GetSecretValueResponse(IAmazonSecretsManager client, string secretName, string? versionStage = null)
        //{
        //    try
        //    {
        //        return client.GetSecretValueAsync(new()
        //        {
        //            SecretId = secretName,
        //            VersionStage = versionStage // VersionStage defaults to AWSCURRENT if unspecified.
        //        }).Result;
        //    }
        //    catch (Exception exception)
        //    {
        //        throw new ProAgAwsException(exception);
        //    }
        //}
    }
}
