using DocuSignPOC2.Models;
using DocuSignPOC2.Services.IDocuSignEnvelope;
using DocuSignPOC2.Services.IUser;
using Microsoft.AspNetCore.Mvc;

namespace DocuSignPOC2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EnvelopeController : ControllerBase
    {

        private readonly ILogger<EnvelopeController> _logger;
        private readonly IDocuSignEnvelopeService _docuSignEnvelopeService;
        private readonly IUserService _userService;

        public EnvelopeController(ILogger<EnvelopeController> logger, IDocuSignEnvelopeService docuSignEnvelopeService, IUserService userService)
        {
            _logger = logger;
            _docuSignEnvelopeService = docuSignEnvelopeService;
            _userService = userService;
        }



        [HttpPost("DocuSigWebHook")]
        public void WebHookPost(dynamic webHookObject)
        {

        }

        [HttpPost("SendEnvelope")]
        public IActionResult SendEnvelope(AgentProducerEnvelope request)
        {
            var user = _userService.GetUserByEmail(request.Producer.Email);
            return Ok(_docuSignEnvelopeService.SendEnvelope(
                request.Agent.Email,
                $"{request.Agent.FirstName} {request.Agent.LastName}",
                request.Producer.Email,
                $"{request.Producer.FirstName} {request.Producer.LastName}",
                request.DocumentBase64
                ));
        }
    }
}