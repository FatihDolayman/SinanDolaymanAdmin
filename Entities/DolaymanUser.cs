
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class DolaymanUser:IdentityUser
    {
        [Display(Name = "Kullanıcı Adı")]
        [MaxLength(50, ErrorMessage = "50 karakterden fazla olamaz")]
        public string FullName { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<DolaymanUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here

           userIdentity.AddClaim(new Claim("FullName", this.FullName));

            userIdentity.AddClaim(new Claim("Id", this.Id));

            return userIdentity;
        }
    }
}
