using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Marjani.Peyment.Bitcoin.Models
{
    public class Response
    {
        public string address { get; set; }
        public int index { get; set; }
        public string callback { get; set; }
    }
}