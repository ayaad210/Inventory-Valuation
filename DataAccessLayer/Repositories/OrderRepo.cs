using DataAccessLayer;
using DataAccessLayer.IRepository;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class OrderRepo : GenericRepository<Order>, IOrderRepo
    {
        public OrderRepo(ApplicationDbContext context) : base(context)
        {
        }
    }
}
