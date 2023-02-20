using DocuSign.eSign.Api;

namespace DocuSignPOC2.DocuSignHandling.Services
{
    public interface IESignAdminService
    {
        public EnvelopesApi EnvelopesApi { get; }
        public string AccountId { get; }
    }
}
