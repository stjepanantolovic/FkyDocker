using DocuSign.eSign.Model;

namespace DocuSignPOC2.Services.IDocuSignEnvelope
{
    public interface IDocuSignEnvelopeService
    {
        public EnvelopeSummary SendEnvelope(string accessToken, string basePath, string accountId, string recipient1Email, string recipient1Name,
             string recipient2Email, string recipient2Name, string documentBase64);
        public EnvelopeDefinition MakeEnvelope(string recipient1Email, string recipient1Name, string recipient2Email, string recipient2Name,
           string documentBase64);
        public EnvelopeAttachmentsResult GetEnvlopes(string accessToken, string basePath, string accountId, string? envelopeId);
    }
}
