using PizzaOrder.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace PizzaOrder.Data.Entities
{
    public class OrderDetail
    {

        public OrderDetail(string addressLine1, string mobileNo, int amount)
        {
            AddressLine1 = addressLine1;
            MobileNo = mobileNo;
            Amount = amount;
            Date = DateTime.UtcNow;
            PizzaDetails = new List<PizzaDetail>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        public string MobileNo { get; set; }

        public List<PizzaDetail> PizzaDetails { get; set; }

        public int Amount { get; set; }
        
        public DateTime Date { get; set; }
        
        [Required]
        public OrderStatus OrderStatus { get; set; }
    }
}
