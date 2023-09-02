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
    public class ItemRepo : GenericRepository<Item>, IItemRepo
    {
        public ItemRepo(ApplicationDbContext context) : base(context)
        {
        }
    }
}
