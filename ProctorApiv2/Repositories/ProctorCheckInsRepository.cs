using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ProctorApiv2.Models;

namespace ProctorApiv2.Repositories
{
    public class ProctorCheckInsRepository : BaseSqlRepository
    {
        public List<UserCheckIn> getBySessionId(int sessionId)
        {
            var spName = "UserCheckInGetBySessionId";
            List<SqlParameter> parms = new List<SqlParameter>();
            parms.Add(IntegerParameter("sessionId", sessionId));
            List<UserCheckIn> userCheckIns = GetFromSQL<UserCheckIn>(_connStr, spName, AutoConvert<UserCheckIn>, parms);
            return userCheckIns;
        }

        public void UpsertProctorCheckIn(UserCheckIn userCheckIn)
        {
            var spName = "UserCheckInUpsert";
            ExecuteStatement(_connStr, (conn, cmd) =>
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = spName;

                cmd.Parameters.AddWithValue("@SessionId", userCheckIn.SessionId);
                cmd.Parameters.AddWithValue("@UserId", userCheckIn.UserId);
                if(userCheckIn.CheckInTime != null)
                    cmd.Parameters.AddWithValue("@CheckInTime", userCheckIn.CheckInTime);

            });
        }
    }
}