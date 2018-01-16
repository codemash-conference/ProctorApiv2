using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using ProctorApiv2.Models;
using System.Data.SqlClient;

namespace ProctorApiv2.Utils
{
    public static class Mappings
    {
        public static User ToUser(SqlDataReader dr)
        {
            User user = new User();



            return user;
        }
    }
}