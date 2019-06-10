using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ilkyar.Contracts.Entities.DTO
{
    public class UploadRequirementListFileDTO
    {
        public string Name { get; set; }
        public DateTime UploadtDate { get; set; }
        public byte[] AttachedFile { get; set; }
        public int FileSize { get; set; }
        public string ContentType { get; set; }
    }
}
