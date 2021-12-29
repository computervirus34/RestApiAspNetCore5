using RestAPI.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestAPI.Core.IConfiguration
{
    public interface IUnitOfWork
    {
        public IUserRepository Users { get; }
        Task CompleteAsync();

    }
}
