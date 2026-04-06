using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreVision.Domain.Entities
{
    public class SupervisorIntern
    {
        public int Id { get; set; }
        public int SupervisorId { get; set; }
        public int InterId { get; set; }
        public DateTime AssignmentDate { get; set; }
        public bool Active { get; set; }
    }
}