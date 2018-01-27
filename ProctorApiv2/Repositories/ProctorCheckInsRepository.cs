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
    }
}