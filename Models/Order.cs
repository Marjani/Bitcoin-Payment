using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace Marjani.Peyment.Bitcoin.Models
{
    public class Order
    {
        [MaxLength(60)]
        public string Name { get; set; }
        [MaxLength(60)]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        [MaxLength(80)]
        public string Email { get; set; }
        [Required]
        public double Total { get; set; }

    }
}