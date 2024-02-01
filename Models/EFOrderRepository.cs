using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Models
{
    public class EFOrderRepository : IOrderRepository
    {
        public BookstoreContext context;

        public EFOrderRepository (BookstoreContext temp)
        {
            context = temp;
        }

        public IQueryable<Order> Orders => context.Orders.Include(x => x.Lines).ThenInclude(x => x.Books);

        public void SaveOrder(Order order)
        {
            context.AttachRange(order.Lines.Select(x => x.Books));

            if (order.OrderId == 0)
            {
                context.Orders.Add(order);
            }

            context.SaveChanges();
        }
    }
}
