using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Entities.Models
{
    public class Order : BaseEntity
    {
        public string ShippingAddress { get; set; }
        public decimal Price { get; set; }
        public int? AppUserId { get; set; } //Eger bir kişi üye degilse ve alısveriş yapabiliyorsa siparişte bu alan bos kalacak ve ben bu alanı bos olan bir Order'in bende üye olmayan birisi tarafından verildigini anlayacagım...O kişi UserEmail ve UserDescription alanlarını dolduracak
        public string? UserEmail { get; set; } //Eger bu alan bossa o zaman anlarız ki benden alısveriş yapan kişi benim bir üyem o yüzden benim bu alana ihtiyacım olmayacak 
        public string? UserDescription { get; set; }

        //Relational Properties
        public virtual AppUser AppUser { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }


    }
}
