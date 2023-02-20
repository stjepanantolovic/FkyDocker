using DocuSignPOC2.Models;

namespace DocuSignPOC2.Services.IPoc
{
    public class PocService : IPocService
    {
        private readonly IConfiguration _config;
        private readonly DataContext _dataContext;

        public PocService(IConfiguration config, DataContext dataContext)
        {
            _config = config;            
            _dataContext = dataContext;
        }
        public Party? AddPartyToDatabase(Party request)
        {
            if (PartyExists(request.Email))
            {
                return null;
            }
            _dataContext.Database.BeginTransaction();
            _dataContext.Parties.Add(request);
            _dataContext.SaveChanges();
            _dataContext.Database.CommitTransaction();
            return request;
        }

        public bool PartyExists(string email)
        {
            return _dataContext.Parties.Any(p => p.Email.ToLower() == email.ToLower());
        }

        public Guid AddWebHook(dynamic webHookRequest)
        {
            _dataContext.Database.BeginTransaction();

            var webHook = new WebHook()
            {
                Json = webHookRequest.ToString(),
                TimeStamp = DateTime.UtcNow
            };
            _dataContext.WebHooks.Add(webHook);
            _dataContext.SaveChanges();
            _dataContext.Database.CommitTransaction();
            return webHook.Id;
        }

        public Envelope AddEnvelopeToDb(Envelope envelope)
        { 
            _dataContext.Database.BeginTransaction();
            _dataContext.Envleopes.Add(envelope);
            _dataContext.SaveChanges();
            _dataContext.Database.CommitTransaction();
            return envelope;
        }

        public List<Envelope> GetAllEnvelopes()
        {
            return _dataContext.Envleopes.ToList();
           
        }
    }
}
