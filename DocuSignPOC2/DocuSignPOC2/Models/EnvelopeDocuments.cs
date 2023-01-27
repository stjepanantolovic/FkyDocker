using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuSign.Models
{
    public class EnvelopeDocuments
    {
        public string EnvelopeId { get; set; }

        public List<EnvelopeDocItem> Documents { get; set; }
    }
}
