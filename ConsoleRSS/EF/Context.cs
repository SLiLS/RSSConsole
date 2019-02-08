using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using ConsoleRSS.Models;
using System.Threading.Tasks;

namespace ConsoleRSS.EF
{
  public  class Context : DbContext
    {
       public DbSet<RSSNews> RSSNews { get; set; }
      public  DbSet<RSSSource> RSSSources { get; set; }

       
        public Context () 
            : base("RSSData")
        {

        }
    }
}
