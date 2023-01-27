using DocuSign.Admin.Model;
using DocuSign.eSign.Model;

namespace DocuSignPOC2.Services.IUser
{
    public interface IUserService
    {
        public NewUsersSummary CreateNewUser(string accessToken,
            string basePath,
            string accountId,
            Guid? organizationId,
            string firstName,
            string lastName,
            string userName,
            string email,
            long permissionProfileId,
            long groupId);

        public (PermissionProfileInformation, GroupInformation) GetPermissionProfilesAndGroups(
           string accessToken, string basePath, string accountId);

        public NewUserRequest ConstructNewUserRequest(
            long permissionProfileId,
            long groupId,
            Guid accountId,
            string email,
            string firstName,
            string lastName,
            string userName);

        public Guid? GetOrganizationId(string adminApiBasePath, string accessToken);       
    }
}
