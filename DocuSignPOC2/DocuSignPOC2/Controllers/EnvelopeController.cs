using DocuSignPOC2.DocuSignHandling.Services;
using DocuSignPOC2.Models;
using DocuSignPOC2.Services.IDocuSignEnvelope;
using DocuSignPOC2.Services.IPoc;
using DocuSignPOC2.Services.IUser;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DocuSignPOC2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnvelopeController : ControllerBase
    {

        private readonly ILogger<EnvelopeController> _logger;
        private readonly IPocService _pocService;
        private readonly IDocuSignService _docuSignService;

        public EnvelopeController(ILogger<EnvelopeController> logger,
         
            IPocService pocService,
            IDocuSignService docuSignService)
        {
            _logger = logger;
            _pocService = pocService;
            _docuSignService = docuSignService;
        }



        [HttpPost("DocuSigWebHook")]
        public IActionResult WebHookPost(dynamic webHookObject)
        {
            var objectAsStrng = webHookObject.ToString() as string;
            _pocService.AddWebHook(webHookObject);
            _logger.LogError(objectAsStrng);
            return Ok("Hopefully Logged");
        }

        [HttpPost("SendEnvelope")]
        public IActionResult SendEnvelope(AgentProducerEnvelope request)
        {
            _pocService.AddPartyToDatabase(request.Agent);
            _pocService.AddPartyToDatabase(request.Producer);
            var envelopeResponse = (_docuSignService.SendEnvelope(
                request.Agent.Email,
                $"{request.Agent.FirstName} {request.Agent.LastName}",
                request.Producer.Email,
                $"{request.Producer.FirstName} {request.Producer.LastName}",
               new List<DocuSign.eSign.Model.Document>() {
                   new DocuSign.eSign.Model.Document() { DocumentBase64 = request.DocumentBase64, Name=$"DocuSignTestDocument from POC2 - {DateTime.Now}"} }
                ));
            var dbEnvelope = new Envelope()
            {
                Parties = new List<Party>() { request.Agent, request.Producer},
                DocuSignId = envelopeResponse.EnvelopeId,
                EnvelopeSentToDocuSignTimeStamp = DateTime.UtcNow
            };
            _pocService.AddEnvelopeToDb(dbEnvelope);
            return Ok(dbEnvelope);

        }
    }
}