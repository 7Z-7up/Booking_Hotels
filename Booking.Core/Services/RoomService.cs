using Booking.Core.Domain.Entities;
using Booking.Core.Domain.RepositoryContracts;
using Booking.Core.DTO;
using Booking.Core.Helpers.Enums;
using Booking.Core.Helpers.Services;
using Booking.Core.ServicesContract;
using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Booking.Core.Services
{
    public class ServiceResult
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
    }
    public class RoomService : IRoomService
    {
        public IUnitOfWork UnitOfWork { get; }
        public RoomService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;

        }
        public async Task<IEnumerable<RoomDTO>> GetAllRooms()
        {
           
            var roomss = await UnitOfWork.Rooms.FindAll(x => x.IsDeleted == false && x.Taken == false);
            
            List<RoomDTO> roomDTOs = new List<RoomDTO>();
            foreach (var room in roomss)
            {
                RoomDTO roomDTO = new RoomDTO();
                roomDTO.ID = room.ID;
                roomDTO.Price = room.Price;
                roomDTO.Taken = room.Taken;
                roomDTO.Type = room.Type;
                roomDTO.RoomNum = room.Number;
                roomDTO.HotelName = "hotelA";
                roomDTO.Images = await UnitOfWork.RoomImages.FindAll(x => x.RoomId == room.ID,
                    img => img.Image);

                roomDTOs.Add(roomDTO);
            }
            return roomDTOs.ToArray();
        
        }

        public async Task<RoomDTO> GetRoomById(Guid id)
        {
            var room = await UnitOfWork.Rooms.Find(x => x.IsDeleted == false && x.ID == id);

                RoomDTO roomDTO = new RoomDTO();
                roomDTO.ID = room.ID;
                roomDTO.Price = room.Price;
                roomDTO.Taken = room.Taken;
                roomDTO.Type = room.Type;
                roomDTO.RoomNum = room.Number;
                roomDTO.HotelName = "Hotel A";
                roomDTO.Images = await UnitOfWork.RoomImages.FindAll(x => x.RoomId == room.ID,
                    img => img.Image);

                return roomDTO;
        }


        public async Task<ServiceResult> AddRoom(RoomDTO roomDTO)
        {
            try
            {
                Room room = new Room
                {
                    ID = new Guid(),
                    Price = roomDTO.Price,
                    Taken = roomDTO.Taken,
                    Type = roomDTO.Type,
                    Number = roomDTO.RoomNum,
                    HotelId = Guid.Parse( "ffdb7ef5-e58d-462b-abe8-34740d30f3a5")
                    // Handle ImageFiles and HotelName as needed
                    // Hotel = roomDTO.HotelName
                };
                UnitOfWork.Rooms.Add(room);


                if (roomDTO.ImageFiles != null)
                {
                    foreach (var image in roomDTO.ImageFiles)
                    {
                        if (image.ContentType.StartsWith("image/"))
                        {
                            string uploadedImage = await HelperService.UploadImage(image, "room");
                            //   room.Images.Add(uploadedImage); // Add the file name to the Images collection
                            var roomimage = new RoomImages()
                            {
                                RoomId = room.ID,
                                Image = uploadedImage
                            };

                            UnitOfWork.RoomImages.Add(roomimage);
                        }
                        else
                        {
                            return new ServiceResult { Success = false, ErrorMessage = "One or more files are not valid images." };
                        }
                    }
                }
                else
                {
                    return new ServiceResult { Success = false, ErrorMessage = "At least one image is required." };
                }

                UnitOfWork.Complete();

                return new ServiceResult { Success = true };
            }
            catch (Exception ex)
            {
                // Log the exception for further investigation
                Console.WriteLine($"Error in AddRoom: {ex.Message}");
                return new ServiceResult { Success = false, ErrorMessage = "An unexpected error occurred." };
            }

        }



        public async Task<ServiceResult> DeleteRoom(Guid id)
        {
            var room = await UnitOfWork.Rooms.GetById(id);
            if (room != null)
            {
                room.IsDeleted = true;

                UnitOfWork.Rooms.Update(room);
                UnitOfWork.Complete();
                return new ServiceResult { Success = true };
            }
            else {
                return new ServiceResult { Success = false, ErrorMessage = "No Room found to deleted " };

            }
        }


    
        public  async Task<ServiceResult>  UpdateRoom(Guid id,RoomDTO newRoom)
        {
            var room = await UnitOfWork.Rooms.Find(r => r.ID == id && r.IsDeleted == false, r=>r.Images);//var room = await UnitOfWork.Rooms.Find(r=>r.ID==id && r.IsDeleted== false,["Images"]);
            if (room != null)
            {
                room.Price = newRoom.Price;
                room.Taken = newRoom.Taken;
                room.Type = newRoom.Type;
                room.Number = newRoom.RoomNum;
            

                UnitOfWork.Rooms.Update(room);
                UnitOfWork.Complete();
                return new ServiceResult { Success = true };
            }
            else
            {
                return new ServiceResult { Success = false, ErrorMessage = "No Room found to Edit " };

            }
        }
    }
}
