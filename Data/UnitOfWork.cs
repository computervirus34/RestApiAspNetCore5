using Microsoft.Extensions.Logging;
using RestAPI.Core.IConfiguration;
using RestAPI.Core.Repositories;
using RestAPI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestAPI.Data
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public IUserRepository Users { get; private set; }
        public UnitOfWork(
            ApplicationDbContext context,
            ILoggerFactory loggerFactory
            )
        {
            _context = context;
            _logger = loggerFactory.CreateLogger("logs");
            Users = new UserRepository(_context, _logger);
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
