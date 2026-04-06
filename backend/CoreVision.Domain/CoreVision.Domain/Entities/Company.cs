using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreVision.Domain.Entities
{
    public class Company
    {
        public int Id { get; set;}
        public string Name { get; set;}
        public int RNC { get; set;}
        public string Email { get; set;}
        public DateTime RegistrationDate { get; set;}
        public bool Active { get; set;}
    }
}