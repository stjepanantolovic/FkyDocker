using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuSign.Models
{
    public class Session
    {
        public string AccountId { get; set; }

        public string AccountName { get; set; }

        public string BasePath { get; set; }

        public string RoomsApiBasePath { get; set; }

        public string AdminApiBasePath { get; set; }
    }
}
