using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ProctorApiv2.Models;
using ProctorApiv2.Utils;

namespace ProctorApiv2.Repositories
{
    public class RolesRepository : BaseSqlRepository
    {
        private readonly ProctorV2Context _context;
        private readonly RoleManager<Role> _roleManager;
        private readonly ApplicationUserManager _userManager;
        private readonly UsersRepository _userRepository;
        

        public RolesRepository()
        {
            _context = new ProctorV2Context();
            _roleManager = new RoleManager<Role>(new RoleStore<Role>(_context));
            _userManager = new ApplicationUserManager(new UserStore<User>(_context));
            _userRepository = new UsersRepository();
            
        }

        public List<Role> GetRoles()
        {
            var spName = "RoleGetAll";
            return GetFromSQL<Role>(_connStr, spName, AutoConvert<Role>);
        }



        public Role GetRoleById(string roleId)
        {
            var spName = "RoleGetById";

            List<SqlParameter> parms = new List<SqlParameter>();
            parms.Add(StringParameter("roleId", roleId));

            return GetFromSQLSingle<Role>(_connStr, spName, AutoConvert<Role>, parms);

            
        }

        public Role GetRoleByName(string roleName)
        {
            var spName = "RoleGetByName";

            List<SqlParameter> parms = new List<SqlParameter>();
            parms.Add(StringParameter("roleName", roleName));

            return GetFromSQLSingle<Role>(_connStr, spName, AutoConvert<Role>, parms);
        }

        public void UpdateRole(Role role)
        {
            _roleManager.Update(role);
        }

        public void CreateRole(string roleName)
        {
            _roleManager.Create(new Role() { Name = roleName });
        }

        public Role DeleteRole(string roleId)
        {

            var spName = "RoleDelete";

            List<SqlParameter> parms = new List<SqlParameter>();
            parms.Add(StringParameter("roleId", roleId));

            return GetFromSQLSingle<Role>(_connStr, spName, AutoConvert<Role>, parms);

        }

        public List<User> GetUsersForRole(string roleId)
        {

            var spName = "RoleGetUsersById";

            List<SqlParameter> parms = new List<SqlParameter>();
            parms.Add(StringParameter("roleId", roleId));

            return GetFromSQL<User>(_connStr, spName, AutoConvert<User>, parms);
        }

        public List<User> GetUsersForRoleName(string roleName)
        {
            var spName = "RoleGetUsersByName";

            List<SqlParameter> parms = new List<SqlParameter>();
            parms.Add(StringParameter("roleName", roleName));

            return GetFromSQL<User>(_connStr, spName, AutoConvert<User>, parms);
        }

        public void AddUserToRole(string userId, string roleId)
        {
            var spName = "RoleAddUser";

            ExecuteStatement(_connStr, (conn, cmd) =>
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = spName;

                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@roleId", roleId);
            });

        }

        public void RemoveUserFromRole(string userId, string roleId)
        {
            var spName = "RoleRemoveUser";

            ExecuteStatement(_connStr, (conn, cmd) =>
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = spName;

                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@roleId", roleId);
            });
        }
    }
}