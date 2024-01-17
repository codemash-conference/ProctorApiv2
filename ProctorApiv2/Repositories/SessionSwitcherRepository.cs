using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProctorApiv2.Models;

namespace ProctorApiv2.Repositories
{
    public class SessionSwitcherRepository : BaseSqlRepository
    {
        public List<SessionSwitch> GetAll()
        {
            return null;
        }

        public List<SessionSwitch> GetAllForUser(string userId)
        {
            return null;
        }

        public List<int> Create(SessionSwitch sessionSwitch)
        {
            return null;
        }

        public bool Accept(int id)
        {
            return true;
        }

        public bool Reject(int id)
        {
            return true;
        }

        public bool Delete(int id)
        {
            return true;
        }

    }
}