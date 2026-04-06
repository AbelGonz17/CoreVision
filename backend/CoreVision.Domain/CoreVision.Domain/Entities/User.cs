using CoreVision.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreVision.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string Name {  get; set; }
        public string PasswordHash { get; set; }
        public UserRol Rol {  get; set; }
        public bool FirstLogin { get; set; }
        public bool Active { get; set; }
    }
}
