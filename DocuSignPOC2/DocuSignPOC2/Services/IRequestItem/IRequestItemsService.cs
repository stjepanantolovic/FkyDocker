using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuSignPOC2.Services.IRequestItem
{
    using DocuSign.Models;
    using System;


    public interface IRequestItemsService
    {
        public string EgName { get; set; }

        public Session Session { get; set; }

        public User User { get; set; }

        public Guid? OrganizationId { get; set; }

        public string AuthenticatedUserEmail { get; set; }

        string EnvelopeId { get; set; }

        public string DocumentId { get; set; }

        public string ClickwrapId { get; set; }

        public string ClickwrapName { get; set; }

        public EnvelopeDocuments EnvelopeDocuments { get; set; }

        public string TemplateId { get; set; }

        public string PausedEnvelopeId { get; set; }

        public string Status { get; set; }

        public string EmailAddress { get; set; }

        public void UpdateUserFromJWT();

        public void Logout();

        public bool CheckToken(int bufferMin);
    }
}
