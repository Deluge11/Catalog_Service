using Catalog_Service_Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogServiceInfrastructure
{
    public class UnitOfWork : IUnitOfWork
    {

        public UnitOfWork(AppDbContext context)
        {
            Context = context;
        }

        public AppDbContext Context { get; }


        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
