using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ProctorApiv2.Models;
using ProctorApiv2.Repositories;

namespace ProctorApiv2.Controllers
{
    public class SessionSwitcherController : ApiController
    {
        private SessionSwitcherRepository _sessionSwitcherRepository;
        public SessionSwitcherController()
        {
            _sessionSwitcherRepository = new SessionSwitcherRepository();
        }

        // GET: api/ScheduleExceptions
        public IList<SessionSwitch> GetAll(string userId)
        {
            return _sessionSwitcherRepository.GetAll();
        }
    }
}
