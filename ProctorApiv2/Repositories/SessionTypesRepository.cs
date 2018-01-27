using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ProctorApiv2.Models;

namespace ProctorApiv2.Repositories
{
    public class SessionTypesRepository : BaseSqlRepository
    {
        public SessionType GetBySessionId(int sessionTypeId)
        {
            var spName = "SessionTypeGetBySessionId";

            List<SqlParameter> parms = new List<SqlParameter>();
            parms.Add(IntegerParameter("sessionTypeId", sessionTypeId));

            var sessionType = GetFromSQLSingle<SessionType>(_connStr, spName, AutoConvert<SessionType>, parms);

            return sessionType;
        }
    }
}