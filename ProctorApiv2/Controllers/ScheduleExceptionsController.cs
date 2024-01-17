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
    public class ScheduleExceptionsController : ApiController
    {
        private ScheduleExceptionsRepository _scheduleExceptionsRepository;
        public ScheduleExceptionsController()
        {
            _scheduleExceptionsRepository = new ScheduleExceptionsRepository();
        }

        // GET: api/ScheduleExceptions
        public IList<ScheduleException> GetSessions()
        {
            return _scheduleExceptionsRepository.GetAll();

        }

        public int PostException(ScheduleException scheduleException)
        {
            return _scheduleExceptionsRepository.CreateException(scheduleException);
        }

        //// GET: api/ScheduleExceptions
        //public IList<ScheduleException GetSessions()
        //{
        //    return _scheduleExceptionsRepository.GetAll();

        //}
    }
}
