using DataAccessLayer.IRepository;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.UnitOfWork_
{
    public interface IUnitOfWork
    {
        public IItemRepo ItemRepository();


        public IRequestRepo RequestRepository();
        public IOrderRepo OrderRepository();


        public Task<bool>  Save();

        public  Task RollBackChangesAsync();




    }
}
