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
        public async Task Create(List<Room>rooms,CreateOrderDTO createOrderDTO)
        {
            Order order = new Order();
            order.CustomerID = new Guid();
            order.ID = new Guid();
            order.PaymentType = PaymentType.PayPal;
            order.TotalCost = 0;
            await UnitOfWork.Orders.Add(order);
            decimal TotalPrice = 0;
            foreach (var room in rooms)
            {
                RoomOrder roomOrder = new RoomOrder();
                roomOrder.Start_Date = createOrderDTO.Start_Date;
                roomOrder.End_Date = createOrderDTO.End_Date;
                roomOrder.OrderID = order.ID;
                roomOrder.RoomID = createOrderDTO.RoomId;
                var roomData= await UnitOfWork.Rooms.GetById(roomOrder.RoomID);
                var roomPrice = roomData.Price;
                int daysDifference = ((int)(roomOrder.End_Date - roomOrder.Start_Date).TotalDays)+1;
                TotalPrice += (roomPrice*daysDifference);
                await UnitOfWork.RoomOrders.Add(roomOrder);
            }
            order.TotalCost =TotalPrice;
            UnitOfWork.Orders.Update(order);
            UnitOfWork.Complete();
        }

        public async Task Delete(Guid RoomId, CreateOrderDTO createOrderDTO)
        {
            var OrderRoom= await UnitOfWork.RoomOrders.Find(o=>o.IsDeleted==false&&o.Order.ID==createOrderDTO.OrderID&&o.RoomID== RoomId);
            OrderRoom.IsDeleted=true;
            int daysDifference = ((int)(OrderRoom.End_Date - OrderRoom.Start_Date).TotalDays) + 1;
            Order order = await UnitOfWork.Orders.GetById(OrderRoom.OrderID);
            Room room = await UnitOfWork.Rooms.GetById(OrderRoom.RoomID);
            order.TotalCost-=daysDifference*room.Price;
            UnitOfWork.RoomOrders.Update(OrderRoom);
            UnitOfWork.Complete();
        }

        public async Task<Room> GetById(Guid RoomId)
        {
            return await UnitOfWork.Rooms.Find(r=>r.IsDeleted==false&&r.ID==RoomId);
        }

        public async Task Update(Guid OrderId, CreateOrderDTO createOrderDTO)
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
        }
    }
}