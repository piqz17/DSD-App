using DSD_WinformsApp.Core.DTOs;

namespace DSD_WinformsApp.Infrastructure.Data.Services
{
    public interface IBackUpFileRepository
    {
        Task<List<BackUpFileDto>> GetRelatedBackupFiles(int documentId);
        Task<bool> DeleteBasedBackupDocumentId(int backupDocumentId);
        Task<bool> DeleteBasedOnDocumentId(int documentId);
    }
}