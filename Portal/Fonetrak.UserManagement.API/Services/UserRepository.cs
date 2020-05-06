using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Fonetrak.IDP.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Fonetrak.UserManagement.API.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(UserManager<ApplicationUser> userManager,
            ILogger<UserRepository> logger)
        {
            _userManager = userManager ?? throw new ArgumentException(nameof(userManager));
            _logger = logger ?? throw new ArgumentNullException(nameof(userManager));
        }

        public IEnumerable<ApplicationUser>  GetUsers()
        {
            return _userManager.Users;
        }

        public async Task<ApplicationUser> GetUserAsync(string id)
        {
            return await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IEnumerable<Claim>> GetClaimsAsync(ApplicationUser user)
        {
            return await _userManager.GetClaimsAsync(user);
        }

        public async Task<List<string>> AddUser(ApplicationUser user, List<Claim> claims, string password)
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

        //public async Task<List<string>> ValidatePasswordAsync(ApplicationUser user, string password)
        //{
        //    List<string> errors = new List<string>();
        //    foreach (var validator in _userManager.PasswordValidators)
        //    {
        //        var result = await validator.ValidateAsync(_userManager, user, password);
        //        foreach (var error in result.Errors)
        //        {
        //            errors.Add(error.Description);
        //        }
        //    }

        //    foreach (var validator in _userManager.UserValidators)
        //    {
        //        var result = await validator.ValidateAsync(_userManager, user);
        //        foreach (var error in result.Errors)
        //        {
        //            errors.Add(error.Description);
        //        }
        //    }

        //    return errors;
        //}
         
        //public async Task<Claim> GetClaims

        public void Save()
        {
            //Do Nothing
        }
    }
}
