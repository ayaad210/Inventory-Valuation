using DataAccessLayer;
using DataAccessLayer.IRepository;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.UnitOfWork_
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private ApplicationDbContext context;
        private IItemRepo itemRepository;
        private IRequestRepo requestRepository;

        private IOrderRepo orderRepository;

        public UnitOfWork(ApplicationDbContext applicationDbContext)
        {
            context=applicationDbContext;
        }

        public async Task<bool> Save()
        {
            var res = await context.SaveChangesAsync();
        return (res )>0;

        }

        private bool disposed = false;



        IItemRepo IUnitOfWork.ItemRepository()
        {


            if (this.itemRepository == null)
            {
                this.itemRepository = new ItemRepo(context);
            }
            return itemRepository;

        }

        IRequestRepo IUnitOfWork.RequestRepository()
        {


            if (this.requestRepository == null)
            {
                 this.requestRepository = new RequestRepo(context);
            }
            return requestRepository;

        }
        protected virtual async  Task Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                   await context.DisposeAsync();
                }
            }
            this.disposed = true;
        }

        public async void Dispose()
        {
            await  Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IOrderRepo OrderRepository()
        {
            if (this.orderRepository == null)
            {
                this.orderRepository = new OrderRepo(context);
            }
            return orderRepository;
        }
        public async Task RollBackChangesAsync()
        {
            await context.Database.RollbackTransactionAsync();
        }

    }
}
