using DSD_WinformsApp.Model;
using Microsoft.EntityFrameworkCore;

namespace DSD_WinformsApp.Infrastructure.Data
{
    public interface IDocumentDbContext
    {
        DbSet<DocumentModel> Documents { get; set; }
        DbSet<BackUpFileModel> BackupFiles { get; set; }
        DbSet<UserCredentialsModel> UserCredentials { get; set; }

        void SetModified(object entity);
        void Dispose();
        int SaveChanges();
    }
}