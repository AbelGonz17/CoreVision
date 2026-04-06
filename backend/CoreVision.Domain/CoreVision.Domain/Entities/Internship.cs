using CoreVision.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreVision.Domain.Entities
{
    public class Internship
    {
        public int Id { get; set; }
        public int InternId { get; set; }
        public int CompanyId { get; set; }
        public int RequiredHours { get; set; }
        public int AccumulatedHours { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public InterShipStatus Status { get; set; }
        public DateTime CreationDate { get; set; }
    }
}