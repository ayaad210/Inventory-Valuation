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
    public class RequestRepo : GenericRepository<Request>, IRequestRepo
    {
        public RequestRepo(ApplicationDbContext context) : base(context)
        {


        }
    }
}
