using DocuSign.eSign.Api;
using DocuSign.eSign.Client;

namespace DocuSignPOC2.Services.IESignAdmin
{
    public interface IeSignAdminService
    {
        public string ESignAdminOrganizationId { get; }
        public string ESignAdminAccountId { get; }
        public string ESignBaseUrl { get; }
        public string ESignAdminToken { get; }
        public ApiClient ESignApiClient { get; }
        public EnvelopesApi EnvelopesApi { get; }
        public DocuSignClient DocuSignClient { get; }
        public UsersApi UsersApi { get; }
        public AccountsApi AccountsApi { get; }
        public GroupsApi GroupsApi { get; }
        public DocuSign.Admin.Api.AccountsApi AdminAccountsApi { get; }
        public DocuSign.Admin.Api.UsersApi AdminUsersApi { get; }
    }
}
