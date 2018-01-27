using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ProctorApiv2.Models;

namespace ProctorApiv2.Repositories
{
    public class RoomsRepository : BaseSqlRepository
    {
        public List<Room> getRooms()
        {
            var spName = "RoomGetAll";
            return GetFromSQL<Room>(_connStr, spName, AutoConvert<Room>);
        }

        public List<Room> GetBySessionId(int sessionId)
        {
            var spName = "RoomGetBySessionId";
            List<SqlParameter> parms = new List<SqlParameter>();
            parms.Add(IntegerParameter("sessionId", sessionId));
            List<Room> rooms = GetFromSQL<Room>(_connStr, spName, AutoConvert<Room>, parms);
            return rooms;            
        }
    }
}