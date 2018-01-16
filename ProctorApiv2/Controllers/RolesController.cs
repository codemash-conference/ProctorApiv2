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
    [Authorize]
    public class RolesController : ApiController
    {
        private RolesRepository _rolesRepository;

        public RolesController()
        {
            _rolesRepository = new RolesRepository();
        }

        // GET api/<controller>
        public List<Role> Get()
        {
            return _rolesRepository.GetRoles();
        }

        // GET api/<controller>/5
        public Role Get(string id)
        {
            return _rolesRepository.GetRoleById(id);
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
            _rolesRepository.CreateRole(value);
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]Role value)
        {
            _rolesRepository.UpdateRole(value);
        }

        // DELETE api/<controller>/5
        public void Delete(string id)
        {
            _rolesRepository.DeleteRole(id);
        }

        // GET api/<controller>/GetUsersForRole/5
        [Route("api/Roles/GetUsersForRole")]
        [HttpGet]
        public List<User> GetUsersForRole(string id)
        {
            return _rolesRepository.GetUsersForRole(id);
        }

        // GET api/<controller>/GetUsersForRoleName/5
        [Route("api/Roles/GetUsersForRoleName")]
        [HttpGet]
        public List<User> GetUsersForRoleName(string name)
        {
            return _rolesRepository.GetUsersForRoleName(name);
        }

        // POST api/<controller>/AddUserToRole
        [Route("api/Roles/AddUserToRole")]
        [HttpPost]
        public void AddUserToRole(string userId, string roleId)
        {
            _rolesRepository.AddUserToRole(userId, roleId);
        }

        // DELETE api/<controller>/RemoveUserFromRole
        [Route("api/Roles/RemoveUserFromRole")]
        [HttpDelete]
        public void RemoveUserFromRole(string userId, string roleId)
        {
            _rolesRepository.RemoveUserFromRole(userId, roleId);
        }
    }
}
