using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ProctorApiv2.Models;

namespace ProctorApiv2.Repositories
{
    public class UsersRepository : BaseSqlRepository
    {
       

        public User DeleteUser(string userId)
        {
            var spName = "UserDelete";

            List<SqlParameter> parms = new List<SqlParameter>();
            parms.Add(StringParameter("userId", userId));

            return GetFromSQLSingle<User>(_connStr, spName, AutoConvert<User>, parms);

        }

        public List<User> GetUsers()
        {
            var spName = "UserGetAll";
            return GetFromSQL<User>(_connStr, spName, AutoConvert<User>);
        }

        public User GetUserById(string userId)
        {
            var spName = "UserGetById";

            List<SqlParameter> parms = new List<SqlParameter>();
            parms.Add(StringParameter("@userId", userId));

            return GetFromSQLSingle<User>(_connStr, spName, AutoConvert<User>, parms);
        }

        internal void UpdateUser(User user)
        {
            var spName = "UserUpdate";

            ExecuteStatement(_connStr, (conn, cmd) =>
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = spName;

                //Make sure nothing is null
                user.Gravatar = user.Gravatar ?? "";
                user.CellNumber = user.CellNumber ?? "";
                user.Email = user.Email ?? "";
                user.LastName = user.LastName ?? "";
                user.FirstName = user.FirstName ?? "";

                cmd.Parameters.AddWithValue("@CellNumber", user.CellNumber);
                cmd.Parameters.AddWithValue("@Email", user.Email);
                cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
                cmd.Parameters.AddWithValue("@Gravatar", user.Gravatar);
                cmd.Parameters.AddWithValue("@UserId", user.Id);
                cmd.Parameters.AddWithValue("@LastName", user.LastName);
                cmd.Parameters.AddWithValue("@UserName", user.UserName);

            });
        }

        public List<User> GetBySessionId(int sessionId)
        {
            var spName = "UserGetBySessionId";
            List<SqlParameter> parms = new List<SqlParameter>();
            parms.Add(IntegerParameter("sessionId", sessionId));
            List<User> users = GetFromSQL<User>(_connStr, spName, AutoConvert<User>, parms);
            return users;
        }
    }
}