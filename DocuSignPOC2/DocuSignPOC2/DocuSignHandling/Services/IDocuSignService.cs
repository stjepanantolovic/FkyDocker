using DocuSign.eSign.Model;

namespace DocuSignPOC2.DocuSignHandling.Services
{
    public interface IDocuSignService
    {
        public EnvelopeSummary SendEnvelope(string agentEmail, string agentName,
            string producerEmail, string producerName, List<Document> docuSignDocuments);
    }
}
