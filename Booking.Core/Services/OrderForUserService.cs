using Booking.Core.Domain.Entities;
using Booking.Core.Domain.RepositoryContracts;
using Booking.Core.DTO;
using Booking.Core.ServicesContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Core.Services
{
    public class OrderForUserService : IOrderForUserService
    {
        private IUnitOfWork UnitOfWork;
        public OrderForUserService(IUnitOfWork unitOfWork)
        {

            UnitOfWork = unitOfWork;

        }

        public async Task<IEnumerable<OrderForUserDTO>> GetHistoryUserOrder(Guid id)
        {
            var OrdersDetailsForUser = await UnitOfWork.RoomOrders.FindAll((R => R.IsDeleted == false && R.Order.Customer.ID == id),R=>R.Order ,R=>R.Room,R=>R.Room.Hotel);
            List<OrderForUserDTO> ordersDetailsForUser = new List<OrderForUserDTO>();
            foreach (var order in OrdersDetailsForUser)
            {
                OrderForUserDTO ordercopy = new OrderForUserDTO();
                ordercopy.Start_Date = order.Start_Date;
                ordercopy.End_Date = order.End_Date;
                ordercopy.HotelRate = order.Room.Hotel.Rate;
                ordercopy.HotelAddress = order.Room.Hotel.Address;
                ordercopy.HotelName = order.Room.Hotel.Name;
                ordercopy.TotalCost = order.Order.TotalCost;
                ordercopy.OrderId = order.OrderID;
                ordersDetailsForUser.Add(ordercopy);
            }
            return ordersDetailsForUser;
        }

        public async Task<bool> Delete(Guid id)
        {
            var order = await UnitOfWork.Orders.GetById(id);
            order.IsDeleted = true;
            UnitOfWork.Orders.Update(order);
            var orderRooms = await UnitOfWork.RoomOrders.FindAll((or) => or.IsDeleted == false && or.Order.ID == id);
            bool OrderIsFinished = true;
            foreach (var it in orderRooms)
            {
                it.IsDeleted = true;
                UnitOfWork.RoomOrders.Update(it);
                if (it.End_Date > DateTime.Now)
                {
                    OrderIsFinished = false;
                }
            }
            UnitOfWork.Complete();
            return OrderIsFinished;
        }

        public async Task<OrderForUserDTO> GetOrderId(Guid id)
        {
            var order = await UnitOfWork.Orders.Find(d => d.IsDeleted == false && d.ID == id);
            OrderForUserDTO ordercopy = new OrderForUserDTO();
            ordercopy.TotalCost= order.TotalCost;
            return ordercopy;
        }
}
}
