using DSD_WinformsApp.Infrastructure.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSD_WinformsApp.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDocumentDbContext _documentsDbContext;
        public IDocumentRepository Documents { get; }
        public IBackUpFileRepository BackUpFiles { get; }
        public IUserRepository Users { get; }

        public UnitOfWork(IDocumentDbContext documentDbContext, IDocumentRepository documentRepository, IBackUpFileRepository backUpFileRepository, IUserRepository userRepository)
        {
            _documentsDbContext = documentDbContext;
            Documents = documentRepository;
            BackUpFiles = backUpFileRepository;
            Users = userRepository;
        }

        public int Complete()
        {
            return _documentsDbContext.SaveChanges();
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _documentsDbContext.Dispose();
            }
        }
    }
}

