using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ProctorApiv2.Models;

namespace ProctorApiv2.Repositories
{
    public class TagsRepository : BaseSqlRepository
    {
        public List<Tag> GetBySessionId(int sessionId)
        {
            var spName = "TagGetBySessionId";
            List<SqlParameter> parms = new List<SqlParameter>();
            parms.Add(IntegerParameter("sessionId", sessionId));
            List<Tag> tags = GetFromSQL<Tag>(_connStr, spName, AutoConvert<Tag>, parms);
            return tags;
        }
    }
}