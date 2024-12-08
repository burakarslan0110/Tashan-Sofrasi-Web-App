using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TashanSofrasi.DataAccessLayer.Abstract;
using TashanSofrasi.DataAccessLayer.Concrete;
using TashanSofrasi.DataAccessLayer.Repositories;
using TashanSofrasi.EntityLayer.Entities;

namespace TashanSofrasi.DataAccessLayer.EntityFramework
{
    public class EFOrderDetailDal : GenericRepository<OrderDetail>, IOrderDetailDal
    {
        public EFOrderDetailDal(TashanSofrasiContext context) : base(context)
        {
        }

        public async void AddOrderDetailAsync()
        {
            using (var context = new TashanSofrasiContext())
            {
                await context.SaveChangesAsync();
            }
        }

        public List<OrderDetail> GetAllOrderDetailsByMenuTableID(int menuTableID)
        {
            using (var context = new TashanSofrasiContext())
            {
                var result = context.OrderDetails
                .Include(od => od.Order)
                .Include(od => od.Product)
                .Where(od => od.Order.MenuTableID == menuTableID)
                .Where(od => od.Order.OrderStatus == false)
                .Select(od => new OrderDetail
                {
                    OrderDetailID = od.OrderDetailID,
                    OrderID = od.OrderID,
                    ProductID = od.ProductID,
                    Count = od.Count,
                    UnitPrice = od.UnitPrice,
                    TotalPrice = od.TotalPrice,
                    Product = od.Product
                })
                .ToList();
                return result;

            }
        }
    }
}
