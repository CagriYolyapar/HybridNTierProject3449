using Microsoft.AspNetCore.Identity;
using Project.Entities.Enums;
using Project.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Entities.Models
{
    public class AppUser : IdentityUser<int>, IEntity
    {
        //Id,UserName,Password,Email
        public Guid ActivationCode { get; set; }
        public bool IsBanned { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public DataStatus Status { get; set; }

        //Relational Properties
        public virtual AppUserProfile AppUserProfile { get; set; }
        public virtual ICollection<AppUserRole> UserRoles { get; set; }
        public virtual ICollection<Order> Orders { get; set; }


    }
}
