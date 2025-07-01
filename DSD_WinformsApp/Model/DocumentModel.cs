using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSD_WinformsApp.Model
{
    public class DocumentModel 
    {
        public int Id { get; set; }
        public string DocumentVersion { get; set; } = string.Empty;
        public string Filename { get; set; } = string.Empty;
        public string FilenameExtension { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.Now.Date;
        public string CreatedBy { get; set; } = string.Empty;
        public string ModifiedBy { get; set; } = string.Empty;
        public DateTime? ModifiedDate { get; set; } 
        public string Notes { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;

    }
}
