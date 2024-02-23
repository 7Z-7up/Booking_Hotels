using Booking.Core.DTO;
using Booking.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Core.ServicesContract
{
    public interface IRoomService
    {
        public  Task<IEnumerable<RoomDTO>> GetAllRooms();
        public  Task<RoomDTO> GetRoomById(Guid id);

        public Task<ServiceResult> AddRoom(RoomDTO roomDTO);
        public Task<ServiceResult> DeleteRoom(Guid id);

        public Task<ServiceResult> UpdateRoom(Guid id, RoomDTO newRoom);

    }
}
