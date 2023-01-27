using DocuSign.eSign.Client;

namespace DocuSignPOC2.Services.IESignAdmin
{
    public interface IeSignAdminService
    {
        public string ESignAdminOrganizationId { get; }
        public string ESignAdminAccountId { get; }
        public string ESignBaseUrl { get; }
        public string ESignAdminToken { get; }
        public ApiClient ApiClient { get;  }
    }
}
