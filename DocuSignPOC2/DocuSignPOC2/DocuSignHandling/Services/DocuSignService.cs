using DocuSign.eSign.Model;
using DocuSignPOC2.DocuSignHandling.Helpers;
using Document = DocuSign.eSign.Model.Document;


namespace DocuSignPOC2.DocuSignHandling.Services
{
    public class DocuSignService : IDocuSignService
    {
        private readonly IESignAdminService _eSignAdminService;
        private readonly IConfiguration _config;

        public DocuSignService(IESignAdminService eSignAdminService, IConfiguration configuration)
        {
            _eSignAdminService = eSignAdminService;
            _config = configuration;
        }

        public EnvelopeSummary SendEnvelope(string agentEmail, string agentName,
            string producerEmail, string producerName, List<Document> docuSignDocuments)
        {
            var emailSubject = _config.GetValue<string>("DocuSignConfiguration:EmailSubject");
            var envelope = DocuSignEnvelopeHelper.MakeEnvelope(
                emailSubject,
                agentEmail,
                agentName,
                producerEmail,
                producerName,
                docuSignDocuments);

            return _eSignAdminService.EnvelopesApi.CreateEnvelope(_eSignAdminService.AccountId, envelope);
        }


    }
}
