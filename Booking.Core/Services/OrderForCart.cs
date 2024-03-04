using Booking.Core.Domain.Entities;
using Booking.Core.Domain.RepositoryContracts;
using Booking.Core.DTO;
using Booking.Core.Helpers.Enums;
using Booking.Core.ServicesContract;
using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Core.Services
{
    public class OrderForCart : IOrderForCart
    {
        private IUnitOfWork UnitOfWork;
        public OrderForCart(IUnitOfWork UnitOfWork)
        {
            this.UnitOfWork = UnitOfWork;
        }
        public async Task<CreateOrderDTO> PutRoomsInDTO(List<Guid>roomid)
        {
            CreateOrderDTO createOrderDTOToAddToList = new CreateOrderDTO();
            
            foreach (var id in roomid)
            {
                Room room = await GetById(id);
                createOrderDTOToAddToList.RoomId.Add(room.ID);
                createOrderDTOToAddToList.Number.Add(room.Number);
                createOrderDTOToAddToList.Price.Add(room.Price);
                createOrderDTOToAddToList.Type.Add(room.Type);
            }
            return createOrderDTOToAddToList;
        }
        public async Task Create(Guid Uesrid, CreateOrderDTO createOrderDTO)
        {
            try
            {
                Order order = new Order();
                Guid orderid = Guid.NewGuid();
                order.ID = orderid;
                decimal TotalPrice = 0;
                order.PaymentType = PaymentType.PayPal;
                order.TotalCost = 0;
                await UnitOfWork.Orders.Add(order);
                for (int i = 0; i < createOrderDTO.RoomId.Count; i++)
                {
                    order.CustomerID = Uesrid;
                    createOrderDTO.OrderID.Add(orderid);
                    createOrderDTO.CustomerId.Add(Uesrid);
                    var x = UnitOfWork.Complete();
                    Room room = await UnitOfWork.Rooms.GetById(createOrderDTO.RoomId[i]);
                    room.Taken = true;
                    UnitOfWork.Rooms.Update(room);
                    var y = UnitOfWork.Complete();
                    RoomOrder roomOrder = new RoomOrder();
                    roomOrder.Start_Date = DateTime.Now;
                    roomOrder.End_Date = DateTime.Now.AddDays(createOrderDTO.NumberofDays[i]);
                    roomOrder.OrderID = orderid;
                    roomOrder.RoomID = createOrderDTO.RoomId[i];
                    await UnitOfWork.RoomOrders.Add(roomOrder);
                    var z = UnitOfWork.Complete();
                    var roomData = await UnitOfWork.Rooms.GetById(createOrderDTO.RoomId[i]);
                    var roomPrice = roomData.Price;
                    int daysDifference = ((int)(roomOrder.End_Date - roomOrder.Start_Date).TotalDays);
                    TotalPrice += (roomPrice * daysDifference);
                    //UnitOfWork.Complete();
                }
                order.TotalCost = TotalPrice;
                UnitOfWork.Orders.Update(order);
                int w = UnitOfWork.Complete();
            }
            catch(Exception e)
            {
                await Console.Out.WriteLineAsync(e.Message);
            }
        }

        public async Task Delete(Guid RoomId, CreateOrderDTO createOrderDTO)
        {
            for(int i=0;i<createOrderDTO.RoomId.Count;i++)
            {
                if (createOrderDTO.RoomId[i]==RoomId)
                {
                    createOrderDTO.RoomId.RemoveAt(i);
                    createOrderDTO.Number.RemoveAt(i);
                    createOrderDTO.Price.RemoveAt(i);
                    createOrderDTO.NumberofDays.RemoveAt(i);
                    createOrderDTO.Type.RemoveAt(i);
                }
            }
        }
        public async Task<Room> GetById(Guid RoomId)
        {
            return await UnitOfWork.Rooms.Find(r => r.IsDeleted == false && r.ID == RoomId);
        }

        public async Task<Order> GetOrderById(Guid Order)
        {
            return await UnitOfWork.Orders.Find(r => r.IsDeleted == false && r.ID == Order);
        }

        /*public async Task Update(Guid OrderId, CreateOrderDTO createOrderDTO)
        {
            var OrderRooms = await UnitOfWork.RoomOrders.FindAll(o => o.IsDeleted == false && o.Order.ID == createOrderDTO.OrderID);
            Order order = await UnitOfWork.Orders.GetById(createOrderDTO.OrderID);
            var rooms = OrderRooms.Where(r => r.Room.IsDeleted == false);
            decimal TotalPrice = 0;
            foreach (var room in rooms)
            {
                RoomOrder roomOrder = new RoomOrder();
                roomOrder.Start_Date = createOrderDTO.Start_Date;
                roomOrder.End_Date = createOrderDTO.End_Date;
                roomOrder.OrderID = order.ID;
                roomOrder.RoomID = createOrderDTO.RoomId;
                var roomData = await UnitOfWork.Rooms.GetById(roomOrder.RoomID);
                var roomPrice = roomData.Price;
                int daysDifference = ((int)(roomOrder.End_Date - roomOrder.Start_Date).TotalDays) + 1;
                TotalPrice += (roomPrice * daysDifference);
                await UnitOfWork.RoomOrders.Add(roomOrder);
            }
            order.TotalCost = TotalPrice;
            UnitOfWork.Orders.Update(order);
            UnitOfWork.Complete();
        }*/
    }
}