using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuSign.Models
{
    public class User
    {
        public string Name { get; set; }

        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }

        public DateTime? ExpireIn { get; set; }

        public string AccountId { get; set; }
    }
}
