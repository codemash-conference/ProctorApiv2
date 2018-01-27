using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ProctorApiv2.Models;

namespace ProctorApiv2.Repositories
{
    public class SpeakersRepository : BaseSqlRepository
    {
        public List<Speaker> GetBySessionId(int sessionId)
        {
            var spName = "SpeakerGetBySessionId";
            List<SqlParameter> parms = new List<SqlParameter>();
            parms.Add(IntegerParameter("sessionId", sessionId));
            List<Speaker> speakers = GetFromSQL<Speaker>(_connStr, spName, AutoConvert<Speaker>, parms);
            return speakers;
        }
    }
}