using DocuSign.eSign.Model;
using DocuSignPOC2.DocuSignHandling.Helpers;
using DocuSignPOC2.Models;
using DocuSignPOC2.Services.IPoc;
using Document = DocuSign.eSign.Model.Document;


namespace DocuSignPOC2.DocuSignHandling.Services
{
    public class DocuSignService : IDocuSignService
    {
        private readonly IESignAdminService _eSignAdminService;
        private readonly IConfiguration _config;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPocService _pocService;

        public DocuSignService(IESignAdminService eSignAdminService, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IPocService pocService)
        {
            _eSignAdminService = eSignAdminService;
            _config = configuration;
            _httpContextAccessor = httpContextAccessor;
            _pocService = pocService;
        }

        public EnvelopeSummary SendEnvelope(string agentEmail, string agentName,
            string producerEmail, string producerName, List<Document> docuSignDocuments)
        {
            var notificationUri = "https://" + _httpContextAccessor.HttpContext.Request.Host + "/api/envelope/DocuSigWebHook/";
            var emailSubject = _config.GetValue<string>("DocuSignConfiguration:EmailSubject");
            var envelope = DocuSignEnvelopeHelper.MakeEnvelope(
                emailSubject,
                agentEmail,
                agentName,
                producerEmail,
                producerName,
                docuSignDocuments,
                notificationUri);

            _pocService.AddWebHook(notificationUri);
            return _eSignAdminService.EnvelopesApi.CreateEnvelope(_eSignAdminService.AccountId, envelope);
        }


    }
}
