using DSD_WinformsApp.Core.DTOs;

namespace DSD_WinformsApp.Presenter
{
    public interface IDocumentPresenter
    {
        Task<bool> DeleteBackUpFile(BackUpFileDto file);
        Task<bool> DeleteDocumentWithBackups(DocumentDto document);
        void EditDocument(DocumentDto document, byte[] fileDataBytes, bool isUploadSuccessful);
        Task<List<BackUpFileDto>> GetRelatedBackupFiles(int documentId);

        // Document Repository methods
        void AddNewDocument(DocumentDto newDocument);
        Task LoadDocuments();
        Task LoadDocumentsByFilter(string currentSearchQuery, string currentFilterCategory);
        Task<bool> CheckForDuplicateFileName(string fileName);
        void NextPage();
        void PreviousPage();
        Task ApplyFilters();
        void SaveDocument(DocumentDto document, byte[] fileDataBytes);
        Task SearchDocuments(string filterCriteria, string searchQuery);
        Task DownloadAllDocuments();
        void ShowDocumentMainView();

        // User Repository methods
        Task LoadUsers();
        Task<List<UserCredentialsDto>> GetAllRegisteredUsers();
        void SaveUserRegistration(UserCredentialsDto userCredentials);
        void EditUser(UserCredentialsDto user);
        Task<bool> ValidateUserCredentials(UserCredentialsDto userCredentials);
        Task<bool> DeleteUser(UserCredentialsDto user);
        Task LoadUsersByFilter(string currentUsersSearchQuery, string currentUsersJobFilter);
        Task<bool> CheckUserAccess(string username);
        Task<bool> CheckForDuplicateUsername(string userName);
        Task ApplyUsersPageFilters();
        void NextUsersPage();
        void BackUsersPage();
    }

}