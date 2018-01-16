using System;
using System.Collections.Generic;
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
    }
}