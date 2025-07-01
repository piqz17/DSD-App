using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSD_WinformsApp.Core.DTOs
{
    public class UserCredentialsDto
    {  
        public int UserId { get; set; }
        public string Firstname { get; set; } = string.Empty;
        public string Lastname { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string JobTitle { get; set; } = string.Empty;
        public string CreatedDate { get; set; } = string.Empty;
        public byte[] ImageData { get; set; } = null!;
        public UserRole UserRole { get; set; } = UserRole.User;
        public string UserName { get; set; }

        public UserCredentialsDto()
        {
            UserName = $"{Firstname} {Lastname}";
        }

    }
}
