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
    public class OrderForAdminService : IOrderForAdminService
    {
        private IUnitOfWork unitOfWork;
        public OrderForAdminService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<OrdersForAdminDTO>> GetAll()
        {
            var orders = await unitOfWork.RoomOrders.FindAll(x => x.IsDeleted == false , i => i.Order, i => i.Room);
            //var resultWithIncludes = await orders;
            List<OrdersForAdminDTO> ordersForAdmin = new List<OrdersForAdminDTO>();
            foreach (var order in orders)
            {
                OrdersForAdminDTO ordertomap = new OrdersForAdminDTO();
                ordertomap.Start_Date = order.Start_Date;
                ordertomap.End_Date = order.End_Date;
                ordertomap.Number = order.Room.Number;
                ordertomap.Type = order.Room.Type;
                ordertomap.TotalCost = order.Order.TotalCost;
                ordertomap.Id = order.Order.ID;
                ordersForAdmin.Add(ordertomap);
            }
            return ordersForAdmin.ToArray();
        }

        public async Task<OrdersForAdminDTO> GetOrderId(Guid id)
        {
            var orders = await unitOfWork.RoomOrders.Find(d => d.IsDeleted == false && d.Order.ID == id, i => i.Order, i => i.Room);

            OrdersForAdminDTO ordersForAdmin = new OrdersForAdminDTO();
            ordersForAdmin.Start_Date = orders.Start_Date;
            ordersForAdmin.End_Date = orders.End_Date;
            ordersForAdmin.Number = orders.Room.Number;
            ordersForAdmin.Type = orders.Room.Type;
            ordersForAdmin.TotalCost = orders.Order.TotalCost;
            ordersForAdmin.Id = orders.Order.ID;
            return ordersForAdmin;
        }
    }
}
