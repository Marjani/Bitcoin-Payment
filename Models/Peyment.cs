using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace Marjani.Peyment.Bitcoin.Models
{
    public class Peyment
    {

        public string Adderess { get; set; }
        public double Total { get; set; }
        [Display(Name="Order Id")]
        public int OrderId { get; set; }
    }
}