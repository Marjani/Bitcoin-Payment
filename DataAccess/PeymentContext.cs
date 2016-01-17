using Marjani.Peyment.Bitcoin.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Marjani.Peyment.Bitcoin.DataAccess
{
    public class PeymentContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
    }
}