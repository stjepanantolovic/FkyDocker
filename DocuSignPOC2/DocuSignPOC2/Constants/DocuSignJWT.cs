using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuSign.Constants
{
    public class DocuSignJWT
    {
        public string ClientId { get; set; }
        public string AuthServer { get; set; }
        public string ImpersonatedUserID { get; set; }
        public string PrivateKeyFile { get; set; }
    }
}
