using Catalog_Service_Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogServiceInfrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {

        public BaseRepository(AppDbContext context)
        {
            
        }


        public Task<T> Add(T entity)
        {

        }
    }
}
