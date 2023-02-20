using DocuSignPOC2.Models;

namespace DocuSignPOC2.Services.IPoc
{
    public interface IPocService
    {
        public Party? AddPartyToDatabase(Party request);
        public bool PartyExists(string email);
        public Guid AddWebHook(dynamic webHookRequest);
        public Envelope AddEnvelopeToDb(Envelope envelope);

    }
}
