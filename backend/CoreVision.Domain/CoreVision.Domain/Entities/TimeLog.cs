using CoreVision.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreVision.Domain.Entities
{
    public class TimeLog
    {
        public int Id { get; set; }
        public int InterShipId { get; set; }
        public int InterId { get; set; }
        public int SupervisorId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan EntryTime { get; set; }
        public TimeSpan DepartureTime { get; set; }
        public decimal HoursWorked { get; set; }
        public TimeLogStatus Status { get; set; }
        public string? SupervisorComment { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime RevisionDate { get; set; }
        public string? Description { get; set; }
    }
}