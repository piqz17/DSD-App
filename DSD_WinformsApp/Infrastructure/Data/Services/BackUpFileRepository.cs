using AutoMapper;
using DSD_WinformsApp.Core.DTOs;
using DSD_WinformsApp.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSD_WinformsApp.Infrastructure.Data.Services
{
    public class BackUpFileRepository : IBackUpFileRepository
    {
        private readonly IDocumentDbContext _dbContext;
        private readonly IMapper _mapper;
        public BackUpFileRepository(IDocumentDbContext documentDbContext, IMapper mapper)
        {

            _dbContext = documentDbContext;
            _mapper = mapper;

        }

        // Get related backup files based on documentId
        public async Task<List<BackUpFileDto>> GetRelatedBackupFiles(int documentId)
        {
            var relatedBackupFiles = await _dbContext.BackupFiles
               .Where(b => b.Id == documentId)
               .OrderByDescending(b => b.BackupId) // Sort by BackFileId in descending order
               .ToListAsync();

            var backUpFiles = _mapper.Map<List<BackUpFileModel>, List<BackUpFileDto>>(relatedBackupFiles);
            return backUpFiles;
        }

        public async Task<bool> DeleteBasedOnDocumentId(int documentId)
        {
            try
            {
                var backupFilesToDelete = await _dbContext.BackupFiles
                    .Where(b => b.Id == documentId)
                    .ToListAsync();

                foreach (var backupFile in backupFilesToDelete)
                {
                    // Delete the backup file from the file system
                    File.Delete(backupFile.BackupFilePath);
                    
                }

                _dbContext.BackupFiles.RemoveRange(backupFilesToDelete);
                _dbContext.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public async Task<bool> DeleteBasedBackupDocumentId(int backupDocumentId)
        {
            try
            {
                var backupFileToDelete = await _dbContext.BackupFiles
                    .Where(b => b.BackupId == backupDocumentId)
                    .FirstOrDefaultAsync();

                if (backupFileToDelete != null)
                {
                    // Delete the backup file from the file system
                    File.Delete(backupFileToDelete.BackupFilePath);

                    // Remove the backup file from the database
                    _dbContext.BackupFiles.Remove(backupFileToDelete);
                    _dbContext.SaveChanges();

                    return true;
                }
                else
                {
                    // Backup file with the specified ID not found
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }


    }
}
