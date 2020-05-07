using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Fonetrak.IDP.Data.Models;
using Fonetrak.UserManagement.API.ResourceParameters;
using Fonetrak.Web.Common.Helpers;

namespace Fonetrak.UserManagement.API.Services
{
    public interface IUserRepository
    {
        PagedList<ApplicationUser> GetUsers(UsersResourceParameters parameters);
        IEnumerable<ApplicationUser> GetUsers(IEnumerable<string> ids);
        Task<ApplicationUser> GetUserAsync(string id);
        Task<List<string>> AddUserAsync(ApplicationUser user, List<Claim> claims, string password);
        Task<List<string>> UpdateUserAsync(ApplicationUser user, List<Claim> claims);
        Task<List<string>> DeleteUserAsync(ApplicationUser user);
        Task<IEnumerable<Claim>> GetClaimsAsync(ApplicationUser user);
        Task<List<string>> DeleteClaimsAsync(ApplicationUser user);
        Task<List<string>> AddClaimAsync(ApplicationUser user, Claim claim);
        Task SaveAsync();
    }
}