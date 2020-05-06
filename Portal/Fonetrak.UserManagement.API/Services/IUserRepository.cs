using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Fonetrak.IDP.Data.Models;

namespace Fonetrak.UserManagement.API.Services
{
    public interface IUserRepository
    {
        IEnumerable<ApplicationUser> GetUsers();
        Task<ApplicationUser> GetUserAsync(string id);
        Task<List<string>> AddUser(ApplicationUser user, List<Claim> claims, string password);
        Task<IEnumerable<Claim>> GetClaimsAsync(ApplicationUser user);
        void Save();
    }
}