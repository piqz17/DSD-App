using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSD_WinformsApp.Model
{
    public class BackUpFileModel
    {
        [Key]
        public int BackupId { get; set; }
        public string DocumentVersion { get; set; } = string.Empty;
        public string Filename { get; set; } = string.Empty;
        public string OriginalFilePath { get; set; } = string.Empty;
        public string BackupFilePath { get; set; } = string.Empty;
        public DateTime BackupDate { get; set; }
        public double Version { get; set; }

        [ForeignKey(nameof(Id))]
        public int Id { get; set; }
    }
}
