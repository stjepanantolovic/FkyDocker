using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuSign.Constants
{
    public enum PermissionProfilesEnum
    {
        Administrator,
        Sender,
        Viewer
    }
    public enum Groups
    {
        AdminGroup,
        Everyone

    }

    public enum ExamplesAPIType
    {
        Rooms = 0,
        ESignature = 1,
        Click = 2,
        Monitor = 3,
        Admin = 4,
    }
}
