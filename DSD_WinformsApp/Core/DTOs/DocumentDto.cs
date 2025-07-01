using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSD_WinformsApp.Core.DTOs
{
    public class DocumentDto
    {
        public int Id { get; set; }

        [MinLength(1), MaxLength(30)]
        public string DocumentVersion { get; set; } = string.Empty;

        [MaxLength(150)]
        public string Filename { get; set; } = string.Empty;
        public string FilenameExtension { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.Now.Date;
        public string CreatedBy { get; set; } = string.Empty;
        public string ModifiedBy { get; set; } = string.Empty;
        public DateTime? ModifiedDate { get; set; }

        [MaxLength(150)]
        public string Notes { get; set; } = string.Empty;
        public byte[] FileData { get; set; } = null!;




    }
}
