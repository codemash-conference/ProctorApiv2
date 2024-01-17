using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProctorApiv2.Models
{
    public class User : IdentityUser
    {

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ContactAddress { get; set; }
        public bool IsActive { get; set; }
        public string Gravatar { get; set; }
        public string CellNumber { get; set; }
        public string Gender { get; set; }
        public string School { get; set; }
        public string Major { get; set; }
        public string TopicsInterestedIn { get; set; }
        public string Essay { get; set; }
        public bool PreviousVolunteer { get; set; }
        public int VolunteerYears { get; set; }
        public bool Accepted { get; set; }
        public DateTime? AcceptedDate { get; set; }
        public bool Cancelled { get; set; }
        public DateTime? CancelledDate { get; set; }

        [ForeignKey("Id")]
        public List<Session> Sessions { get; set; }

        [ForeignKey("UserId")]
        public List<UserCheckIn> ProctorCheckIns { get; set; }

    }
}