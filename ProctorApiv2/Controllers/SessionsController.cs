using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ProctorApiv2.Models;
using ProctorApiv2.Repositories;
using ProctorApiv2.ViewModels;

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

        // PUT: api/Sessions/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSession(int id, Session session)
        {
            if (id != session.Id)
            {
                return BadRequest();
            }
            try
            {
                _sessionsRepository.Update(id, session);
                return StatusCode(HttpStatusCode.NoContent);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }


        // POST: api/Sessions
        [ResponseType(typeof(Session))]
        public IHttpActionResult PostSession(Session session)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _sessionsRepository.Create(session);         

            return CreatedAtRoute("DefaultApi", new { id = session.Id }, session);
        }

        // DELETE: api/Sessions/5
        [ResponseType(typeof(Session))]
        public IHttpActionResult DeleteSession(int id)
        {
            _sessionsRepository.Delete(id);
            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                
            }
            base.Dispose(disposing);
        }        

        // GET api/<controller>/GetUsersForRole/5
        [Route("api/Sessions/ImportFromFeed")]
        [HttpGet]
        public IHttpActionResult ImportFromFeed()
        {
            _sessionsRepository.ImportFromFeed();
            return Ok();
        }

        [Route("api/Sessions/UpdateFromFeed")]
        [HttpPut]
        public IHttpActionResult UpdateFromFeed()
        {
            _sessionsRepository.UpdateFromFeed();
            return Ok();
        }

        // PUT api/<controller>/AutoAssign
        [Route("api/Sessions/AutoAssign")]
        [HttpPut]
        public IHttpActionResult AutoAssign()
        {
            _sessionsRepository.AutoAssign();
            return Ok();
        }

        // GET api/<controller>/GetSessionsPerUser
        [Route("api/Sessions/GetSessionsPerUser")]
        [HttpGet]
        public List<User> GetSessionsPerUser()
        {
            return _sessionsRepository.GetSessionsPerUser();
        }

        // GET api/<controller>/GetUserSchedule/5
        [Route("api/Sessions/GetUserSchedule")]
        [HttpGet]
        public User GetSessionsForUser(string userId)
        {
            return _sessionsRepository.GetSessionsForUser(userId);
        }

        // GET api/<controller>/AddUserToSession/5
        [Route("api/Sessions/AddUserToSession")]
        [HttpPost]
        public IHttpActionResult AddUserToSession(string userId, int sessionId)
        {
            _sessionsRepository.AddUserToSession(userId, sessionId);
            return Ok();
        }

        // GET api/<controller>/RemoveUserFromSession
        [Route("api/Sessions/RemoveUserFromSession")]
        [HttpDelete]
        public IHttpActionResult RemoveUserFromSession(string userId, int sessionId)
        {
            _sessionsRepository.RemoveUserFromSession(userId, sessionId);
            return Ok();
        }

        // GET api/<controller>/Results
        [Route("api/Sessions/Results")]
        [HttpGet]
        public List<SessionResult> GetSessionResults()
        {
            return _sessionsRepository.GetSessionResults();

        }

        // PUT api/<controller>/Results
        [Route("api/Sessions/Update")]
        [HttpPut]
        public string UpdateSessions()
        {
            try
            {
                return _sessionsRepository.UpdateSessionsFromFeed().ToString();
            }
            catch(Exception ex)
            {
                return ex.Message;
            }

        }
    }
}
