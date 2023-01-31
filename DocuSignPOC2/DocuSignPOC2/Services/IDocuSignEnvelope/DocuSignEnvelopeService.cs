using DocuSign.eSign.Api;
using DocuSign.eSign.Client;
using DocuSign.eSign.Model;
using DocuSignPOC2.Services.IESignAdmin;

namespace DocuSignPOC2.Services.IDocuSignEnvelope
{
    public class DocuSignEnvelopeService : IDocuSignEnvelopeService
    {
        private readonly IeSignAdminService _ieSignAdminService;

        public DocuSignEnvelopeService(IeSignAdminService ieSignAdminService)
        {
            this._ieSignAdminService = ieSignAdminService;
        }
        /// <summary>
        /// Unpauses signature workflow
        /// </summary>
        /// <param name="accessToken">Access Token for API call (OAuth)</param>
        /// <param name="basePath">BasePath for API calls (URI)</param>
        /// <param name="accountId">The DocuSign Account ID (GUID or short version) for which the APIs call would be made</param>
        /// <param name="recipient1Email">The signer email</param>
        /// <param name="recipient1Name">The signer name</param>
        /// <param name="conditionalRecipient1Email">The first conditional signer email</param>
        /// <param name="conditionalRecipient1Name">The first conditional signer name</param>
        /// <param name="recipient2Email">The second conditional signer email</param>
        /// <param name="recipient2Name">The second conditional signer name</param>
        /// <returns>The update summary of the envelopes</returns>
        public EnvelopeSummary SendEnvelope(string recipient1Email, string recipient1Name,
             string recipient2Email, string recipient2Name, string documentBase64)
        {          
            

            // Construct request body
            var envelope = MakeEnvelope(
                recipient1Email,
                recipient1Name,
                recipient2Email,
                recipient2Name,
                documentBase64);

            // Call the eSignature REST API
            

            return _ieSignAdminService.EnvelopesApi.CreateEnvelope(_ieSignAdminService.ESignAdminAccountId, envelope);
        }

        public EnvelopeDefinition MakeEnvelope(string recipient1Email, string recipient1Name, string recipient2Email, string recipient2Name,
            string documentBase64)
        {
            var document = new Document()
            {
                DocumentBase64 = documentBase64,
                DocumentId = "1",
                FileExtension = "txt",
                Name = "Welcome",
            };



            var workflowStep = new WorkflowStep()
            {
                Action = "pause_before",
                TriggerOnItem = "routing_order",
                ItemId = "2",
                Status = "pending"
            };

            var agent = new Signer()
            {
                Email = recipient1Email,
                Name = recipient1Name,
                RecipientId = "1",
                RoutingOrder = "1",
                RoleName = "Purchaser",
                Tabs = new Tabs
                {
                    SignHereTabs = new List<SignHere>()
                    {
                        new SignHere()
                        {
                           AnchorString = "**asn**",
                            AnchorUnits = "pixels",
                            AnchorYOffset = "0",
                            AnchorXOffset = "0",
                        },
                    }
                },
            };

            var producer = new Signer()
            {
                Email = recipient2Email,
                Name = recipient2Name,
                RecipientId = "2",
                RoutingOrder = "2",
                RoleName = "Purchaser",
                Tabs = new Tabs
                {
                    SignHereTabs = new List<SignHere>
                    {
                        new SignHere()
                        {
                           AnchorString = "**psn**",
                           AnchorUnits = "pixels",
                           AnchorYOffset = "0",
                           AnchorXOffset = "0",
                        }
                    },
                },
            };

            var envelopeDefinition = new EnvelopeDefinition()
            {
                Documents = new List<Document> { document, },
                EmailSubject = "Proag Docusign Test",
                //Workflow = new Workflow { WorkflowSteps = new List<WorkflowStep> { workflowStep, } },
                Recipients = new Recipients { Signers = new List<Signer> { agent, producer } },
                Status = "Sent",
            };

            return envelopeDefinition;
        }

        public EnvelopeAttachmentsResult GetEnvlopes(string? envelopeId)
        {           

            // Call the eSignature REST API
            
            var attachments =_ieSignAdminService.EnvelopesApi.GetAttachments(_ieSignAdminService.ESignAdminAccountId, envelopeId);
            var documentsResult = _ieSignAdminService.EnvelopesApi.ListDocuments(_ieSignAdminService.ESignAdminAccountId, envelopeId);
            foreach (var item in documentsResult.EnvelopeDocuments)
            {

                using (MemoryStream data = new MemoryStream())
                {
                    var document = _ieSignAdminService.EnvelopesApi.GetDocument(_ieSignAdminService.ESignAdminAccountId, envelopeId, item.DocumentId);
                    document.CopyTo(data);
                    var documentAsByteArray = data.ToArray();
                    var documentBase64 = Convert.ToBase64String(documentAsByteArray);
                }
            }

            return attachments;

        }
    }
}

