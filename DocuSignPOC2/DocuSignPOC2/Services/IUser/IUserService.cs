using DocuSign.Admin.Model;
using DocuSign.eSign.Model;

namespace DocuSignPOC2.Services.IUser
{
    public interface IUserService
    {
        public NewUsersSummary CreateNewUser(
            string firstName,
            string lastName,
            string userName,
            string email,
            long permissionProfileId
            );

        public (PermissionProfileInformation, GroupInformation) GetPermissionProfilesAndGroups(
          );

        public NewUserRequest ConstructNewUserRequest(
            long permissionProfileId,
            long groupId,
            string email,
            string firstName,
            string lastName,
            string userName);

        public UsersDrilldownResponse GetUserByEmail(string email);
    }
}

