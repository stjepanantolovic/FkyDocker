using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocuSign.eSign.Model;

namespace DocuSignPOC2.DocuSignHandling.Helpers
{
    public static class DocuSignEnvelopeHelper
    {
        public static EnvelopeDefinition MakeEnvelope(string emailSubject, string agentEmail, string agentName,
            string producerEmail, string producerName, List<Document> docuSignDocuments, string notifcationUri, bool? useDefaultDocuments = true)
        {

            var envelopeDefinition = new EnvelopeDefinition()
            {
                NotificationUri = notifcationUri,
                Password = "test",


                EventNotification = new EventNotification()
                {
                    DeliveryMode = "SIM",
                    EventData = new ConnectEventData() { Version = "restv2.1" },
                    Events = new List<string>()
                   {

                    "envelope-created",
                    "envelope-sent",
                    "envelope-delivered",
                    "envelope-completed",
                    "recipient-delivered",
                    "recipient-completed",
                    "recipient-sent",
                    "template-modified"
                   }
                },
                Documents = docuSignDocuments.PrepareDocumentsForSigning(),
                //Documents = GetDefaultDocuments("Test"),
                EmailSubject = emailSubject,
                Recipients = new Recipients
                {
                    Signers = GetAgentAndProducerAsSigners(agentEmail, agentName, producerEmail, producerName)
                },
                Status = "Sent",
            };

            return envelopeDefinition;
        }
        public static List<Signer> GetAgentAndProducerAsSigners(string agentEmail, string agentName, string producerEmail, string producerName)
        {

            var agent = new Signer()
            {
                Email = agentEmail,
                Name = agentName,
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
                    },
                    DateSignedTabs = new List<DateSigned>
                    {
                        new DateSigned()
                        {
                            AnchorString="**dasn**"
                        }
                    }
                },
            };

            var producer = new Signer()
            {
                Email = producerEmail,
                Name = producerName,
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
                    DateSignedTabs = new List<DateSigned>
                    {
                        new DateSigned()
                        {
                            AnchorString="**dpsn**"
                        }
                    }
                },
            };

            return new List<Signer>() { agent, producer };
        }

        private static List<Document>? PrepareDocumentsForSigning(this List<Document> documents)
        {
            if (documents == null) { return null; };

            for (int i = 0; i < documents.Count(); i++)
            {
                documents[i].DocumentId = (i + 1).ToString();
                documents[i].FileExtension = "txt";
            }

            return documents;
        }

        public static List<Document> GetDefaultDocuments(string documentName)
        {
            var result = new List<Document>();
            var documentBase64 = GetDocument(documentName);
            result.Add(new Document()
            {
                DocumentId="1",
                DocumentBase64 = documentBase64,
                Name = $"{documentName} - {DateTime.Now}",
                FileExtension="txt"
            });
            return result;
        }


        public static string? GetDocument(string documentName)
        {

            var path = @"Documents/" + documentName + ".docx";
            if (File.Exists(path))
            {
                var fileAsByteArray = System.IO.File.ReadAllBytes(path);
                var fileAsBase64String = Convert.ToBase64String(fileAsByteArray);
                return fileAsBase64String;
            }

            throw new Exception("Document " + path + " does not exist.");

        }

        public static EnvelopeDefinition MakeEnvelope2(string recipient1Email, string recipient1Name, string recipient2Email, string recipient2Name)
        {
            var document = new Document()
            {
                DocumentBase64 = GetDocument("DrpApp"),
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
                NotificationUri = "https://notchtestbus.azurewebsites.net/api/DocuSignHttpTrigger?code=GUPj2tupllGzodHN41EMomMsasFv7AYr2Cotlq3a6YgEAzFuvJLc4Q==",
                Password = "test",


                EventNotification = new EventNotification()
                {
                    DeliveryMode = "SIM",
                    EventData = new ConnectEventData() { Version = "restv2.1" },
                    Events = new List<string>()
                   {

                    "envelope-created",
                    "envelope-sent",
                    "envelope-delivered",
                    "envelope-completed",
                    "recipient-delivered",
                    "recipient-completed",
                    "recipient-sent",
                    "template-modified"
                   }
                },
                Documents = new List<Document> { document, },
                EmailSubject = "Proag Docusign Test",
                //Workflow = new Workflow { WorkflowSteps = new List<WorkflowStep> { workflowStep, } },
                Recipients = new Recipients { Signers = new List<Signer> { agent, producer } },
                Status = "Sent",
            };

            return envelopeDefinition;
        }
    }
}
