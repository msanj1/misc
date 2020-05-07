using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Fonetrak.IDP.Data.Data;
using Fonetrak.IDP.Data.Models;
using Fonetrak.UserManagement.API.ResourceParameters;
using Fonetrak.Web.Common.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Fonetrak.UserManagement.API.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<UserRepository> _logger;
        private readonly ApplicationDbContext _context;

        public UserRepository(UserManager<ApplicationUser> userManager,
            ILogger<UserRepository> logger,
            ApplicationDbContext context)
        {
            _userManager = userManager ?? throw new ArgumentException(nameof(userManager));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _context = context ?? throw new ArgumentNullException(nameof(context)); ;
        }

        public PagedList<ApplicationUser> GetUsers(UsersResourceParameters parameters)
        {
            return PagedList<ApplicationUser>.Create(_userManager.Users,
                parameters.PageNumber, parameters.PageSize);
        }

        public IEnumerable<ApplicationUser> GetUsers(IEnumerable<string> ids)
        {
            return _userManager.Users.Where(u => ids.Contains(u.Id));
        }

        public async Task<ApplicationUser> GetUserAsync(string id)
        {
            return await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IEnumerable<Claim>> GetClaimsAsync(ApplicationUser user)
        {
            return await _userManager.GetClaimsAsync(user);
        }

        public async Task<List<string>> DeleteClaimsAsync(ApplicationUser user)
        {
            List<string> errorMessages = new List<string>();
            var claims = await _userManager.GetClaimsAsync(user);
            var operationResult = await _userManager.RemoveClaimsAsync(user, claims);
            foreach (var error in operationResult.Errors)
            {
                errorMessages.Add(error.Description);
            }

            return errorMessages;
        }

        public async Task<List<string>> DeleteUserAsync(ApplicationUser user)
        {
            List<string> errorMessages = new List<string>();
            var operationResult = await _userManager.DeleteAsync(user);
            foreach (var error in operationResult.Errors)
            {
                errorMessages.Add(error.Description);
            }

            return errorMessages;
        }

        public async Task<List<string>> UpdateUserAsync(ApplicationUser user, List<Claim> claims)
        {
            List<string> errorMessages = new List<string>();

            var updateUserResult = await _userManager.UpdateAsync(user);
            if (updateUserResult.Succeeded)
            {
                var allClaims = await _userManager.GetClaimsAsync(user);
                var removeAllClaimsResult = await _userManager.RemoveClaimsAsync(user, allClaims);
                errorMessages.AddRange(
                    removeAllClaimsResult.Errors.Select(e=>e.Description).ToList());
                var addClaimsResult = await _userManager.AddClaimsAsync(user, claims);
                errorMessages.AddRange(
                    addClaimsResult.Errors.Select(e => e.Description).ToList());
            }

            errorMessages.AddRange(
                updateUserResult.Errors.Select(e => e.Description).ToList());

            return errorMessages;
        }

        public async Task<List<string>> AddUserAsync(ApplicationUser user, List<Claim> claims, string password)
        {
            List<string> errorMessages = new List<string>();
            user.Active = true;
            var createUserResult = await _userManager.CreateAsync(user, password.Trim());
            
            foreach (var error in createUserResult.Errors)
            {
                errorMessages.Add(error.Description);
            }

            if (createUserResult.Succeeded && claims.Count > 0)
            {
                var createClaimResult = await _userManager.AddClaimsAsync(user, claims);
                foreach (var error in createClaimResult.Errors)
                {
                    errorMessages.Add(error.Description);
                }
            }

            return errorMessages;
        }

        public async Task<List<string>> AddClaimAsync(ApplicationUser user, Claim claim)
        {
            List<string> errorMessages = new List<string>();
            var operationResult = await _userManager.RemoveClaimAsync(user, claim);
            errorMessages.AddRange(
                operationResult.Errors.Select(e => e.Description).ToList());

            return errorMessages;
        }

        public Task SaveAsync()
        {
            //Do Nothing
            return Task.FromResult(0);
        }
    }
}
