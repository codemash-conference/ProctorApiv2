using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using ProctorApiv2.Models;
using ProctorApiv2.Repositories;
using ProctorApiv2.Utils;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using ProctorApiv2.ViewModels;

namespace ProctorApiv2.Controllers
{
    [Authorize]
    public class UsersController : ApiController
    {
        private ProctorV2Context db = new ProctorV2Context();
        private UsersRepository _userRepository;
        private RolesRepository _roleRepository;

        public UsersController()
        {
            _userRepository = new UsersRepository();
            _roleRepository = new RolesRepository();
        }

        // GET: api/Users
        public IList<User> GetUsers()
        {
            var users = _userRepository.GetUsers();
            return users;
        }

        // GET: api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult GetUser(string id)
        {
            User user = _userRepository.GetUserById(id);
            return Ok(user);
        }

        // PUT: api/Users/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUser(string id, User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.Id)
            {
                return BadRequest();
            }

            _userRepository.UpdateUser(user);

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Users
        [ResponseType(typeof(User))]
        public IHttpActionResult PostUser(User user)
        {
            
            var userManager = new ApplicationUserManager(new UserStore<User>(db));

            var newUser = userManager.Create(user, "dothemash25");

            if (newUser.Succeeded)
            {
                userManager.AddToRole(user.Id, "Everyone");
                userManager.AddToRole(user.Id, "Volunteers");
            }

            return CreatedAtRoute("DefaultApi", new { id = user.Id }, user);
        }

        [AllowAnonymous]
        [ResponseType(typeof(User))]
        [Route("api/register")]
        public IHttpActionResult Register(User user)
        {            
            var userManager = new ApplicationUserManager(new UserStore<User>(db));

            var newUser = userManager.Create(user, "dothemash25");

            if (newUser.Succeeded)
            {
                userManager.AddToRole(user.Id, "Everyone");                
            }

            return CreatedAtRoute("DefaultApi", new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult DeleteUser(string id)
        {
            var user =_userRepository.DeleteUser(id);

            return Ok(user);
        }

        // GET api/<controller>/PasswordReset
        [Route("api/Users/PasswordReset")]
        [HttpPut]
        public IHttpActionResult PasswordReset(PasswordReset passwordReset)
        {
            var userManager = new ApplicationUserManager(new UserStore<User>(db));
            var user = userManager.FindById(passwordReset.UserId);

            if (!userManager.HasPassword(passwordReset.UserId))
            {
                var result = userManager.AddPassword(passwordReset.UserId, passwordReset.NewPassword);

                if (result.Succeeded)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest(result.Errors.First());
                }
            }

            if (userManager.CheckPassword(user, passwordReset.OldPassword))
            {
                userManager.RemovePassword(passwordReset.UserId);
                var result = userManager.AddPassword(passwordReset.UserId, passwordReset.NewPassword);

                if (result.Succeeded)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest(result.Errors.First());
                }

            }
            else
            {
                return BadRequest("Old password is incorrect.");
            }
        }

        // GET api/<controller>/AdminPasswordReset
        [Route("api/Users/AdminPasswordReset")]
        [HttpPut]
        public IHttpActionResult AdminPasswordReset(PasswordReset passwordReset)
        {
            var userManager = new ApplicationUserManager(new UserStore<User>(db));

            userManager.RemovePassword(passwordReset.UserId);
            var result = userManager.AddPassword(passwordReset.UserId, passwordReset.NewPassword);

            if (result.Succeeded)
            {
                return Ok();
            }
            else
            {
                return BadRequest(result.Errors.First());
            }
        }
    }
}
