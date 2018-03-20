using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ExternalAuthorization.Models.Account
{
    public class UserStore : IUserStore<ApplicationUser>
    {
        private readonly ExternalAuthData.ExternalAuthContext dbcontext;
        public UserStore(ExternalAuthData.ExternalAuthContext DbContext)
        {
            dbcontext = DbContext;
        }

        public Task<IdentityResult> CreateAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            ExternalAuthData.User eauser = new ExternalAuthData.User()
            {
                UserId = user.Id,
                GivenName = user.GivenName,
                SurName = user.SurName,
                Email = user.Email,
                UserName = user.Email,

            };

            if (eauser.Claims == null)
            {
                eauser.Claims = new List<ExternalAuthData.Claim>();
            }

            eauser.Claims.Add(new ExternalAuthData.Claim()
            {
                ClaimType = "ProviderKey",
                ClaimValue = user.Id
            });
            eauser.Claims.Add(new ExternalAuthData.Claim()
            {
                ClaimType = "ProviderName",
                ClaimValue = user.AuthenticationProvider
            });

            dbcontext.Users.Add(eauser);
            dbcontext.SaveChanges();

            return Task.FromResult(IdentityResult.Success);
        }

        public Task<IdentityResult> DeleteAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
        }

        public Task<ApplicationUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            return Task.FromResult<ApplicationUser>(new ApplicationUser());
        }

        public Task<string> GetNormalizedUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetUserIdAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult<string>("userid");
        }

        public Task<string> GetUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult<string>("username");
        }

        public Task SetNormalizedUserNameAsync(ApplicationUser user, string normalizedName, CancellationToken cancellationToken)
        {
            return Task.FromResult<object>(null);
        }

        public Task SetUserNameAsync(ApplicationUser user, string userName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> UpdateAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
