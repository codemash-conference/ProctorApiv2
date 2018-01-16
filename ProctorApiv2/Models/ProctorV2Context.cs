using ProctorApiv2.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
 
namespace ProctorApiv2.Models
{
    public class ProctorV2Context : IdentityDbContext<User>
    {
        public ProctorV2Context()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<Session> Sessions { get; set; }
        public DbSet<Speaker> Speakers { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<UserCheckIn> UserCheckIns { get; set; }
        public DbSet<ScheduleException> ScheduleExceptions { get; set; }
        public DbSet<SessionType> SessionType { get; set; }


        public static ProctorV2Context Create()
        {
            return new ProctorV2Context();
        }
    }

}