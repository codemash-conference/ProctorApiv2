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
    public class ApplicationController : ApiController
    {
        private ProctorV2Context db = new ProctorV2Context();
        private ApplicationRepository _applicationRepository;

        public ApplicationController()
        {
            _applicationRepository = new ApplicationRepository();
        }

        [Authorize]
        [HttpGet]
        public IList<Application> GetApplications()
        {
            return _applicationRepository.GetAllApplications();
        }

        [Authorize]
        [HttpGet]
        public IHttpActionResult GetApplications(int id)
        {
            Application application = _applicationRepository.GetApplicationById(id);
            return Ok(application);
        }

        [HttpPost]
        public IHttpActionResult PostApplication(Application application)
        {
            var result = _applicationRepository.CreateApplication(application);

            return Ok();
        }

        [Authorize]
        [HttpPut, Route("{id}")]
        public IHttpActionResult UpdateApplication(int id, Application application)
        {
            Application applicationUpdated = _applicationRepository.UpdateApplication(id, application);
            return Ok(applicationUpdated);
        }

        [Authorize]
        [HttpDelete]
        public IHttpActionResult DeleteApplication(int id)
        {
            Application application = _applicationRepository.DeleteApplication(id);
            return Ok();
        }

    }
}
