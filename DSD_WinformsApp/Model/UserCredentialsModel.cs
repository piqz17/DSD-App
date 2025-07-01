using DSD_WinformsApp.Core.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSD_WinformsApp.Model
{
    public class UserCredentialsModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Auto-increment
        public int UserId { get; set; }
        public string Firstname { get; set; } = string.Empty;
        public string Lastname { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
        public string Password {get; set; } = string.Empty;
        public string JobTitle { get; set; } = string.Empty;
        public string CreatedDate { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;

        [EnumDataType(typeof(UserRole))]
        public UserRole UserRole { get; set; } = UserRole.User;
        public string UserName { get; set; } 

        public UserCredentialsModel()
        {
            UserName = $"{Firstname} {Lastname}";
        }

    }
}
