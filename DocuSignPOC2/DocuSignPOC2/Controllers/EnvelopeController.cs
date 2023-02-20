using DocuSignPOC2.DocuSignHandling.Services;
using DocuSignPOC2.Models;
using DocuSignPOC2.Services.IDocuSignEnvelope;
using DocuSignPOC2.Services.IUser;
using Microsoft.AspNetCore.Mvc;

namespace DocuSignPOC2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnvelopeController : ControllerBase
    {

        private readonly ILogger<EnvelopeController> _logger;
        //private readonly IDocuSignEnvelopeService _docuSignEnvelopeService;
        //private readonly IUserService _userService;
        private readonly IDocuSignService _docuSignService;

        public EnvelopeController(ILogger<EnvelopeController> logger, 
            //IDocuSignEnvelopeService docuSignEnvelopeService, 
            //IUserService userService,
            IDocuSignService docuSignService)
        {
            _logger = logger;
            //_docuSignEnvelopeService = docuSignEnvelopeService;
            //_userService = userService;
            _docuSignService = docuSignService;
        }



        [HttpPost("DocuSigWebHook")]
        public void WebHookPost(dynamic webHookObject)
        {

        }

        [HttpPost("SendEnvelope")]
        public IActionResult SendEnvelope(AgentProducerEnvelope request)
        {
            //_userService.AddPartyToDatabase(request.Agent);
            //_userService.AddPartyToDatabase(request.Producer);
            //var user = _userService.GetUserByEmail(request.Producer.Email);
            return Ok(_docuSignService.SendEnvelope(
                request.Agent.Email,
                $"{request.Agent.FirstName} {request.Agent.LastName}",
                request.Producer.Email,
                $"{request.Producer.FirstName} {request.Producer.LastName}",
               new List<DocuSign.eSign.Model.Document>() {
                   new DocuSign.eSign.Model.Document() { DocumentBase64 = request.DocumentBase64, Name=$"DocuSignTestDocument from POC2 - {DateTime.Now}"} }
                ));
        }
    }
}