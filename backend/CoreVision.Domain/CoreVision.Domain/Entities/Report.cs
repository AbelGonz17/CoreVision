using CoreVision.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreVision.Domain.Entities
{
    public class Report
    {
        public int Id { get; set; }
        public int InterShipId { get; set; }
        public int GeneratedBy { get; set; }
        public DateTime GenerationDate { get; set; }
        public ReportType Type { get; set; }
        public string FilePath { get; set; }
    }
}
