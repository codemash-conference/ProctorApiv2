using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ProctorApiv2.Models;
using ProctorApiv2.Repositories;

namespace ProctorApiv2.Controllers
{
    [Authorize]
    public class RoomsController : ApiController
    {
        
        private RoomsRepository _roomsRepository;

        public RoomsController()
        {
            _roomsRepository = new RoomsRepository();
        }

        // GET: api/Rooms
        public IList<Room> GetRooms()
        {
            return _roomsRepository.getRooms();
        }
    }
}
