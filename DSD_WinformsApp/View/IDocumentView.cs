using DSD_WinformsApp.Core.DTOs;
using DSD_WinformsApp.Infrastructure.Data;
using System.Reflection.Metadata;

namespace DSD_WinformsApp.View
{
    public interface IDocumentView
    {
        void BindDataMainView(List<DocumentDto> documents);
        void BindDataManageUsers(List<UserCredentialsDto> users);
       // void UpdatePaginationControls(int currentPage, int totalPageCount);
        void ShowDocumentView();
        string GetSearchQuery();
        string GetFilterCategory();
        void ToggleAdminRights(bool isAdminUser);

        string GetSearchUserQuery();
        string GetFilterUsersCategory();
        void SetUsernameLabel(string username);
        void UpdatePageLabel(int currentPage, int totalPages);
        void UpdateUsersPageLabel(int currentPageUsers, int UsersTotalPages);
       
    }
}