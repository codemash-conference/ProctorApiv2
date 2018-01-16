using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ProctorApiv2.Models;
using ProctorApiv2.Repositories;

namespace ProctorApiv2.Controllers
{
    public class SessionsController : ApiController
    {
        private SessionsRepository _sessionsRepository;
        private RoomsRepository _roomsRepository;


        public SessionsController()
        {
            _sessionsRepository = new SessionsRepository();
            _roomsRepository = new RoomsRepository();
            //_userCheckInRepository = new UserCheckInRepository();
        }

        // GET: api/Sessions
        public IList<Session> GetSessions()
        {
            //var sessions =  db.Sessions
            //    .Include("Rooms")
            //    .ToList();
            //return sessions;
            return _sessionsRepository.getSessions();

        }

        // GET: api/Sessions/5
        [ResponseType(typeof(Session))]
        public IHttpActionResult GetSession(int id)
        {
            var session = _sessionsRepository.getSessionById(id);
            

            return Ok(session);
        }
    }
}
