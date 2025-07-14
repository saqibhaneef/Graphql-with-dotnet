using PizzaOrder.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaOrder.Data.Entities
{
    public class PizzaDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Description { get; set; }

        public Topping Topping { get; set; }

        // Foreign key to OrderDetail
        public int OrderDetailId { get; set; }

        [ForeignKey(nameof(OrderDetailId))]
        public OrderDetail OrderDetail { get; set; }
    }
}
